using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.PacketConverter;
using RtsCore.Generic;

namespace Ratatoskr.PacketConverters.Grouping
{
    internal enum RuleType
    {
        None,
        DataContentsMatch,
        DataLengthMatch,
        MSecPassingFromPrevPacket,
        MSecPassingFromFirstPacket,
    }


    [Serializable]
    internal sealed class PacketConverterPropertyImpl : PacketConverterProperty
    {
        public EnumConfig<RuleType>                             Rule                               { get; } = new EnumConfig<RuleType>(RuleType.None);
        public DataContentsMatch.ConvertMethodProperty          DataContentsMatchProperty          { get; } = new DataContentsMatch.ConvertMethodProperty();
        public DataLengthMatch.ConvertMethodProperty            DataLengthMatchProperty            { get; } = new DataLengthMatch.ConvertMethodProperty();
        public MSecPassingFromPrevPacket.ConvertMethodProperty  MSecPassingFromPrevPacketProperty  { get; } = new MSecPassingFromPrevPacket.ConvertMethodProperty();
        public MSecPassingFromFirstPacket.ConvertMethodProperty MSecPassingFromFirstPacketProperty { get; } = new MSecPassingFromFirstPacket.ConvertMethodProperty();

        public BoolConfig Global_DivideByEventDetect    { get; } = new BoolConfig(false);
        public BoolConfig Local_EachAlias               { get; } = new BoolConfig(true);
        public BoolConfig Local_EachDirection           { get; } = new BoolConfig(true);
        public BoolConfig Local_DivideByDirectionChange { get; } = new BoolConfig(true);


        public override PacketConverterProperty Clone()
        {
            return (ClassUtil.Clone(this));
        }
    }
}
