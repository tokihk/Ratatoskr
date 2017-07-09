using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.OptionForm.Pages
{
    internal partial class ConfigPage_AutoUpdate : OptionFormPage
    {
        public ConfigPage_AutoUpdate()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            ChkBox_AutoUpdate.Checked = Config.NewVersionAutoUpdate.Value;
        }

        protected override void OnFlushConfig()
        {
            Config.NewVersionAutoUpdate.Value = ChkBox_AutoUpdate.Checked;
        }
    }
}
