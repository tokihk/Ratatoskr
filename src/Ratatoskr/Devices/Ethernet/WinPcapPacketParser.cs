using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;
using SharpPcap;

namespace Ratatoskr.Devices.Ethernet
{
    internal static class WinPcapPacketParser
    {
        public sealed class PacketInfo
        {
            public DateTime DateTime    { get; set; }
            public string   Source      { get; set; }
            public string   Destination { get; set; }
            public byte[]   Data        { get; set; }

            internal PacketInfo(RawCapture packet)
            {
                DateTime = packet.Timeval.Date;
                Source = "";
                Destination = "";
                Data = packet.Data;
            }
        }


        public static PacketObject Convert(RawCapture packet, WinPcapPacketParserOption option)
        {
            var info = Parse(packet, option);

            return (new StaticDataPacketObject(
                            PacketFacility.Device,
                            "",
                            PacketPriority.Standard,
                            info.DateTime,
                            "",
                            PacketDirection.Recv,
                            info.Source,
                            info.Destination,
                            0x00,
                            info.Data));
        }

        public static PacketInfo Parse(RawCapture packet, WinPcapPacketParserOption option)
        {
            var packet_info = new PacketInfo(packet);

            /* パケット解析 */
            Analyze(PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data), option, packet_info);

            return (packet_info);
        }

        private static void Analyze(PacketDotNet.Packet packet, WinPcapPacketParserOption option, PacketInfo packet_info)
        {
            if (packet == null)return;

            var type = packet.GetType();

            if (type == typeof(PacketDotNet.EthernetPacket)) {
                AnalyzePacket_Ethernet((PacketDotNet.EthernetPacket)packet, option, packet_info);

            } else if (type == typeof(PacketDotNet.IPv4Packet)) {
                AnalyzePacket_IPv4((PacketDotNet.IPv4Packet)packet, option, packet_info);

            } else if (type == typeof(PacketDotNet.IPv6Packet)) {
                AnalyzePacket_IPv6((PacketDotNet.IPv6Packet)packet, option, packet_info);

            } else if (type == typeof(PacketDotNet.TcpPacket)) {
                AnalyzePacket_Tcp((PacketDotNet.TcpPacket)packet, option, packet_info);

            } else if (type == typeof(PacketDotNet.UdpPacket)) {
                AnalyzePacket_Udp((PacketDotNet.UdpPacket)packet, option, packet_info);
            }

            /* ペイロードを解析 */
            Analyze(packet.PayloadPacket, option, packet_info);
        }

        private static void AnalyzePacket_Ethernet(PacketDotNet.EthernetPacket packet, WinPcapPacketParserOption option, PacketInfo packet_info)
        {
            /* Source */
            if (option.SourceInfo == SourceInfoType.MacAddress) {
                packet_info.Source = packet.SourceHwAddress.ToString();
            }

            /* Destination */
            if (option.DestinationInfo == DestinationInfoType.MacAddress) {
                packet_info.Destination = packet.DestinationHwAddress.ToString();
            }
        }

        private static void AnalyzePacket_IPv4(PacketDotNet.IPv4Packet packet, WinPcapPacketParserOption option, PacketInfo packet_info)
        {
            /* Source */
            if (   (option.SourceInfo == SourceInfoType.IpAddress)
                || (option.SourceInfo == SourceInfoType.IpAddressAndPortNo)
            ) {
                packet_info.Source = packet.SourceAddress.ToString();
            }

            /* Destination */
            if (   (option.DestinationInfo == DestinationInfoType.IpAddress)
                || (option.DestinationInfo == DestinationInfoType.IpAddressAndPortNo)
            ) {
                packet_info.Destination = packet.DestinationAddress.ToString();
            }
        }

        private static void AnalyzePacket_IPv6(PacketDotNet.IPv6Packet packet, WinPcapPacketParserOption option, PacketInfo packet_info)
        {
            /* Source */
            if (   (option.SourceInfo == SourceInfoType.IpAddress)
                || (option.SourceInfo == SourceInfoType.IpAddressAndPortNo)
            ) {
                packet_info.Source = packet.SourceAddress.ToString();
            }

            /* Destination */
            if (   (option.DestinationInfo == DestinationInfoType.IpAddress)
                || (option.DestinationInfo == DestinationInfoType.IpAddressAndPortNo)
            ) {
                packet_info.Destination = packet.DestinationAddress.ToString();
            }
        }

        private static void AnalyzePacket_Tcp(PacketDotNet.TcpPacket packet, WinPcapPacketParserOption option, PacketInfo packet_info)
        {
            /* Source */
            if (   (option.SourceInfo == SourceInfoType.PortNo)
                || (option.SourceInfo == SourceInfoType.IpAddressAndPortNo)
            ) {
                if (packet_info.Source.Length > 0) {
                    packet_info.Source += ':';
                }
                packet_info.Source += packet.SourcePort.ToString();
            }

            /* Destination */
            if (   (option.DestinationInfo == DestinationInfoType.PortNo)
                || (option.DestinationInfo == DestinationInfoType.IpAddressAndPortNo)
            ) {
                if (packet_info.Destination.Length > 0) {
                    packet_info.Destination += ':';
                }
                packet_info.Destination += packet.DestinationPort.ToString();
            }

            /* Data */
            if (option.DataContents == DataContentsType.Payload) {
                packet_info.Data = packet.PayloadData;
            }
        }

        private static void AnalyzePacket_Udp(PacketDotNet.UdpPacket packet, WinPcapPacketParserOption option, PacketInfo packet_info)
        {
            /* Source */
            if (   (option.SourceInfo == SourceInfoType.PortNo)
                || (option.SourceInfo == SourceInfoType.IpAddressAndPortNo)
            ) {
                if (packet_info.Source.Length > 0) {
                    packet_info.Source += ':';
                }
                packet_info.Source += packet.SourcePort.ToString();
            }

            /* Destination */
            if (   (option.DestinationInfo == DestinationInfoType.PortNo)
                || (option.DestinationInfo == DestinationInfoType.IpAddressAndPortNo)
            ) {
                if (packet_info.Destination.Length > 0) {
                    packet_info.Destination += ':';
                }
                packet_info.Destination += packet.DestinationPort.ToString();
            }

            /* Data */
            if (option.DataContents == DataContentsType.Payload) {
                packet_info.Data = packet.PayloadData;
            }
        }
        
    }
}
