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
        public override string   Name          => "pcap format";

        public override string[] FileExtension => new [] { "pcap" };

        public override Image    Icon          => Properties.Resources.wireshark_32x32;

        public override bool     CanRead       => true;
        public override bool     CanWrite      => false;


        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl());
        }
    }
}
