using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Pcap;

namespace Ratatoskr.FileFormat.PacketLog_Pcap
{
	[Serializable]
    internal sealed class FileFormatReaderOptionImpl : FileFormatOption
    {
        public string					 PcapFilter            { get; set; } = "";

		public PcapPacketInfoType		 PacketInfoType		   { get; set; } = PcapPacketInfoType.TopProtocolName;
        public PcapPacketSourceType      PacketSourceType      { get; set; } = PcapPacketSourceType.IpAddress;
        public PcapPacketDestinationType PacketDestinationType { get; set; } = PcapPacketDestinationType.IpAddress;
        public PcapPacketDataType		 PacketDataType        { get; set; } = PcapPacketDataType.Raw;


        public FileFormatReaderOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (new FileFormatReaderOptionEditorImpl());
        }

		public override string ToString()
		{
			var infos = new List<string>();

			infos.Add(string.Format("Src={0}", PacketSourceType.ToString()));
			infos.Add(string.Format("Dst={0}", PacketDestinationType.ToString()));
			infos.Add(string.Format("Data={0}", PacketDataType.ToString()));

			if (PcapFilter != "") {
				infos.Add(string.Format("Filter={0}", PcapFilter));
			}

			return (string.Join(",", infos));
		}
	}
}
