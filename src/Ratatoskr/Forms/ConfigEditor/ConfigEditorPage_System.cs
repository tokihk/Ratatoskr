using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.ConfigEditor
{
    internal partial class ConfigEditorPage_System : ConfigEditorPage
    {
        public ConfigEditorPage_System() : base()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
			Num_GateNumber.Value = Config.System.ApplicationCore.GateNum.Value;

            Num_RawPacketCountLimit.Value = Math.Min(Math.Max(Config.System.ApplicationCore.RawPacketCountLimit.Value, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);

            Num_Packet_ViewPacketCountLimit.Value = Math.Min(Math.Max(Config.System.ApplicationCore.Packet_ViewPacketCountLimit.Value, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);

            ChkBox_Sequential_ViewCharCountLimit.Checked = Config.System.ApplicationCore.Sequential_ViewCharCountLimitEnable.Value;
            Num_Sequential_ViewCharCountLimit.Value = Math.Min(Math.Max(Config.System.ApplicationCore.Sequential_ViewCharCountLimit.Value, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);
            ChkBox_Sequential_WinApiMode.Checked = Config.System.ApplicationCore.Sequential_WinApiMode.Value;
            ChkBox_Sequential_LineNumberVisible.Checked = Config.System.ApplicationCore.Sequential_LineNoVisible.Value;

            UpdateEnableStatus();
        }

        protected override void OnFlushConfig()
        {
			Config.System.ApplicationCore.GateNum.Value = Num_GateNumber.Value;

            Config.System.ApplicationCore.RawPacketCountLimit.Value = Num_RawPacketCountLimit.Value;

            Config.System.ApplicationCore.Packet_ViewPacketCountLimit.Value = Num_Packet_ViewPacketCountLimit.Value;

            Config.System.ApplicationCore.Sequential_ViewCharCountLimitEnable.Value = ChkBox_Sequential_ViewCharCountLimit.Checked;
            Config.System.ApplicationCore.Sequential_ViewCharCountLimit.Value = Num_Sequential_ViewCharCountLimit.Value;
            Config.System.ApplicationCore.Sequential_WinApiMode.Value = ChkBox_Sequential_WinApiMode.Checked;
            Config.System.ApplicationCore.Sequential_LineNoVisible.Value = ChkBox_Sequential_LineNumberVisible.Checked;
        }

        private void UpdateEnableStatus()
        {
            ChkBox_Sequential_ViewCharCountLimit.Enabled = ChkBox_Sequential_WinApiMode.Checked;
            Num_Sequential_ViewCharCountLimit.Enabled = (ChkBox_Sequential_WinApiMode.Checked && ChkBox_Sequential_ViewCharCountLimit.Checked);
            ChkBox_Sequential_LineNumberVisible.Enabled = ChkBox_Sequential_WinApiMode.Checked;
        }

        private void ChkBox_Sequential_ViewCharCountLimit_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnableStatus();
        }

        private void ChkBox_Sequential_WinApiMode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnableStatus();
        }
    }
}
