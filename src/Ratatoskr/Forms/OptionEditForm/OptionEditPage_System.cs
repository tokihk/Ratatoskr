using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_System : OptionEditPage
    {
        public OptionEditPage_System()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            Num_RawPacketCountLimit.Value = Math.Min(Math.Max(Config.RawPacketCountLimit, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);

            Num_Packet_ViewPacketCountLimit.Value = Math.Min(Math.Max(Config.Packet_ViewPacketCountLimit, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);

            ChkBox_Sequential_ViewCharCountLimit.Checked = Config.Sequential_ViewCharCountLimitEnable;
            Num_Sequential_ViewCharCountLimit.Value = Math.Min(Math.Max(Config.Sequential_ViewCharCountLimit, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);
            ChkBox_Sequential_WinApiMode.Checked = Config.Sequential_WinApiMode;
            ChkBox_Sequential_LineNumberVisible.Checked = Config.Sequential_LineNoVisible;

            UpdateEnableStatus();
        }

        protected override void OnFlushConfig()
        {
            Config.RawPacketCountLimit = Num_RawPacketCountLimit.Value;

            Config.Packet_ViewPacketCountLimit = Num_Packet_ViewPacketCountLimit.Value;

            Config.Sequential_ViewCharCountLimitEnable = ChkBox_Sequential_ViewCharCountLimit.Checked;
            Config.Sequential_ViewCharCountLimit = Num_Sequential_ViewCharCountLimit.Value;
            Config.Sequential_WinApiMode = ChkBox_Sequential_WinApiMode.Checked;
            Config.Sequential_LineNoVisible = ChkBox_Sequential_LineNumberVisible.Checked;
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
