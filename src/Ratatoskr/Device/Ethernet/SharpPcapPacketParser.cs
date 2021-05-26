using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using SharpPcap;

namespace RtsPlugin.Pcap.Utility
{
    internal static class SharpPcapPacketParser
    {
        private class PacketAnalyzeParam
        {
            public ICaptureDevice Device;

            public PacketDotNet.Packet CurrentPacket;

            public bool IsSend;

            public string Protocol;

            public PhysicalAddress SourceHwAddress;
            public PhysicalAddress DestinationHwAddress;

            public IPAddress SourceAddress;
            public IPAddress DestinationAddress;

            public ushort SourcePort;
            public ushort DestinationPort;

            public byte[] DataLinkFrame;
            public byte[] PayloadFrame;

            public PacketAnalyzeParam(ICaptureDevice dev, RawCapture packet)
            {
                Device = dev;

                IsSend = false;

                CurrentPacket = PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data);
 
                DataLinkFrame = PayloadFrame = packet.Data;
            }
        }

        public static PacketObject Convert(ICaptureDevice dev, RawCapture packet, PcapPacketParserOption option)
        {
            if (packet == null)return (null);

            var info = Parse(dev, packet, option);

            return (new PacketObject(
                            "WinPcap",
                            PacketFacility.Device,
                            "",
                            PacketPriority.Standard,
                            PacketAttribute.Data,
                            info.DateTime,
                            "",
                            info.Direction,
                            info.Source,
                            info.Destination,
                            0x00,
                            null,
                            info.Data));
        }

        public static PcapPacketInfo Parse(ICaptureDevice dev, RawCapture packet, PcapPacketParserOption option)
        {
            var analyze_param = new PacketAnalyzeParam(dev, packet);

            /* パケット解析 */
            Analyze(analyze_param);

            return (CreatePacketInfo(packet, analyze_param, option));
        }

        private static PcapPacketInfo CreatePacketInfo(RawCapture packet, PacketAnalyzeParam param, PcapPacketParserOption option)
        {
            var packet_info = new PcapPacketInfo();

            packet_info.DateTime = packet.Timeval.Date;

            packet_info.Direction = (param.IsSend) ? (PacketDirection.Send) : (PacketDirection.Recv);

            packet_info.Information = param.Protocol;

            switch (option.SourceInfo) {
                case SourceInfoType.MacAddress:
                    if (param.SourceHwAddress != null) {
                        packet_info.Source = param.SourceHwAddress.ToString();
                    }
                    break;
                case SourceInfoType.IpAddressAndPortNo:
                    if (param.SourceAddress != null) {
                        packet_info.Source = string.Format("{0}:{1}", param.SourceAddress.ToString(), param.SourcePort);
                    }
                    break;
                case SourceInfoType.IpAddress:
                    if (param.SourceAddress != null) {
                        packet_info.Source = param.SourceAddress.ToString();
                    }
                    break;
                case SourceInfoType.PortNo:
                    if (param.SourceAddress != null) {
                        packet_info.Source = string.Format(":{0}", param.SourcePort.ToString());
                    }
                    break;
            }

            switch (option.DestinationInfo) {
                case DestinationInfoType.MacAddress:
                    if (param.DestinationHwAddress != null) {
                        packet_info.Destination = param.DestinationHwAddress.ToString();
                    }
                    break;
                case DestinationInfoType.IpAddressAndPortNo:
                    if (param.DestinationAddress != null) {
                        packet_info.Destination = string.Format("{0}:{1}", param.DestinationAddress.ToString(), param.DestinationPort);
                    }
                    break;
                case DestinationInfoType.IpAddress:
                    if (param.DestinationAddress != null) {
                        packet_info.Destination = param.DestinationAddress.ToString();
                    }
                    break;
                case DestinationInfoType.PortNo:
                    if (param.DestinationAddress != null) {
                        packet_info.Destination = string.Format(":{0}", param.DestinationPort.ToString());
                    }
                    break;
            }

            switch (option.DataContents) {
                case DataContentsType.Payload:
                    packet_info.Data = param.PayloadFrame;
                    break;

                case DataContentsType.Raw:
                default:
                    packet_info.Data = param.DataLinkFrame;
                    break;
            }

            if (packet_info.Data == null) {
                packet_info.Data = new byte[] { };
            }

            return (packet_info);
        }

        private static void Analyze(PacketAnalyzeParam param)
        {
            if (param.CurrentPacket == null)return;

            try {
                switch (param.CurrentPacket) {
                    case PacketDotNet.EthernetPacket packet_i:
                        AnalyzePacket_Ethernet(packet_i, param);
                        break;

                    case PacketDotNet.PppPacket packet_i:
                        AnalyzePacket_PPP(packet_i, param);
                        break;

                    case PacketDotNet.Ieee8021QPacket packet_i:
                        AnalyzePacket_IEEE8021Q(packet_i, param);
                        break;

                    case PacketDotNet.IPPacket packet_i:
                        AnalyzePacket_IP(packet_i, param);
                        break;

                    case PacketDotNet.ArpPacket packet_i:
                        AnalyzePacket_Arp(packet_i, param);
                        break;

                    case PacketDotNet.IcmpV4Packet packet_i:
                        AnalyzePacket_ICMPv4(packet_i, param);
                        break;

                    case PacketDotNet.IcmpV6Packet packet_i:
                        AnalyzePacket_ICMPv6(packet_i, param);
                        break;

                    case PacketDotNet.TcpPacket packet_i:
                        AnalyzePacket_Tcp(packet_i, param);
                        break;

                    case PacketDotNet.UdpPacket packet_i:
                        AnalyzePacket_Udp(packet_i, param);
                        break;

                    default:
                        break;
                }

                /* ペイロードを解析 */
                param.CurrentPacket = param.CurrentPacket.PayloadPacket;
                Analyze(param);
            } catch {
            }
        }

        private static void AnalyzePacket_Ethernet(PacketDotNet.EthernetPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "Ethernet";
            param.SourceHwAddress = packet.SourceHardwareAddress;
            param.DestinationHwAddress = packet.DestinationHardwareAddress;
        }

        private static void AnalyzePacket_PPP(PacketDotNet.PppPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "PPP";
        }

        private static void AnalyzePacket_IEEE8021Q(PacketDotNet.Ieee8021QPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "IEEE802.1Q";
        }

        private static void AnalyzePacket_IP(PacketDotNet.IPPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "IP";
            param.SourceAddress = packet.SourceAddress;
            param.DestinationAddress = packet.DestinationAddress;
        }

        private static void AnalyzePacket_Arp(PacketDotNet.ArpPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "ARP";
        }

        private static void AnalyzePacket_ICMPv4(PacketDotNet.IcmpV4Packet packet, PacketAnalyzeParam param)
        {
            param.Protocol = "ICMPv4";
        }

        private static void AnalyzePacket_ICMPv6(PacketDotNet.IcmpV6Packet packet, PacketAnalyzeParam param)
        {
            param.Protocol = "ICMPv6";
        }

        private static void AnalyzePacket_Tcp(PacketDotNet.TcpPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "TCP";
            param.SourcePort = packet.SourcePort;
            param.DestinationPort = packet.DestinationPort;
            param.PayloadFrame = packet.PayloadData;
        }

        private static void AnalyzePacket_Udp(PacketDotNet.UdpPacket packet, PacketAnalyzeParam param)
        {
            param.Protocol = "UDP";
            param.SourcePort = packet.SourcePort;
            param.DestinationPort = packet.DestinationPort;
            param.PayloadFrame = packet.PayloadData;
        }
    }
}
