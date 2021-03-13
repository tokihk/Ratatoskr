using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_NotifyMail : OptionEditPage
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


        private MailConfig PageConfig;


        public OptionEditPage_NotifyMail()
        {
            InitializeComponent();
            InitializeConnectMode();
        }

        public OptionEditPage_NotifyMail(MailConfig config) : this()
        {
            PageConfig = config;
        }

        private void InitializeConnectMode()
        {
            CBox_ConnectMode.BeginUpdate();
            {
                foreach (var mode in Enum.GetValues(typeof(SmtpConnectModeType))) {
                    CBox_ConnectMode.Items.Add(mode);
                }
            }
            CBox_ConnectMode.EndUpdate();
        }

        protected override void OnLoadConfig()
        {
            TBox_ServerName.Text = PageConfig.SmtpHost;
            Num_ServerPort.Value = PageConfig.SmtpPort;
            CBox_ConnectMode.SelectedItem = PageConfig.SmtpConnectMode;
            TBox_Username.Text = PageConfig.SmtpUsername;
            TBox_Password.Text = PageConfig.SmtpPassword;
            Num_MinIval.Value = PageConfig.MinimumInterval;
            TBox_TimmingMask.Text = PageConfig.NotifyTimmingMask;
        }

        protected override void OnFlushConfig()
        {
            PageConfig.SmtpHost = TBox_ServerName.Text;
            PageConfig.SmtpPort = (uint)Num_ServerPort.Value;
            PageConfig.SmtpConnectMode = (SmtpConnectModeType)CBox_ConnectMode.SelectedItem;
            PageConfig.SmtpUsername = TBox_Username.Text;
            PageConfig.SmtpPassword = TBox_Password.Text;
            PageConfig.MinimumInterval = (uint)Num_MinIval.Value;
            PageConfig.NotifyTimmingMask = TBox_TimmingMask.Text;
        }
    }
}
