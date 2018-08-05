using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Drivers.USBPcap
{
    internal class USBPcapDeviceInfo : USBPcapDeviceNode
    {
        public USBPcapDeviceInfo(string devname, string hid) : base(devname, hid)
        {
        }
    }
}
