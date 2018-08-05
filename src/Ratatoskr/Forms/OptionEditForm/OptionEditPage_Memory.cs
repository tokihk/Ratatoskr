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
    internal partial class OptionEditPage_Memory : OptionEditPage
    {
        public OptionEditPage_Memory()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            Num_RawPacketCountLimit.Value = Math.Min(Math.Max(Config.RawPacketCountLimit, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);
            Num_Packet_ViewPacketCountLimit.Value = Math.Min(Math.Max(Config.Packet_ViewPacketCountLimit, Num_RawPacketCountLimit.Minimum), Num_RawPacketCountLimit.Maximum);;
        }

        protected override void OnFlushConfig()
        {
            Config.RawPacketCountLimit = Num_RawPacketCountLimit.Value;
            Config.Packet_ViewPacketCountLimit = Num_Packet_ViewPacketCountLimit.Value;
        }
    }
}
