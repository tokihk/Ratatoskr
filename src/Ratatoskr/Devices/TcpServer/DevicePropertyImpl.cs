using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.Device;
using RtsCore.Generic;

namespace Ratatoskr.Devices.TcpServer
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public IntegerConfig            Capacity     { get; } = new IntegerConfig(10);

        public IntegerConfig            LocalPortNo  { get; } = new IntegerConfig(50000);


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
                "{0:G}",
                LocalPortNo.Value));
        }
    }
}
