using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.MicInput
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig  InputDeviceId { get; } = new StringConfig("");
        public IntegerConfig SamplingRate  { get; } = new IntegerConfig(8000);
        public IntegerConfig BitsPerSample { get; } = new IntegerConfig(8);
        public IntegerConfig ChannelNum    { get; } = new IntegerConfig(1);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}:({1:G}Hz,{2:G}Bits,{3:G})",
                MicDeviceInfo.GetDeviceText(InputDeviceId.Value),
                SamplingRate.Value,
                BitsPerSample.Value,
                ChannelNum.Value.ToString()));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
