using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.FileFormats
{
    internal class UserConfigReader : FileFormatReader
    {
        public (UserConfig config, string profile_id) Load()
        {
            if (!IsOpen)return (null, null);

            return (OnLoad());
        }

        protected virtual (UserConfig config, string profile_id) OnLoad()
        {
            return (null, null);
        }
    }
}
