using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Binary
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override string   Name          => "Binary format";

        public override string[] FileExtension => new [] { "bin" };

        public override Image    Icon          => null;

        public override bool     CanRead       => false;
        public override bool     CanWrite      => true;


        public override FileFormatWriter CreateWriter()
        {
            return (new FileFormatWriterImpl());
        }

        public override FileFormatOption CreateWriterOption()
        {
            return (null);
        }
    }
}
