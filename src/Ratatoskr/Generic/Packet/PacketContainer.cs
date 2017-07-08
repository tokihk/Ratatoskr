using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet
{
    internal sealed class PacketContainer
        : IEnumerable<PacketObject>
    {
        private const int PACKET_BLOCK_SIZE = 1000;


        private List< List< List<PacketObject> > >  packets_l1_ = new List<List<List<PacketObject>>>(PACKET_BLOCK_SIZE);
        private ulong total_count_ = 0;


        public PacketContainer()
        {
        }

        public ulong Count
        {
            get { return (total_count_); }
        }

        public IEnumerable<PacketObject> Add(PacketObject packet)
        {
            return (Add(new [] { packet }));
        }

        public IEnumerable<PacketObject> Add(IEnumerable<PacketObject> packets)
        {
            var packets_lost = new Queue<PacketObject>();

            foreach (var packet in packets) {
                while (!PushBack(packet)) {
                    packets_lost.Enqueue(PopFront());
                }
            }

            return (packets_lost);
        }

        private PacketObject PopFront()
        {
            if (packets_l1_.Count == 0)return (null);

            var packets_l2 = packets_l1_.First();

            if (packets_l2.Count == 0)return (null);

            var packets_l3 = packets_l2.First();

            if (packets_l3.Count == 0)return (null);

            var packet = packets_l3.First();

            /* L3リストから削除 */
            packets_l3.RemoveAt(0);

            /* L3リストが空になった場合はL2リストから削除 */
            if (packets_l3.Count == 0) {
                packets_l2.RemoveAt(0);
            }

            /* L2リストが空になった場合はL1リストから削除 */
            if (packets_l2.Count == 0) {
                packets_l1_.RemoveAt(0);
            }

            total_count_--;

            return (packet);
        }

        private bool PushBack(PacketObject packet)
        {
            /* L2リストが存在しない場合はL2リストを作成 */
            if (   (packets_l1_.Count == 0)
                || (packets_l1_.Last().Count >= packets_l1_.Last().Capacity)
            ) {
                /* リストの限界数に達しているときは無視 */
                if (packets_l1_.Count() >= packets_l1_.Capacity) {
                    return (false);
                }
                packets_l1_.Add(new List<List<PacketObject>>(PACKET_BLOCK_SIZE));
            }

            var packets_l2 = packets_l1_.Last();

            /* L1リストが存在しない場合はL1リストを作成 */
            if (   (packets_l2.Count == 0)
                || (packets_l2.Last().Count >= packets_l2.Last().Capacity)
            ) {
                /* リストの限界数に達しているときは無視 */
                if (packets_l2.Count() >= packets_l2.Capacity) {
                    return (false);
                }
                packets_l2.Add(new List<PacketObject>(PACKET_BLOCK_SIZE));
            }

            var packets_l3 = packets_l2.Last();

            /* リストの限界数に達しているときは無視 */
            if (packets_l3.Count() >= packets_l3.Capacity) {
                return (false);
            }

            packets_l3.Add(packet);
            total_count_++;

            return (true);
        }

        public IEnumerator<PacketObject> GetEnumerator()
        {
            /* L1 */
            foreach (var packets_l2 in packets_l1_) {
                foreach (var packets_l3 in packets_l2) {
                    foreach (var packet in packets_l3) {
                        yield return (packet);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (GetEnumerator());
        }
    }
}
