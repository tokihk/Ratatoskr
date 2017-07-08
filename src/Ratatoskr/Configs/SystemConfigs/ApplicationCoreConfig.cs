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
    }
}
