using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Devices
{
    internal enum ConnectState
    {
        Disconnected,
        DisconnectBusy,
        ConnectBusy,
        Connected,
    }

    internal enum EventResult
    {
        Success,
        Error,
        Busy,
    }

    internal enum PollState
    {
        Idle,
        Busy,
    }

    internal abstract class DeviceInstance : IDisposable
    {
        private const int  DEVICE_THREAD_IVAL_BUSY = 1;
        private const int  DEVICE_THREAD_IVAL_IDLE = 15;

        private const long DISPOSE_TIMEOUT = 60000;

        private enum ConnectSequence
        {
            Disconnected,
            DisconnectBusy,
            DisconnectStart,
            ConnectStart,
            ConnectBusy,
            Connected,
        }


        public delegate void PacketCreatedDelegate(PacketObject packet);
        public delegate void StatusChangedDelegate();
        public delegate void SendDataRequstDelegate();

        public event PacketCreatedDelegate PacketCreated = delegate(PacketObject packet) { };
        public event StatusChangedDelegate StatusChanged = delegate() { };
        public event SendDataRequstDelegate SendDataRequest = delegate() { };


        private DeviceManager  devm_ = null;
        private DeviceClass    devd_ = null;
        private DeviceProperty devp_ = null;

        private Thread devt_;

        private Guid   id_;
        private string name_;

        private bool shutdown_req_   = false;
        private bool shutdown_state_ = false;

        private bool connect_req_ = false;
        private bool reboot_req_ = false;

        private bool send_enable_ = true;
        private bool recv_enable_ = true;

        private Queue<byte[]> send_data_queue_ = new Queue<byte[]>();
        private byte[]        send_data_ = null;
        private int           send_data_offset_ = 0;
        private object        send_data_sync_ = new object();

        private volatile ConnectSequence connect_seq_ = ConnectSequence.Disconnected;


        public DeviceInstance(DeviceManager devm, DeviceClass devd, DeviceProperty devp, Guid id, string name)
        {
            devm_ = devm;
            devd_ = devd;
            devp_ = devp;
            id_ = id;
            name_ = name;

            ThreadStart();
        }

        public virtual void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        private void ThreadStart()
        {
            devt_ = new Thread(OnThread);
            devt_.Start();
        }

        private void ThreadStop()
        {
            if (!devt_.Join(5000)) {
                devt_.Abort();
            }

            Dispose();
        }

        private void OnThread(object state)
        {
            while (!shutdown_state_) {
                /* タスク実行 */
                Poll();
            }
        }

        public DeviceManager Manager
        {
            get { return (devm_); }
        }

        public DeviceClass Class
        {
            get { return (devd_); }
        }

        public DeviceProperty Property
        {
            get { return (devp_); }
        }

        public Guid ID
        {
            get { return (id_); }
        }

        public bool IsShutdown
        {
            get { return ((shutdown_req_) && (shutdown_state_)); }
        }

        public void ShutdownRequest()
        {
            shutdown_req_ = true;
            ThreadStop();
        }

        public virtual string GetStatusString()
        {
            return (Property.GetStatusString());
        }

        public virtual bool HasControlPanel
        {
            get { return (false); }
        }

        public DeviceControlPanel CreateControlPanel()
        {
            return (OnCreateControlPanel());
        }

        public bool ConnectRequest
        {
            get { return (connect_req_); }
            set { connect_req_ = value;  }
        }

        public ConnectState ConnectStatus
        {
            get {
                if (connect_req_) {
                    return ((connect_seq_ == ConnectSequence.Connected) ?
                            (ConnectState.Connected) : (ConnectState.ConnectBusy));
                } else {
                    return ((connect_seq_ == ConnectSequence.Disconnected) ?
                            (ConnectState.Disconnected) : (ConnectState.DisconnectBusy));
                }
            }
        }

        public void ConnectReboot()
        {
            reboot_req_ = true;
        }

        private void LoadSendData()
        {
            lock (send_data_sync_) {
                if (send_data_ == null) {
                    
                }
            }
        }

        public void PushSendDataBlock(byte[] data)
        {
            if (data == null)return;
            if (ConnectStatus != ConnectState.Connected)return;

            /* 送信データブロックを追加 */
            lock (send_data_queue_) {
                send_data_queue_.Enqueue(data);
            }

            /* 送信割込み */
            OnSendInterrupt();
        }

        private byte[] PopSendDataBlock()
        {
            lock (send_data_queue_) {
                if (send_data_queue_.Count == 0)return (null);

                return (send_data_queue_.Dequeue());
            }
        }

        private void UpdateSendDataBlock()
        {
            lock (send_data_sync_) {
                /* バッファにデータが存在しない場合は新しいデータブロックを読み込み */
                if ((send_data_ == null) || (send_data_offset_ >= send_data_.Length)) {
                    /* データブロックキューが空の場合は上層にデータを要求 */
                    if (send_data_queue_.Count == 0) {
                        SendDataRequest();
                    }

                    /* データブロックキューからデータを取得 */
                    send_data_ = PopSendDataBlock();
                    if (send_data_ != null) {
                        send_data_offset_ = 0;
                    }
                }
            }
        }

        protected int GetSendData(byte[] buffer, int size)
        {
            if (buffer == null)return (0);

            size = Math.Min(buffer.Length, size);

            if (size == 0)return (0);

            UpdateSendDataBlock();

            lock (send_data_sync_) {
                if (send_data_ == null)return (0);

                /* コピーサイズ取得 */
                size = Math.Min(size, send_data_.Length - send_data_offset_);

                /* 送信データをバッファにコピー */
                Array.Copy(send_data_, send_data_offset_, buffer, 0, size);
                send_data_offset_ += size;

                return (size);
            }
        }

        protected byte[] GetSendData()
        {
            UpdateSendDataBlock();

            lock (send_data_sync_) {
                if (send_data_ == null)return (null);

                var data = (byte[])null;

                if (send_data_offset_ == 0) {
                    data = send_data_;
                } else {
                    data = new byte[send_data_.Length - send_data_offset_];
                    Array.Copy(send_data_, send_data_offset_, data, 0, data.Length);
                }

                send_data_offset_ += data.Length;

                return (data);
            }
        }

        public void NotifyPacket(PacketObject packet)
        {
            /* パケット生成イベント */
            PacketCreated(packet);

            /* パケットを登録 */
            devm_.SetupPacket(packet);
        }

        public void NotifyMessage(PacketPriority prio, string info, string message)
        {
            NotifyPacket(
                new MessagePacketObject(
                    PacketFacility.Device,
                    name_,
                    PacketPriority.Standard,
                    DateTime.UtcNow,
                    info,
                    0x00,
                    message));
        }

        public void NotifySendComplete(DateTime dt_utc, string info, string src, string dst, byte[] data)
        {
            NotifyPacket(
                new StaticDataPacketObject(
                    PacketFacility.Device,
                    name_,
                    PacketPriority.Standard,
                    dt_utc,
                    info,
                    PacketDirection.Send,
                    src,
                    dst,
                    0x00,
                    data));
        }

        public void NotifySendComplete(string info, string src, string dst, byte[] data)
        {
            NotifySendComplete(DateTime.UtcNow, info, src, dst, data);
        }

        public void NotifyRecvComplete(DateTime dt_utc, string info, string src, string dst, byte[] data)
        {
            NotifyPacket(
                new StaticDataPacketObject(
                    PacketFacility.Device,
                    name_,
                    PacketPriority.Standard,
                    dt_utc,
                    info,
                    PacketDirection.Recv,
                    src,
                    dst,
                    0x00,
                    data));
        }

        public void NotifyRecvComplete(string info, string src, string dst, byte[] data)
        {
            NotifyRecvComplete(DateTime.UtcNow, info, src, dst, data);
        }

        public void Poll()
        {
            var state = PollState.Idle;
            var connect_seq_prev = connect_seq_;
            var connect_req =  (connect_req_)
                            && (!reboot_req_)
                            && (!shutdown_req_);

            /* 接続/切断処理 */
            if (connect_req) {
                state = ConnectPoll();
            } else {
                state = DisconnectPoll();
            }

            if (connect_seq_prev != connect_seq_) {
                /* 状態変化を確認 */
                StatusChanged();
            } else {
                /* 状態変化がないときのみスレッドスリープ */
                Thread.Sleep((state == PollState.Busy) ? (DEVICE_THREAD_IVAL_BUSY) : (DEVICE_THREAD_IVAL_IDLE));
            }

            if (   (shutdown_req_)
                && (connect_seq_ == ConnectSequence.Disconnected)
            ) {
                shutdown_state_ = true;
            }
        }

        private PollState ConnectPoll()
        {
            var state = PollState.Idle;

            switch (connect_seq_) {
                case ConnectSequence.ConnectStart:
                {
                    NotifyMessage(PacketPriority.Notice, "<< Connect Start >>", "");
                    OnConnectStart();
                    connect_seq_++;
                    state = PollState.Busy;
                }
                    break;

                case ConnectSequence.ConnectBusy:
                {
                    if (OnConnectBusy() == EventResult.Success) {
                        NotifyMessage(PacketPriority.Notice, "<< Connected >>", "");
                        OnConnected();
                        connect_seq_++;
                        state = PollState.Busy;
                    }
                }
                    break;

                case ConnectSequence.Connected:
                {
                    state = OnPoll();

                    UpdateSendDataBlock();
                }
                    break;

                default:
                    connect_seq_++;
                    break;
            }

            return (state);
        }

        private PollState DisconnectPoll()
        {
            var state = PollState.Idle;

            switch (connect_seq_) {
                case ConnectSequence.DisconnectStart:
                {
                    NotifyMessage(PacketPriority.Notice, "<< Disconnect Start >>", "");
                    OnDisconnectStart();
                    connect_seq_--;
                    state = PollState.Busy;
                }
                    break;

                case ConnectSequence.DisconnectBusy:
                {
                    if (OnDisconnectBusy() == EventResult.Success) {
                        NotifyMessage(PacketPriority.Notice, "<< Disconnected >>", "");
                        OnDisconnected();
                        connect_seq_--;
                        state = PollState.Busy;
                    }
                }
                    break;

                case ConnectSequence.Disconnected:
                {
                    /* 再起動のための切断完了 */
                    reboot_req_ = false;
                }
                    break;

                default:
                    connect_seq_--;
                    break;
            }

            return (state);
        }

        protected virtual DeviceControlPanel OnCreateControlPanel() { return (null); }

        protected virtual EventResult OnConnectStart() { return (EventResult.Success); }
        protected virtual EventResult OnConnectBusy()  { return (EventResult.Success); }
        protected virtual void        OnConnected()    { }

        protected virtual void        OnDisconnectStart() { }
        protected virtual EventResult OnDisconnectBusy()  { return (EventResult.Success); }
        protected virtual void        OnDisconnected()    { }

        protected virtual PollState   OnPoll() { return (PollState.Idle); }

        protected virtual void        OnSendInterrupt() { }
    }
}
