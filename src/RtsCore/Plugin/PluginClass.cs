using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;
using RtsCore.Framework.Device;
using RtsCore.Framework.FileFormat;
using RtsCore.Framework.PacketView;
using RtsCore.Protocol;
using RtsCore.Utility;

namespace RtsCore.Framework.Plugin
{
    public abstract class PluginClass
    {
        public Guid ID { get; }

        public abstract string Name                 { get; }
        public abstract string Details              { get; }
        public abstract string Copyright            { get; }

        public virtual ModuleVersion Version { get; } = new ModuleVersion(0, 0, 0, "");

        public virtual LicenseInfo[] ThirdPartyLicenses { get; } = null;


        public PluginClass(Guid id)
        {
            ID = id;
        }

        public PluginInstance CreateInstance()
        {
            return (OnCreateInstance());
        }

        protected abstract PluginInstance OnCreateInstance();

        public PluginProperty CreateProperty()
        {
            return (OnCreateProperty());
        }

        protected abstract PluginProperty OnCreateProperty();
    }
}
