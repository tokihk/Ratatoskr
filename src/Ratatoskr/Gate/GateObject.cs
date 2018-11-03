using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Devices;
using RtsCore.Generic;
using RtsCore.Utility;
using RtsCore.Framework.Device;

namespace Ratatoskr.Gate
{
    internal sealed class GateObject
    {
        public enum SendRequestStatus
        {
            Accept,         // 正常に受理された/キューに追加された
            Ignore,         // 無視された/捨てられた
            Pending,        // 今は判断できない/後で再トライ
        }

        private const int SEND_DATA_QUEUE_LIMIT = 4;


        public delegate void StatusChangedDelegate(object sender, EventArgs e);
        public event StatusChangedDelegate StatusChanged = delegate(object sender, EventArgs e) { };

        public delegate void SendBufferEmptyDelegate(object sender, EventArgs e);
        public event SendBufferEmptyDelegate SendBufferEmpty = delegate(object sender, EventArgs e) { };

        public delegate void DataRateUpdatedHandler(object sender, ulong data_rate);
        public event DataRateUpdatedHandler DataRateUpdated = delegate(object sender, ulong data_rate) { };

        private GateProperty   gatep_;
        private DeviceConfig   devconf_;
        private DeviceInstance devi_;

        private Queue<byte[]> send_queue_ = new Queue<byte[]>();
        private object        send_queue_sync_ = new object();

        private byte[] connect_command_ = null;


        public GateObject(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            ChangeDevice(gatep, devconf, devc_id, devp);
        }

        public GateProperty GateProperty
        {
            get { return (ClassUtil.Clone(gatep_)); }
            set
            {
                if (value == null)return;

                gatep_ = value;
                ApplyGateProperty();
            }
        }

        public DeviceConfig DeviceConfig
        {
            get { return (ClassUtil.Clone(devconf_)); }
        }

        public bool IsDeviceSetup
        {
            get { return (devi_ != null); }
        }

        public Guid DeviceClassID
        {
            get { return ((devi_ != null) ? (devi_.Class.ID) : (Guid.Empty)); }
        }

        public string DeviceClassName
        {
            get { return ((devi_ != null) ? (devi_.Class.Name) : ("Empty")); }
        }

        public DeviceProperty DeviceProperty
        {
            get { return ((devi_ != null) ? (devi_.Property) : (null)); }
        }

        public string DeviceStatusText
        {
            get { return ((devi_ != null) ? (devi_.Property.GetStatusString()) : ("")); }
        }

        public string Alias
        {
            get { return (gatep_.Alias); }
        }

        public bool ConnectRequest
        {
            get { return (gatep_.ConnectRequest); }
            set
            {
                gatep_.ConnectRequest = value;
                ApplyGateProperty();

                if (gatep_.ConnectRequest) {
                    SendRequest(connect_command_);
                }
            }
        }

        public ConnectState ConnectStatus
        {
            get {
                return ((devi_ != null) ? (devi_.ConnectStatus) : (ConnectState.Disconnected));
            }
        }

        public Control CreateDeviceControlPanel()
        {
            if (devi_ == null)return (null);

            return (devi_.CreateControlPanel());
        }

        private void ApplyGateProperty()
        {
            if (devi_ != null) {
                devi_.Alias = gatep_.Alias;
                devi_.ConnectRequest = gatep_.ConnectRequest;
                devi_.RedirectAlias = gatep_.RedirectAlias;
                devi_.DataRateTarget = gatep_.DataRateTarget;
            }

            StatusChanged(this, EventArgs.Empty);
        }

        public void ChangeDevice(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            if (gatep != null) {
                gatep_ = gatep;
            }

            if (devconf != null) {
                devconf_ = devconf;
            }

            /* いずれかの状態が変化したらデバイスを再構築 */
            if (   (devi_ == null)
                || (!devconf_.Equals(devconf))
                || (devc_id != devi_.Class.ID)
                || (!ClassUtil.Compare(devi_.Property, devp))
            ) {
                SetupDevice(GateManager.CreateDeviceObject(devconf, devc_id, devp));
            }

            ApplyGateProperty();

            /* 接続コマンドを再構築 */
            connect_command_ = HexTextEncoder.ToByteArray(gatep_.ConnectCommand);
        }

        private void SetupDevice(DeviceInstance devi)
        {
            /* 直前のデバイスを終了 */
            if (devi_ != null) {
                /* 登録イベント解除 */
                devi_.StatusChanged -= OnDeviceStatusChanged;
                devi_.SendDataRequest -= OnDeviceSendBufferEmpty;
                devi_.DataRateUpdated -= OnDeviceDataRateUpdated;

                /* デバイス終了 */
                devi_.DeviceShutdownRequest();
            }

            /* 新しいデバイスをセットアップ */
            if (devi != null) {
                /* イベント登録 */
                devi.StatusChanged += OnDeviceStatusChanged;
                devi.SendDataRequest += OnDeviceSendBufferEmpty;
                devi.DataRateUpdated += OnDeviceDataRateUpdated;

                /* ゲートの設定/状態を反映 */
                devi.ConnectRequest = gatep_.ConnectRequest;
                devi.DataRateTarget = gatep_.DataRateTarget;
            }

            /* インスタンス入れ替え */
            lock (send_queue_sync_) {
                devi_ = devi;
            }
        }

        public void ReleaseDevice()
        {
            SetupDevice(null);
            ApplyGateProperty();
        }

        private void SendExec()
        {
            if (send_queue_.Count == 0)return;

            lock (send_queue_sync_) {
                if (   (send_queue_.Count > 0)
                    && (devi_ != null)
                ) {
                    var data = send_queue_.Peek();

                    if (devi_.PushSendUserData(data).discard_req) {
                        /* データをセットできたときのみキューからデータを削除 */
                        send_queue_.Dequeue();
                    }
                }
            }
        }

        public (bool discard_req, SendRequestStatus status) SendRequest(byte[] data)
        {
            lock (send_queue_sync_) {
                /* 切断/送信禁止時は無視 */
                if (   (!gatep_.ConnectRequest)
                    || (!devconf_.SendEnable)
                    || (devi_ == null)
                ) {
                    return (true, SendRequestStatus.Ignore);
                }

                /* 送信データキューがいっぱいのときは再送要求 */
                if (send_queue_.Count >= SEND_DATA_QUEUE_LIMIT) {
                    return (false, SendRequestStatus.Pending);
                }

                /* 送信データキューにデータをセット */
                if (data != null) {
                    send_queue_.Enqueue(data);
                }
            }

            /* 送信実行 */
            SendExec();

            return (true, SendRequestStatus.Accept);
        }

        private void OnDeviceStatusChanged()
        {
            StatusChanged(this, EventArgs.Empty);
        }

        private void OnDeviceSendBufferEmpty()
        {
            SendExec();
        }

        private void OnDeviceDataRateUpdated(object sender, ulong value)
        {
            DataRateUpdated(this, value);
        }
    }
}
