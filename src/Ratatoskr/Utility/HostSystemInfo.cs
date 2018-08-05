using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Ratatoskr.Utility
{
    internal static class HostSystemInfo
    {
        public enum DotNetFrameworkVersion
        {
            Version45,
            Version451,
            Version452,
            Version46,
            Version461,
            Version462,
            Version47,
            Version471,
            Version472,
        }

        public static DotNetFrameworkVersion GetDotNetFrameworkVersion()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            var version = DotNetFrameworkVersion.Version45;

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey)) {
                if (ndpKey != null && ndpKey.GetValue("Release") != null) {
                    var releaseKey = (int)ndpKey.GetValue("Release");

                    if (releaseKey >= 461808) {
                        version = DotNetFrameworkVersion.Version472;
                    }
                    if (releaseKey >= 461308) {
                        version = DotNetFrameworkVersion.Version471;
                    }
                    if (releaseKey >= 460798) {
                        version = DotNetFrameworkVersion.Version47;
                    }
                    if (releaseKey >= 394802) {
                        version = DotNetFrameworkVersion.Version462;
                    }
                    if (releaseKey >= 394254) {
                        version = DotNetFrameworkVersion.Version461;
                    }
                    if (releaseKey >= 393295) {
                        version = DotNetFrameworkVersion.Version46;
                    }
                    if (releaseKey >= 379893) {
                        version = DotNetFrameworkVersion.Version452;
                    }
                    if (releaseKey >= 378675) {
                        version = DotNetFrameworkVersion.Version451;
                    }
                    if (releaseKey >= 378389) {
                        version = DotNetFrameworkVersion.Version45;
                    }
                } 
            }

            return (version);
        }
    }
}
