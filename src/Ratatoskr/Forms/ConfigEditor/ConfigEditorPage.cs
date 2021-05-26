using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.ConfigEditor
{
    internal class ConfigEditorPage : UserControl
    {
        public ConfigEditorPage()
        {
        }

		public ConfigEditorData Config { get; private set; }

        public void LoadConfig(ConfigEditorData config)
        {
			Config = config;

            OnLoadConfig();
        }

        public void FlushConfig()
        {
            OnFlushConfig();
        }

		public void Active(bool active)
		{

		}

        protected virtual void OnLoadConfig()
        {
        }

        protected virtual void OnFlushConfig()
        {
        }
    }
}
