using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Packet;

namespace RtsCore.Framework.PacketFilter
{
    public sealed class PacketFilterCallStack
    {
        public ulong        PacketCount { get; private set; } = 0;

        public PacketObject PrevPacket { get; set; } = null;
        public PacketObject LastPacket { get; set; } = null;

        public void PacketCountInc()
        {
            if (PacketCount < ulong.MaxValue) {
                PacketCount++;
            }
        }
    }
}
