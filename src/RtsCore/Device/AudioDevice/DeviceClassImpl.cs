using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Device.AudioDevice
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("BA643D49-F936-4ED2-A1CF-CA8E509AF8C9"))
        {
        }

        public override string Name
        {
            get { return ("Audio device"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("AudioDevice"); }
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
