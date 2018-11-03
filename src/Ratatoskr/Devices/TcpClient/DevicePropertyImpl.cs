using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.Device;
using RtsCore.Generic;

namespace Ratatoskr.Devices.TcpClient
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public IntegerConfig            LocalPortNo   { get; } = new IntegerConfig(0);

        public StringConfig             RemoteAddress { get; } = new StringConfig("localhost");
        public IntegerConfig            RemotePortNo  { get; } = new IntegerConfig(50000);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}:{1:G}",
                RemoteAddress.Value,
                RemotePortNo.Value));
        }
    }
}
