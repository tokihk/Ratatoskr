using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Configs.SystemConfigs
{
    internal sealed class ApplicationCoreConfig : ConfigHolder
    {
        public IntegerConfig AppTimerInterval { get; } = new IntegerConfig(10);

        public BoolConfig NewVersionAutoUpdate { get; } = new BoolConfig(false);

        public IntegerConfig RawPacketCountLimit    { get; } = new IntegerConfig(999999);
        public IntegerConfig AutoHighSpeedDrawLimit { get; } = new IntegerConfig(999999);
    }
}
