using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace Ratatoskr.PacketConverters.Separator.MSecPassingFromPrevPacket
{
    [Serializable]
    internal sealed class RuleProperty : ConfigHolder
    {
        public IntegerConfig Interval { get; } = new IntegerConfig(100);
    }
}
