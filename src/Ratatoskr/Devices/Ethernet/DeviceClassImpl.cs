using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.Ethernet
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("A9E5B950-5BA2-41F4-A1D8-894DB6224E6E"))
        {
        }

        public override string Name
        {
            get { return ("イーサネット"); }
        }

        public override string Details
        {
            get { return ("イーサネットを制御します。"); }
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
