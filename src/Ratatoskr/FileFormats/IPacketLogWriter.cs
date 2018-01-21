using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.FileFormats
{
    internal interface IPacketLogWriter
    {
        void WritePacket(PacketObject packet);
        void WritePacket(IEnumerable<PacketObject> packets);
    }
}
