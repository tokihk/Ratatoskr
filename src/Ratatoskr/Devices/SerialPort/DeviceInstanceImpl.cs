//#define ASYNC_MODE

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
    internal unsafe sealed class DeviceInstanceImpl : DeviceInstance
    {
        private const uint LOOP_CONTINUOUS_LIMIT = 10;
        private const uint RECV_BUFFER_SIZE = 8192;

        private const uint COMM_MASK = NativeMethods.EV_RXCHAR | NativeMethods.EV_TXEMPTY;

        private IntPtr handle_;

        private object  send_sync_ = new object();
        private byte[]  send_buffer_;

#if ASYNC_MODE
        private enum DeviceTaskEventType
        {
            Exit,
            CommEvent,
            SendRequest,
            SendComplete,
            RecvComplete,
        }

        private byte[]  recv_buffer_;

        private IAsyncResult device_task_ar_;

        private IntPtr           exit_event_ = IntPtr.Zero;

        private NativeOverlapped comm_ovl_;
        private IntPtr           comm_event_end_;
        private bool             comm_async_busy_ = false;
        private uint             comm_flag_ = 0;

        private NativeOverlapped send_ovl_;
        private IntPtr           send_event_req_;
        private IntPtr           send_event_end_;
        private bool             send_async_busy_ = false;
        private uint             send_size_ = 0;

        private NativeOverlapped recv_ovl_;
        private IntPtr           recv_event_end_;
        private bool             recv_async_busy_ = false;
        private uint             recv_size_ = 0;
#else
        private bool exit_req_ = false;

        private IAsyncResult device_task_send_ar_;
        private IAsyncResult device_task_recv_ar_;
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

#if ASYNC_MODE
            handle_ = NativeMethods.CreateFile(
                "\\\\.\\" + prop.PortName.Value,
                NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                0,
                NativeMethods.Null,
                NativeMethods.OPEN_EXISTING,
                NativeMethods.FILE_FLAG_OVERLAPPED,
                NativeMethods.Null);
#else
            handle_ = NativeMethods.CreateFile(
                "\\\\.\\" + prop.PortName.Value,
                NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                0,
                NativeMethods.Null,
                NativeMethods.OPEN_EXISTING,
                0,
                NativeMethods.Null);
#endif
            
            var error = NativeMethods.GetLastError();

            return ((handle_ != NativeMethods.INVALID_HANDLE_VALUE) ? (EventResult.Success) : (EventResult.Busy));
        }

        protected override void OnConnected()
        {
            SerialPortSetup(handle_, Property as DevicePropertyImpl);

            /* === バッファ初期化 === */
            send_buffer_ = null;

#if ASYNC_MODE
            recv_buffer_ = new byte[RECV_BUFFER_SIZE];

            /* イベント初期化 */
            exit_event_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            send_event_req_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            
            comm_ovl_ = new NativeOverlapped();
            comm_ovl_.EventHandle = comm_event_end_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);

            send_ovl_ = new NativeOverlapped();
            send_ovl_.EventHandle = send_event_end_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            send_async_busy_ = false;

            recv_ovl_ = new NativeOverlapped();
            recv_ovl_.EventHandle = recv_event_end_ = NativeMethods.CreateEvent(IntPtr.Zero, true, false, null);
            recv_async_busy_ = false;

            /* 送信/受信タスク開始 */
            device_task_ar_ = (new DeviceTaskHandler(DeviceTask)).BeginInvoke(null, null);
#else
            exit_req_ = false;

            /* 送信/受信タスク開始 */
            device_task_send_ar_ = (new DeviceTaskHandler(DeviceSendTask)).BeginInvoke(null, null);
            device_task_recv_ar_ = (new DeviceTaskHandler(DeviceRecvTask)).BeginInvoke(null, null);
