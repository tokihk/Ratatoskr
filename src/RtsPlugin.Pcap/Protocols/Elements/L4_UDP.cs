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
    public class L4_UDP : LayerPacket<UdpPacket>
    {
        public L4_UDP(ProtocolFrameElement parent, UdpPacket packet) : base(parent, "UDP", packet)
        {
        }

        protected override bool OnPacketToUnpackElements(UdpPacket packet)
        {
            /* Source Port */
            new ProtocolFrameElement_Integer(this, "Source Port", 16, packet.SourcePort);

            /* Destination Port */
            new ProtocolFrameElement_Integer(this, "Destination Port", 16, packet.DestinationPort);

            /* Length */
            new ProtocolFrameElement_Integer(this, "Length", 16, (ulong)packet.Length);

            /* Check Sum */
            new P_CheckSum16(this, "Check Sum", packet.Checksum);

            return (true);
        }
    }
}
