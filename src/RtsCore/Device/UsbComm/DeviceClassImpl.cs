using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Device.UsbComm
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("E61010AE-4E24-4DF4-ADB6-A0BFC95756B2"))
        {
        }

        public override string Name
        {
            get { return ("USB Device"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("UsbDevice"); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(DevicePropertyImpl));
        }

        public override DeviceProperty CreateProperty()
        {
            return (new DevicePropertyImpl());
        }

        protected override DeviceInstance OnCreateInstance(DeviceConfig devconf, DeviceProperty devp)
        {
            return (new DeviceInstanceImpl(devconf, this, devp));
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
