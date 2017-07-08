using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Binary
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override bool CanRead  { get { return (false); } }
        public override bool CanWrite { get { return (true); } }


        public override string Name
        {
            get { return ("バイナリデータ"); }
        }

        public override string Detail
        {
            get { return ("パケットのデータ部分のみを出力します。"); }
        }

        public override string[] FileExtension
        {
            get { return (new [] { "bin" }); }
        }

        public override FileFormatWriter CreateWriter()
        {
            return (new FileFormatWriterImpl());
        }

        public override FileFormatOption CreateWriterOption()
        {
            return (new FileFormatWriterOptionImpl());
        }
    }
}
