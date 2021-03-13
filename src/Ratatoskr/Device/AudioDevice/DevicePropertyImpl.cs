using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.General;

namespace Ratatoskr.Device.AudioDevice
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public BoolConfig    InputEnable        { get; } = new BoolConfig(true);
        public StringConfig  InputDeviceId      { get; } = new StringConfig("");
        public IntegerConfig InputSamplingRate  { get; } = new IntegerConfig(8000);
        public IntegerConfig InputBitsPerSample { get; } = new IntegerConfig(8);
        public IntegerConfig InputChannelNum    { get; } = new IntegerConfig(1);

        public BoolConfig    OutputEnable        { get; } = new BoolConfig(true);
        public StringConfig  OutputDeviceId      { get; } = new StringConfig("");
        public IntegerConfig OutputSamplingRate  { get; } = new IntegerConfig(8000);
        public IntegerConfig OutputBitsPerSample { get; } = new IntegerConfig(8);
        public IntegerConfig OutputChannelNum    { get; } = new IntegerConfig(1);
        public IntegerConfig OutputVolume        { get; } = new IntegerConfig(100);

        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}:({1:G}Hz,{2:G}Bits,{3:G})",
                MicDeviceInfo.GetDeviceText(InputDeviceId.Value),
                InputSamplingRate.Value,
                InputBitsPerSample.Value,
                InputChannelNum.Value.ToString()));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
