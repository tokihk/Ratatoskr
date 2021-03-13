using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public class ModuleInfo : ModuleVersion
    {
        public ModuleInfo(string url, ushort major, ushort minor, ushort bugfix, string model, string comment)
            : base(major, minor, bugfix, model)
        {
            DownloadUrl = url;
            Comment = comment;
        }

        public string DownloadUrl { get; }
        public string Comment     { get; }
    }
}
