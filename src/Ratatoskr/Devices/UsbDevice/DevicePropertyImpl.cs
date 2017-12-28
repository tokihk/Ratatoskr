using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.UsbDevice
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public IntegerConfig VendorID  { get; } = new IntegerConfig(0x0000);
        public IntegerConfig ProductID { get; } = new IntegerConfig(0x0000);



        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "VID 0x{0:X4} - PID 0x{1:X4}",
                VendorID.Value,
                ProductID.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
