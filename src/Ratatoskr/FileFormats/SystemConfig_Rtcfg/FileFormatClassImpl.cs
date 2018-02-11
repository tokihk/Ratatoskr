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

        
        public override string   Name          { get; } = ConfigManager.Fixed.ApplicationName.Value + " Config";

        public override string[] FileExtension { get; } = new [] { "rtcfg" };

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

        public override FileFormatOption CreateWriterOption()
        {
            return (new SystemConfigOption());
        }
    }
}
