using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Generic;

namespace Ratatoskr.Devices.SerialPort
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private const uint COMM_MASK = NativeMethods.EV_RXCHAR | NativeMethods.EV_TXEMPTY;

        private IntPtr handle_;

        private object send_sync_ = new object();
        private byte[] send_buffer_;

//        private byte[] recv_buffer_;

//        private bool         task_exit_req_ = false;
//        private IAsyncResult task_recv_ar_;

#if false
        private IntPtr       task_exit_event_ = IntPtr.Zero;
        private IntPtr       task_send_req_event_ = IntPtr.Zero;
        private IntPtr       task_send_end_event_ = IntPtr.Zero;
        private IntPtr       task_recv_event_ = IntPtr.Zero;
#endif

        public DeviceInstanceImpl(DeviceManager devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
        }

        protected override EventResult OnConnectStart()
        {
            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            var prop = Property as DevicePropertyImpl;

            handle_ = NativeMethods.CreateFile(
                "\\\\.\\" + prop.PortName.Value,
                NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                0,
                NativeMethods.Null,
                NativeMethods.OPEN_EXISTING,
                0,
                NativeMethods.Null);
            
            var error = NativeMethods.GetLastError();

            return ((handle_ != NativeMethods.INVALID_HANDLE_VALUE) ? (EventResult.Success) : (EventResult.Busy));
        }

        protected override void OnConnected()
        {
            var prop = Property as DevicePropertyImpl;
            var dcb = new NativeMethods.DCB();

            dcb.DCBlength = (uint)Marshal.SizeOf(dcb.GetType());

            /* === 基本設定 === */
            {
                /* === 基本設定読み込み === */
                NativeMethods.GetCommState(handle_, out dcb);

                /* --- Baudrate --- */
                dcb.BaudRate = (uint)prop.BaudRate.Value;

                /* --- Parity --- */
                switch (prop.Parity.Value) {
                    case Parity.None:   dcb.Parity = NativeMethods.NOPARITY;       break;
                    case Parity.Odd:    dcb.Parity = NativeMethods.ODDPARITY;      break;
                    case Parity.Even:   dcb.Parity = NativeMethods.EVENPARITY;     break;
                    case Parity.Mark:   dcb.Parity = NativeMethods.MASKPARITY;     break;
                    case Parity.Space:  dcb.Parity = NativeMethods.SPACEPARITY;    break;
                    default:            dcb.Parity = NativeMethods.NOPARITY;       break;
                }

                /* --- BitSize --- */
                dcb.ByteSize = (byte)prop.DataBits.Value;

                /* --- StopBits --- */
                switch (prop.StopBits.Value) {
                    case StopBits.One:          dcb.StopBits = NativeMethods.ONESTOPBIT;   break;
                    case StopBits.OnePointFive: dcb.StopBits = NativeMethods.ONE5STOPBITS; break;
                    case StopBits.Two:          dcb.StopBits = NativeMethods.TWOSTOPBIT;   break;
                    default:                    dcb.StopBits = NativeMethods.ONESTOPBIT;   break;
                }

                /* バイナリモード */
                dcb.fBinary = 1;

                dcb.fOutxDsrFlow = (prop.fOutxCtsFlow.Value) ? (1U) : (0U);
                dcb.fOutxDsrFlow = (prop.fOutxDsrFlow.Value) ? (1U) : (0U);
                dcb.fDsrSensitivity = (prop.fDsrSensitivity.Value) ? (1U) : (0U);
                dcb.fTXContinueOnXoff = (prop.fTXContinueOnXoff.Value) ? (1U) : (0U);
                dcb.fOutX = (prop.fOutX.Value) ? (1U) : (0U);
                dcb.fInX = (prop.fInX.Value) ? (1U) : (0U);

                switch (prop.fDtrControl.Value) {
                    case fDtrControlType.DTR_CONTROL_DISABLE:   dcb.fDtrControl = NativeMethods.DTR_CONTROL_DISABLE;   break;
                    case fDtrControlType.DTR_CONTROL_ENABLE:    dcb.fDtrControl = NativeMethods.DTR_CONTROL_ENABLE;    break;
                    case fDtrControlType.DTR_CONTROL_HANDSHAKE: dcb.fDtrControl = NativeMethods.DTR_CONTROL_HANDSHAKE; break;
                }

                switch (prop.fRtsControl.Value) {
                    case fRtsControlType.RTS_CONTROL_DISABLE:   dcb.fRtsControl = NativeMethods.RTS_CONTROL_DISABLE;   break;
                    case fRtsControlType.RTS_CONTROL_ENABLE:    dcb.fRtsControl = NativeMethods.RTS_CONTROL_ENABLE;    break;
                    case fRtsControlType.RTS_CONTROL_HANDSHAKE: dcb.fRtsControl = NativeMethods.RTS_CONTROL_HANDSHAKE; break;
                    case fRtsControlType.RTS_CONTROL_TOGGLE:    dcb.fRtsControl = NativeMethods.RTS_CONTROL_TOGGLE;    break;
                }

                dcb.XonLim = (ushort)prop.XonLim.Value;
                dcb.XoffLim = (ushort)prop.XoffLim.Value;
                dcb.XonChar = (sbyte)prop.XonChar.Value;
                dcb.XoffChar = (sbyte)prop.XoffChar.Value;

                /* === 更新 === */
                NativeMethods.SetCommState(handle_, ref dcb);
            }

#if true
            /* タイムアウト設定 */
            var timeout = new NativeMethods.COMMTIMEOUTS();

            timeout.ReadIntervalTimeout = uint.MaxValue;
            timeout.ReadTotalTimeoutMultiplier = uint.MaxValue;
            timeout.ReadTotalTimeoutConstant = uint.MaxValue;
            timeout.WriteTotalTimeoutConstant = 100;

            NativeMethods.SetCommTimeouts(handle_, ref timeout);
#endif

            /* === バッファ初期化 === */
            send_buffer_ = null;
//            recv_buffer_ = new byte[4096];

#if false
            task_exit_event_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            task_send_req_event_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            task_send_end_event_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            task_recv_event_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
#endif
            /* 送信/受信タスク開始 */
//            task_exit_req_ = false;
//            task_ar_ = (new TaskDelegate(Task)).BeginInvoke(null, null);
        }

        protected override void OnDisconnectStart()
        {
//            NativeMethods.SetEvent(task_exit_event_);

            /* 送信/受信タスク停止 */
//            task_exit_req_ = true;
//            NativeMethods.SetCommMask(handle_, COMM_MASK);
//            while ((task_ar_ != null) && (!task_ar_.IsCompleted)) {
//                Thread.Sleep(1);
//            }

            /* ポートクローズ */
            if (handle_ != NativeMethods.INVALID_HANDLE_VALUE) {
                NativeMethods.CloseHandle(handle_);
                handle_ = NativeMethods.INVALID_HANDLE_VALUE;
            }

#if false
            NativeMethods.CloseHandle(task_exit_event_);
            task_exit_event_ = IntPtr.Zero;

            NativeMethods.CloseHandle(task_send_req_event_);
            task_send_req_event_ = IntPtr.Zero;

            NativeMethods.CloseHandle(task_send_end_event_);
            task_send_end_event_ = IntPtr.Zero;

            NativeMethods.CloseHandle(task_recv_event_);
            task_recv_event_ = IntPtr.Zero;
#endif
        }

        protected override PollState OnPoll()
        {
#if false
            return (PollState.Idle);
#else
            var busy = false;

            SendPoll(ref busy);
            RecvPoll(ref busy);

            return ((busy) ? (PollState.Active) : (PollState.Idle));
#endif
        }

        protected override void OnSendRequest()
        {
            SendPoll();
//            NativeMethods.SetCommMask(handle_, COMM_MASK);

//            NativeMethods.SetEvent(task_send_req_event_);
        }