#endif
        }

        protected override void OnDisconnectStart()
        {
#if ASYNC_MODE
            /* タスク停止イベント */
            NativeMethods.ResetEvent(exit_event_);
            NativeMethods.SetEvent(exit_event_);

            /* 送信/受信タスク停止 */
            while ((device_task_ar_ != null) && (!device_task_ar_.IsCompleted)) {
                Thread.Sleep(1);
            }

            /* イベント破棄 */
            NativeMethods.CloseHandle(exit_event_);
            exit_event_ = IntPtr.Zero;

            NativeMethods.CloseHandle(comm_event_end_);
            comm_event_end_ = IntPtr.Zero;

            NativeMethods.CloseHandle(send_event_req_);
            send_event_req_ = IntPtr.Zero;

            NativeMethods.CloseHandle(send_event_end_);
            send_event_end_ = IntPtr.Zero;

            NativeMethods.CloseHandle(recv_event_end_);
            recv_event_end_ = IntPtr.Zero;
#else
            exit_req_ = true;

            /* 処理中の操作を全てキャンセル */
            NativeMethods.PurgeComm(
                handle_,
                  NativeMethods.PURGE_RXABORT
                | NativeMethods.PURGE_RXCLEAR
                | NativeMethods.PURGE_TXABORT
                | NativeMethods.PURGE_TXCLEAR);

            NativeMethods.EscapeCommFunction(handle_, NativeMethods.CLRDTR);
            NativeMethods.SetCommMask(handle_, 0);

            /* 受信タスク停止 */
            while (   ((device_task_send_ar_ != null) && (!device_task_send_ar_.IsCompleted))
                   || ((device_task_recv_ar_ != null) && (!device_task_recv_ar_.IsCompleted))
            ) {
                Thread.Sleep(1);
            }
#endif

            /* ポートクローズ */
            if (handle_ != NativeMethods.INVALID_HANDLE_VALUE) {
                NativeMethods.CloseHandle(handle_);
                handle_ = NativeMethods.INVALID_HANDLE_VALUE;
            }
        }

        protected override PollState OnPoll()
        {
            return (PollState.Idle);
        }

        protected override void OnSendRequest()
        {
#if ASYNC_MODE
            NativeMethods.SetEvent(send_event_req_);
#else
            DeviceSendExec();
#endif
        }

        private delegate void DeviceTaskHandler();

