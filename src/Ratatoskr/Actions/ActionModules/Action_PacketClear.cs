﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Forms;
using Ratatoskr.Gate;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_PacketClear : ActionObject
    {
        public Action_PacketClear()
        {
        }

        protected override void OnExecStart()
        {
            GatePacketManager.ClearPacket();

            SetResult(ActionResultType.Success, null);
        }
    }
}
