using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.Ethernet
{
    internal enum SourceInfoType
    {
        MacAddress,
        IpAddress,
        PortNo,
        IpAddressAndPortNo
    }

    internal enum DestinationInfoType
    {
        MacAddress,
        IpAddress,
        PortNo,
        IpAddressAndPortNo
    }

    internal enum DataContentsType
    {
        Raw,
        Payload,
    }

    internal sealed class WinPcapPacketParserOption
    {
        public SourceInfoType      SourceInfo      { get; set; }
        public DestinationInfoType DestinationInfo { get; set; }
        public DataContentsType    DataContents    { get; set; }

        public WinPcapPacketParserOption(SourceInfoType src_info, DestinationInfoType dst_info, DataContentsType data_type)
        {
            SourceInfo = src_info;
            DestinationInfo = dst_info;
            DataContents = data_type;
        }
    }
}
