using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs.UserConfigs;

namespace RtsCore.Framework.FileFormat
{
    internal class UserConfigReader : FileFormatReader
    {
        public UserConfigReader(FileFormatClass fmtc) : base(fmtc)
        {
        }

        public UserConfigData Load()
        {
            if (!IsOpen)return (null);

            return (OnLoad());
        }

        protected virtual UserConfigData OnLoad()
        {
            return (null);
        }
    }
}
