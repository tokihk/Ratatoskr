using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.ConfigForm
{
    internal class ConfigPage : UserControl
    {
        public OptionConfig Config { get; private set; }


        public ConfigPage()
        {
        }

        public void LoadConfig(OptionConfig config)
        {
            Config = config;

            OnLoadConfig();
        }

        public void FlushConfig()
        {
            OnFlushConfig();
        }

        protected virtual void OnLoadConfig()
        {
        }

        protected virtual void OnFlushConfig()
        {
        }
    }
}
