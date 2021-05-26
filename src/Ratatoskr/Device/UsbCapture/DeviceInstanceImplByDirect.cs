//#define ASYNC_RECV_MODE

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Debugger;
using Ratatoskr.Native.Windows;

namespace Ratatoskr.Device.UsbCapture
{
    internal sealed class DeviceInstanceImplByDirect : DeviceInstance
    {
        private const int RECV_BUFFER_SIZE = 65535;

        private DevicePropertyImpl prop_;

        private IntPtr handle_ = IntPtr.Zero;

#if ASYNC_RECV_MODE
        private IntPtr		exit_event_handle_ = IntPtr.Zero;
        private IntPtr		recv_event_handle_ = IntPtr.Zero;
#else
		private bool		exit_req_ = false;
#endif

        private IAsyncResult recv_task_ar_;

        private byte[]              recv_buffer_;
//        private UsbPcapRecordParser recv_parser_;
		private USBPcapParser       recv_parser_;


        public DeviceInstanceImplByDirect(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
            prop_ = devp as DevicePropertyImpl;
        }

        protected override void OnConnectStart()
        {
        }

        protected override EventResult OnConnectBusy()
        {
//            handle_ = UsbPcapManager.OpenDevice(prop_.DeviceName.Value);
            handle_ = UsbPcapManager.OpenDevice(prop_.DeviceName.Value, false);

            return ((handle_ != WinAPI.INVALID_HANDLE_VALUE) ? (EventResult.Success) : (EventResult.Busy));
        }

        protected override void OnConnected()
        {
            /* Pcapファイルヘッダーパケットを読み飛ばす */
//            ThrowRecvPacket(1);

            recv_buffer_ = new byte[RECV_BUFFER_SIZE];
//            recv_parser_ = new UsbPcapRecordParser();
            recv_parser_ = new USBPcapParser();

#if ASYNC_RECV_MODE
            exit_event_handle_ = WinAPI.CreateEvent(IntPtr.Zero, true, false, null);
            recv_event_handle_ = WinAPI.CreateEvent(IntPtr.Zero, true, false, null);
#else
			exit_req_ = false;
#endif

            /* タスク開始 */
            recv_task_ar_ = (new MethodInvoker(RecvTask)).BeginInvoke(null, null);
        }

        protected override void OnDisconnectStart()
        {
            /* タスク終了要求 */
#if ASYNC_RECV_MODE
            WinAPI.SetEvent(exit_event_handle_);
#else
			exit_req_ = true;
#endif
        }

        protected override EventResult OnDisconnectBusy()
        {
			if (recv_task_ar_ != null) {
				if (!recv_task_ar_.AsyncWaitHandle.WaitOne(0)) {
					return (EventResult.Busy);
				}
			}

//            if (WinAPI.WaitForSingleObject(exit_event_handle_, 0) != WinAPI.WAIT_OBJECT_0) {
//                return (EventResult.Busy);
//            }

            return (EventResult.Success);
        }

        protected override void OnDisconnected()
        {
#if ASYNC_RECV_MODE
            WinAPI.CloseHandle(exit_event_handle_);
            exit_event_handle_ = IntPtr.Zero;

            WinAPI.CloseHandle(recv_event_handle_);
            recv_event_handle_ = IntPtr.Zero;
#endif

            UsbPcapManager.CloseDevice(handle_);
            handle_ = WinAPI.INVALID_HANDLE_VALUE;
        }

        protected override PollState OnPoll()
        {
            return (PollState.Idle);
        }

        private unsafe void RecvTask()
        {
#if ASYNC_RECV_MODE
            var task_exit = false;
            var wait_event_list = new IntPtr[] { exit_event_handle_, recv_event_handle_ };
            var recv_overlapped = new NativeOverlapped();

            recv_overlapped.EventHandle = recv_event_handle_;
#endif

            fixed (byte *recv_buff = recv_buffer_)
            {
                var recv_size = (uint)0;

#if ASYNC_RECV_MODE
                WinAPI.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, ref recv_overlapped);

                while (!task_exit) {
                    switch ((uint)WinAPI.WaitForMultipleObjects((uint)wait_event_list.Length, wait_event_list, false, WinAPI.INFINITE)) {
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
#if false
                                foreach (var info in recv_parser_.Parse(recv_buffer_, 0, (int)recv_size)) {
                                    if (NotifyFilter(info)) {
                                        NotifyExec(info);
                                    }
                                }
#else
								foreach (var packet in recv_parser_.InputData(recv_buffer_, (int)recv_size)) {

									if (NotifyUSBPcapPacketFilter(packet)) {
//										NotifyRecvComplete(packet.MakeTime, "", "", "", packet.RawData);
										NotifyUSBPcapPacket(packet);
									}
								}

//                                NotifyRecvComplete("", "", "", ClassUtil.CloneCopy(recv_buffer_, (int)recv_size));
                            }
#endif

                            WinAPI.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, ref recv_overlapped);
                        }
                            break;
                    }
                }
#else
                while (!exit_req_) {
					if (WinAPI.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, WinAPI.Null)) {
#if true
                        if (recv_size > 0) {
							foreach (var packet in recv_parser_.InputData(recv_buffer_, (int)recv_size)) {
								if (NotifyUSBPcapPacketFilter(packet)) {
									NotifyUSBPcapPacket(packet);
								}
							}
                        }
#endif
					} else {
					}
                }
#endif
            }
        }

		private bool NotifyUSBPcapPacketFilter(USBPcapPacket packet)
		{
			var throwgh = false;

			switch (packet.PacketHeader.transfer) {
                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_ISOCHRONOUS:
                {
                    throwgh = prop_.Filter_IsochronousTransfer.Value;
                }
                    break;

                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_INTERRUPT:
                {
                    throwgh = prop_.Filter_InterruptTransfer.Value;
                }
                    break;

                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_CONTROL:
                {
                    throwgh = prop_.Filter_ControlTransfer.Value;
                }
                    break;

                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_BULK:
                {
                    throwgh = prop_.Filter_BulkTransfer.Value;
                }
                    break;
			}

			return (throwgh);
		}

		private void NotifyUSBPcapPacket(USBPcapPacket packet)
		{
            var packet_info = new StringBuilder("");

			switch (packet.PacketHeader.transfer) {
                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_ISOCHRONOUS:
                {
					packet_info.Append("ISOCHRONUS");
                }
                    break;

                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_INTERRUPT:
                {
                    packet_info.Append("INTERRUPT");
                }
                    break;

                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_CONTROL:
                {
                    packet_info.Append("CONTROL");
                }
                    break;

                case USBPcapPacket.USBPCAP_TRANSFER.USBPCAP_TRANSFER_BULK:
                {
                    packet_info.Append("BULK");
                }
                    break;
			}

			var device_address = String.Format("{0}.{1}.{2}", packet.PacketHeader.bus, packet.PacketHeader.device, packet.PacketHeader.endpoint);

			byte[] packet_data;

			switch (prop_.DataContentsType.Value) {
				case UsbCaptureDataContentsType.Raw:
					packet_data = packet.RawData;
					break;

				default:
					packet_data = packet.Payload;
					break;
			}

			if (packet.PacketHeader.info == 0) {
				/* Host -> Device */
				NotifySendComplete(packet.MakeTime, packet_info.ToString(), "host", device_address, packet_data);
			} else {
				/* Host <- Device */
				NotifyRecvComplete(packet.MakeTime, packet_info.ToString(), device_address, "host", packet_data);
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
