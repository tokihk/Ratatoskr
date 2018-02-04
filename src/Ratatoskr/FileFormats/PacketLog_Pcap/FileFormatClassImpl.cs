using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Pcap
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override string   Name          { get; } = "pcap format";

        public override string[] FileExtension { get; } = new [] { "pcap" };

        public override Image    Icon          { get; } = Properties.Resources.wireshark_32x32;

        public override bool     CanRead       { get; } = true;
        public override bool     CanWrite      { get; } = false;


        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl());
        }
    }
}
