using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.Elements
{
    public class L2_EthernetII : LayerPacket<EthernetPacket>
    {
        public L2_EthernetII(ProtocolFrameElement parent, EthernetPacket packet) : base(parent, "Ethernet II", packet)
        {
        }

        protected override bool OnPacketToUnpackElements(EthernetPacket packet)
        {
            /* Destination */
            new P_MacAddress(this, "Destination", packet.DestinationHardwareAddress);

            /* Source */
            new P_MacAddress(this, "Source", packet.SourceHardwareAddress);

            /* EtherType */
            new P_EtherType(this, "EtherType", packet.Type);

            return (true);
        }
    }
}
