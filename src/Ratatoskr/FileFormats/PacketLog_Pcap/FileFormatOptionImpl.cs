using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Drivers.WinPcap;

namespace Ratatoskr.FileFormats.PacketLog_Pcap
{
    internal sealed class FileFormatOptionImpl : FileFormatOption
    {
        public string              Filter              { get; set; } = "";
        public SourceInfoType      ViewSourceType      { get; set; } = SourceInfoType.IpAddress;
        public DestinationInfoType ViewDestinationType { get; set; } = DestinationInfoType.IpAddress;
        public DataContentsType    ViewDataType        { get; set; } = DataContentsType.Raw;


        public FileFormatOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
        }
    }
}
