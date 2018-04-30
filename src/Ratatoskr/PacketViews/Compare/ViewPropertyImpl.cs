using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketViews.Compare
{
    internal enum DrawDataType
    {
        ASCII,
        ShiftJIS,
        UTF8,
        HEX,
        BIN,
    }

    internal class ViewPropertyImpl : ViewProperty
    {
        public IntegerConfig            ShiftBit       { get; } = new IntegerConfig(0);
        public StringConfig             EndLinePattern { get; } = new StringConfig("");
        public BoolConfig               EchoBack       { get; } = new BoolConfig(false);

        public EnumConfig<DrawDataType> DrawType       { get; } = new EnumConfig<DrawDataType>(DrawDataType.UTF8);
        public StringConfig             BoundaryText   { get; } = new StringConfig(" ");


        public override ViewProperty Clone()
        {
            return (ClassUtil.Clone<ViewPropertyImpl>(this));
        }
    }
}
