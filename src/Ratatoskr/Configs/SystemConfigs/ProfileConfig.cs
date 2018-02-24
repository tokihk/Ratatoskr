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
    internal sealed class ProfileConfig : ConfigHolder
    {
        public StringConfig ProfileDir { get; } = new StringConfig("./profiles");
        public GuidConfig   ProfileID  { get; } = new GuidConfig(Guid.NewGuid());
    }
}
