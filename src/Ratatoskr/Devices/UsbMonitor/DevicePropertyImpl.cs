using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.Device;
using RtsCore.Generic;

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

        public BoolConfig   InfoOut_UsbDeviceID   { get; } = new BoolConfig(true);
        public BoolConfig   InfoOut_EndPoint      { get; } = new BoolConfig(true);
        public BoolConfig   InfoOut_IrpID         { get; } = new BoolConfig(false);
        public BoolConfig   InfoOut_FunctionType  { get; } = new BoolConfig(true);
        public BoolConfig   InfoOut_FunctionParam { get; } = new BoolConfig(true);

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
