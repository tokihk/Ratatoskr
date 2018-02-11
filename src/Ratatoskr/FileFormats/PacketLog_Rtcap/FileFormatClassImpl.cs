using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;

namespace Ratatoskr.FileFormats.PacketLog_Rtcap
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public static readonly byte[] FORMATCODE = new byte[]{ 0x65, 0x0C, 0xF9, 0x30, 0xCE, 0x4B, 0x43, 0x8B };


        public override string   Name          { get; } = ConfigManager.Fixed.ApplicationName.Value + " format";

        public override string[] FileExtension { get; } = new [] { "rtcap" };

        public override Image    Icon          { get; } = Properties.Resources.app_icon_32x32;

        public override bool     CanRead       { get; } = true;
        public override bool     CanWrite      { get; } = true;
        

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
