using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat.PacketLog_Binary
{
	internal enum PacketAliasModeType
	{
		FileName,
		ParentDirectoryName,
		Custom,
	}

	[Serializable]
    internal sealed class FileFormatReaderOptionImpl : FileFormatOption
    {
		public PacketAliasModeType	PacketAliasMode   { get; set; } = PacketAliasModeType.FileName;
        public string				PacketCustomAlias { get; set; } = "File";

		public DateTime				PacketBaseTime		{ get; set; } = DateTime.Now;
        public uint					PacketIntervalUsec	{ get; set; } = 10;

        public uint					DataMaxSize			 { get; set; } = 1024;
		public string				DataTerminatePattern { get; set; } = "";

        public override FileFormatOptionEditor GetEditor()
        {
            return (new FileFormatReaderOptionEditorImpl());
        }
    }
}