#if false
        private delegate void TaskDelegate();
        private void Task()
        {
            var comm_ev = (uint)0;

            while (!task_exit_req_) {
                SendPoll();

                /* イベント待ち */
                if (!NativeMethods.WaitCommEvent(handle_, out comm_ev, IntPtr.Zero))continue;

                switch (comm_ev) {
                    case NativeMethods.EV_RXCHAR:
                        RecvPoll();
                        break;
                }
            }
        }
#endif

#if false
        private delegate void TaskDelegate();
        private unsafe void Task()
        {
            var task_exit = false;
            var send_overlapped = new NativeOverlapped();
            var recv_overlapped = new NativeOverlapped();

            send_overlapped.EventHandle = task_send_end_event_;
            recv_overlapped.EventHandle = task_recv_event_;

            fixed (byte *recv_buff = recv_buffer_)
            {
                var event_list = new IntPtr[] { task_exit_event_, task_send_req_event_, task_send_end_event_, task_recv_event_ };
                var send_size = (uint)0;
                var recv_size = (uint)0;
                var recv_data = (byte[])null;
                var result = (int)0;

                NativeMethods.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, ref recv_overlapped);

                var err = NativeMethods.GetLastError();

                while (!task_exit) {
                    /* 送信データ読み込み */
                    if (send_buffer_ == null) {
                        send_buffer_ = GetSendData();

                        if (send_buffer_ != null) {
                            /* 送信開始 */
                            NotifySendComplete("", "", "", send_buffer_);
                            NativeMethods.WriteFile(handle_, send_buffer_, (uint)send_buffer_.Length, out send_size, ref send_overlapped);
                        }
                    }

                    result = NativeMethods.WaitForMultipleObjects((uint)event_list.Length, event_list, false, NativeMethods.INFINITE);

                    switch ((uint)result) {
                        /* スレッド停止イベント */
                        case NativeMethods.WAIT_OBJECT_0:
                        {
                            task_exit = true;
                        }
                            break;

                        /* 送信要求イベント */
                        case NativeMethods.WAIT_OBJECT_0 + 1:
                        {
                            NativeMethods.ResetEvent(task_send_req_event_);
                        }
                            break;

                        /* 送信完了イベント */
                        case NativeMethods.WAIT_OBJECT_0 + 2:
                        {
                            NativeMethods.GetOverlappedResult(handle_, ref send_overlapped, out send_size, true);
                            NativeMethods.ResetEvent(send_overlapped.EventHandle);

                            /* 全データが送信できなかった場合は残りを送信 */
                            if (send_size < send_buffer_.Length) {
                                send_buffer_ = ClassUtil.CloneCopy(send_buffer_, (int)send_size);
                                NativeMethods.WriteFile(handle_, send_buffer_, (uint)send_buffer_.Length, out send_size, ref send_overlapped);

                            /* 全データを送信できた場合は次のデータをリロード */
                            } else {
                                send_buffer_ = null;
                            }
                        }
                            break;

                        /* 受信イベント */
                        case NativeMethods.WAIT_OBJECT_0 + 3:
                        {
                            NativeMethods.GetOverlappedResult(handle_, ref recv_overlapped, out recv_size, true);
                            NativeMethods.ResetEvent(recv_overlapped.EventHandle);

                            if (recv_size > 0) {
                                /* 受信バッファの空きを詰める */
                                recv_data = recv_buffer_;
                                if (recv_size < recv_buffer_.Length) {
                                    recv_data = ClassUtil.CloneCopy(recv_buffer_, (int)recv_size);
                                }

                                /* 通知 */
                                NotifyRecvComplete("", "", "", recv_data);
                            }

                            NativeMethods.ReadFile(handle_, recv_buff, (uint)recv_buffer_.Length, out recv_size, ref recv_overlapped);
                        }
                            break;
                        
                        /**/
                        default:
                        {
                            var i = 0;
                        }
                            break;
                    }
                }

                /*  */
            }
        }
