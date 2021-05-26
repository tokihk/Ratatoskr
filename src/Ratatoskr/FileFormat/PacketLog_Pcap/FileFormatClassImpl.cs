using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormat;

namespace Ratatoskr.FileFormat.PacketLog_Pcap
{
    internal sealed class FileFormatClassImpl : PacketLogFormatClass
    {
        public override string   Name          { get; } = "Wireshark/tcpdump";

        public override string[] FileExtension { get; } = new [] { ".pcap", ".cap" };

//        public override Image    Icon          { get; } = Properties.Resource.wireshark_32x32;

        public override bool     CanRead       { get; } = true;
        public override bool     CanWrite      { get; } = true;


        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl(this));
        }

        public override FileFormatOption CreateReaderOption()
        {
            return (new FileFormatReaderOptionImpl());
        }

		public override FileFormatWriter CreateWriter()
		{
			return (new FileFormatWriterImpl(this));
		}

		public override FileFormatOption CreateWriterOption()
		{
			return (new FileFormatWriterOptionImpl());
		}
	}
}
