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

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_AutoTimeStamp : OptionEditPage
    {
        public OptionEditPage_AutoTimeStamp()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            ChkBox_Trigger_RecvPeriod.Checked = Config.AutoTimeStampTrigger.HasFlag(AutoTimeStampTriggerType.LastRecvPeriod);
            Num_Trigger_RecvPeriod.Value = Config.AutoTimeStampValue_LastRecvPeriod;
        }

        protected override void OnFlushConfig()
        {
            Config.AutoTimeStampTrigger = 0;
            Config.AutoTimeStampTrigger |= (ChkBox_Trigger_RecvPeriod.Checked) ? (AutoTimeStampTriggerType.LastRecvPeriod) : (0);
            Config.AutoTimeStampValue_LastRecvPeriod = Num_Trigger_RecvPeriod.Value;
        }
    }
}
