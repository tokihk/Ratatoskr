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
    public class L3_IGMP : LayerPacket<IgmpV2Packet>
    {
        public L3_IGMP(ProtocolFrameElement parent, IgmpV2Packet packet) : base(parent, "IGMP", packet)
        {
        }
    }
}
