using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;

namespace Ratatoskr.FileFormats.SystemConfig_Rtcfg
{
    internal sealed class FileFormatClassImpl : FileFormatClass
    {
        public static readonly byte[] FORMATCODE = new byte[]{ 0x60, 0xCB, 0x7F, 0xF0, 0x68, 0x9C, 0x44, 0xD9 };

        
        public override string   Name          => ConfigManager.Fixed.ApplicationName.Value + " config file";

        public override string[] FileExtension => new [] { "gdcfg" };

        public override Image    Icon          => Properties.Resources.app_icon_32x32;

        public override bool     CanRead       => true;
        public override bool     CanWrite      => true;


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
