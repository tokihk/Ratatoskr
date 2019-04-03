using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.FileFormat;

namespace Ratatoskr.FileFormats.PacketLog_Binary
{
    internal enum SaveDataType
    {
        RecvDataOnly,
        SendDataOnly,
        RecvAndSendData,
    }


    internal sealed class FileFormatWriterOptionImpl : FileFormatOption
    {
        public SaveDataType SaveData { get; set; } = SaveDataType.RecvDataOnly;


        public FileFormatWriterOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (new FileFormatWriterOptionEditorImpl(this));
        }
    }
}
