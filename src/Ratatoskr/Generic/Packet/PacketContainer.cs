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

namespace Ratatoskr.Generic.Packet
{
    internal sealed class PacketContainer
        : SkipList<PacketObject>
    {
        private const ulong PACKET_COUNT_MAX = 99999999999;

        public PacketContainer(ulong count_max) : base(count_max)
        {
        }

        public PacketContainer(IEnumerable<PacketObject> packets, ulong count_max) : base(packets, count_max)
        {
        }
    }
}
