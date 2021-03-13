using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.System
{
    public sealed class ProfileConfig : ConfigHolder
    {
        public StringConfig ProfileDir { get; } = new StringConfig("./profiles");
        public GuidConfig   ProfileID  { get; } = new GuidConfig(Guid.NewGuid());
    }
}
