﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Forms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_ScreenUpdate : ActionObject
    {
        public Action_ScreenUpdate()
        {
        }

        protected override ExecState OnExecStart()
        {
            FormTaskManager.RedrawPacketRequest();

            return (ExecState.Complete);
        }
    }
}
