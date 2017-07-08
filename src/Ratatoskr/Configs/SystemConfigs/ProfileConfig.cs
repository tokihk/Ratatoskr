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
        public ProfileListConfig ProfileList  { get; } = new ProfileListConfig();
        public StringConfig      ProfileName  { get; } = new StringConfig("default");
        public BoolConfig        ProfileCheck { get; } = new BoolConfig(true);
    }
}
