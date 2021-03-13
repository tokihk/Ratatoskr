using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Data.User;

namespace Ratatoskr.FileFormat
{
    internal class UserConfigData
    {
        public Guid                       ProfileID   { get; set; } = Guid.Empty;
        public UserConfig                 Config      { get; set; } = null;
        public Dictionary<string, byte[]> ExtDataList { get; } = new Dictionary<string, byte[]>();
    }
}
