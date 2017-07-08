using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Configs.FixedConfigs
{
    public sealed class FixedConfig : ConfigManagerBase<FixedConfig>
    {
        public FixedConfig() : base("fixed")
        {
            ApplicationListUrl.Value.Add("");
        }

        public StringConfig  ApplicationName     { get; } = new StringConfig("Ratatoskr");
        public StringConfig  Copyright           { get; } = new StringConfig("Copyright 2017 H.Kouno");

        public StringConfig  SystemConfigPath    { get; } = new StringConfig("startup.ini");

        public IntegerConfig GateControllerLimit { get; } = new IntegerConfig(10);

        public IntegerConfig PopupStatusTextTime { get; } = new IntegerConfig(2000);

        public StringListConfig ApplicationListUrl { get; } = new StringListConfig();
    }
}
