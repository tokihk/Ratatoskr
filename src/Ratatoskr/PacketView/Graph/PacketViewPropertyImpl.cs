using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.General;

namespace Ratatoskr.PacketView.Graph
{
    internal enum DisplayModeType
    {
        OscilloScope,
        SpectrumAnalyzer,
    }

    internal enum GraphTargetType
    {
        DataValue,
        DataValueSum,
        DataBlockCount,
    }

	internal enum SamplingSettingTemplateType
	{
		PCM_8kHz_8bit_1ch,
		PCM_8kHz_8bit_2ch,
		PCM_8kHz_16bit_1ch,
		PCM_8kHz_16bit_2ch,
		PCM_44_1kHz_8bit_1ch,
		PCM_44_1kHz_8bit_2ch,
		PCM_44_1kHz_16bit_1ch,
		PCM_44_1kHz_16bit_2ch,
		PCM_48kHz_8bit_1ch,
		PCM_48kHz_8bit_2ch,
		PCM_48kHz_16bit_1ch,
		PCM_48kHz_16bit_2ch,
	}

	internal enum SamplingTriggerType
	{
		DataBlockDetect,
		TimeInterval,
	}

	internal enum SamplingIntervalUnitType
	{
		Hz,
		kHz,
		sec,
		msec,
		usec,
	}

    internal class PacketViewPropertyImpl : PacketViewProperty
    {
        public EnumConfig<DisplayModeType>			DisplayMode				{ get; } = new EnumConfig<DisplayModeType>(DisplayModeType.OscilloScope);

        public EnumConfig<GraphTargetType>			GraphTarget				{ get; } = new EnumConfig<GraphTargetType>(GraphTargetType.DataValue);

        public EnumConfig<SamplingTriggerType>		SamplingTrigger			{ get; } = new EnumConfig<SamplingTriggerType>(SamplingTriggerType.DataBlockDetect);
        public IntegerConfig						SamplingInterval		{ get; } = new IntegerConfig(1);
        public EnumConfig<SamplingIntervalUnitType>	SamplingIntervalUnit	{ get; } = new EnumConfig<SamplingIntervalUnitType>(SamplingIntervalUnitType.sec);
		public IntegerConfig						InputDataBlockSize		{ get; } = new IntegerConfig(1);
		public IntegerConfig						InputDataChannelNum		{ get; } = new IntegerConfig(1);


        public IntegerConfig		Oscillo_RecordPoint		{ get; } = new IntegerConfig(100000);
        public IntegerConfig		Oscillo_DisplayPoint	{ get; } = new IntegerConfig(1000);

        public GraphChannelListConfig	ChannelList		{ get; } = new GraphChannelListConfig();


        public PacketViewPropertyImpl()
        {
        }

        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
