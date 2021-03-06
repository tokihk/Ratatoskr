﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat.PacketLog_Binary
{
	[Serializable]
    internal sealed class FileFormatReaderOptionImpl : FileFormatOption
    {
        public string PacketAlias    { get; set; } = "External";
        public uint   PacketDataSize { get; set; } = 1024;
        public uint   PacketInterval { get; set; } = 10;


        public FileFormatReaderOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (new FileFormatReaderOptionEditorImpl());
        }
    }
}
