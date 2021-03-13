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

namespace Ratatoskr.Forms.Dialog
{
    public partial class ProfileManagerDialog : Form
    {
        private enum ProfileListSubColumnID
        {
            CreateDateTime,
            ProfileName,
        }


        public ProfileManagerDialog()
        {
            InitializeComponent();
            InitializeProfileList();

            UpdateProfileList();
        }

        private void InitializeProfileList()
        {
            var columns_list = new []
            {
                new { id = ProfileListSubColumnID.CreateDateTime, width = 120, text = "Create Datetime" },
                new { id = ProfileListSubColumnID.ProfileName,    width = 120, text = "Profile Name"    },
            };

            LView_ProfileList.BeginUpdate();
            {
                var column = (ColumnHeader)null;

                /* メインヘッダー */
                column = new ColumnHeader();
                column.Text = "No.";
                column.Width = 50;
                LView_ProfileList.Columns.Add(column);

                /* サブヘッダー */
                foreach (var info in columns_list) {
                    column = new ColumnHeader();
                    column.Text = info.text;
                    column.Width = info.width;
                    column.Tag = info.id;
                    LView_ProfileList.Columns.Add(column);
                }
            }
            LView_ProfileList.EndUpdate();
        }

        private void UpdateProfileList()
        {
            var item = (ListViewItem)null;

            LView_ProfileList.BeginUpdate();
            {
                LView_ProfileList.Items.Clear();

                foreach (var profile in ConfigManager.GetProfileList()) {
                    item = ProfileToListViewItem(profile);

                    if (item == null)continue;

                    LView_ProfileList.Items.Add(item);
                }
            }
            LView_ProfileList.EndUpdate();
        }

        private ListViewItem ProfileToListViewItem(ConfigManager.ProfileData profile)
        {
            var item = new ListViewItem();

            /* メイン */
            item.Tag = profile;

            /* サブ */
            foreach (ColumnHeader column in LView_ProfileList.Columns) {
                item.SubItems.Add(ProfileToListViewSubItem((ProfileListSubColumnID)column.Tag, profile));
            }

            return (item);
        }

        private ListViewItem.ListViewSubItem ProfileToListViewSubItem(ProfileListSubColumnID column_id, ConfigManager.ProfileData profile)
        {
            var item = new ListViewItem.ListViewSubItem();

            switch (column_id) {
                case ProfileListSubColumnID.CreateDateTime:
                    item.Text = profile.Config.CreateDateTime.Value.ToLocalTime().ToString("yyyy-MM-dd");
                    break;
                case ProfileListSubColumnID.ProfileName:
                    item.Text = profile.Config.ProfileName.Value;
                    break;
            }

            return (item);
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Import_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {

        }
    }
}
