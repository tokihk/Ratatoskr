﻿//#define ASYNC_MODE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RtsCore.Framework.Device;
using RtsCore.Framework.Drivers.SerialPort;
using RtsCore.Generic;
using RtsCore.Packet;

namespace Ratatoskr.Devices.SerialPort
{
    internal unsafe sealed class DeviceInstanceImpl : DeviceInstance
    {
        private const uint LOOP_CONTINUOUS_LIMIT = 10;
        private const uint RECV_BUFFER_SIZE = 8192;

        private DevicePropertyImpl prop_;

        private SerialPortController port_ = new SerialPortController();

        private object  send_sync_ = new object();
        private byte[]  send_buffer_;

        private Stopwatch send_wait_timer_       = new Stopwatch();
        private uint      send_wait_time_        = 0;

        private Stopwatch recv_hold_timer_ = new Stopwatch();

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

        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
            port_.CommStatusUpdated += OnCommStatusUpdated;
        }

        protected override EventResult OnConnectStart()
        {
            prop_ = Property as DevicePropertyImpl;

            port_.PortName = prop_.PortName.Value;
            port_.BaudRate = (uint)prop_.BaudRate.Value;
            port_.Parity = prop_.Parity.Value;
            port_.DataBits = (byte)prop_.DataBits.Value;
            port_.StopBits = prop_.StopBits.Value;

            port_.fOutxCtsFlow = prop_.fOutxCtsFlow.Value;
            port_.fOutxDsrFlow = prop_.fOutxDsrFlow.Value;
            port_.fDsrSensitivity = prop_.fDsrSensitivity.Value;
            port_.fTXContinueOnXoff = prop_.fTXContinueOnXoff.Value;
            port_.fOutX = prop_.fOutX.Value;
            port_.fInX = prop_.fInX.Value;

            port_.fDtrControl = prop_.fDtrControl.Value;
            port_.fRtsControl = prop_.fRtsControl.Value;

            port_.XonLim = (ushort)prop_.XonLim.Value;
            port_.XoffLim = (ushort)prop_.XoffLim.Value;
            port_.XonChar = (sbyte)prop_.XonChar.Value;
            port_.XoffChar = (sbyte)prop_.XoffChar.Value;

            port_.SimplexMode = prop_.SimplexMode.Value;

            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            if (!port_.Open()) {
                return (EventResult.Busy);
            }

            if (!port_.Setup()) {
                return (EventResult.Busy);
            }

            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
            port_.Setup();

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
            Debugger.DebugManager.MessageOut("Serial Port - Disconnect Start");
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

            port_.Purge();

            /* 受信タスク停止 */
            while (   ((device_task_send_ar_ != null) && (!device_task_send_ar_.IsCompleted))
                   || ((device_task_recv_ar_ != null) && (!device_task_recv_ar_.IsCompleted))
            ) {
                Thread.Sleep(1);
            }
#endif

            /* ポートクローズ */
            port_.Close();

            Debugger.DebugManager.MessageOut("Serial Port - Disconnect Start - End");
        }

        protected override PollState OnPoll()
        {
            if (port_.GetDeviceDetachStatus()) {
                NotifyMessage(PacketPriority.Alert, "Device Event", "Device has been disconnected.");
                ConnectReboot();
            }

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
                /* 送信遅延中であればスキップ */
                if (send_wait_timer_.IsRunning) {
                    if (send_wait_timer_.ElapsedMilliseconds >= send_wait_time_) {
                        send_wait_timer_.Stop();
                    }
                }

                /* 受信中であればスキップ */
                if (recv_hold_timer_.IsRunning) {
                    if (recv_hold_timer_.ElapsedMilliseconds >= prop_.RecvHoldTimer.Value) {
                        recv_hold_timer_.Stop();
                    }
                }

                /* 送信中であればスキップ */
                if (   (port_.IsWriteBusy)
                    || (send_wait_timer_.IsRunning)
                    || (recv_hold_timer_.IsRunning)
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

                var send_data = send_buffer_;

                /* バイト単位の送信遅延が設定されている場合は1バイトずつ */
                if (prop_.SendByteWaitTimer.Value > 0) {
                    send_data = new byte[1] { send_buffer_[0] };
                }

                /* 送信開始 */
                var send_size = port_.Write(send_data);

                if (send_size == 0)return;

                SendComplete(send_buffer_, send_size);

                busy = true;
            }
        }

