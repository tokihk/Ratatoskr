using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal class SystemConfigWriter : FileFormatWriter, ISystemConfigWriter
    {
        public bool Save()
        {
            if (!IsOpen)return (false);

            return (OnSave());
        }

        protected virtual bool OnSave()
        {
            return (true);
        }
    }
}
