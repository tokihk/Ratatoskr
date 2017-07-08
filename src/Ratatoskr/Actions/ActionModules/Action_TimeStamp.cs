using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_TimeStamp : ActionObject
    {
        public Action_TimeStamp()
        {
        }

        protected override ExecState OnExecStart()
        {
            GatePacketManager.SetTimeStamp();

            return (ExecState.Complete);
        }
    }
}
