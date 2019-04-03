using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Drivers.USBPcap;
using RtsCore.Framework.Device;
using RtsCore.Framework.Native;

namespace Ratatoskr.Devices.UsbMonitor
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private const int RECV_BUFFER_SIZE = 65535;

        private DevicePropertyImpl prop_;

        private IntPtr handle_ = IntPtr.Zero;

        private IAsyncResult task_ar_;
        private IntPtr       task_exit_event_ = IntPtr.Zero;
        private IntPtr       task_recv_event_ = IntPtr.Zero;

        private byte[]              recv_buffer_;
        private UsbPcapRecordParser recv_parser_;


        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
            prop_ = devp as DevicePropertyImpl;
        }

        protected override EventResult OnConnectStart()
        {
            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            handle_ = UsbPcapManager.OpenDevice(prop_.DeviceName.Value);

            return ((handle_ != WinAPI.INVALID_HANDLE_VALUE) ? (EventResult.Success) : (EventResult.Busy));
        }

        protected override void OnConnected()
        {
            /* Pcapファイルヘッダーパケットを読み飛ばす */
            ThrowRecvPacket(1);

            recv_buffer_ = new byte[RECV_BUFFER_SIZE];
            recv_parser_ = new UsbPcapRecordParser();

            task_exit_event_ = WinAPI.CreateEvent(IntPtr.Zero, true, false, null);
            task_recv_event_ = WinAPI.CreateEvent(IntPtr.Zero, true, false, null);

            task_ar_ = (new TaskDelegate(Task)).BeginInvoke(null, null);
        }

        protected override void OnDisconnectStart()
        {
            WinAPI.SetEvent(task_exit_event_);

            while ((task_ar_ != null) && (!task_ar_.IsCompleted)) {
                Thread.Sleep(1);
            }

            WinAPI.CloseHandle(task_exit_event_);
            task_exit_event_ = IntPtr.Zero;

            WinAPI.CloseHandle(task_recv_event_);
            task_recv_event_ = IntPtr.Zero;

            UsbPcapManager.CloseDevice(handle_);
            handle_ = WinAPI.INVALID_HANDLE_VALUE;
        }

        protected override PollState OnPoll()
        {
            return (PollState.Idle);
        }

        private delegate void TaskDelegate();
        private unsafe void Task()
        {
            var task_exit = false;
            var recv_overlapped = new NativeOverlapped();

            recv_overlapped.EventHandle = task_recv_event_;

            fixed (byte *recv_buff = recv_buffer_)
            {
                var event_list = new IntPtr[] { task_exit_event_, recv_overlapped.EventHandle };
                var recv_size = (uint)0;
                var result = (int)0;

                WinAPI.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, ref recv_overlapped);

                while (!task_exit) {
                    result = WinAPI.WaitForMultipleObjects((uint)event_list.Length, event_list, false, WinAPI.INFINITE);

                    switch ((uint)result) {
                        /* スレッド停止イベント */
                        case WinAPI.WAIT_OBJECT_0:
                        {
                            task_exit = true;
                        }
                            break;

                        /* 受信イベント */
                        case WinAPI.WAIT_OBJECT_0 + 1:
                        {
                            WinAPI.GetOverlappedResult(handle_, ref recv_overlapped, out recv_size, true);
                            WinAPI.ResetEvent(recv_overlapped.EventHandle);

                            if (recv_size > 0) {
#if true
                                foreach (var info in recv_parser_.Parse(recv_buffer_, 0, (int)recv_size)) {
                                    if (NotifyFilter(info)) {
                                        NotifyExec(info);
                                    }
                                }
#else
                                NotifyRecvComplete("", "", "", ClassUtil.CloneCopy(recv_buffer_, (int)recv_size));
#endif
                            }

                            WinAPI.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, ref recv_overlapped);
                        }
                            break;
                    }
                }
            }
        }

        private bool NotifyFilter(UsbPcapRecordParser.PacketInfo info)
        {
            var enable = false;

            switch ((UsbPcapRecordParser.UrbFunctionType)info.UsbPcapHeader.transfer) {
                case UsbPcapRecordParser.UrbFunctionType.Isochronous:
                {
                    enable = prop_.Filter_IsochronousTransfer.Value;
                }
                    break;

                case UsbPcapRecordParser.UrbFunctionType.Interrupt:
                {
                    enable = prop_.Filter_InterruptTransfer.Value;
                }
                    break;

                case UsbPcapRecordParser.UrbFunctionType.Control:
                {
                    enable = prop_.Filter_ControlTransfer.Value;
                }
                    break;

                case UsbPcapRecordParser.UrbFunctionType.Bulk:
                {
                    enable = prop_.Filter_BulkTransfer.Value;
                }
                    break;
            }

            return (enable);
        }

        private void NotifyExec(UsbPcapRecordParser.PacketInfo info)
        {
            var info_text = new StringBuilder("");

            /* Device ID */
            if (prop_.InfoOut_UsbDeviceID.Value) {
                info_text.AppendFormat("DEV={0:X4} ", info.UsbPcapHeader.device);
            }

            /* End Point */
            if (prop_.InfoOut_EndPoint.Value) {
                info_text.AppendFormat("EP={0:X} ", info.UsbPcapHeader.endpoint & 0x7F);
            }

            /* I/O Request Packet ID */
            if (prop_.InfoOut_IrpID.Value) {
                info_text.AppendFormat("IrpID={0:X8} ", info.UsbPcapHeader.irpId);
            }

            /* Function Type */
            if (prop_.InfoOut_FunctionType.Value) {
                info_text.Append(((UsbPcapRecordParser.UrbFunctionType)info.UsbPcapHeader.transfer).ToString());

                if (prop_.InfoOut_FunctionParam.Value) {
                    /* Function Parameter */
                    switch ((UsbPcapRecordParser.UrbFunctionType)info.UsbPcapHeader.transfer) {
                        case UsbPcapRecordParser.UrbFunctionType.Isochronous:
                        {
                        }
                            break;

                        case UsbPcapRecordParser.UrbFunctionType.Interrupt:
                        {
                        }
                            break;

                        case UsbPcapRecordParser.UrbFunctionType.Control:
                        {
                            info_text.AppendFormat("-{0}", ((UsbPcapRecordParser.UsbPcapControlStage)info.UsbPcapHeader.control.stage).ToString());
                        }
                            break;

                        case UsbPcapRecordParser.UrbFunctionType.Bulk:
                        {
                        }
                            break;
                    }
                }
            }

            if (info.UsbPcapHeader.info == 0) {
                NotifySendComplete(info.PcapHeader.GetDateTime(), info_text.ToString(), "", "", info.Data);
            } else {
                NotifyRecvComplete(info.PcapHeader.GetDateTime(), info_text.ToString(), "", "", info.Data);
            }
        }

        private unsafe void ThrowRecvPacket(uint num)
        {
            var recv_buffer_core = new byte[4096];
            var recv_overlapped = new NativeOverlapped();

            fixed (byte *recv_buff = recv_buffer_core)
            {
                var recv_size = (uint)0;

                while (num > 0) {
                    WinAPI.ReadFile(handle_, recv_buff, (uint)recv_buffer_core.Length, out recv_size, ref recv_overlapped);
                    WinAPI.GetOverlappedResult(handle_, ref recv_overlapped, out recv_size, true);
                    num--;
                }
            }
        }
    }
}
