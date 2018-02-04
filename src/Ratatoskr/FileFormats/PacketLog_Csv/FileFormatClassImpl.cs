using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Csv
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override string   Name          { get; } = "CSV format";

        public override string[] FileExtension { get; } = new [] { "csv" };

        public override Image    Icon          { get; } = Properties.Resources.csv_48x48;

        public override bool     CanRead       { get; } = true;
        public override bool     CanWrite      { get; } = true;


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
