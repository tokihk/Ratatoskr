using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Ncom
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public override string   Name          { get; } = "NCOM log format";

        public override string[] FileExtension { get; } = new [] { "log" };

        public override Image    Icon          { get; } = Properties.Resources.ncom_32x32;

        public override bool     CanRead       { get; } = true;
        public override bool     CanWrite      { get; } = false;


        public override FileFormatReader CreateReader()
        {
            return (new FileFormatReaderImpl(this));
        }
    }
}
