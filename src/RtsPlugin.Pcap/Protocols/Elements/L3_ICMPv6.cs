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
    public class L3_ICMPv6 : LayerPacket<IcmpV6Packet>
    {
        public L3_ICMPv6(ProtocolFrameElement parent, IcmpV6Packet packet) : base(parent, "ICMPv6", packet)
        {
        }
    }
}
