using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Packet
{
    internal interface IPacketContainer : IEnumerable<PacketObject>, IDisposable, IPacketContainerReadOnly
    {
        void Clear();

        void Add(PacketObject packet);
        void AddRange(IEnumerable<PacketObject> packets);
    }
}