        private void DeviceRecvExec(ref bool busy)
        {
            busy = false;

            /* 受信していなければスキップ */
            var recv_size = port_.RecvDataSize;

            if (recv_size == 0)return;

            var recv_data = new byte[recv_size];

            /* 受信 */
            recv_size = port_.Recv(recv_data);

            if (recv_size == 0)return;

            RecvComplete(recv_data, recv_size);

            busy = true;
        }
#endif

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

            /* 送信遅延を設定 */
            send_wait_time_ = (uint)((send_buffer_ != null)
                            ? (prop_.SendByteWaitTimer.Value)
                            : (prop_.SendPacketWaitTimer.Value));

            if (send_wait_time_ > 0) {
                send_wait_timer_.Restart();
            }
        }

        private void RecvComplete(byte[] recv_data, uint recv_size)
        {
            /* 受信バッファの空きを詰める */
            if (recv_data.Length > recv_size) {
                recv_data = ClassUtil.CloneCopy(recv_data, (int)recv_size);
            }

            /* 通知 */
            NotifyRecvComplete("", "", "", recv_data);

            /* 受信ホールドタイマー */
            if (prop_.RecvHoldTimer.Value > 0) {
                recv_hold_timer_.Restart();
            }
        }

        private void CommEventMessage(string msg)
        {
            NotifyMessage(
                PacketPriority.Notice,
                "Comm Event",
                msg);
        }

        private void CommEventMessage(string event_name, string status)
        {
            CommEventMessage(string.Format("{0} {1}", event_name, status));
        }

        private void CommEventMessage(string event_name, bool status)
        {
            CommEventMessage(event_name, (status) ? ("ON") : ("OFF"));
        }

        private void OnCommStatusUpdated(object sender, SerialPortController.CommStatusUpdatedEventArgs e)
        {
            if (e.ErrorStatus != 0) {
                NotifyMessage(PacketPriority.Error, "Comm Error", e.ErrorStatus.ToString());
            }

            if (e.CommStatusMask != 0) {
                if (e.CommStatusMask.HasFlag(CommStatus.CTS_HOLD)) {
                    CommEventMessage("Dst -> Src - CTS", e.CommStatus.HasFlag(CommStatus.CTS_HOLD));
                }

                if (e.CommStatusMask.HasFlag(CommStatus.DSR_HOLD)) {
                    CommEventMessage("Dst -> Src - DSR", e.CommStatus.HasFlag(CommStatus.DSR_HOLD));
                }

                if (e.CommStatusMask.HasFlag(CommStatus.RLSD_HOLD)) {
                    CommEventMessage("Dst -> Src - RLSD", e.CommStatus.HasFlag(CommStatus.RLSD_HOLD));
                }

                if (e.CommStatusMask.HasFlag(CommStatus.XOFF_HOLD)) {
                    if (e.CommStatus.HasFlag(CommStatus.XOFF_HOLD)) {
                        CommEventMessage("Dst -> Src - XOFF");
                    } else {
                        CommEventMessage("Dst -> Src - XON");
                    }
                }

                if (e.CommStatusMask.HasFlag(CommStatus.XOFF_SENT)) {
                    if (e.CommStatus.HasFlag(CommStatus.XOFF_SENT)) {
                        CommEventMessage("Src -> Dst - XOFF");
                    } else {
                        CommEventMessage("Src -> Dst - XON");
                    }
                }
            }
        }
    }
}
