using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Data.System;

namespace Ratatoskr.Forms.ConfigEditor
{
    internal partial class ConfigEditorPage_AutoTimeStamp : ConfigEditorPage
    {
        public ConfigEditorPage_AutoTimeStamp() : base()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            ChkBox_Trigger_RecvPeriod.Checked = Config.System.AutoTimeStamp.Trigger.Value.HasFlag(AutoTimeStampTriggerType.LastRecvPeriod);
            Num_Trigger_RecvPeriod.Value = Config.System.AutoTimeStamp.Value_LastRecvPeriod.Value;
        }

        protected override void OnFlushConfig()
        {
            Config.System.AutoTimeStamp.Trigger.Value = (ChkBox_Trigger_RecvPeriod.Checked) ? (AutoTimeStampTriggerType.LastRecvPeriod) : (0);
            Config.System.AutoTimeStamp.Value_LastRecvPeriod.Value = Num_Trigger_RecvPeriod.Value;
        }
    }
}
