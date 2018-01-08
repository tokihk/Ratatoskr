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
            ApplicationName.Value = AppInfo.Name;
            Copyright.Value = AppInfo.Copyright;
            Version.Value = AppInfo.Version;

            ApplicationListUrl.Value.Add("");
        }

        public StringConfig  ApplicationName     { get; } = new StringConfig("");
        public StringConfig  Copyright           { get; } = new StringConfig("");
        public StringConfig  Version             { get; } = new StringConfig("");
        public StringConfig  HomePage            { get; } = new StringConfig("https://github.com/tokihk/Ratatoskr");

        public StringConfig  SystemConfigPath    { get; } = new StringConfig("startup.ini");

        public IntegerConfig GateControllerLimit { get; } = new IntegerConfig(10);

        public IntegerConfig PopupStatusTextTime { get; } = new IntegerConfig(2000);

        public StringListConfig ApplicationListUrl { get; } = new StringListConfig();
    }
}
