using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.Fixed
{
    public sealed class FixedConfig : ConfigRoot<FixedConfig>
    {
        public FixedConfig() : base("fixed")
        {
        }

        public StringConfig  ApplicationName     { get; } = new StringConfig("");
        public StringConfig  ApplicationID       { get; } = new StringConfig("rt");

        public StringConfig  Copyright           { get; } = new StringConfig("");
        public StringConfig  Version             { get; } = new StringConfig("");
        public StringConfig  HomePage            { get; } = new StringConfig("https://github.com/tokihk/Ratatoskr");

        public StringConfig  SystemConfigPath    { get; } = new StringConfig("startup.ini");

        public IntegerConfig GateControllerLimit { get; } = new IntegerConfig(10);

        public IntegerConfig PopupStatusTextTime { get; } = new IntegerConfig(2000);

        public StringListConfig ApplicationListUrl { get; } = new StringListConfig();
    }
}
