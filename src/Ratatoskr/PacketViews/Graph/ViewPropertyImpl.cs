using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketViews.Graph
{
    internal enum ViewModeType
    {
        Value,
        Amount,
    }

    internal enum EndianType
    {
        BigEndian,
        LittleEndian
    }

    internal enum DataType
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

    internal class ViewPropertyImpl : ViewProperty
    {
        public EnumConfig<ViewModeType>        ViewMode             { get; } = new EnumConfig<ViewModeType>(ViewModeType.Value);
        public EnumConfig<SeriesChartType>     GraphType            { get; } = new EnumConfig<SeriesChartType>(SeriesChartType.Line);
        public IntegerConfig                   DrawPointNum         { get; } = new IntegerConfig(10000);
        public IntegerConfig                   DrawInterval         { get; } = new IntegerConfig(1000);
        public IntegerConfig                   DataValueMin         { get; } = new IntegerConfig(-1000);
        public BoolConfig                      DataValueMinAuto     { get; } = new BoolConfig(true);
        public IntegerConfig                   DataValueMax         { get; } = new IntegerConfig(1000);
        public BoolConfig                      DataValueMaxAuto     { get; } = new BoolConfig(true);
        public IntegerConfig                   DataValueOffset      { get; } = new IntegerConfig(0);
        public EnumConfig<DataType>            InputDataType        { get; } = new EnumConfig<DataType>(DataType.SignedWord);
        public EnumConfig<EndianType>          InputByteEndian      { get; } = new EnumConfig<EndianType>(EndianType.BigEndian);
        public IntegerConfig                   InputCycle           { get; } = new IntegerConfig(1);
        public IntegerConfig                   SamplingInterval     { get; } = new IntegerConfig(1);


        public ViewPropertyImpl()
        {
        }

        public override ViewProperty Clone()
        {
            return (ClassUtil.Clone<ViewPropertyImpl>(this));
        }

        public int GetInputDataTypeSize()
        {
            switch (InputDataType.Value) {
                case DataType.UnsignedByte:     return (1);
                case DataType.UnsignedWord:     return (2);
                case DataType.UnsignedDword:    return (4);
                case DataType.UnsignedQword:    return (8);
                case DataType.SignedByte:       return (1);
                case DataType.SignedWord:       return (2);
                case DataType.SignedDword:      return (4);
                case DataType.SignedQword:      return (8);
                case DataType.IEEE754_Float:    return (4);
                case DataType.IEEE754_Double:   return (8);
                default:                        return (1);
            }
        }
    }
}
