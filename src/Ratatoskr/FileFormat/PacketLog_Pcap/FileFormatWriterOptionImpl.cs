using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormat;
using Ratatoskr.General.Pcap;

namespace Ratatoskr.FileFormat.PacketLog_Pcap
{
	[Serializable]
    internal sealed class FileFormatWriterOptionImpl : FileFormatOption
    {
        public string              Filter              { get; set; } = "";
        public PcapPacketSourceType      ViewSourceType      { get; set; } = PcapPacketSourceType.IpAddress;
        public PcapPacketDestinationType ViewDestinationType { get; set; } = PcapPacketDestinationType.IpAddress;
        public PcapPacketDataType    ViewDataType        { get; set; } = PcapPacketDataType.Raw;


        public FileFormatWriterOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
        }
    }
}
