using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;
using Ratatoskr.General.Packet;
using PacketDotNet;

namespace Ratatoskr.ProtocolParser.Ethernet
{
	class ProtocolParserInstanceImpl : ProtocolParserInstance
	{
		public ProtocolParserInstanceImpl(ProtocolParserManager manager) : base(manager)
		{
		}

		protected override ProtocolParsePacketInfo OnParsePacket(PacketObject packet)
		{
			try {
				var info = new ProtocolParsePacketInfo();

				var packet_dotnet = PacketDotNet.Packet.ParsePacket(LinkLayers.Ethernet, packet.Data);

				info.PacketInfo.Text = packet_dotnet.ToString();

				ParsePacketDetails(info.PacketInfo, packet_dotnet);

				return (info);

			} catch {
				return (null);
			}
		}

		private void ParsePacketDetails(ProtocolParseInfo.InformationObject info, Packet packet)
		{
			var info_sub = (ProtocolParseInfo.InformationObject)null;

			switch (packet) {
				case ArpPacket packet_arp:
					break;

				case DhcpV4Packet packet_dhcpv4:
					break;

				case DrdaDdmPacket packet_drdaddm:
					break;

				case DrdaPacket packet_drda:
					break;

				case EthernetPacket packet_ethernet:
					info_sub = ParsePacketDetails_Ethernet(packet_ethernet);
					break;

				case GrePacket packet_gre:
					break;

				case IcmpV4Packet packet_icmpv4:
					break;

				case IcmpV6Packet packet_icmpv6:
					break;

				case Ieee8021QPacket packet_ieee8021q:
					break;

				case IgmpV2Packet packet_igmpv2:
					break;

				case InternetLinkLayerPacket packet_internetlinklayer:
					break;

				case IPv4Packet packet_ipv4:
					break;

				case IPv6Packet packet_ipv6:
					break;

				case TcpPacket packet_tcp:
					break;

				case UdpPacket packet_udp:
					break;
			}

			if (info_sub != null) {
				info.SubItems.Add(info_sub);
			}

			/* Next Layer */
			if (packet.HasPayloadPacket) {
				ParsePacketDetails(info, packet.PayloadPacket);
			}
		}

		private ProtocolParseInfo.InformationObject ParsePacketDetails_Ethernet(EthernetPacket packet)
		{
			var info = new ProtocolParseInfo.InformationObject("Ethernet");

			/* MAC - Destination */
			info.Add(string.Format("DstMAC: {0}", packet.DestinationHardwareAddress.ToString()));

			/* MAC - Source */
			info.Add(string.Format("SrcMAC: {0}", packet.SourceHardwareAddress.ToString()));

			/* EtherType */
			info.Add(string.Format("EtherType: {0}({1:X4})", packet.Type, (uint)packet.Type));

			/* Payload */
			if (packet.HasPayloadData) {
				info.Add(string.Format("Data: {0}", HexTextEncoder.ToHexText(packet.PayloadData)));
			}

			return (info);
		}
	}
}
