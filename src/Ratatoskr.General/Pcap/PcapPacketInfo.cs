using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General.Pcap
{
    [Flags]
    public enum PcapPacketInfoItemFlags
    {
        SourceHwAdress          = 1 << 0,
        DestinationHwAddress    = 1 << 1,
        SourceIpAddress         = 1 << 2,
        DestinationIpAddress    = 1 << 3,
        SourcePortNo            = 1 << 4,
        DestinationPortNo       = 1 << 5,
    }

    public class PcapPacketInfo
    {
        public DateTime                     DateTime;

        public bool                         IsSendPacket = false;

        public PcapPacketInfoItemFlags      ItemFlags = 0;

        public List<string>                 ProtocolNames { get; } = new List<string>();

        public PhysicalAddress              SourceHwAddress;
        public PhysicalAddress              DestinationHwAddress;

        public IPAddress                    SourceIpAddress;
        public IPAddress                    DestinationIpAddress;

        public ushort                       SourcePortNo;
        public ushort                       DestinationPortNo;

        public byte[]                       RawData;
        public byte[]                       TopProtocolDataUnit;
    }
}
