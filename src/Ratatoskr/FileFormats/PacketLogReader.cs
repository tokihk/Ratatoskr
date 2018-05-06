using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Packet;

namespace Ratatoskr.FileFormats
{
    internal class PacketLogReader : FileFormatReader
    {
        public PacketObject ReadPacket()
        {
            if (!IsOpen)return (null);

            return (OnReadPacket());
        }

        protected virtual PacketObject OnReadPacket()
        {
            return (null);
        }
    }
}
