using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.General.Pcap;
using SharpPcap;

namespace Ratatoskr.General.Pcap.SharpPcap
{
    public static class SharpPcapParser
    {
        public static PacketObject ParseAndBuild(RawCapture packet_sp, PcapPacketParserOption option)
        {
            return (ParseAndBuild(Parse(packet_sp), option));
        }

		public static PacketObject ParseAndBuild(PcapPacketInfo packet_info, PcapPacketParserOption option)
		{
            if (   (packet_info == null)
                || (option == null)
            ) {
                return (null);
            }

            var info = "";
            var src_info = "";
            var dst_info = "";
            var data_contents = new byte[]{ };

            if (packet_info.ProtocolNames.Count > 0) {
                switch (option.InfoType) {
                    case PacketInfoType.AllProtocolName:
                        info = String.Join(".", packet_info.ProtocolNames);
                        break;
                    case PacketInfoType.TopProtocolName:
                        info = packet_info.ProtocolNames.Last();
                        break;
                }
            }

            switch (option.SourceType) {
                case SourceInfoType.MacAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.SourceHwAdress)) {
                        src_info = packet_info.SourceHwAddress.ToString();
                    }
                    break;
                case SourceInfoType.IpAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.SourceIpAddress)) {
                        src_info = packet_info.SourceIpAddress.ToString();
                    }
                    break;
                case SourceInfoType.IpAddressAndPortNo:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.SourceIpAddress)) {
                        if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.SourcePortNo)) {
                            if (packet_info.SourceIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) {
                                src_info = String.Format("[{0}]:{1}", packet_info.SourceIpAddress.ToString(), packet_info.SourcePortNo);
                            } else {
                                src_info = String.Format("{0}:{1}", packet_info.SourceIpAddress.ToString(), packet_info.SourcePortNo);
                            }
                        } else {
                            src_info = packet_info.SourceIpAddress.ToString();
                        }
                    }
                    break;
            }

            switch (option.DestinationType) {
                case DestinationInfoType.MacAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.DestinationHwAddress)) {
                        dst_info = packet_info.DestinationHwAddress.ToString();
                    }
                    break;
                case DestinationInfoType.IpAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.DestinationIpAddress)) {
                        dst_info = packet_info.DestinationIpAddress.ToString();
                    }
                    break;
                case DestinationInfoType.IpAddressAndPortNo:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.DestinationIpAddress)) {
                        if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.DestinationPortNo)) {
                            if (packet_info.DestinationIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) {
                                dst_info = String.Format("[{0}]:{1}", packet_info.DestinationIpAddress.ToString(), packet_info.DestinationPortNo);
                            } else {
                                dst_info = String.Format("{0}:{1}", packet_info.DestinationIpAddress.ToString(), packet_info.DestinationPortNo);
                            }
                        } else {
                            dst_info = packet_info.DestinationIpAddress.ToString();
                        }
                    }
                    break;
            }

            switch (option.DataContentsType) {
                case DataContentsType.Raw:
                    data_contents = packet_info.RawData;
                    break;
                case DataContentsType.TopProtocolDataUnit:
                    data_contents = packet_info.TopProtocolDataUnit;
                    break;
            }

            return (new PacketObject(
                            "Pcap",
                            PacketFacility.Device,
                            "",
                            PacketPriority.Standard,
                            PacketAttribute.Data,
                            packet_info.DateTime,
                            info,
                            packet_info.IsSendPacket ? PacketDirection.Send : PacketDirection.Recv,
                            src_info,
                            dst_info,
                            0x00,
                            null,
                            data_contents));
		}

        public static PcapPacketInfo Parse(RawCapture packet_sp)
        {
            if (packet_sp == null)return (null);

            return (Parse(packet_sp.Timeval.Date, packet_sp.LinkLayerType, packet_sp.Data));
        }

        public static PcapPacketInfo Parse(DateTime packet_time, in PcapLinkType packet_linktype, in byte[] packet_data)
        {
            /* PacketDotNetで未定義のリンクタイプの場合は処理しない */
            if (!Enum.IsDefined(typeof(PacketDotNet.LinkLayers), (int)packet_linktype))return (null);

            return (Parse(packet_time, (PacketDotNet.LinkLayers)Enum.ToObject(typeof(PacketDotNet.LinkLayers), (int)packet_linktype), packet_data));
        }

        public static PcapPacketInfo Parse(DateTime packet_time, in PacketDotNet.LinkLayers packet_linktype, in byte[] packet_data)
        {
            /* PacketDotNetで解析 */
            var packet_pdn = PacketDotNet.Packet.ParsePacket(packet_linktype, packet_data);

            if (packet_pdn == null)return (null);

            var packet_info = new PcapPacketInfo();

            packet_info.DateTime = packet_time;
            packet_info.RawData = packet_pdn.Bytes;
            packet_info.TopProtocolDataUnit = packet_pdn.Bytes;

            AnalyzePcapPacket(packet_info, packet_pdn);

            return (packet_info);
        }

        private static void AnalyzePcapPacket(PcapPacketInfo packet_info, PacketDotNet.Packet packet_pdn, ushort ether_type = 0)
        {
            if (packet_pdn == null)return;

            ushort payload_ether_type = 0;

            try {
                switch (packet_pdn) {
                    case PacketDotNet.EthernetPacket packet_i:
                        AnalyzePcapPacket_Ethernet(packet_info, packet_i);
                        payload_ether_type = (ushort)packet_i.Type;
                        break;

                    case PacketDotNet.PppPacket packet_i:
                        AnalyzePcapPacket_PPP(packet_info, packet_i);
                        break;

                    case PacketDotNet.Ieee8021QPacket packet_i:
                        AnalyzePcapPacket_IEEE8021Q(packet_info, packet_i);
                        payload_ether_type = (ushort)packet_i.Type;
                        break;

                    case PacketDotNet.LldpPacket packet_i:
                        AnalyzePcapPacket_LLDP(packet_info, packet_i);
                        break;

                    case PacketDotNet.IPPacket packet_i:
                        AnalyzePcapPacket_IP(packet_info, packet_i);
                        break;

                    case PacketDotNet.ArpPacket packet_i:
                        AnalyzePcapPacket_Arp(packet_info, packet_i);
                        break;

                    case PacketDotNet.IcmpV4Packet packet_i:
                        AnalyzePcapPacket_ICMPv4(packet_info, packet_i);
                        break;

                    case PacketDotNet.IcmpV6Packet packet_i:
                        AnalyzePcapPacket_ICMPv6(packet_info, packet_i);
                        break;

                    case PacketDotNet.TcpPacket packet_i:
                        AnalyzePcapPacket_Tcp(packet_info, packet_i);
                        break;

                    case PacketDotNet.UdpPacket packet_i:
                        AnalyzePcapPacket_Udp(packet_info, packet_i);
                        break;

                    default:
                        AnalyzePcapPacket_FromEtherType(packet_info, packet_pdn, ether_type);
                        break;
                }

                /* 現在のプロトコルのペイロードを記録 */
                packet_info.TopProtocolDataUnit = packet_pdn.PayloadData;

                /* ペイロードを解析 */
                AnalyzePcapPacket(packet_info, packet_pdn.PayloadPacket);

            } catch {
            }
        }

        private static void AnalyzePcapPacket_Ethernet(PcapPacketInfo packet_info, PacketDotNet.EthernetPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("Ethernet");

            packet_info.SourceHwAddress = packet_pdn.SourceHardwareAddress;
            packet_info.DestinationHwAddress = packet_pdn.DestinationHardwareAddress;
        }

        private static void AnalyzePcapPacket_PPP(PcapPacketInfo packet_info, PacketDotNet.PppPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("PPP");
        }

        private static void AnalyzePcapPacket_IEEE8021Q(PcapPacketInfo packet_info, PacketDotNet.Ieee8021QPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add(String.Format("VLAN={0}", packet_pdn.VlanIdentifier));
        }

        private static void AnalyzePcapPacket_LLDP(PcapPacketInfo packet_info, PacketDotNet.LldpPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("LLDP");
        }

        private static void AnalyzePcapPacket_IP(PcapPacketInfo packet_info, PacketDotNet.IPPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("IP");

            packet_info.SourceIpAddress = packet_pdn.SourceAddress;
            packet_info.DestinationIpAddress = packet_pdn.DestinationAddress;
        }

        private static void AnalyzePcapPacket_Arp(PcapPacketInfo packet_info, PacketDotNet.ArpPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("ARP");
        }

        private static void AnalyzePcapPacket_ICMPv4(PcapPacketInfo packet_info, PacketDotNet.IcmpV4Packet packet_pdn)
        {
            packet_info.ProtocolNames.Add("ICMPv4");
        }

        private static void AnalyzePcapPacket_ICMPv6(PcapPacketInfo packet_info, PacketDotNet.IcmpV6Packet packet_pdn)
        {
            packet_info.ProtocolNames.Add("ICMPv6");
        }

        private static void AnalyzePcapPacket_Tcp(PcapPacketInfo packet_info, PacketDotNet.TcpPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("TCP");

            packet_info.SourcePortNo = packet_pdn.SourcePort;
            packet_info.DestinationPortNo = packet_pdn.DestinationPort;
        }

        private static void AnalyzePcapPacket_Udp(PcapPacketInfo packet_info, PacketDotNet.UdpPacket packet_pdn)
        {
            packet_info.ProtocolNames.Add("UDP");

            packet_info.SourcePortNo = packet_pdn.SourcePort;
            packet_info.DestinationPortNo = packet_pdn.DestinationPort;
        }

        private static void AnalyzePcapPacket_FromEtherType(PcapPacketInfo packet_info, PacketDotNet.Packet packet_pdn, ushort ether_type)
        {
        }
    }
}
