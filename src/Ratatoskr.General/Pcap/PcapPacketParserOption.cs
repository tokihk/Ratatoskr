using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General.Pcap
{
    public enum PcapPacketInfoType
    {
        TopProtocolName,
        AllProtocolName,
    }

    public enum PcapPacketSourceType
    {
        MacAddress,
        IpAddress,
        IpAddressAndPortNo
    }

    public enum PcapPacketDestinationType
    {
        MacAddress,
        IpAddress,
        IpAddressAndPortNo
    }

    public enum PcapPacketDataType
    {
        Raw,
        TopProtocolDataUnit,
    }

    public class PcapPacketParserOption
    {
        public PcapLinkType                 LinkType            = PcapLinkType.LINKTYPE_NULL;

        public PcapPacketInfoType           InfoType            = PcapPacketInfoType.TopProtocolName;

        public PcapPacketSourceType         SourceType          = PcapPacketSourceType.MacAddress;
        public PcapPacketDestinationType    DestinationType     = PcapPacketDestinationType.MacAddress;

        public PcapPacketDataType           DataType            = PcapPacketDataType.Raw;
    }
}
