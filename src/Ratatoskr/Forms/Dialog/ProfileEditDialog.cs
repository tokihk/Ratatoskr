using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.User;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class ProfileEditDialog : Form
    {
        public ProfileEditDialog()
        {
            InitializeComponent();
        }

        public string       ProfileName         { get; set; } = "";
        public string       ProfileComment      { get; set; } = "";
        public bool         ProfileReadOnly     { get; set; } = false;
        public bool         ProfileReadOnlyLock { get; set; } = false;
        public List<string> ProfileEnableNames  { get; } = new List<string>();

        private void UpdateProfileNameStatus()
        {
            var profile_name = TBox_ProfileName.Text.Trim();
            var name_ok = false;

            /* 同名のプロファイルが存在する場合はエラー表示 */
            if (profile_name.Length > 0) {
                if (   (!ConfigManager.ProfileIsExist(profile_name))
                    || (ProfileEnableNames.Contains(profile_name))
                ) {
                    name_ok = true;
                }
            }

            TBox_ProfileName.BackColor = (name_ok)
                                       ? (Ratatoskr.Resource.AppColors.Ok)
                                       : (Ratatoskr.Resource.AppColors.Ng);
            Btn_Ok.Enabled = name_ok;
        }

        private void ProfileEditDialog_Load(object sender, EventArgs e)
        {
            TBox_ProfileName.Text = ProfileName;
            TBox_ProfileComment.Text = ProfileComment;
            ChkBox_ReadOnly.Checked = ProfileReadOnly;

            if (ProfileReadOnlyLock) {
                ChkBox_ReadOnly.Enabled = false;
            }
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            ProfileName = TBox_ProfileName.Text.Trim();
            ProfileComment = TBox_ProfileComment.Text;
            ProfileReadOnly = ChkBox_ReadOnly.Checked;

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
