using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.PacketConverter.Grouping.DataLengthMatch
{
    [Serializable]
    internal sealed class ConvertMethodProperty : ConfigHolder
    {
        public IntegerConfig Length { get; } = new IntegerConfig(16);
    }
}
