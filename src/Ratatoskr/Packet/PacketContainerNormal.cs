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
using Ratatoskr.Generic.Container;

namespace Ratatoskr.Packet
{
    internal sealed class PacketContainerNormal
        : IPacketContainer
    {
        private const ulong PACKET_COUNT_MIN = 9999;
        private const ulong PACKET_COUNT_MAX = 9999999;


        private List<PacketObject>  packets_ = new List<PacketObject>();

        private ulong packet_count_max_ = 0;


        public PacketContainerNormal(ulong count_max) : base()
        {
            packet_count_max_ = count_max;
            packet_count_max_ = Math.Min(packet_count_max_, PACKET_COUNT_MAX);
            packet_count_max_ = Math.Max(packet_count_max_, PACKET_COUNT_MIN);
        }

        public PacketContainerNormal(IEnumerable<PacketObject> packets, ulong count_max) : base()
        {
        }

        public void Dispose()
        {
            packets_ = null;

            GC.Collect();
        }

        public ulong Count
        {
            get { return ((ulong)packets_.Count); }
        }

        public void Clear()
        {
            packets_ = new List<PacketObject>();
        }

        public void Add(PacketObject packet)
        {
            var over_count = (long)packets_.Count + 1 - (long)packet_count_max_;

            if (over_count > 0) {
                packets_.RemoveRange(0, (int)Math.Max(over_count, (long)packet_count_max_ / 10));
            }

            packets_.Add(packet);
        }

        public void AddRange(IEnumerable<PacketObject> packets)
        {
            var over_count = (long)packets_.Count + (long)packets.Count() - (long)packet_count_max_;

            if (over_count > 0) {
                packets_.RemoveRange(0, (int)Math.Max(over_count, (long)packet_count_max_ / 10));
            }

            packets_.AddRange(packets);
        }

        public IEnumerator<PacketObject> GetEnumerator()
        {
            foreach (var packet in packets_) {
                yield return (packet);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }
    }
}
