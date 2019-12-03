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
    public class L2_ARP : LayerPacket<ArpPacket>
    {
        public L2_ARP(ProtocolFrameElement parent, ArpPacket packet) : base(parent, "ARP", packet)
        {
        }
    }
}
