﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Generic;

namespace Ratatoskr.Configs.SystemConfigs
{
    internal sealed class SystemConfig : ConfigManagerBase<SystemConfig>
    {
        public ApplicationCoreConfig ApplicationCore { get; } = new ApplicationCoreConfig();
        public ProfileConfig         Profile         { get; } = new ProfileConfig();


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