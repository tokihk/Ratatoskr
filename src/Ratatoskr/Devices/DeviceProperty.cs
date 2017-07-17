using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs;
using Ratatoskr.Gate;

namespace Ratatoskr.Devices
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
