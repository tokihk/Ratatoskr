using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.PacketConverter;
using Ratatoskr.General;

namespace Ratatoskr.PacketConverter.Filter
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