#endif

#if false
        private void SendPoll()
        {
            lock (send_sync_)
            {
                /* === 送信キュー状態を取得 === */
                var comm_error = (uint)0;
                var comm_stat = new NativeMethods.COMSTAT();

                /* 送信中であればスキップ */
                if (   (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat))
                    || (comm_stat.cbOutQue > 0)
                ) {
                    return;
                }

                /* 送信バッファが空の場合はキューからロード */
                if (send_buffer_ == null) {
                    send_buffer_ = GetSendData();
                }
                if (send_buffer_ == null)return;

                /* === 送信開始 === */
                uint send_size = 0;

                if (!NativeMethods.WriteFile(handle_, send_buffer_, (uint)send_buffer_.Length, out send_size, NativeMethods.Null))return;
                if (send_size == 0)return;

                var send_data = send_buffer_;
                var next_data = (byte[])null;

                if (send_size < send_data.Length) {
                    send_data = ClassUtil.CloneCopy(send_buffer_, (int)send_size);
                    next_data = ClassUtil.CloneCopy(send_buffer_, (int)send_size, int.MaxValue);
                }

                /* 通知 */
                NotifySendComplete("", "", "", send_data);

                /* セットできたデータを削除 */
                send_buffer_ = next_data;
            }
        }

        private unsafe void RecvPoll()
        {
            /* === 受信キュー状態を取得 === */
            var comm_error = (uint)0;
            var comm_stat = new NativeMethods.COMSTAT();

            /* --- 受信されていなければスキップ --- */
            if (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat))return;
            if (comm_stat.cbInQue == 0)return;

            /* === 受信開始 === */
            var read_data = new byte[comm_stat.cbInQue];
            var read_size = (uint)0;

            fixed (byte *buff = read_data)
            {
                if (!NativeMethods.ReadFile(handle_, buff, (uint)read_data.Length, out read_size, NativeMethods.Null))return;
                if (read_size == 0)return;
            }

            /* 受信バッファの空きを詰める */
            if (read_size < read_data.Length) {
                read_data = ClassUtil.CloneCopy(read_data, (int)read_size);
            }

            /* 通知 */
            NotifyRecvComplete("", "", "", read_data);
        }
