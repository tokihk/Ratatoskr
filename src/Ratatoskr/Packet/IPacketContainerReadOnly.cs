using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Packet
{
    internal interface IPacketContainerReadOnly : IEnumerable<PacketObject>
    {
        ulong Count { get; }
    }
}
