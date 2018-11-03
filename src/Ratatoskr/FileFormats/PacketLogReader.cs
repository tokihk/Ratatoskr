﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.FileFormats
{
    internal class PacketLogReader : FileFormatReader
    {
        public PacketLogReader(FileFormatClass fmtc) : base(fmtc)
        {
        }

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
