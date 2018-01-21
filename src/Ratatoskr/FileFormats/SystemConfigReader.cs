using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal class SystemConfigReader : FileFormatReader, ISystemConfigReader
    {
        public bool Load()
        {
            if (!IsOpen)return (false);

            return (OnLoad());
        }

        protected virtual bool OnLoad()
        {
            return (true);
        }
    }
}
