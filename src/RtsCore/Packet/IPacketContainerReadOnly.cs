using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Packet
{
    public interface IPacketContainerReadOnly : IEnumerable<PacketObject>
    {
        ulong Count { get; }
    }
}
