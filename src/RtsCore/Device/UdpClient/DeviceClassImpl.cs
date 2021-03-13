using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Device.UdpClient
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("1C3381C2-89B6-4047-95BD-561813A5F3A2"))
        {
        }

        public override string Name
        {
            get { return ("UDP Client(IPv4/IPv6)"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("UdpClient"); }
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
