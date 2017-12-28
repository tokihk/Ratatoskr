using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.UsbDevice
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("7EDEB1DD-D74D-4DCB-8296-1323769FB114"))
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

        public override Type GetPropertyType()
        {
            return (typeof(DevicePropertyImpl));
        }

        public override DeviceProperty CreateProperty()
        {
            return (new DevicePropertyImpl());
        }

        protected override DeviceInstance OnCreateInstance(DeviceManager devm, DeviceConfig devconf, DeviceProperty devp)
        {
            return (new DeviceInstanceImpl(devm, devconf, this, devp));
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
