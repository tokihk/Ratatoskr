using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.FileFormats
{
    internal sealed class UserConfigWriterOption : FileFormatOption
    {
        public Guid TargetProfileID { get; set; } = Guid.Empty;


        public UserConfigWriterOption()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
//            return (new FileFormatOptionEditorImpl(this));
        }
    }
}
