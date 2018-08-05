using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.UsbMonitor
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("F04B54F3-0D02-4C79-9B35-019ECC97E9C2"))
        {
        }

        public override string Name
        {
            get { return ("USB Monitor"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("UsbMonitor"); }
        }

        public override bool AdminOnly => true;

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
