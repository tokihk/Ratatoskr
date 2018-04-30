﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Forms;

namespace Ratatoskr.Configs.SystemConfigs
{
    internal enum AutoTimeStampTriggerType
    {
        LastRecvPeriod = 1 << 0,
    }

    [Serializable]
    internal sealed class AutoTimeStampConfig : ConfigHolder
    {
        public BoolConfig                           Enable               { get; } = new BoolConfig(false);
        public EnumConfig<AutoTimeStampTriggerType> Trigger              { get; } = new EnumConfig<AutoTimeStampTriggerType>(AutoTimeStampTriggerType.LastRecvPeriod);
        public IntegerConfig                        Value_LastRecvPeriod { get; } = new IntegerConfig(1000);
    }
}
