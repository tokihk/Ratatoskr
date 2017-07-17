using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices
{
    [Serializable]
    internal class DeviceConfig
    {
        public bool SendEnable     { get; set; } = true;
        public bool RecvEnable     { get; set; } = true;
        public bool RedirectEnable { get; set; } = true;

        public uint SendDataQueueLimit     { get; set; } = 1;
        public uint RedirectDataQueueLimit { get; set; } = 1000;
    }
}
