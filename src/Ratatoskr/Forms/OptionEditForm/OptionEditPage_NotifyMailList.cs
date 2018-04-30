using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_NotifyMailList : OptionEditPage
    {
        private class SettingItem
        {
            public MailConfig Config { get; set; }


            public SettingItem(MailConfig config)
            {
                Config = config;
            }

            public override string ToString()
            {
                var str = new StringBuilder();

                str.Append(Config.SmtpHost);

                if (Config.SmtpUsername.Length > 0) {
                    str.AppendFormat(" - {0}", Config.SmtpUsername);
                }

                return (str.ToString());
            }
        }

        public OptionEditPage_NotifyMailList()
        {
            InitializeComponent();
        }

        private void EditConfig()
        {
            var item = LBox_SettingList.SelectedItem as SettingItem;

            if (item == null)return;

            var dialog = new Dialog.NotifyMailConfigDialog(item.Config);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            item.Config = dialog.Config;

            OnFlushConfig();
            OnLoadConfig();

            UpdateSelectConfigView();
        }

        private void UpdateSelectConfigView()
        {
            Label_SettingDetails.Text = "";

            var item = LBox_SettingList.SelectedItem as SettingItem;

            if (item == null)return;

            var str = new StringBuilder();

            str.AppendLine(string.Format("{0, 20}: {1}", "Server name", item.Config.SmtpHost));
            str.AppendLine(string.Format("{0, 20}: {1}", "Port no", item.Config.SmtpPort));
            str.AppendLine(string.Format("{0, 20}: {1}", "Username", item.Config.SmtpUsername));
            str.AppendLine(string.Format("{0, 20}: {1}", "Connection mode", item.Config.SmtpConnectMode.ToString()));

            Label_SettingDetails.Text = str.ToString();
        }

        protected override void OnLoadConfig()
        {
            LBox_SettingList.BeginUpdate();
            {
                LBox_SettingList.Items.Clear();
                foreach (var config in Config.MailList) {
                    LBox_SettingList.Items.Add(new SettingItem(config));
                }
            }
            LBox_SettingList.EndUpdate();
        }

        protected override void OnFlushConfig()
        {
            Config.MailList.Clear();

            foreach (SettingItem item in LBox_SettingList.Items) {
                Config.MailList.Add(item.Config);
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            /* 新規設定作成 */
            var config = new MailConfig();

            var dialog = new Dialog.NotifyMailConfigDialog(config);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            LBox_SettingList.Items.Add(new SettingItem(dialog.Config));

            UpdateSelectConfigView();
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            EditConfig();
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            var item = LBox_SettingList.SelectedItem as SettingItem;

            if (item == null)return;

            LBox_SettingList.Items.Remove(item);

            UpdateSelectConfigView();
        }

        private void LBox_SettingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectConfigView();
        }

        private void LBox_SettingList_DoubleClick(object sender, EventArgs e)
        {
            EditConfig();
        }
    }
}
