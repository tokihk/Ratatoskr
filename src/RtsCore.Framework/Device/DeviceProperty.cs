using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Generic;
using RtsCore.Config;

namespace RtsCore.Framework.Device
{
    [Serializable]
    public abstract class DeviceProperty : ConfigHolder
    {
        public abstract DeviceProperty Clone();

        public abstract DevicePropertyEditor GetPropertyEditor();

        public virtual string GetStatusString()
        {
            return ("");
        }
    }
}
