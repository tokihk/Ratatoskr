using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Data.User;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_AutoUpdate : OptionEditPage
    {
        public OptionEditPage_AutoUpdate()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
//            ChkBox_AutoUpdate.Checked = Config.NewVersionAutoUpdate.Value;
        }

        protected override void OnFlushConfig()
        {
//            Config.NewVersionAutoUpdate.Value = ChkBox_AutoUpdate.Checked;
        }
    }
}
