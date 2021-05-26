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
        public string              Filter                { get; set; } = "";
        public SourceInfoType      PacketSourceType      { get; set; } = SourceInfoType.IpAddress;
        public DestinationInfoType PacketDestinationType { get; set; } = DestinationInfoType.IpAddress;
        public DataContentsType    PacketDataType        { get; set; } = DataContentsType.Raw;


        public FileFormatReaderOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
        }

		public override string ToString()
		{
			var infos = new List<string>();

			if (Filter != "") {
				infos.Add(string.Format("Filter={0}", Filter));
			}
			infos.Add(string.Format("Src={0}", PacketSourceType.ToString()));
			infos.Add(string.Format("Dst={0}", PacketDestinationType.ToString()));
			infos.Add(string.Format("Data={0}", PacketDataType.ToString()));

			return (string.Join(",", infos));
		}
	}
}
