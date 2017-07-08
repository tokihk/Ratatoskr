using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters.Separator
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
        public EnumConfig<RuleType>                    Rule                               { get; } = new EnumConfig<RuleType>(RuleType.None);
        public DataContentsMatch.RuleProperty          DataContentsMatchProperty          { get; } = new DataContentsMatch.RuleProperty();
        public DataLengthMatch.RuleProperty            DataLengthMatchProperty            { get; } = new DataLengthMatch.RuleProperty();
        public MSecPassingFromPrevPacket.RuleProperty  MSecPassingFromPrevPacketProperty  { get; } = new MSecPassingFromPrevPacket.RuleProperty();
        public MSecPassingFromFirstPacket.RuleProperty MSecPassingFromFirstPacketProperty { get; } = new MSecPassingFromFirstPacket.RuleProperty();

        public BoolConfig EventDetectDivide     { get; } = new BoolConfig(true);
        public BoolConfig DirectionChangeDivide { get; } = new BoolConfig(true);


        public override PacketConverterProperty Clone()
        {
            return (ClassUtil.Clone(this));
        }
    }
}
