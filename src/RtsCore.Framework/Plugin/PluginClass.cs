using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public abstract string Name      { get; }
        public abstract string Details   { get; }
        public abstract string Copyright { get; }

        public virtual ModuleVersion Version { get; } = new ModuleVersion(0, 0, 0, "");


        public PluginClass(Guid id)
        {
            ID = id;
        }

        public DeviceClass[] LoadDeviceClasses()
        {
            return (OnLoadDeviceClasses());
        }

        protected virtual DeviceClass[] OnLoadDeviceClasses()
        {
            return (null);
        }

        public PacketViewClass[] LoadPacketViewClasses()
        {
            return (OnLoadPacketViewClasses());
        }

        protected virtual PacketViewClass[] OnLoadPacketViewClasses()
        {
            return (null);
        }

        public FileFormatClass[] LoadPacketLogFormatClasses()
        {
            return (OnLoadPacketLogFormatClasses());
        }

        protected virtual FileFormatClass[] OnLoadPacketLogFormatClasses()
        {
            return (null);
        }
        public ProtocolEncoderClass[] LoadProtocolEncoderClasses()
        {
            return (OnLoadProtocolEncoderClasses());
        }

        protected virtual ProtocolEncoderClass[] OnLoadProtocolEncoderClasses()
        {
            return (null);
        }

        public ProtocolDecoderClass[] LoadProtocolDecoderClasses()
        {
            return (OnLoadProtocolDecoderClasses());
        }

        protected virtual ProtocolDecoderClass[] OnLoadProtocolDecoderClasses()
        {
            return (null);
        }
    }
}
