using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;

namespace Ratatoskr.Devices.UsbMonitor
{
    internal sealed class UsbPcapDeviceObject
    {
        public sealed class DeviceTreeNode
        {
            public DeviceTreeNode(string devname, string text)
            {
                DeviceName = devname;
                ExplainText = text;
            }

            public string DeviceName  { get; }
            public string ExplainText { get; }

            public IEnumerable<DeviceTreeNode> GetDeviceTree()
            {
                return (null);
            }
        }


        public UsbPcapDeviceObject(string devname, string hid)
        {
            DeviceName = devname;
            HardwareID = hid;
        }

        public string DeviceName { get; }
        public string HardwareID { get; }

        public IEnumerable<DeviceTreeNode> GetDeviceTree()
        {
            return (null);
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            var obj_c = obj as string;

            if (obj_c != null) {
                return (obj_c == DeviceName);
            }

            return (base.Equals(obj));
        }

        public override string ToString()
        {
            return (HardwareID);
        }
    }
}
