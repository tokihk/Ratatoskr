using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    public abstract class FileFormatOption
    {
        public abstract FileFormatOptionEditor GetEditor();
    }
}
