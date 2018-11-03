using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Utility
{
    public class LicenseInfo
    {
        public LicenseInfo(string name, string homepage, string license_name, string license_body)
        {
            Name = name;
            Homepage = homepage;
            LicenseName = license_name;
            LicenseBody = license_body;
        }

        public string Name        { get; } = "";
        public string Homepage    { get; } = "";
        public string LicenseName { get; } = "";
        public string LicenseBody { get; } = "";
    }
}
