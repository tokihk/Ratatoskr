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
    public class L3_IEEE8021Q : LayerPacket<Ieee8021QPacket>
    {
        public L3_IEEE8021Q(ProtocolFrameElement parent, Ieee8021QPacket packet) : base(parent, "IEEE802.1Q", packet)
        {
        }

        protected override bool OnPacketToUnpackElements(Ieee8021QPacket packet)
        {
            /* PCP */
            new ProtocolFrameElement_Integer(this, "PCP", 3, (ulong)packet.PriorityControlPoint);

            /* DEI */
            new ProtocolFrameElement_Integer(this, "DEI", 1, (packet.CanonicalFormatIndicator) ? (1u) : (0u));

            /* VID */
            new ProtocolFrameElement_Integer(this, "VID", 12, packet.VlanIdentifier);

            return (true);
        }
    }
}