#if ASYNC_MODE
        private unsafe void DeviceTask()
        {
            var event_list = new IntPtr[] { exit_event_, send_event_req_, send_event_end_, recv_event_end_ };

            fixed (byte *recv_buffer_p = recv_buffer_)
            {
                var task_exit_req = false;
                var result = (uint)0;

                while (!task_exit_req) {
                    /* COMイベント取得処理 */
//                    DeviceCommTask();

                    /* 送信処理 */
                    DeviceSendTask();

                    /* 受信処理 */
                    DeviceRecvTask(recv_buffer_p, (uint)recv_buffer_.Length);

                    /* イベント待ち */
                    result = (uint)NativeMethods.WaitForMultipleObjects(
                                    (uint)event_list.Length,
                                    event_list, false,
                                    NativeMethods.FILE_DEVICE_INFINIBAND);

                    switch (result) {
                        /* タスク終了イベント */
                        case (NativeMethods.WAIT_OBJECT_0 + (uint)DeviceTaskEventType.Exit):
                        {
                            task_exit_req = true;
                        }
                            break;

                        /* COMイベント取得イベント */
                        case (NativeMethods.WAIT_OBJECT_0 + (uint)DeviceTaskEventType.CommEvent):
                        {
                        }
                            break;

                        /* 送信要求イベント */
                        case (NativeMethods.WAIT_OBJECT_0 + (uint)DeviceTaskEventType.SendRequest):
                        {
                            NativeMethods.ResetEvent(send_event_req_);
                        }
                            break;
                        
                        /* 送信完了イベント */
                        case (NativeMethods.WAIT_OBJECT_0 + (uint)DeviceTaskEventType.SendComplete):
                        {
                        }
                            break;

                        /* 受信完了イベント */
                        case (NativeMethods.WAIT_OBJECT_0 + (uint)DeviceTaskEventType.RecvComplete):
                        {
                        }
                            break;
                        
                        default:
                            break;
                    }
                }
            }
        }

        private void DeviceCommTask()
        {
            /* 非同期イベント取得の完了処理 */
            if (comm_async_busy_) {
                if (NativeMethods.GetOverlappedResult(handle_, ref comm_ovl_, out comm_flag_, false)) {
                    /* イベント取得成功 */
                    CommEventComplete(comm_flag_);
                } else {
                    /* APIエラー */
                }
                comm_async_busy_ = false;
            }

            /* イベント取得処理開始 */
            /* (最大連続受信回数に達するか非同期受信が開始されるまで繰り返す) */
            for (var loop_count = 0; loop_count < LOOP_CONTINUOUS_LIMIT && !send_async_busy_; loop_count++) {
                if (!NativeMethods.WaitCommEvent(handle_, out comm_flag_, comm_ovl_)) {
                    if (NativeMethods.GetLastError() == NativeMethods.ERROR_IO_PENDING) {
                        /* 非同期イベント取得開始 */
                        comm_async_busy_ = true;
                    } else {
                        /* APIエラー */
                    }
                } else {
                    /* 非同期無しでイベント取得完了 */
                    CommEventComplete(comm_flag_);
                }
            }
        }

        private void DeviceSendTask()
        {
            /* 非同期送信の完了処理 */
            if (send_async_busy_) {
                if (NativeMethods.GetOverlappedResult(handle_, ref send_ovl_, out send_size_, false)) {
                    /* 送信成功 */
                    SendComplete(send_buffer_, send_size_);
                } else {
                    /* APIエラー */
                }
                send_async_busy_ = false;
            }

            /* 送信処理開始 */
            /* (最大連続受信回数に達するか非同期受信が開始されるまで繰り返す) */
            for (var loop_count = 0; loop_count < LOOP_CONTINUOUS_LIMIT && !send_async_busy_; loop_count++) {
                /* 送信データ読み込み */
                if (send_buffer_ == null) {
                    send_buffer_ = GetSendData();

                    /* 送信データが存在しない場合は終了 */
                    if (send_buffer_ == null)break;
                }

                if (!NativeMethods.WriteFile(handle_, send_buffer_, (uint)send_buffer_.Length, out send_size_, ref send_ovl_)) {
                    if (NativeMethods.GetLastError() == NativeMethods.ERROR_IO_PENDING) {
                        /* 非同期送信開始 */
                        send_async_busy_ = true;
                    } else {
                        /* APIエラー */
                    }
                } else {
                    /* 非同期無しで送信完了 */
                    SendComplete(send_buffer_, send_size_);
                }
            }
        }

        private unsafe void DeviceRecvTask(byte *recv_buff, uint recv_buff_size)
        {
            /* 非同期受信の完了処理 */
            if (recv_async_busy_) {
                if (NativeMethods.GetOverlappedResult(handle_, ref recv_ovl_, out recv_size_, false)) {
                    /* 受信成功 */
                    RecvComplete(recv_buffer_, recv_size_);
                } else {
                    /* APIエラー */
                }
                recv_async_busy_ = false;
            }

            /* 受信処理開始 */
            /* (最大連続受信回数に達するか非同期受信が開始されるまで繰り返す) */
            for (var loop_count = 0; loop_count < LOOP_CONTINUOUS_LIMIT && !recv_async_busy_; loop_count++) {
                if (!NativeMethods.ReadFile(handle_, recv_buff, recv_buff_size, out recv_size_, ref recv_ovl_)) {
                    if (NativeMethods.GetLastError() == NativeMethods.ERROR_IO_PENDING) {
                        /* 非同期受信開始 */
                        recv_async_busy_ = true;
                    } else {
                        /* APIエラー */
                    }
                } else {
                    /* 非同期無しで受信完了 */
                    RecvComplete(recv_buffer_, recv_size_);
                }
            }
        }

