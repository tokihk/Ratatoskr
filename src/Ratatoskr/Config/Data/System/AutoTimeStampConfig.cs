using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.System
{
    public enum AutoTimeStampTriggerType
    {
        LastRecvPeriod = 1 << 0,
    }

    [Serializable]
    public sealed class AutoTimeStampConfig : ConfigHolder
    {
        public BoolConfig                           Enable               { get; } = new BoolConfig(false);
        public EnumConfig<AutoTimeStampTriggerType> Trigger              { get; } = new EnumConfig<AutoTimeStampTriggerType>(AutoTimeStampTriggerType.LastRecvPeriod);
        public IntegerConfig                        Value_LastRecvPeriod { get; } = new IntegerConfig(1000);
    }
}
