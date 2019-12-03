using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.Device;

namespace RtsPlugin.Pcap.Devices.EthernetCapture
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("A9E5B950-5BA2-41F4-A1D8-894DB6224E6E"))
        {
        }

        public override string Name
        {
            get { return ("Ethernet Capture"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("EthernetCapture"); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(DevicePropertyImpl));
        }

        public override DeviceProperty CreateProperty()
        {
            return (new DevicePropertyImpl());
        }

        protected override DeviceInstance OnCreateInstance(DeviceManagementClass devm, DeviceConfig devconf, DeviceProperty devp)
        {
            return (new DeviceInstanceImpl(devm, devconf, this, devp));
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
