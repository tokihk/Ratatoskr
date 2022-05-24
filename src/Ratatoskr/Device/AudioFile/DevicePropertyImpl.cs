using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.General;

namespace Ratatoskr.Device.AudioFile
{
	internal enum SamplingRateType
	{
		Auto,
		Preset_8kHz,
		Preset_44_1kHz,
		Preset_48kHz,
	}

	internal enum BitPerSampleType
	{
		Auto,
		Preset_8bit,
		Preset_16bit,
	}

	internal enum ChannelNumberType
	{
		Auto,
		Preset_Monoral,
		Preset_Stereo,
		Preset_5_1ch,
		Preset_6_1ch,
		Preset_7_1ch,
	}

    internal enum ConnectActionType
    {
        Restart,
        Continue,
    }

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig  InputFilePath { get; } = new StringConfig("");
        public EnumConfig<SamplingRateType>		InputSamplingRate  { get; } = new EnumConfig<SamplingRateType>(SamplingRateType.Auto);
        public EnumConfig<BitPerSampleType>		InputBitsPerSample { get; } = new EnumConfig<BitPerSampleType>(BitPerSampleType.Auto);
        public EnumConfig<ChannelNumberType>	InputChannelNum    { get; } = new EnumConfig<ChannelNumberType>(ChannelNumberType.Auto);

        public IntegerConfig                 RepeatCount   { get; } = new IntegerConfig(1);
        public EnumConfig<ConnectActionType> ConnectAction { get; } = new EnumConfig<ConnectActionType>(ConnectActionType.Restart);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}(Connect:{1:G},Repeat:{2:G})",
                InputFilePath.Value,
                ConnectAction.Value.ToString(),
                RepeatCount.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
