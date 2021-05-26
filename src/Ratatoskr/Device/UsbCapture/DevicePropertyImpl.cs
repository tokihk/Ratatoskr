using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.Device;
using Ratatoskr.General;

namespace Ratatoskr.Device.UsbCapture
{
	public enum UsbCaptureDataContentsType
	{
		Raw,
		Payload,
	}


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

		public EnumConfig<UsbCaptureDataContentsType> DataContentsType { get; } = new EnumConfig<UsbCaptureDataContentsType>(UsbCaptureDataContentsType.Payload);


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
