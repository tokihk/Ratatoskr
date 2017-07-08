using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.UsbMonitor
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig DeviceName                 { get; } = new StringConfig("");
        public BoolConfig   Filter_ControlTransfer     { get; } = new BoolConfig(true);
        public BoolConfig   Filter_BulkTransfer        { get; } = new BoolConfig(true);
        public BoolConfig   Filter_InterruptTransfer   { get; } = new BoolConfig(true);
        public BoolConfig   Filter_IsochronousTransfer { get; } = new BoolConfig(true);

        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format("{0:G}", DeviceName.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
