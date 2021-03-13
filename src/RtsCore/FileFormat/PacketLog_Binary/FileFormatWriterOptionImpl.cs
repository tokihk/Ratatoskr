using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.FileFormat.PacketLog_Binary
{
    public enum SaveDataType
    {
        RecvDataOnly,
        SendDataOnly,
        RecvAndSendData,
    }

    public sealed class FileFormatWriterOptionImpl : FileFormatOption
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
