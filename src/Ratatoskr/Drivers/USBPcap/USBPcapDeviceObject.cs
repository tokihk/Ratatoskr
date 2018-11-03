using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.UsbMonitor
{
    internal sealed class USBPcapDeviceObject
    {
        private IntPtr handle_ = IntPtr.Zero;


        public USBPcapDeviceObject(string devname, string hid)
        {
            DeviceName = devname;
            HardwareID = hid;
        }

        public string DeviceName { get; }
        public string HardwareID { get; }


    }
}
