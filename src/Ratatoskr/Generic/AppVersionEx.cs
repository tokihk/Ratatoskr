using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic
{
    internal class AppVersionEx : AppVersion
    {
        public AppVersionEx(string url, ushort major, ushort minor, ushort bugfix, string model, string comment)
            : base(major, minor, bugfix, model)
        {
            DownloadUrl = url;
            Comment = comment;
        }

        public string DownloadUrl { get; }
        public string Comment     { get; }
    }
}
