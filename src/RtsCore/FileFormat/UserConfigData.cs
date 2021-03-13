using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.FileFormat
{
    public class UserConfigData
    {
        public Guid                       ProfileID   { get; set; } = Guid.Empty;
        public UserConfig                 Config      { get; set; } = null;
        public Dictionary<string, byte[]> ExtDataList { get; } = new Dictionary<string, byte[]>();
    }
}
