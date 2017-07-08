using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.TcpClient
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("9BBC32D2-AE0B-4BD7-AB73-4B6F5C15D0BE"))
        {
        }

        public override string Name
        {
            get { return ("TCPクライアント"); }
        }

        public override string Details
        {
            get { return ("TCPクライアント(IPv4/IPv6)を制御します"); }
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
