using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Forms.Controls;
using Ratatoskr.PacketViews.Packet.Configs;
using RtsCore.Config.Types;
using RtsCore.Framework.PacketView;
using RtsCore.Generic;

namespace Ratatoskr.PacketViews.Packet
{
    internal enum ColumnType
    {
        Class,
        Alias,
        Datetime_UTC,
        Datetime_Local,
        Information,
        Mark,
        Source,
        Destination,
        DataLength,
        DataPreviewBinary,
        DataPreviewBinaryWithoutDivider,
        DataPreviewText,
        DataPreviewCustom,
    }

    internal enum CharCodeType
    {
        ASCII,
        ShiftJIS,
        UTF8,
    }

    internal class PacketViewPropertyImpl : PacketViewProperty
    {
        public ColumnListConfig           ColumnList      { get; } = new ColumnListConfig();
        public IntegerConfig              PreviewDataSize { get; } = new IntegerConfig(16);
        public EnumConfig<CharCodeType>   CharCode        { get; } = new EnumConfig<CharCodeType>(CharCodeType.UTF8);
        public StringConfig               CustomFormat    { get; } = new StringConfig("A: ${HEXTEXT:BYTE:0:8} B: ${BITTEXT:BIT:0:16} C: ${UINT16B:BIT:0:12}");

        public BoolConfig ExtViewSelectPacketCount { get; } = new BoolConfig(true);
        public BoolConfig ExtViewSelectTotalSize   { get; } = new BoolConfig(true);
        public BoolConfig ExtViewFirstPacketInfo   { get; } = new BoolConfig(true);
        public BoolConfig ExtViewLastPacketInfo    { get; } = new BoolConfig(true);
        public BoolConfig ExtViewSelectDelta       { get; } = new BoolConfig(true);
        public BoolConfig ExtViewSelectRate        { get; } = new BoolConfig(true);


        public PacketViewPropertyImpl()
        {
        }

        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }

        public BinEditBox.CharCodeType ToBinEditBoxCharCode()
        {
            switch (CharCode.Value) {
                case CharCodeType.ASCII:    return (BinEditBox.CharCodeType.ASCII);
                case CharCodeType.ShiftJIS: return (BinEditBox.CharCodeType.ShiftJIS);
                case CharCodeType.UTF8:     return (BinEditBox.CharCodeType.UTF8);
                default:                    return (BinEditBox.CharCodeType.ASCII);
            }
        }
    }
}
