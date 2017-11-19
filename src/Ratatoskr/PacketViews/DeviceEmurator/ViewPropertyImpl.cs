using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketViews.DeviceEmurator
{
    internal enum DataViewType
    {
        Char,
        HexText,
        BinCode,
    }

    internal enum CharCodeType
    {
        ASCII,
        ShiftJIS,
        UTF8,
    }

    internal class ViewPropertyImpl : ViewProperty
    {
        public EnumConfig<DataViewType> DataView       { get; } = new EnumConfig<DataViewType>(DataViewType.Char);
        public EnumConfig<CharCodeType> CharCode       { get; } = new EnumConfig<CharCodeType>(CharCodeType.ASCII);
        public StringConfig             EndLinePattern { get; } = new StringConfig("");
        public IntegerConfig            ShiftBit       { get; } = new IntegerConfig(0);
        public BoolConfig               EchoBack       { get; } = new BoolConfig(false);


        public override ViewProperty Clone()
        {
            return (ClassUtil.Clone<ViewPropertyImpl>(this));
        }
    }
}
