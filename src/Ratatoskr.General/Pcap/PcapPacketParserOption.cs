using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General.Pcap
{
    public enum PacketInfoType
    {
        TopProtocolName,
        AllProtocolName,
    }

    public enum SourceInfoType
    {
        MacAddress,
        IpAddress,
        IpAddressAndPortNo
    }

    public enum DestinationInfoType
    {
        MacAddress,
        IpAddress,
        IpAddressAndPortNo
    }

    public enum DataContentsType
    {
        Raw,
        TopProtocolDataUnit,
    }

    public class PcapPacketParserOption
    {
        public PcapLinkType             LinkType            = PcapLinkType.LINKTYPE_NULL;

        public PacketInfoType           InfoType            = PacketInfoType.TopProtocolName;

        public SourceInfoType           SourceType          = SourceInfoType.MacAddress;
        public DestinationInfoType      DestinationType     = DestinationInfoType.MacAddress;

        public DataContentsType         DataContentsType    = DataContentsType.Raw;
    }
}
