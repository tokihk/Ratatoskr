using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.FileFormat;

namespace Ratatoskr.FileFormats.PacketLog_Binary
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override string   Name          { get; } = "Binary format";

        public override string[] FileExtension { get; } = new [] { ".bin" };

        public override Image    Icon          { get; } = null;

        public override bool     CanRead       { get; } = true;
        public override bool     CanWrite      { get; } = true;


        public override FileFormatOption CreateReaderOption()
        {
            return (new FileFormatReaderOptionImpl());
        }

        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl(this));
        }

        public override FileFormatWriter CreateWriter()
        {
            return (new FileFormatWriterImpl(this));
        }

        public override FileFormatOption CreateWriterOption()
        {
            return (null);
        }
    }
}
