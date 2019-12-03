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
    public class L3_IPv4 : LayerPacket<IPv4Packet>
    {
        public L3_IPv4(ProtocolFrameElement parent, IPv4Packet packet) : base(parent, "IPv4", packet)
        {
        }

        protected override bool OnPacketToUnpackElements(IPv4Packet packet)
        {
            /* Version */
            new ProtocolFrameElement_Integer(this, "Version", 4, (ulong)packet.Version);

            /* Header Length */
            new P_IpHeaderLength(this, "Header Length", packet.HeaderLength);

            /* Type Of Service */
            new P_IpTypeOfService(this, "Type Of Service(ToS)", packet.TypeOfService);

            /* Total Length */
            new ProtocolFrameElement_Integer(this, "Total Length", 16, (ulong)packet.TotalLength);

            /* Identification */
            new ProtocolFrameElement_Integer(this, "Identification", 16, (ulong)packet.Id);

            /* Fragment Flags */
            new P_IpFragmentFlags(this, "Fragment Flags", packet.FragmentFlags);

            /* Fragment Offset */
            new P_IpFragmentOffset(this, "Fragment Offset", packet.FragmentOffset);

            /* Time to Live (TTL) */
            new ProtocolFrameElement_Integer(this, "Time to Live (TTL)", 8, (ulong)packet.TimeToLive);

            /* Protocol */
            new ProtocolFrameElement_Enum<ProtocolType>(this, "Protocol", 8, packet.Protocol);

            /* Header Checksum */
            new P_CheckSum16(this, "Header Checksum", packet.Checksum);

            /* Source IP Address */
            new P_IpAddressV4(this, "Source IP Address", packet.SourceAddress);

            /* Destination IP Address */
            new P_IpAddressV4(this, "Destination IP Address", packet.DestinationAddress);

            return (true);
        }
    }
}
