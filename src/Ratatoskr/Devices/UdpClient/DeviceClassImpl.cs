using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.UdpClient
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("1C3381C2-89B6-4047-95BD-561813A5F3A2"))
        {
        }

        public override string Name
        {
            get { return ("UDPクライアント"); }
        }

        public override string Details
        {
            get { return ("UDP通信(IPv4/IPv6)を制御します"); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(DevicePropertyImpl));
        }

        public override DeviceProperty CreateProperty()
        {
            return (new DevicePropertyImpl());
        }

        protected override DeviceInstance OnCreateInstance(DeviceManager devm, Guid obj_id, string name, DeviceProperty devp)
        {
            return (new DeviceInstanceImpl(devm, this, devp, obj_id, name));
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
