﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat.PacketLog_Csv
{
    internal sealed class FileFormatClassImpl : PacketLogFormatClass
    {
        public override string   Name          { get; } = "CSV";

        public override string[] FileExtension { get; } = new [] { ".csv" };

        public override Image    Icon          { get; } = Ratatoskr.Resource.Images.csv_48x48;

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
            return (new FileFormatReaderImpl(this));
        }

        public override FileFormatWriter CreateWriter()
        {
            return (new FileFormatWriterImpl(this));
        }
    }
}
