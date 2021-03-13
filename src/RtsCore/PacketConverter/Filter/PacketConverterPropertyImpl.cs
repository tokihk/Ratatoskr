using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.PacketConverter;
using RtsCore.Generic;

namespace Ratatoskr.PacketConverters.Filter
{
    internal sealed class PacketConverterPropertyImpl : PacketConverterProperty
    {
        public StringListConfig ExpList { get; } = new StringListConfig();


        public override PacketConverterProperty Clone()
        {
            return (ClassUtil.Clone(this));
        }
    }
}
