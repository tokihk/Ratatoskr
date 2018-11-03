using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.Device;
using RtsCore.Framework.PacketView;

namespace RtsCore.Framework.Plugin
{
    public abstract class PluginClass
    {
        public Guid ID { get; }

        public abstract string Name    { get; }
        public abstract string Details { get; }


        public PluginClass(Guid id)
        {
            ID = id;
        }

        public DeviceClass[] LoadDeviceClasses()
        {
            return (OnLoadDeviceClasses());
        }

        public PacketViewClass[] LoadPacketViewClasses()
        {
            return (OnLoadPacketViewClasses());
        }

        protected virtual DeviceClass[] OnLoadDeviceClasses()
        {
            return (null);
        }

        protected PacketViewClass[] OnLoadPacketViewClasses()
        {
            return (OnLoadPacketViewClasses());
        }
    }
}
