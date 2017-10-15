﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Gate;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_TimeStamp : ActionObject
    {
        public Action_TimeStamp()
        {
        }

        protected override void OnExecStart()
        {
            GatePacketManager.SetTimeStamp(ConfigManager.Language.MainMessage.TimeStampManual.Value);

            SetResult(ActionResultType.Success, null);
        }
    }
}
