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
    public class L3_ICMPv4 : LayerPacket<IcmpV4Packet>
    {
        public L3_ICMPv4(ProtocolFrameElement parent, IcmpV4Packet packet) : base(parent, "ICMPv4", packet)
        {
        }
    }
}
