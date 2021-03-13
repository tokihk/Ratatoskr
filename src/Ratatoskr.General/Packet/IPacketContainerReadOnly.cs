using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General.Packet
{
    public interface IPacketContainerReadOnly : IEnumerable<PacketObject>
    {
        ulong Count { get; }
    }
}
