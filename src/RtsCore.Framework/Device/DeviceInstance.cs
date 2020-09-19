using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using RtsCore.Generic;
using RtsCore.Packet;
using RtsCore.Utility;

namespace RtsCore.Framework.Device
{
    public enum ConnectState
    {
        Disconnected,
        DisconnectBusy,
        ConnectBusy,
        Connected,
    }

    public enum EventResult
    {
        Success,
        Error,
        Busy,
    }

    public enum PollState
    {
        Idle,
        Active,
        Busy,
    }

    [Flags]
    public enum DeviceDataRateTarget
    {
        SendData = 1 << 0,
        RecvData = 1 << 1,
    }

    public abstract class DeviceInstance : IDisposable
    {
        private const int  DEVICE_THREAD_IVAL_ACTIVE = 1;
        private const int  DEVICE_THREAD_IVAL_IDLE   = 200;
        
        private const long DISPOSE_TIMEOUT = 60000;

        private enum DevicePollEventID
        {
            ActiveRequest,
            DataRateSampling,
        }

        private enum ConnectSequence
        {
            Disconnected,
            DisconnectBusy,
            DisconnectStart,
            ConnectStart,
            ConnectBusy,
            Connected,
        }

        public enum SendRequestStatus
        {
            Accept,         // 正常に受理された/キューに追加された
            Ignore,         // 無視された/捨てられた
            Pending,        // 今は判断できない/後で再トライ
        }


        public delegate void StatusChangedDelegate();
        public event StatusChangedDelegate StatusChanged = delegate() { };

        public delegate void SendDataRequstDelegate();
        public event SendDataRequstDelegate SendDataRequest = delegate() { };

        public delegate void DataRateUpdatedHandler(object sender, ulong value);
        public event DataRateUpdatedHandler DataRateUpdated = delegate(object sender, ulong value) { };

        private DeviceManagementClass  devm_ = null;
        private DeviceConfig   devconf_ = null;
        private DeviceClass    devd_ = null;
        private DeviceProperty devp_ = null;

        private Thread devt_;

        private string alias_ = "";
        private string alias_redirect_ = "";

        private DeviceInstance[] redirect_list_ = null;

        private bool shutdown_req_   = false;
        private bool shutdown_state_ = false;

        private bool connect_req_ = false;
        private bool reboot_req_ = false;

        private Queue<byte[]> send_queue_user_ = new Queue<byte[]>();
        private Queue<byte[]> send_queue_redirect_ = new Queue<byte[]>();
        private object        send_queue_sync_ = new object();

        private byte[] send_data_busy_ = null;
        private int    send_data_offset_ = 0;
        private object send_data_sync_ = new object();

        private volatile ConnectSequence connect_seq_ = ConnectSequence.Disconnected;
        private volatile ConnectState    connect_state_ = ConnectState.Disconnected;

        private Stopwatch       data_rate_timer_ = new Stopwatch();
        private ulong           data_rate_value_busy_ = 0;

        private WaitHandle[]        device_poll_events_ = null;
        private int                 device_poll_ival_ = 0;

        private ManualResetEvent    shutdown_event_ = new ManualResetEvent(false);
        private AutoResetEvent      active_request_event_ = new AutoResetEvent(false);


