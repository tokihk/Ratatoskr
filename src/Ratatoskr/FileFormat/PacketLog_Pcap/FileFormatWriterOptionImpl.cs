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
        public SourceInfoType      ViewSourceType      { get; set; } = SourceInfoType.IpAddress;
        public DestinationInfoType ViewDestinationType { get; set; } = DestinationInfoType.IpAddress;
        public DataContentsType    ViewDataType        { get; set; } = DataContentsType.Raw;


        public FileFormatWriterOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
        }
    }
}
