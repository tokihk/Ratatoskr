using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Types;
using Ratatoskr.General;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class NotifyMailConfigDialog : Form
    {
        public MailConfig Config { get; private set; }


        public NotifyMailConfigDialog()
        {
            InitializeComponent();
            InitializeConnectMode();
        }

        public NotifyMailConfigDialog(MailConfig config) : this()
        {
            Config = ClassUtil.Clone(config);

            LoadConfig();
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

        private void LoadConfig()
        {
            TBox_ServerName.Text = Config.SmtpHost;
            Num_ServerPort.Value = Config.SmtpPort;
            CBox_ConnectMode.SelectedItem = Config.SmtpConnectMode;
            TBox_Username.Text = Config.SmtpUsername;
            TBox_Password.Text = Config.SmtpPassword;
            Num_MinIval.Value = Config.MinimumInterval;
            TBox_TimmingMask.Text = Config.NotifyTimmingMask;
        }

        private void FlushConfig()
        {
            Config.SmtpHost = TBox_ServerName.Text;
            Config.SmtpPort = (uint)Num_ServerPort.Value;
            Config.SmtpConnectMode = (SmtpConnectModeType)CBox_ConnectMode.SelectedItem;
            Config.SmtpUsername = TBox_Username.Text;
            Config.SmtpPassword = TBox_Password.Text;
            Config.MinimumInterval = (uint)Num_MinIval.Value;
            Config.NotifyTimmingMask = TBox_TimmingMask.Text;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            FlushConfig();

            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
