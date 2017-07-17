using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        Active,
        Busy,
    }

    internal abstract class DeviceInstance : IDisposable
    {
        private const int  DEVICE_THREAD_IVAL_ACTIVE = 1;
        private const int  DEVICE_THREAD_IVAL_IDLE   = 15;
        
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


        public delegate void StatusChangedDelegate();
        public delegate void SendDataRequstDelegate();

        public event StatusChangedDelegate StatusChanged = delegate() { };
        public event SendDataRequstDelegate SendDataRequest = delegate() { };


        private DeviceManager  devm_ = null;
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

        private Queue<byte[]> send_data_user_ = new Queue<byte[]>();
        private Queue<byte[]> send_data_redirect_ = new Queue<byte[]>();

        private byte[] send_data_busy_ = null;
        private int    send_data_offset_ = 0;
        private object send_data_sync_ = new object();

        private volatile ConnectSequence connect_seq_ = ConnectSequence.Disconnected;


        public DeviceInstance(DeviceManager devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
        {
            devm_ = devm;
            devconf_ = devconf;
            devd_ = devd;
            devp_ = devp;
        }

        public virtual void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public DeviceManager Manager
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
                alias_ = (value != null) ? (value) : ("");
                devm_.UpdateRedirectMap();
            }
        }

        public string RedirectAlias
        {
            get { return (alias_redirect_); }
            set
            {
                alias_redirect_ = (value != null) ? (value) : ("");
                devm_.UpdateRedirectMap();
            }
        }

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

        public bool PushSendData(byte[] data)
        {
            if (data == null)return (false);

            /* 送信禁止状態のときは失敗 */
            if (!Config.SendEnable)return (false);

            /* 接続状態ではないときは失敗 */
            if (ConnectStatus != ConnectState.Connected)return (false);

            lock (send_data_sync_) {
                /* 送信データキューに空が無い場合は失敗 */
                if (send_data_user_.Count >= Config.SendDataQueueLimit)return (false);

                /* 送信データを追加 */
                send_data_user_.Enqueue(data);
            }

            /* 送信割込み */
            OnSendRequest();

            return (true);
        }

        private void PushRedirectData(byte[] data)
        {
            /* リダイレクト禁止状態のときは失敗 */
            if (!Config.RedirectEnable)return;

            /* 接続状態ではないときは失敗 */
            if (ConnectStatus != ConnectState.Connected)return;

            lock (send_data_sync_) {
                /* 送信データキューに空が無い場合は失敗 */
                if (send_data_redirect_.Count >= Config.RedirectDataQueueLimit)return;

                /* 送信データを追加 */
                send_data_redirect_.Enqueue(data);
            }

            /* 送信割込み */
            OnSendRequest();
        }

        private byte[] PopSendData()
        {
            /* リダイレクトデータ */
            if (send_data_redirect_.Count > 0) {
                return (send_data_redirect_.Dequeue());
            }

            /* 通常送信データ */
            if (send_data_user_.Count > 0) {
                return (send_data_user_.Dequeue());
            }

            return (null);
        }

        private void ReloadSendDataTry()
        {
            /* データキューから読込 */
            lock (send_data_sync_) {
                if (send_data_busy_ == null) {
                    send_data_busy_ = PopSendData();
                    if (send_data_busy_ != null) {
                        send_data_offset_ = 0;
                    }
                }
            }
        }

        private void ReloadSendData()
        {
            /* データキューから読込 */
            ReloadSendDataTry();

            /* データが存在しないときは送信データを要求する */
            if (send_data_busy_ == null) {
                SendDataRequest();
            }

            /* もう一度データキューから読込 */
            ReloadSendDataTry();
        }

        protected int GetSendData(byte[] buffer, int size)
        {
            if (buffer == null)return (0);

            size = Math.Min(buffer.Length, size);

            if (size == 0)return (0);

            /* 送信データ準備 */
            ReloadSendData();

            lock (send_data_sync_) {
                /* 送信データが存在しないときは終了 */
                if (send_data_busy_ == null)return (0);

                /* コピーサイズ取得 */
                var send_size = Math.Min(size, send_data_busy_.Length - send_data_offset_);

                /* 送信データをバッファにコピー */
                if (send_size > 0) {
                    Array.Copy(send_data_busy_, send_data_offset_, buffer, 0, send_size);
                    send_data_offset_ += send_size;
                }

                /* 最後まで送信したときはバッファをクリア */
                if (send_data_offset_ >= send_data_busy_.Length) {
                    send_data_offset_ = 0;
                    send_data_busy_ = null;
                }

                return (send_size);
            }
        }

        protected byte[] GetSendData()
        {
            /* 送信データ準備 */
            ReloadSendData();

            lock (send_data_sync_) {
                /* 送信データが存在しないときは終了 */
                if (send_data_busy_ == null)return (null);

                var send_data = (byte[])null;

                if (send_data_offset_ == 0) {
                    send_data = send_data_busy_;

                } else {
                    var send_size = Math.Max(0, send_data_busy_.Length - send_data_offset_);

                    if (send_size > 0) {
                        send_data = new byte[send_size];
                        Array.Copy(send_data_busy_, send_data_offset_, send_data, 0, send_size);
                    }
                }

                send_data_busy_ = null;
                send_data_offset_ = 0;

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

        private void SetupRedirectData(byte[] data)
        {
            var redirect_list = redirect_list_;

            if (redirect_list_ == null)return;

            foreach (var target in redirect_list_) {
                target.PushRedirectData(data);
            }
        }

        public void NotifyPacket(PacketObject packet)
        {
            /* パケットを登録 */
            devm_.SetupPacket(packet);
        }

        public void NotifyMessage(PacketPriority prio, string info, string message)
        {
            NotifyPacket(
                new MessagePacketObject(
                    PacketFacility.Device,
                    alias_,
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
                    alias_,
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
            /* 受信禁止状態のときは無視 */
            if (!Config.RecvEnable)return;

            /* リダイレクト要求 */
            SetupRedirectData(data);

            NotifyPacket(
                new StaticDataPacketObject(
                    PacketFacility.Device,
                    alias_,
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

            /* 接続/切断処理 */
            state = ConnectPoll();

            /* シャットダウン判定 */
            if (   (shutdown_req_)
                && (connect_seq_ == ConnectSequence.Disconnected)
            ) {
                shutdown_state_ = true;
            }

            /* スリープ処理 */
            switch (state) {
                case PollState.Active:  Thread.Sleep(DEVICE_THREAD_IVAL_ACTIVE);    break;
                case PollState.Idle:    Thread.Sleep(DEVICE_THREAD_IVAL_IDLE);      break;
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
            if (connect_seq_prev != connect_seq_) {
                StatusChanged();
                state = PollState.Active;
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
                    state = OnPoll();
                    
                    ReloadSendData();
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

        protected virtual EventResult OnConnectStart() { return (EventResult.Success); }
        protected virtual EventResult OnConnectBusy()  { return (EventResult.Success); }
        protected virtual void        OnConnected()    { }

        protected virtual void        OnDisconnectStart() { }
        protected virtual EventResult OnDisconnectBusy()  { return (EventResult.Success); }
        protected virtual void        OnDisconnected()    { }

        protected virtual PollState   OnPoll() { return (PollState.Idle); }

        protected virtual void        OnSendRequest() { }
    }
}