#endif

#if true
        private void SendPoll()
        {
            var busy = false;

            SendPoll(ref busy);
        }
        private void SendPoll(ref bool busy)
        {
            lock (send_sync_)
            {
                /* === 送信キュー状態を取得 === */
                var comm_error = (uint)0;
                var comm_stat = new NativeMethods.COMSTAT();

                /* 送信中であればスキップ */
                if (   (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat))
                    || (comm_stat.cbOutQue > 0)
                ) {
                    busy = true;
                    return;
                }

                /* === 送信データリロード === */
                if (send_buffer_ == null) {
                    send_buffer_ = GetSendData();
                }
                if (send_buffer_ == null)return;

                /* === 送信開始 === */
                uint send_size = 0;

                if (!NativeMethods.WriteFile(handle_, send_buffer_, (uint)send_buffer_.Length, out send_size, NativeMethods.Null))return;
                if (send_size == 0)return;

                var send_data = send_buffer_;
                var next_data = (byte[])null;

                if (send_size < send_data.Length) {
                    send_data = ClassUtil.CloneCopy(send_buffer_, (int)send_size);
                    next_data = ClassUtil.CloneCopy(send_buffer_, (int)send_size, int.MaxValue);
                }

                /* 通知 */
                NotifySendComplete("", "", "", send_data);

                /* セットできたデータを削除 */
                send_buffer_ = next_data;

                busy = true;
            }
        }

        private unsafe void RecvPoll(ref bool busy)
        {
            /* === 受信キュー状態を取得 === */
            var comm_error = (uint)0;
            var comm_stat = new NativeMethods.COMSTAT();

            /* --- 受信されていなければスキップ --- */
            if (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat))return;
            if (comm_stat.cbInQue == 0)return;

            /* === 受信開始 === */
            var read_data = new byte[comm_stat.cbInQue];
            var read_size = (uint)0;

            fixed (byte *buff = read_data)
            {
                if (!NativeMethods.ReadFile(handle_, buff, (uint)read_data.Length, out read_size, NativeMethods.Null))return;
                if (read_size == 0)return;
            }

            /* 受信バッファの空きを詰める */
            if (read_size < read_data.Length) {
                read_data = ClassUtil.CloneCopy(read_data, (int)read_size);
            }

            /* 通知 */
            NotifyRecvComplete("", "", "", read_data);

            busy = true;
        }
#endif
    }
}
