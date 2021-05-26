using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace RtsPlugin.Pcap.Utility
{
    public class PcapPacketInfo
    {
        public PacketDirection Direction;
        public DateTime DateTime;
        public string Information;
        public string Source;
        public string Destination;
        public byte[] Data;
    }
}
