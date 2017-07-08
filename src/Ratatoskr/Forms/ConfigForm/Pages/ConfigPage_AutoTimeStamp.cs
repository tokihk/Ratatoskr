using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.ConfigForm.Pages
{
    internal partial class ConfigPage_AutoTimeStamp : ConfigPage
    {
        public ConfigPage_AutoTimeStamp()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            ChkBox_Trigger_RecvPeriod.Checked = Config.AutoTimeStampTrigger.Value.HasFlag(AutoTimeStampTriggerType.LastRecvPeriod);
            Num_Trigger_RecvPeriod.Value = Config.AutoTimeStampValue_LastRecvPeriod.Value;
        }

        protected override void OnFlushConfig()
        {
            Config.AutoTimeStampTrigger.Value = 0;
            Config.AutoTimeStampTrigger.Value |= (ChkBox_Trigger_RecvPeriod.Checked) ? (AutoTimeStampTriggerType.LastRecvPeriod) : (0);
            Config.AutoTimeStampValue_LastRecvPeriod.Value = Num_Trigger_RecvPeriod.Value;
        }
    }
}
