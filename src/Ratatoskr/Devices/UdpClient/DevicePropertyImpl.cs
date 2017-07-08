using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.UdpClient
{
    internal enum BindModeType
    {
        None,
        Bind,
        Multicast,
    }

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public EnumConfig<BindModeType> BindMode { get; } = new EnumConfig<BindModeType>(BindModeType.Bind);

        public StringConfig             LocalAddress { get; } = new StringConfig("localhost");
        public IntegerConfig            LocalPortNo  { get; } = new IntegerConfig(50001);

        public StringConfig             RemoteAddress { get; } = new StringConfig("localhost");
        public IntegerConfig            RemotePortNo  { get; } = new IntegerConfig(50002);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "Local {0:G}:{1:G}\nRemote {2:G}:{3:G}",
                LocalAddress.Value,
                LocalPortNo.Value,
                RemoteAddress.Value,
                RemotePortNo.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
