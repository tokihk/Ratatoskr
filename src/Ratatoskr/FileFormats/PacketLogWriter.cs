using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.FileFormats
{
    internal class PacketLogWriter : FileFormatWriter
    {
        public PacketLogWriter(FileFormatClass fmtc) : base(fmtc)
        {
        }

        public void WritePacket(PacketObject packet)
        {
            if (!IsOpen)return;

            OnWritePacket(packet);
        }

        public void WritePacket(IEnumerable<PacketObject> packets)
        {
            if (!IsOpen)return;

            ProgressMax = (ulong)packets.Count();
            ProgressNow = 0;

            foreach (var packet in packets) {
                OnWritePacket(packet);
                ProgressNow++;
            }
        }

        protected virtual void OnWritePacket(PacketObject packet)
        {
        }
    }
}
