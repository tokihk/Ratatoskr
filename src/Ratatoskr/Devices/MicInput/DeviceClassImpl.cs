using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.MicInput
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("BA643D49-F936-4ED2-A1CF-CA8E509AF8C9"))
        {
        }

        public override string Name
        {
            get { return ("マイク入力"); }
        }

        public override string Details
        {
            get { return ("マイク端子からA/D値を取得します。"); }
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
