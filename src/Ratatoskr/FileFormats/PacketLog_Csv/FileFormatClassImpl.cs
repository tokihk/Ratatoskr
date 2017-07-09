using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Csv
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override bool CanRead  { get { return (true); } }
        public override bool CanWrite { get { return (true); } }


        public override string Name
        {
            get { return ("CSV format"); }
        }

        public override string Detail
        {
            get { return ("CSV format"); }
        }

        public override string[] FileExtension
        {
            get { return (new [] { "csv" }); }
        }

        public override FileFormatOption CreateReaderOption()
        {
            return (new FileFormatOptionImpl());
        }

        public override FileFormatOption CreateWriterOption()
        {
            return (new FileFormatOptionImpl());
        }

        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl());
        }

        public override FileFormatWriter CreateWriter()
        {
            return (new FileFormatWriterImpl());
        }
    }
}
