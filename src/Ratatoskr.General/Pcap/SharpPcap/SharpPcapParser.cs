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
        private static readonly EtherType[] EtherTypeValues = (EtherType[])Enum.GetValues(typeof(EtherType));


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
                    case PcapPacketInfoType.AllProtocolName:
                        info = String.Join(".", packet_info.ProtocolNames);
                        break;
                    case PcapPacketInfoType.TopProtocolName:
                        info = packet_info.ProtocolNames.Last();
                        break;
                }
            }

            switch (option.SourceType) {
                case PcapPacketSourceType.MacAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.SourceHwAdress)) {
                        src_info = packet_info.SourceHwAddress.ToString();
                    }
                    break;
                case PcapPacketSourceType.IpAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.SourceIpAddress)) {
                        src_info = packet_info.SourceIpAddress.ToString();
                    }
                    break;
                case PcapPacketSourceType.IpAddressAndPortNo:
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
                case PcapPacketDestinationType.MacAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.DestinationHwAddress)) {
                        dst_info = packet_info.DestinationHwAddress.ToString();
                    }
                    break;
                case PcapPacketDestinationType.IpAddress:
                    if (packet_info.ItemFlags.HasFlag(PcapPacketInfoItemFlags.DestinationIpAddress)) {
                        dst_info = packet_info.DestinationIpAddress.ToString();
                    }
                    break;
                case PcapPacketDestinationType.IpAddressAndPortNo:
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

            switch (option.DataType) {
                case PcapPacketDataType.Raw:
                    data_contents = packet_info.RawData;
                    break;
                case PcapPacketDataType.TopProtocolDataUnit:
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

        private static void AnalyzePcapPacket(PcapPacketInfo packet_info, PacketDotNet.Packet packet_pdn, int ether_type = -1, int protocol_type = -1)
        {
            if (packet_pdn == null)return;

            int payload_ether_type = -1;
            int payload_protocol_type = -1;

            try {
                var protocol_name = "";

                switch (packet_pdn) {
                    case PacketDotNet.EthernetPacket packet_i:
                        protocol_name = AnalyzePcapPacket_Ethernet(packet_info, packet_i);
                        payload_ether_type = (ushort)packet_i.Type;
                        break;

                    case PacketDotNet.Ieee8021QPacket packet_i:
                        protocol_name = AnalyzePcapPacket_IEEE8021Q(packet_info, packet_i);
                        payload_ether_type = (ushort)packet_i.Type;
                        break;

                    case PacketDotNet.IPPacket packet_i:
                        protocol_name = AnalyzePcapPacket_IP(packet_info, packet_i);
                        payload_protocol_type = (byte)packet_i.Protocol;
                        break;

                    case PacketDotNet.TcpPacket packet_i:
                        protocol_name = AnalyzePcapPacket_Tcp(packet_info, packet_i);
                        break;

                    case PacketDotNet.UdpPacket packet_i:
                        protocol_name = AnalyzePcapPacket_Udp(packet_info, packet_i);
                        break;
                }

                /* 現在のEtherTypeもしくはプロトコルタイプを取得 */
                if ((protocol_name == null) || (protocol_name.Length == 0)) {
                    protocol_name = GetProtocolName(ether_type, protocol_type);
                }

                if ((protocol_name != null) && (protocol_name.Length > 0)) {
                    packet_info.ProtocolNames.Add(protocol_name);
                }

                /* 現在のプロトコルのペイロードを記録 */
//                packet_info.TopProtocolDataUnit = packet_pdn.PayloadData;

                /* ペイロードを解析 */
                AnalyzePcapPacket(packet_info, packet_pdn.PayloadPacket, payload_ether_type, payload_protocol_type);

            } catch {
            }
        }

        private static string AnalyzePcapPacket_Ethernet(PcapPacketInfo packet_info, PacketDotNet.EthernetPacket packet_pdn)
        {
            packet_info.ItemFlags |= PcapPacketInfoItemFlags.SourceHwAdress;
            packet_info.SourceHwAddress = packet_pdn.SourceHardwareAddress;

            packet_info.ItemFlags |= PcapPacketInfoItemFlags.DestinationHwAddress;
            packet_info.DestinationHwAddress = packet_pdn.DestinationHardwareAddress;

            return (null);
        }

        private static string AnalyzePcapPacket_IEEE8021Q(PcapPacketInfo packet_info, PacketDotNet.Ieee8021QPacket packet_pdn)
        {
            return (String.Format("VLAN={0}", packet_pdn.VlanIdentifier));
        }

        private static string AnalyzePcapPacket_IP(PcapPacketInfo packet_info, PacketDotNet.IPPacket packet_pdn)
        {
            packet_info.ItemFlags |= PcapPacketInfoItemFlags.SourceIpAddress;
            packet_info.SourceIpAddress = packet_pdn.SourceAddress;

            packet_info.ItemFlags |= PcapPacketInfoItemFlags.DestinationIpAddress;
            packet_info.DestinationIpAddress = packet_pdn.DestinationAddress;

            return (null);
        }

        private static string AnalyzePcapPacket_Tcp(PcapPacketInfo packet_info, PacketDotNet.TcpPacket packet_pdn)
        {
            packet_info.ItemFlags |= PcapPacketInfoItemFlags.SourcePortNo;
            packet_info.SourcePortNo = packet_pdn.SourcePort;

            packet_info.ItemFlags |= PcapPacketInfoItemFlags.DestinationPortNo;
            packet_info.DestinationPortNo = packet_pdn.DestinationPort;

            return (null);
        }

        private static string AnalyzePcapPacket_Udp(PcapPacketInfo packet_info, PacketDotNet.UdpPacket packet_pdn)
        {
            packet_info.ItemFlags |= PcapPacketInfoItemFlags.SourcePortNo;
            packet_info.SourcePortNo = packet_pdn.SourcePort;

            packet_info.ItemFlags |= PcapPacketInfoItemFlags.DestinationPortNo;
            packet_info.DestinationPortNo = packet_pdn.DestinationPort;

            return (null);
        }

        private static string GetProtocolName(int ether_type, int protocol_type)
        {
            if (ether_type >= 0) {
                switch ((ushort)ether_type) {
                    case ushort type when (type >= 0x0000) && (type <= 0x05DC): return "IEEE802.3 Length Field";
                    case ushort type when (type >= 0x0101) && (type <= 0x01FF): return "Experimental";
                    case 0x0200: return "XEROX PUP";
                    case 0x0201: return "PUP Addr Trans";
                    case 0x0400: return "Nixdorf";
                    case 0x0600: return "XEROX NS IDP";
                    case 0x0660: return "DLOG_0x0660";
                    case 0x0661: return "DLOG_0x0661";
                    case 0x0800: return "IPv4";
                    case 0x0801: return "X.75";
                    case 0x0802: return "NBS";
                    case 0x0803: return "ECMA";
                    case 0x0804: return "Chaosnet";
                    case 0x0805: return "X.25 Level3";
                    case 0x0806: return "ARP";
                    case 0x0807: return "XNS Compatability";
                    case 0x0808: return "Frame Relay ARP";
                    case 0x081C: return "Symbolics Private";
                    case 0x0842: return "WakeOnLan";
                    case ushort type when (type >= 0x0888) && (type <= 0x088A): return "Xyplex";
                    case 0x0900: return "Ungermann-Bass net debugr";
                    case 0x0A00: return "Xerox IEEE802.3 PUP";
                    case 0x0A01: return "PUP Addr Trans";
                    case 0x0BAD: return "Banyan VINES";
                    case 0x0BAE: return "VINES Loopback";
                    case 0x0BAF: return "VINES Echo";
                    case 0x1000: return "Berkeley Trailer nego";
                    case ushort type when (type >= 0x1001) && (type <= 0x100F): return "Berkeley Trailer encap/IP";
                    case 0x1600: return "Valid Systems";
                    case 0x22F3: return "TRILL";
                    case 0x22F4: return "L2-IS-IS";
                    case 0x4242: return "PCS Basic Block Protocol";
                    case 0x5208: return "BBN Simnet";
                    case 0x6000: return "DEC Unassigned (Exp.)";
                    case 0x6001: return "DEC MOP Dump/Load";
                    case 0x6002: return "DEC MOP Remote Console";
                    case 0x6003: return "DEC DECNET Phase IV Route";
                    case 0x6004: return "DEC LAT";
                    case 0x6005: return "DEC Diagnostic Protocol";
                    case 0x6006: return "DEC Customer Protocol";
                    case 0x6007: return "DEC LAVC, SCA";
                    case ushort type when (type >= 0x6008) && (type <= 0x6009): return "DEC Unassigned";
                    case ushort type when (type >= 0x6010) && (type <= 0x6014): return "3Com Corporation";
                    case 0x6558: return "Trans Ether Bridging";
                    case 0x6559: return "Raw Frame Relay";
                    case 0x7000: return "Ungermann-Bass download";
                    case 0x7002: return "Ungermann-Bass dia/loop";
                    case ushort type when (type >= 0x7020) && (type <= 0x7029): return "LRT";
                    case 0x7030: return "Proteon";
                    case 0x7034: return "Cabletron";
                    case 0x8003: return "Cronus VLN";
                    case 0x8004: return "Cronus Direct";
                    case 0x8005: return "HP Probe";
                    case 0x8006: return "Nestar";
                    case 0x8008: return "AT&T";
                    case 0x8010: return "Excelan";
                    case 0x8013: return "SGI diagnostics";
                    case 0x8014: return "SGI network games";
                    case 0x8015: return "SGI reserved";
                    case 0x8016: return "SGI bounce server";
                    case 0x8019: return "Apollo Domain";
                    case 0x802E: return "Tymshare";
                    case 0x802F: return "Tigan, Inc.";
                    case 0x8035: return "Reverse Address Resolution Protocol (RARP)";
                    case 0x8036: return "Aeonic Systems";
                    case 0x8038: return "DEC LANBridge";
                    case ushort type when (type >= 0x8039) && (type <= 0x803C): return "DEC Unassigned";
                    case 0x803D: return "DEC Ethernet Encryption";
                    case 0x803E: return "DEC Unassigned";
                    case 0x803F: return "DEC LAN Traffic Monitor";
                    case ushort type when (type >= 0x8040) && (type <= 0x8042): return "DEC Unassigned";
                    case 0x8044: return "Planning Research Corp.";
                    case 0x8046: return "AT&T";
                    case 0x8047: return "AT&T";
                    case 0x8049: return "ExperData";
                    case 0x805B: return "Stanford V Kernel exp.";
                    case 0x805C: return "Stanford V Kernel prod.";
                    case 0x805D: return "Evans & Sutherland";
                    case 0x8060: return "Little Machines";
                    case 0x8062: return "Counterpoint Computers";
                    case 0x8065: return "Univ. of Mass. @ Amherst";
                    case 0x8066: return "Univ. of Mass. @ Amherst";
                    case 0x8067: return "Veeco Integrated Auto.";
                    case 0x8068: return "General Dynamics";
                    case 0x8069: return "AT&T";
                    case 0x806A: return "Autophon";
                    case 0x806C: return "ComDesign";
                    case 0x806D: return "Computgraphic Corp.";
                    case ushort type when (type >= 0x806E) && (type <= 0x8077): return "Landmark Graphics Corp.";
                    case 0x807A: return "Matra";
                    case 0x807B: return "Dansk Data Elektronik";
                    case 0x807C: return "Merit Internodal";
                    case ushort type when (type >= 0x807D) && (type <= 0x807F): return "Vitalink Communications";
                    case 0x8080: return "Vitalink TransLAN III";
                    case ushort type when (type >= 0x8081) && (type <= 0x8083): return "Counterpoint Computers";
                    case 0x809B: return "Appletalk";
                    case ushort type when (type >= 0x809C) && (type <= 0x809E): return "Datability";
                    case 0x809F: return "Spider Systems Ltd.";
                    case 0x80A3: return "Nixdorf Computers";
                    case ushort type when (type >= 0x80A4) && (type <= 0x80B3): return "Siemens Gammasonics Inc.";
                    case ushort type when (type >= 0x80C0) && (type <= 0x80C3): return "DCA Data Exchange Cluster";
                    case 0x80C4: return "Banyan Systems";
                    case 0x80C5: return "Banyan Systems";
                    case 0x80C6: return "Pacer Software";
                    case 0x80C7: return "Applitek Corporation";
                    case ushort type when (type >= 0x80C8) && (type <= 0x80CC): return "Intergraph Corporation";
                    case ushort type when (type >= 0x80CD) && (type <= 0x80CE): return "Harris Corporation";
                    case ushort type when (type >= 0x80CF) && (type <= 0x80D2): return "Taylor Instrument";
                    case ushort type when (type >= 0x80D3) && (type <= 0x80D4): return "Rosemount Corporation";
                    case 0x80D5: return "IBM SNA Service on Ether";
                    case 0x80DD: return "Varian Associates";
                    case ushort type when (type >= 0x80DE) && (type <= 0x80DF): return "Integrated Solutions TRFS";
                    case ushort type when (type >= 0x80E0) && (type <= 0x80E3): return "Allen-Bradley";
                    case ushort type when (type >= 0x80E4) && (type <= 0x80F0): return "Datability";
                    case 0x80F2: return "Retix";
                    case 0x80F3: return "AppleTalk AARP";
                    case ushort type when (type >= 0x80F4) && (type <= 0x80F5): return "Kinetics";
                    case 0x80F7: return "Apollo Computer";
                    case 0x80FF: return "Wellfleet Communications";
                    case 0x8100: return "VLAN C-Tag";
                    case ushort type when (type >= 0x8101) && (type <= 0x8103): return "Wellfleet Communications";
                    case ushort type when (type >= 0x8107) && (type <= 0x8109): return "Symbolics Private";
                    case 0x8130: return "Hayes Microcomputers";
                    case 0x8131: return "VG Laboratory Systems";
                    case ushort type when (type >= 0x8132) && (type <= 0x8136): return "Bridge Communications";
                    case ushort type when (type >= 0x8137) && (type <= 0x8138): return "Novell, Inc.";
                    case ushort type when (type >= 0x8139) && (type <= 0x813D): return "KTI";
                    case 0x8148: return "Logicraft";
                    case 0x8149: return "Network Computing Devices";
                    case 0x814A: return "Alpha Micro";
                    case 0x814C: return "SNMP";
                    case 0x814D: return "BIIN";
                    case 0x814E: return "BIIN";
                    case 0x814F: return "Technically Elite Concept";
                    case 0x8150: return "Rational Corp";
                    case ushort type when (type >= 0x8151) && (type <= 0x8153): return "Qualcomm";
                    case ushort type when (type >= 0x815C) && (type <= 0x815E): return "Computer Protocol Pty Ltd";
                    case ushort type when (type >= 0x8164) && (type <= 0x8166): return "Charles River Data System";
                    case 0x817D: return "XTP";
                    case 0x817E: return "SGITime Warner prop.";
                    case 0x8180: return "HIPPI-FP encapsulation";
                    case 0x8181: return "STP, HIPPI-ST";
                    case 0x8182: return "Reserved for HIPPI-6400";
                    case 0x8183: return "Reserved for HIPPI-6400";
                    case ushort type when (type >= 0x8184) && (type <= 0x818C): return "Silicon Graphics prop.";
                    case 0x818D: return "Motorola Computer";
                    case ushort type when (type >= 0x819A) && (type <= 0x81A3): return "Qualcomm";
                    case 0x81A4: return "ARAI Bunkichi";
                    case ushort type when (type >= 0x81A5) && (type <= 0x81AE): return "RAD Network Devices";
                    case ushort type when (type >= 0x81B7) && (type <= 0x81B9): return "Xyplex";
                    case ushort type when (type >= 0x81CC) && (type <= 0x81D5): return "Apricot Computers";
                    case ushort type when (type >= 0x81D6) && (type <= 0x81DD): return "Artisoft";
                    case ushort type when (type >= 0x81E6) && (type <= 0x81EF): return "Polygon";
                    case ushort type when (type >= 0x81F0) && (type <= 0x81F2): return "Comsat Labs";
                    case ushort type when (type >= 0x81F3) && (type <= 0x81F5): return "SAIC";
                    case ushort type when (type >= 0x81F6) && (type <= 0x81F8): return "VG Analytical";
                    case ushort type when (type >= 0x8203) && (type <= 0x8205): return "Quantum Software";
                    case ushort type when (type >= 0x8221) && (type <= 0x8222): return "Ascom Banking Systems";
                    case ushort type when (type >= 0x823E) && (type <= 0x8240): return "Advanced Encryption Syste";
                    case ushort type when (type >= 0x827F) && (type <= 0x8282): return "Athena Programming";
                    case ushort type when (type >= 0x8263) && (type <= 0x826A): return "Charles River Data System";
                    case ushort type when (type >= 0x829A) && (type <= 0x829B): return "Inst Ind Info Tech";
                    case ushort type when (type >= 0x829C) && (type <= 0x82AB): return "Taurus Controls";
                    case ushort type when (type >= 0x82AC) && (type <= 0x8693): return "Walker Richer & Quinn";
                    case ushort type when (type >= 0x8694) && (type <= 0x869D): return "Idea Courier";
                    case ushort type when (type >= 0x869E) && (type <= 0x86A1): return "Computer Network Tech";
                    case ushort type when (type >= 0x86A3) && (type <= 0x86AC): return "Gateway Communications";
                    case 0x86DB: return "SECTRA";
                    case 0x86DE: return "Delta Controls";
                    case 0x86DD: return "IPv6";
                    case 0x86DF: return "ATOMIC";
                    case ushort type when (type >= 0x86E0) && (type <= 0x86EF): return "Landis GyrPowers";
                    case ushort type when (type >= 0x8700) && (type <= 0x8710): return "Motorola";
                    case 0x876B: return "TCP/IP Compression";
                    case 0x876C: return "IP Autonomous Systems";
                    case 0x876D: return "Secure Data";
                    case 0x8808: return "EPON";
                    case 0x880B: return "PPP";
                    case 0x880C: return "GSMP";
                    case 0x8847: return "MPLS Unicast";
                    case 0x8848: return "MPLS Multicast";
                    case 0x8861: return "MCAP";
                    case 0x8863: return "PPPoE Discovery Stage";
                    case 0x8864: return "PPPoE Session Stage";
                    case 0x888E: return "IEEE 802.1X";
                    case 0x88A8: return "IEEE 802.1Q S-Tag";
                    case ushort type when (type >= 0x8A96) && (type <= 0x8A97): return "Invisible Software";
                    case 0x88B5: return "IEEE 802 Local Experimental Ethertype";
                    case 0x88B6: return "IEEE 802 Local Experimental Ethertype";
                    case 0x88B7: return "IEEE 802 OUI Extended Ethertype";
                    case 0x88C7: return "IEEE 802.11 Pre-Authentication";
                    case 0x88CC: return "LLDP";
                    case 0x88E5: return "MACSec";
                    case 0x88E7: return "Provider Backbone Bridging Instance tag";
                    case 0x88F5: return "MVRP";
                    case 0x88F6: return "MMRP";
                    case 0x88F7: return "PTP";
                    case 0x890D: return "FastRoamingRemoteRequest";
                    case 0x8917: return "MediaIndependentHandoverProtocol";
                    case 0x8929: return "Multiple I-SidRegistrationProtocol";
                    case 0x893B: return "TRILL FGL";
                    case 0x8940: return "ECP Protocol";
                    case 0x8946: return "TRILL RBridge Channel";
                    case 0x8947: return "GeoNetworking";
                    case 0x894F: return "NSH";
                    case 0x9000: return "Loopback";
                    case 0x9001: return "3Com XNS Sys Mgmt";
                    case 0x9002: return "3Com TCP-IP Sys";
                    case 0x9003: return "3Com loop detect";
                    case 0x9100: return "VLAN Double-Tag";
                    case 0x9A22: return "Multi-Topology";
                    case 0xA0ED: return "LoWPAN encapsulation";
                    case 0xB7EA: return "GRE packets";
                    case 0xFF00: return "BBN VITAL-LanBridge cache";
                    case ushort type when (type >= 0xFF00) && (type <= 0xFF0F): return "ISC Bunker Ramo";
                    case 0xFFFF: return "Reserved";
                }

                return ("Unknown");
            }

            if (protocol_type >= 0) {
                switch ((byte)protocol_type) {
                    case 0x00: return "HOPOPT";
                    case 0x01: return "ICMP";
                    case 0x02: return "IGMP";
                    case 0x03: return "GGP";
                    case 0x04: return "IPv4";
                    case 0x05: return "ST";
                    case 0x06: return "TCP";
                    case 0x07: return "CBT";
                    case 0x08: return "EGP";
                    case 0x09: return "IGP";
                    case 0x0A: return "BBN-RCC-MON";
                    case 0x0B: return "NVP-II";
                    case 0x0C: return "PUP";
                    case 0x0D: return "ARGUS (deprecated)";
                    case 0x0E: return "EMCON";
                    case 0x0F: return "XNET";
                    case 0x10: return "CHAOS";
                    case 0x11: return "UDP";
                    case 0x12: return "MUX";
                    case 0x13: return "DCN-MEAS";
                    case 0x14: return "HMP";
                    case 0x15: return "PRM";
                    case 0x16: return "XNS-IDP";
                    case 0x17: return "TRUNK-1";
                    case 0x18: return "TRUNK-2";
                    case 0x19: return "LEAF-1";
                    case 0x1A: return "LEAF-2";
                    case 0x1B: return "RDP";
                    case 0x1C: return "IRTP";
                    case 0x1D: return "ISO-TP4";
                    case 0x1E: return "NETBLT";
                    case 0x1F: return "MFE-NSP";
                    case 0x20: return "MERIT-INP";
                    case 0x21: return "DCCP";
                    case 0x22: return "3PC";
                    case 0x23: return "IDPR";
                    case 0x24: return "XTP";
                    case 0x25: return "DDP";
                    case 0x26: return "IDPR-CMTP";
                    case 0x27: return "TP++";
                    case 0x28: return "IL";
                    case 0x29: return "IPv6";
                    case 0x2A: return "SDRP";
                    case 0x2B: return "IPv6-Route";
                    case 0x2C: return "IPv6-Frag";
                    case 0x2D: return "IDRP";
                    case 0x2E: return "RSVP";
                    case 0x2F: return "GRE";
                    case 0x30: return "DSR";
                    case 0x31: return "BNA";
                    case 0x32: return "ESP";
                    case 0x33: return "AH";
                    case 0x34: return "I-NLSP";
                    case 0x35: return "SWIPE (deprecated)";
                    case 0x36: return "NARP";
                    case 0x37: return "MOBILE";
                    case 0x38: return "TLSP";
                    case 0x39: return "SKIP";
                    case 0x3A: return "IPv6-ICMP";
                    case 0x3B: return "IPv6-NoNxt";
                    case 0x3C: return "IPv6-Opts";
                    case 0x3E: return "CFTP";
                    case 0x40: return "SAT-EXPAK";
                    case 0x41: return "KRYPTOLAN";
                    case 0x42: return "RVD";
                    case 0x43: return "IPPC";
                    case 0x45: return "SAT-MON";
                    case 0x46: return "VISA";
                    case 0x47: return "IPCV";
                    case 0x48: return "CPNX";
                    case 0x49: return "CPHB";
                    case 0x4A: return "WSN";
                    case 0x4B: return "PVP";
                    case 0x4C: return "BR-SAT-MON";
                    case 0x4D: return "SUN-ND";
                    case 0x4E: return "WB-MON";
                    case 0x4F: return "WB-EXPAK";
                    case 0x50: return "ISO-IP";
                    case 0x51: return "VMTP";
                    case 0x52: return "SECURE-VMTP";
                    case 0x53: return "VINES";
                    case 0x54: return "TTP/IPTM";
                    case 0x55: return "NSFNET-IGP";
                    case 0x56: return "DGP";
                    case 0x57: return "TCF";
                    case 0x58: return "EIGRP";
                    case 0x59: return "OSPFIGP";
                    case 0x5A: return "Sprite-RPC";
                    case 0x5B: return "LARP";
                    case 0x5C: return "MTP";
                    case 0x5D: return "AX.25";
                    case 0x5E: return "IPIP";
                    case 0x5F: return "MICP (deprecated)";
                    case 0x60: return "SCC-SP";
                    case 0x61: return "ETHERIP";
                    case 0x62: return "ENCAP";
                    case 0x64: return "GMTP";
                    case 0x65: return "IFMP";
                    case 0x66: return "PNNI";
                    case 0x67: return "PIM";
                    case 0x68: return "ARIS";
                    case 0x69: return "SCPS";
                    case 0x6A: return "QNX";
                    case 0x6B: return "A/N";
                    case 0x6C: return "IPComp";
                    case 0x6D: return "SNP";
                    case 0x6E: return "Compaq-Peer";
                    case 0x6F: return "IPX-in-IP";
                    case 0x70: return "VRRP";
                    case 0x71: return "PGM";
                    case 0x73: return "L2TP";
                    case 0x74: return "DDX";
                    case 0x75: return "IATP";
                    case 0x76: return "STP";
                    case 0x77: return "SRP";
                    case 0x78: return "UTI";
                    case 0x79: return "SMP";
                    case 0x7A: return "SM (deprecated)";
                    case 0x7B: return "PTP";
                    case 0x7C: return "ISIS over IPv4";
                    case 0x7D: return "FIRE";
                    case 0x7E: return "CRTP";
                    case 0x7F: return "CRUDP";
                    case 0x80: return "SSCOPMCE";
                    case 0x81: return "IPLT";
                    case 0x82: return "SPS";
                    case 0x83: return "PIPE";
                    case 0x84: return "SCTP";
                    case 0x85: return "FC";
                    case 0x86: return "RSVP-E2E-IGNORE";
                    case 0x87: return "Mobility Header";
                    case 0x88: return "UDPLite";
                    case 0x89: return "MPLS-in-IP";
                    case 0x8A: return "manet";
                    case 0x8B: return "HIP";
                    case 0x8C: return "Shim6";
                    case 0x8D: return "WESP";
                    case 0x8E: return "ROHC";
                    case 0x8F: return "Ethernet";
                }

                return ("Unknown");
            }

            return (null);
        }
    }
}
