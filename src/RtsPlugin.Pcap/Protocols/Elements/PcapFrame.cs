using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Protocol;
using RtsCore.Utility;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.Elements
{
    public class PcapParser
    {
        public static ProtocolFrameElement ParsePacket(ProtocolFrameElement parent, PacketDotNet.Packet packet)
        {
            var elem = (ProtocolFrameElement)null;

            if (packet.IsPayloadInitialized) {
                switch (packet) {
                    case EthernetPacket  packet_c:       elem = new L2_EthernetII(parent, packet_c);     break;
                    case ArpPacket       packet_c:       elem = new L2_ARP(parent, packet_c);            break;
                    case Ieee8021QPacket packet_c:       elem = new L3_IEEE8021Q(parent, packet_c);      break;
                    case IcmpV4Packet    packet_c:       elem = new L3_ICMPv4(parent, packet_c);         break;
                    case IcmpV6Packet    packet_c:       elem = new L3_ICMPv6(parent, packet_c);         break;
                    case IgmpV2Packet    packet_c:       elem = new L3_IGMP(parent, packet_c);           break;
                    case IPv4Packet      packet_c:       elem = new L3_IPv4(parent, packet_c);           break;
                    case TcpPacket       packet_c:       elem = new L4_TCP(parent, packet_c);            break;
                    case UdpPacket       packet_c:       elem = new L4_UDP(parent, packet_c);            break;
                }
            }

            if (elem == null) {
                var packet_bitlen = (uint)packet.HeaderDataSegment.BytesLength * 8;

                elem = new ProtocolFrameElement_BitData(parent, "Payload Data", packet_bitlen, new BitData(packet.HeaderDataSegment.Bytes, packet_bitlen));
            }

            elem.UpdateFromPackData();

            return (elem);
        }

        public static ProtocolFrameElement ParsePacketPayload(ProtocolFrameElement parent, PacketDotNet.Packet packet)
        {
            if (packet.HasPayloadPacket) {
                return (ParsePacket(parent, packet.PayloadPacket));
            }

            if (packet.HasPayloadData) {
                var payload_data = packet.PayloadData;

                return (new ProtocolFrameElement_BitData(
                                parent,
                                "Payload Data",
                                (uint)packet.TotalPacketLength * 8,
                                new BitData(payload_data, (uint)payload_data.Length * 8)));
            }

            return (null);
        }
    }
}
