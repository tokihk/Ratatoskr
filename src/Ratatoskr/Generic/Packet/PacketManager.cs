using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet
{
    internal class PacketManager
    {
        public delegate void PacketEntryDelegate(PacketObject packet, ref bool ignore);
 
        public event PacketEntryDelegate PacketEntry = delegate(PacketObject packet, ref bool ignore) { };
               

        private Queue<PacketObject> packet_list_ = new Queue<PacketObject>();
        private object              packet_list_sync_ = new object();


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

                if (!ignore) {
                    packet_list_.Enqueue(packet);
                }
            }
        }

        public IEnumerable<PacketObject> DequeueAll()
        {
            if (packet_list_.Count == 0)return (null);

            var packets = (IEnumerable<PacketObject>)null;
            var packet_list_new = new Queue<PacketObject>();

            lock (packet_list_sync_) {
                packets = packet_list_;
                packet_list_ = packet_list_new;
            }

            return (packets.ToArray());
        }

    }
}
