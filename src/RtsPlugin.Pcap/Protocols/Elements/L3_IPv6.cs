using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.Elements
{
    public class L3_IPv6 : LayerPacket<IPv6Packet>
    {
        public L3_IPv6(ProtocolFrameElement parent, IPv6Packet packet) : base(parent, "IPv6", packet)
        {
        }

        protected override bool OnPacketToUnpackElements(IPv6Packet packet)
        {
            return (true);
        }
    }
}
