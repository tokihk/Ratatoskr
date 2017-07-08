using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Pcap
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override bool CanRead  { get { return (true);  } }
        public override bool CanWrite { get { return (false); } }


        public override string Name
        {
            get { return ("pcapフォーマット"); }
        }

        public override string Detail
        {
            get { return ("pcapフォーマット"); }
        }

        public override string[] FileExtension
        {
            get { return (new [] { "pcap" }); }
        }

        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl());
        }
    }
}