        public DeviceInstance(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
        {
            devm_ = devm;
            devconf_ = devconf;
            devd_ = devd;
            devp_ = devp;

            data_rate_timer_.Restart();

            /* イベント初期化 */
            device_poll_events_ = new WaitHandle[Enum.GetValues(typeof(DevicePollEventID)).Length];
            device_poll_events_[(int)DevicePollEventID.ActiveRequest] = active_request_event_;
            device_poll_events_[(int)DevicePollEventID.DataRateSampling] = devm_.WaitHandle_1000ms;
        }

        public virtual void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public DeviceManagementClass Manager
        {
            get { return (devm_); }
        }

        public DeviceConfig Config
        {
            get { return (devconf_); }
        }

        public DeviceClass Class
        {
            get { return (devd_); }
        }

        public DeviceProperty Property
        {
            get { return (devp_); }
        }

        public string Alias
        {
            get { return (alias_); }
            set
            {
                alias_ = value ?? "";
                devm_.UpdateRedirectMap();
            }
        }

        public string RedirectAlias
        {
            get { return (alias_redirect_); }
            set
            {
                alias_redirect_ = value ?? "";
                devm_.UpdateRedirectMap();
            }
        }

        public DeviceDataRateTarget DataRateTarget { get; set; } = 0;
        public ulong                DataRateValue  { get; private set; } = 0;

        public bool IsShutdown
        {
            get { return ((shutdown_req_) && (shutdown_state_)); }
        }

        public void DeviceStart()
        {
            ThreadStart();
        }

        public void DeviceShutdownRequest()
        {
            shutdown_req_ = true;
            ThreadStop();
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

        private (bool discard_req, SendRequestStatus status) PushSendData(Queue<byte[]> data_queue, uint queue_limit, byte[] data)
        {
            if (   (data == null)           // データが存在しない
                || (!Config.SendEnable)     // 送信禁止状態
                || (!connect_req_)          // 切断要求
            ) {
                return (true, SendRequestStatus.Ignore);
            }

            lock (send_queue_sync_) {
                /* 送信データキューに空が無い場合は失敗 */
                if (data_queue.Count >= queue_limit) {
                    return (false, SendRequestStatus.Pending);
                }

                /* 送信データを追加 */
                data_queue.Enqueue(data);
            }

            Kernel.DebugMessage("DeviceInstance.OnSendRequest", SystemMessageAttr.System | SystemMessageAttr.SendAction);

            /* 送信要求に対する最速の通知 */
            OnSendRequest();

            /* Device Pollを即座に呼び出す */
            ActiveReqeust();

            return (true, SendRequestStatus.Accept);
        }

        public (bool discard_req, SendRequestStatus status) PushSendUserData(byte[] data)
        {
            return (PushSendData(send_queue_user_, Config.SendDataQueueLimit, data));
        }

        public (bool discard_req, SendRequestStatus status) PushRedirectData(byte[] data)
        {
            return (PushSendData(send_queue_redirect_, Config.RedirectDataQueueLimit, data));
        }

        protected void ActiveReqeust()
        {
            active_request_event_.Set();
        }

        private byte[] PopSendData()
        {
            lock (send_queue_sync_) {
                /* リダイレクトデータ */
                if (send_queue_redirect_.Count > 0) {
                    return (send_queue_redirect_.Dequeue());
                }

                /* 通常送信データ */
                if (send_queue_user_.Count > 0) {
                    return (send_queue_user_.Dequeue());
                }

                return (null);
            }
        }

        private void LoadSendBuffer()
        {
            lock (send_data_sync_) {
                if (send_data_busy_ == null) {
                    send_data_busy_ = PopSendData();
                    if (send_data_busy_ != null) {
                        send_data_offset_ = 0;
                    }
                }
            }
        }

        protected int GetSendData(byte[] buffer, int size)
        {
            if (buffer == null)return (0);

            /* 読込サイズをバッファサイズで補正 */
            size = Math.Min(buffer.Length, size);
            if (size == 0)return (0);

            /* 送信データ準備 */
            LoadSendBuffer();

            lock (send_data_sync_) {
                /* 送信データが存在しないときは終了 */
                if (send_data_busy_ == null)return (0);

                /* コピーサイズ取得 */
                var send_size = Math.Min(size, send_data_busy_.Length - send_data_offset_);

                /* 送信データをバッファにコピー */
                if (send_size > 0) {
                    Buffer.BlockCopy(send_data_busy_, send_data_offset_, buffer, 0, send_size);
                    send_data_offset_ += send_size;
                }

                /* 最後まで送信したときはバッファをクリア */
                if (send_data_offset_ >= send_data_busy_.Length) {
                    send_data_offset_ = 0;
                    send_data_busy_ = null;
                }

                Kernel.DebugMessage("DeviceInstance.GetSendData", SystemMessageAttr.Device | SystemMessageAttr.SendAction);

                return (send_size);
            }
        }

        protected byte[] GetSendData()
        {
            /* 送信データ準備 */
            LoadSendBuffer();

            lock (send_data_sync_) {
                /* 送信データが存在しないときは終了 */
                if (send_data_busy_ == null)return (null);

                var send_data = (byte[])null;

                if (send_data_offset_ == 0) {
                    /* 送信バッファを全てセットするのでオブジェクトをそのまま流用 */
                    send_data = send_data_busy_;

                } else {
                    /* 送信バッファの途中から適用するのでバッファを用意 */
                    send_data = ClassUtil.CloneCopy(send_data_busy_, send_data_offset_, send_data_busy_.Length);
                }

                /* 送信バッファをクリア */
                send_data_busy_ = null;
                send_data_offset_ = 0;

                Kernel.DebugMessage("DeviceInstance.GetSendData", SystemMessageAttr.Device | SystemMessageAttr.SendAction);

                return (send_data);
            }
        }

        internal void UpdateRedirectMap(IEnumerable<DeviceInstance> devi_list_all)
        {
            var alias_list = RedirectAlias.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            /* パターン一致するデバイスを検索 */
            var devi_list = from alias in alias_list
                            from devi in devi_list_all
                            where devi != this
                            where !devi.IsShutdown
                            where alias == devi.Alias
                            select devi;

            if (   (devi_list != null)
                && (devi_list.Count() > 0)
            ) {
                redirect_list_ = devi_list.ToArray();
            } else {
                redirect_list_ = null;
            }
        }

        private void RedirectRequest(byte[] data)
        {
            /* 途中でリダイレクト先が変化しないようにバックアップで処理 */
            var redirect_list = redirect_list_;

            if (redirect_list == null)return;

            foreach (var target in redirect_list) {
                target.PushRedirectData(data);
            }
        }

        private void DataRateSampling()
        {
            if (data_rate_timer_.ElapsedMilliseconds >= 1000) {
                data_rate_timer_.Restart();

                DataRateValue = data_rate_value_busy_;
                data_rate_value_busy_ = 0;

                DataRateUpdated(this, DataRateValue);
            }
        }

        private void DataRateValueUpdate(PacketObject packet)
        {
            if (DataRateTarget == 0)return;

            /* 通信レート計算 */
            if (packet.Attribute == PacketAttribute.Data) {
                if (   ((packet.Direction == PacketDirection.Recv) && (DataRateTarget.HasFlag(DeviceDataRateTarget.RecvData)))
                    || ((packet.Direction == PacketDirection.Send) && (DataRateTarget.HasFlag(DeviceDataRateTarget.SendData)))
                ) {
                    data_rate_value_busy_ += (ulong)packet.DataLength;
                }
            }
        }

        public void NotifyPacket(PacketObject packet)
        {
            /* パケットを登録 */
            devm_.SetupPacket(packet);

            /* データレートを計算 */
            DataRateValueUpdate(packet);
        }

        public void NotifyMessage(PacketPriority prio, string info, string message)
        {
            NotifyPacket(
                new PacketObject(
                    Class.DescID,
                    PacketFacility.Device,
                    alias_,
                    PacketPriority.Standard,
                    PacketAttribute.Message,
                    HighResolutionDateTime.UtcNow,
                    info,
                    PacketDirection.Recv,
                    "",
                    "",
                    0x00,
                    message,
                    null));
        }

        public void NotifySendComplete(DateTime dt_utc, string info, string src, string dst, byte[] data)
        {
            NotifyPacket(
                new PacketObject(
                    Class.DescID,
                    PacketFacility.Device,
                    alias_,
                    PacketPriority.Standard,
                    PacketAttribute.Data,
                    dt_utc,
                    info,
                    PacketDirection.Send,
                    src,
                    dst,
                    0x00,
                    null,
                    data));
        }

        public void NotifySendComplete(string info, string src, string dst, byte[] data)
        {
            NotifySendComplete(HighResolutionDateTime.UtcNow, info, src, dst, data);
        }

        public void NotifyRecvComplete(DateTime dt_utc, string info, string src, string dst, byte[] data)
        {
            /* 受信禁止状態のときは無視 */
            if (!Config.RecvEnable)return;

            /* リダイレクト要求 */
            RedirectRequest(data);

            NotifyPacket(
                new PacketObject(
                    Class.DescID,
                    PacketFacility.Device,
                    alias_,
                    PacketPriority.Standard,
                    PacketAttribute.Data,
                    dt_utc,
                    info,
                    PacketDirection.Recv,
                    src,
                    dst,
                    0x00,
                    null,
                    data));
        }

        public void NotifyRecvComplete(string info, string src, string dst, byte[] data)
        {
            NotifyRecvComplete(HighResolutionDateTime.UtcNow, info, src, dst, data);
        }

        public void Poll()
        {
            /* イベント発生待ち */
            var wait_result = WaitHandle.WaitAny(device_poll_events_, device_poll_ival_);

#if DEBUG
            if (wait_result == (int)DevicePollEventID.ActiveRequest) {
                RtsCore.Kernel.DebugMessage(string.Format("DeviceInstance.Poll = {0}", wait_result), SystemMessageAttr.Device);
            }
#endif

            /* 接続/切断処理 */
            var state = ConnectPoll();

            /* シャットダウン判定 */
            if (   (shutdown_req_)
                && (connect_seq_ == ConnectSequence.Disconnected)
            ) {
                shutdown_state_ = true;
            }

            /* データレート計算 */
            if (wait_result == (int)DevicePollEventID.DataRateSampling) {
                DataRateSampling();
            }

            /* スリープ処理 */
            switch (state) {
                case PollState.Active:  device_poll_ival_ = DEVICE_THREAD_IVAL_ACTIVE;    break;
                case PollState.Idle:    device_poll_ival_ = DEVICE_THREAD_IVAL_IDLE;      break;
            }
        }

        private PollState ConnectPoll()
        {
            var state = PollState.Idle;
            var connect_seq_prev = connect_seq_;
            var connect_req =  (connect_req_)
                            && (!reboot_req_)
                            && (!shutdown_req_);

            /* 接続/切断処理 */
            if (connect_req) {
                state = ConnectProc();
            } else {
                state = DisconnectProc();
            }

            /* 状態変化チェック */
            if (connect_seq_ != connect_seq_prev) {
                state = PollState.Active;
            }

            var connect_state = ConnectStatus;

            if (connect_state_ != connect_state) {
                connect_state_ = connect_state;
                StatusChanged();
            }

            return (state);
        }

        private PollState ConnectProc()
        {
            var state = PollState.Idle;

            switch (connect_seq_) {
                case ConnectSequence.ConnectStart:
                {
                    NotifyMessage(PacketPriority.Notice, "<< Connect Start >>", "");
                    OnConnectStart();
                    connect_seq_++;
                    state = PollState.Active;
                }
                    break;

                case ConnectSequence.ConnectBusy:
                {
                    if (OnConnectBusy() == EventResult.Success) {
                        NotifyMessage(PacketPriority.Notice, "<< Connected >>", "");
                        OnConnected();
                        connect_seq_++;
                        state = PollState.Active;
                    }
                }
                    break;

                case ConnectSequence.Connected:
                {
                    /* データが存在しないときは送信データを要求する */
                    if (send_data_busy_ == null) {
                        SendDataRequest();
                    }

                    state = OnPoll();
                }
                    break;

                default:
                    connect_seq_++;
                    break;
            }

            return (state);
        }

        private PollState DisconnectProc()
        {
            var state = PollState.Idle;

            switch (connect_seq_) {
                case ConnectSequence.DisconnectStart:
                {
                    NotifyMessage(PacketPriority.Notice, "<< Disconnect Start >>", "");
                    OnDisconnectStart();
                    connect_seq_--;
                    state = PollState.Active;
                }
                    break;

                case ConnectSequence.DisconnectBusy:
                {
                    if (OnDisconnectBusy() == EventResult.Success) {
                        NotifyMessage(PacketPriority.Notice, "<< Disconnected >>", "");
                        OnDisconnected();
                        connect_seq_--;
                        state = PollState.Active;
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

        protected virtual void        OnConnectStart() { }
        protected virtual EventResult OnConnectBusy()  { return (EventResult.Success); }
        protected virtual void        OnConnected()    { }

        protected virtual void        OnDisconnectStart() { }
        protected virtual EventResult OnDisconnectBusy()  { return (EventResult.Success); }
        protected virtual void        OnDisconnected()    { }

        protected virtual PollState   OnPoll() { return (PollState.Idle); }

        protected virtual void        OnSendRequest() { }
    }
}
