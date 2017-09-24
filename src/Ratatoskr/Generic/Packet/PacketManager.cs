using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet
{
    [Flags]
    internal enum PacketDataRateTarget
    {
        RecvData = 1 << 0,
        SendData = 1 << 1,
    }


    internal class PacketManager : IDisposable
    {
        public delegate void PacketEntryDelegate(PacketObject packet, ref bool ignore);
 
        public event PacketEntryDelegate PacketEntry = delegate(PacketObject packet, ref bool ignore) { };
               

        private Queue<PacketObject> packet_list_ = new Queue<PacketObject>();
        private object              packet_list_sync_ = new object();

        private System.Threading.Timer rate_timer_ = null;
        private ulong                  rate_value_ = 0;
        private ulong                  rate_value_busy_ = 0;


        public PacketManager(bool rate_measure = false)
        {
            if (rate_measure) {
                DataRateInstrumentationStart();
            }
        }

        public void Dispose()
        {
            DataRateInstrumentationStop();
        }

        public PacketDataRateTarget DataRateTarget { get; set; } = 0;

        public void Clear()
        {
            lock (packet_list_sync_) {
                packet_list_ = new Queue<PacketObject>();
            }
        }

        public void Enqueue(PacketObject packet)
        {
            lock (packet_list_sync_) {
                var ignore = false;

                /* パケット追加確認 */
                PacketEntry(packet, ref ignore);

                /* 追加無し */
                if (ignore)return;

                /* パケットリストに追加 */
                packet_list_.Enqueue(packet);

                /* 通信レート計算 */
                if (packet.Attribute == PacketAttribute.Data) {
                    if (   ((packet.Direction == PacketDirection.Recv) && (DataRateTarget.HasFlag(PacketDataRateTarget.RecvData)))
                        || ((packet.Direction == PacketDirection.Send) && (DataRateTarget.HasFlag(PacketDataRateTarget.SendData)))
                    ) {
                        rate_value_busy_ += (ulong)packet.GetDataSize();
                    }
                }
            }
        }

        public IEnumerable<PacketObject> DequeueAll(ref ulong rate)
        {
            lock (packet_list_sync_) {
                rate = rate_value_;

                if (packet_list_.Count == 0)return (null);

                var packets = (IEnumerable<PacketObject>)null;
                var packet_list_new = new Queue<PacketObject>();

                packets = packet_list_;
                packet_list_ = packet_list_new;

                return (packets.ToArray());
            }
        }

        private void DataRateInstrumentationStart()
        {
            DataRateInstrumentationStop();

            rate_timer_ = new System.Threading.Timer(DataRateInstrumentationTask, null, 0, 1000);
        }

        private void DataRateInstrumentationStop()
        {
            if (rate_timer_ != null) {
                rate_timer_.Dispose();
                rate_timer_ = null;
            }
        }

        private void DataRateInstrumentationTask(object obj)
        {
            lock (packet_list_sync_) {
                rate_value_ = rate_value_busy_;
                rate_value_busy_ = 0;
            }
        }
    }
}
