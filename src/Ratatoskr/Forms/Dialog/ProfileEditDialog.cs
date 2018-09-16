using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Resources;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class ProfileEditDialog : Form
    {
        private UserConfig config_ = null;

        private string[] ignore_names_ = null;


        public ProfileEditDialog()
        {
            InitializeComponent();
        }

        public ProfileEditDialog(UserConfig config, params string[] ignore_names): this()
        {
            config_ = config;
            ignore_names_ = ignore_names;

            TBox_ProfileName.Text = config_.ProfileName.Value;
            TBox_ProfileComment.Text = config_.ProfileComment.Value;
            ChkBox_ReadOnly.Checked = config_.ReadOnly.Value;

            if (config_.ReadOnlyLock.Value) {
                ChkBox_ReadOnly.Enabled = false;
            }
        }

        private void UpdateProfileNameStatus()
        {
            var profile_name = TBox_ProfileName.Text.Trim();
            var name_ok = false;

            /* 同名のプロファイルが存在する場合はエラー表示 */
            if (profile_name.Length > 0) {
                if (   (!ConfigManager.ProfileIsExist(profile_name))
                    || (ignore_names_.Contains(profile_name))
                ) {
                    name_ok = true;
                }
            }

            TBox_ProfileName.BackColor = (name_ok)
                                       ? (AppColors.PATTERN_OK)
                                       : (AppColors.PATTERN_NG);
            Btn_Ok.Enabled = name_ok;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            config_.ProfileName.Value = TBox_ProfileName.Text.Trim();
            config_.ProfileComment.Value = TBox_ProfileComment.Text;
            config_.ReadOnly.Value = ChkBox_ReadOnly.Checked;

            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TBox_ProfileName_TextChanged(object sender, EventArgs e)
        {
            UpdateProfileNameStatus();
        }
    }
}
