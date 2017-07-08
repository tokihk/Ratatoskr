using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Devices;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Gate
{
    internal sealed class GateObject
    {
        public delegate void StatusChangedDelegate();
        public delegate void SendBufferEmptyDelegate();

        public event StatusChangedDelegate   StatusChanged = delegate() { };
        public event SendBufferEmptyDelegate SendBufferEmpty = delegate() { };


        private string alias_;

        private bool connect_req_ = false;

        private Queue<byte[]> send_data_queue_ = new Queue<byte[]>();

        private DeviceInstance devi_;


        public GateObject(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; } = Guid.Empty;

        public string Alias
        {
            get { return (alias_); }
            set {
                alias_ = value;
                StatusChanged();
            }
        }

        public bool ConnectRequest
        {
            get { return (connect_req_); }
            set {
                connect_req_ = value;

                /* 切断するときは送信バッファを全てクリア */
                if (!connect_req_) {
                    SendDataClear();
                }

                if (devi_ != null) {
                    devi_.ConnectRequest = connect_req_;
                }

                StatusChanged();
            }
        }

        public ConnectState ConnectStatus
        {
            get {
                if (devi_ != null) {
                    return (devi_.ConnectStatus);
                } else {
                    return (ConnectState.Disconnected);
                }
            }
        }

        public DeviceInstance Device
        {
            get { return (devi_); }
            set {
                if (devi_ == value)return;

                if (devi_ != null) {
                    /* 登録イベント解除 */
                    devi_.PacketCreated -= OnDevicePacketCreated;
                    devi_.StatusChanged -= OnDeviceStatusChanged;
                    devi_.SendDataRequest -= OnDeviceSendBufferEmpty;

                    /* デバイス終了 */
                    devi_.ShutdownRequest();
                }

                devi_ = value;

                if (devi_ != null) {
                    /* イベント登録 */
                    devi_.PacketCreated += OnDevicePacketCreated;
                    devi_.StatusChanged += OnDeviceStatusChanged;
                    devi_.SendDataRequest += OnDeviceSendBufferEmpty;

                    /* 接続要求反映 */
                    devi_.ConnectRequest = connect_req_;
                }

                StatusChanged();
            }
        }

        public bool SendDataEmpty
        {
            get { return (send_data_queue_.Count == 0); }
        }

        private void SendDataClear()
        {
            lock (send_data_queue_) {
                send_data_queue_.Clear();
            }
        }

        public void SendDataPush(byte[] data)
        {
            if (!connect_req_)return;

            lock (send_data_queue_) {
                send_data_queue_.Enqueue(data);
            }
        }

        private byte[] SendDataPop()
        {
            if (send_data_queue_.Count == 0) {
                SendBufferEmpty();
            }

            if (send_data_queue_.Count == 0)return (null);

            lock (send_data_queue_) {
                return (send_data_queue_.Dequeue());
            }
        }

        public void RedirectPacket(PacketObject packet)
        {
            if (packet.Attribute == PacketAttribute.Data) {
                RedirectDataPacket(packet as DataPacketObject);
            }
        }

        private void RedirectDataPacket(DataPacketObject packet)
        {
            /* 受信パケットのみ転送 */
            if (packet.Direction == PacketDirection.Recv) {
                SendDataPush(packet.GetData());
            }
        }

        private void OnDevicePacketCreated(PacketObject packet)
        {
            packet.Alias = alias_;
        }

        private void OnDeviceStatusChanged()
        {
            StatusChanged();
        }

        private void OnDeviceSendBufferEmpty()
        {
            if (devi_ == null)return;

            devi_.PushSendDataBlock(SendDataPop());
        }
    }
}
