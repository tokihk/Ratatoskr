using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Data.User;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal class OptionEditPage : UserControl
    {
        public OptionDataMap Config { get; private set; }


        public OptionEditPage()
        {
        }

        public void LoadConfig(OptionDataMap config)
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
