using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.TcpServer
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("70482E0B-C09D-4D43-8A80-A6C06EA6B250"))
        {
        }

        public override string Name
        {
            get { return ("TCPサーバー"); }
        }

        public override string Details
        {
            get { return ("TCPサーバー(IPv4/IPv6)を制御します"); }
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
