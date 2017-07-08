using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Forms;
using Ratatoskr.Gate;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_EventClear : ActionObject
    {
        public Action_EventClear()
        {
        }

        protected override ExecState OnExecStart()
        {
            ClearEvent();

            return (ExecState.Complete);
        }

        private delegate void ClearEventDelegate();
        private void ClearEvent()
        {
            if (FormUiManager.InvokeRequired()) {
                FormUiManager.Invoke(new ClearEventDelegate(ClearEvent));
                return;
            }

            GatePacketManager.ClearPacket();
        }
    }
}
