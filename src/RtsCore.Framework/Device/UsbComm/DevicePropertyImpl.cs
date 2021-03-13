using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.Device;
using RtsCore.Generic;

namespace Ratatoskr.Devices.UsbComm
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public BoolConfig    DeviceEventCapture { get; } = new BoolConfig(false);
        public BoolConfig    DeviceComm         { get; } = new BoolConfig(false);
        public IntegerConfig CommVendorID       { get; } = new IntegerConfig(0x0000);
        public IntegerConfig CommProductID      { get; } = new IntegerConfig(0x0000);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "VID 0x{0,4:X4} - PID 0x{1,4:X4}",
                (uint)CommVendorID.Value,
                (uint)CommProductID.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
