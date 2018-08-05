using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.SerialPort
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("4859D896-854B-4CCC-A923-688ABDAFF7DE"))
        {
        }

        public override string Name
        {
            get { return ("Serial port"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("SerialPort"); }
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
