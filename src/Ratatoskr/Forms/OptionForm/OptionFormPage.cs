using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.OptionForm
{
    internal class OptionFormPage : UserControl
    {
        public OptionConfig Config { get; private set; }


        public OptionFormPage()
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