#else

        private void DeviceSendTask()
        {
            var busy = false;

            while (!exit_req_) {
                DeviceSendExec(ref busy);

                if (!busy) {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        private void DeviceRecvTask()
        {
            var busy = false;

            while (!exit_req_) {
                DeviceRecvExec(ref busy);

                if (!busy) {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        private void DeviceSendExec()
        {
            bool busy = false;

            DeviceSendExec(ref busy);
        }

        private void DeviceSendExec(ref bool busy)
        {
            busy = false;

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

                /* 送信データ読込 */
                if (send_buffer_ == null) {
                    send_buffer_ = GetSendData();
                }

                /* 送信データが存在しない */
                if (send_buffer_ == null) {
                    return;
                }

                /* === 送信開始 === */
                uint send_size = 0;

                if (!NativeMethods.WriteFile(handle_, send_buffer_, (uint)send_buffer_.Length, out send_size, NativeMethods.Null))return;

                if (send_size == 0)return;

                SendComplete(send_buffer_, send_size);

                busy = true;
            }
        }

        private void DeviceRecvExec(ref bool busy)
        {
            busy = false;

            /* === 受信サイズを取得 === */
            var comm_error = (uint)0;
            var comm_stat = new NativeMethods.COMSTAT();

            /* 受信していなければスキップ */
            if (   (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat))
                || (comm_stat.cbInQue == 0)
            ) {
                return;
            }

            var recv_data = new byte[comm_stat.cbInQue];
            var read_size = (uint)0;

            fixed (byte *buff = recv_data)
            {
                if (!NativeMethods.ReadFile(handle_, buff, (uint)recv_data.Length, out read_size, NativeMethods.Null))return;
            }

            if (read_size == 0)return;

            RecvComplete(recv_data, read_size);

            busy = true;
        }
#endif

        private void SerialPortSetup(IntPtr handle, DevicePropertyImpl prop)
        {
            /* === COMポート設定読み込み === */
            var dcb = new NativeMethods.DCB();

            NativeMethods.GetCommState(handle, out dcb);

            /* Baudrate */
            dcb.BaudRate = (uint)prop.BaudRate.Value;

            /* Parity */
            switch (prop.Parity.Value) {
                case Parity.None:   dcb.Parity = NativeMethods.NOPARITY;       break;
                case Parity.Odd:    dcb.Parity = NativeMethods.ODDPARITY;      break;
                case Parity.Even:   dcb.Parity = NativeMethods.EVENPARITY;     break;
                case Parity.Mark:   dcb.Parity = NativeMethods.MASKPARITY;     break;
                case Parity.Space:  dcb.Parity = NativeMethods.SPACEPARITY;    break;
                default:            dcb.Parity = NativeMethods.NOPARITY;       break;
            }

            /* BitSize */
            dcb.ByteSize = (byte)prop.DataBits.Value;

            /* StopBits */
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

            /* === COMポート設定更新 === */
            if (!NativeMethods.SetCommState(handle, ref dcb)) {
                /* API失敗時は再接続 */
                ConnectReboot();
                return;
            }

            /* === タイムアウト設定 === */
            var timeout = new NativeMethods.COMMTIMEOUTS();

            timeout.ReadIntervalTimeout = 0;
            timeout.ReadTotalTimeoutMultiplier = 0;
            timeout.ReadTotalTimeoutConstant = 10;
            timeout.WriteTotalTimeoutConstant = 100;

            NativeMethods.SetCommTimeouts(handle, ref timeout);

            /* === イベント設定 === */
            if (!NativeMethods.SetCommMask(
                handle,
//                  NativeMethods.EV_BREAK
//                | NativeMethods.EV_CTS
//                | NativeMethods.EV_DSR
//                | NativeMethods.EV_ERR
//                | NativeMethods.EV_RING
//                | NativeMethods.EV_RLSD
                  NativeMethods.EV_RXCHAR
//                | NativeMethods.EV_RXFLAG
//                | NativeMethods.EV_TXEMPTY
                )
            ) {
                return;
            }
        }

        private void CommEventComplete(uint event_flag)
        {
        }

        private void SendComplete(byte[] send_data, uint send_size)
        {
            var next_data = (byte[])null;

            if (send_data.Length > send_size) {
                send_data = ClassUtil.CloneCopy(send_buffer_, (int)send_size);
                next_data = ClassUtil.CloneCopy(send_buffer_, (int)send_size, int.MaxValue);
            }

            /* 通知 */
            NotifySendComplete("", "", "", send_data);

            /* セットできたデータを削除 */
            send_buffer_ = next_data;
        }

        private void RecvComplete(byte[] recv_data, uint recv_size)
        {
            /* 受信バッファの空きを詰める */
            if (recv_data.Length > recv_size) {
                recv_data = ClassUtil.CloneCopy(recv_data, (int)recv_size);
            }

            /* 通知 */
            NotifyRecvComplete("", "", "", recv_data);
        }

    }
}
