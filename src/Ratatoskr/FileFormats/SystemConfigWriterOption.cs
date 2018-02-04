using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.FileFormats
{
    internal sealed class SystemConfigWriterOption : FileFormatOption
    {
        public string TargetProfileID { get; set; } = "";


        public SystemConfigWriterOption()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
//            return (new FileFormatOptionEditorImpl(this));
        }
    }
}
