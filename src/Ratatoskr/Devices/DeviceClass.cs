using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices
{
    internal abstract class DeviceClass
    {
        private Guid id_ = Guid.Empty;


        public DeviceClass(Guid id)
        {
            id_ = id;
        }


        public Guid ID { get { return id_; } }

        public abstract string Name { get; }
        public abstract string Details { get; }

        public abstract Type GetPropertyType();
        public abstract DeviceProperty CreateProperty();

        public virtual bool CanUse()  { return (true); }
        public virtual bool CanSend() { return (true); }
        public virtual bool CanRecv() { return (true); }


        internal DeviceInstance CreateInstance(DeviceManager devm, Guid obj_id, string name, DeviceProperty devp)
        {
            if (devp.GetType() != GetPropertyType())return (null);

            return (OnCreateInstance(devm, obj_id, name, devp));
        }

        protected virtual DeviceInstance OnCreateInstance(DeviceManager devm, Guid obj_id, string name, DeviceProperty devp)
        {
            return (null);
        }
    }
}
