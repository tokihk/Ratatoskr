using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class ProfileConfigDialog : Form
    {
        public UserConfig Config { get; } = null;


        public ProfileConfigDialog()
        {
            InitializeComponent();
        }

        public ProfileConfigDialog(UserConfig config): this()
        {
            Config = config;

            TBox_ProfileName.Text = Config.ProfileName.Value;
            TBox_ProfileComment.Text = Config.ProfileComment.Value;
            ChkBox_ReadOnly.Checked = Config.ReadOnly.Value;

            if (Config.ReadOnlyLock.Value) {
                ChkBox_ReadOnly.Enabled = false;
            }
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            Config.ProfileName.Value = TBox_ProfileName.Text;
            Config.ProfileComment.Value = TBox_ProfileComment.Text;
            Config.ReadOnly.Value = ChkBox_ReadOnly.Checked;

            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
