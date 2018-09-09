﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;
using Ratatoskr.PacketViews.Graph.Configs;

namespace Ratatoskr.PacketViews.Graph
{
    internal enum DisplayModeType
    {
        Oscillo,
//        Spectrum,
    }

    internal enum DataFormatType
    {
        UnsignedByte,
        UnsignedWord,
        UnsignedDword,
        UnsignedQword,
        SignedByte,
        SignedWord,
        SignedDword,
        SignedQword,
        IEEE754_Float,
        IEEE754_Double,
    }

    internal enum DataEndianType
    {
        BigEndian,
        LittleEndian
    }

    internal enum DataCollectModeType
    {
        Value,
        ValueSum,
        Count,
    }

    internal class ViewPropertyImpl : ViewProperty
    {
        public EnumConfig<DisplayModeType>     DisplayMode          { get; } = new EnumConfig<DisplayModeType>(DisplayModeType.Oscillo);

        public EnumConfig<DataFormatType>      DataFormat           { get; } = new EnumConfig<DataFormatType>(DataFormatType.SignedWord);
        public EnumConfig<DataEndianType>      DataEndian           { get; } = new EnumConfig<DataEndianType>(DataEndianType.LittleEndian);
        public IntegerConfig                   DataChannelNum       { get; } = new IntegerConfig(2);
        public EnumConfig<DataCollectModeType> DataCollectMode      { get; } = new EnumConfig<DataCollectModeType>(DataCollectModeType.Value);

        public IntegerConfig SamplingPoint    { get; } = new IntegerConfig(1000000);
        public IntegerConfig SamplingInterval { get; } = new IntegerConfig(0);

        public IntegerConfig DisplayPoint { get; } = new IntegerConfig(10000);
        public IntegerConfig AxisY_ValueMin { get; } = new IntegerConfig(-100000);
        public IntegerConfig AxisY_ValueMax { get; } = new IntegerConfig(100000);

        public IntegerConfig     CurrentChannel { get; } = new IntegerConfig(0);
        public ChannelListConfig ChannelList    { get; } = new ChannelListConfig();


        public ViewPropertyImpl()
        {
        }

        public override ViewProperty Clone()
        {
            return (ClassUtil.Clone<ViewPropertyImpl>(this));
        }
    }
}
