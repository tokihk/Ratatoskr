using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs.Types;
using Ratatoskr.Generic.Container;

namespace Ratatoskr.Configs.SystemConfigs
{
    internal sealed class SystemConfig : ConfigManagerBase<SystemConfig>
    {
        public ApplicationCoreConfig ApplicationCore { get; } = new ApplicationCoreConfig();
        public ProfileListConfig     ProfileList     { get; } = new ProfileListConfig();
        public ProfileConfig         Profile         { get; } = new ProfileConfig();
        public StringConfig          LanguageFile    { get; } = new StringConfig("");
        public MainWindowConfig      MainWindow      { get; } = new MainWindowConfig();


        public SystemConfig() : base("system") { }

        public bool Load()
        {
            return (LoadConfig(Program.GetWorkspaceDirectory(ConfigManager.Fixed.SystemConfigPath.Value)));
        }

        public bool Save()
        {
            return (SaveConfig(Program.GetWorkspaceDirectory(ConfigManager.Fixed.SystemConfigPath.Value)));
        }
    }
}
