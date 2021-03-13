using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
    internal class UserConfigWriter : FileFormatWriter
    {
        public UserConfigWriter(FileFormatClass fmtc) : base(fmtc)
        {
        }

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
