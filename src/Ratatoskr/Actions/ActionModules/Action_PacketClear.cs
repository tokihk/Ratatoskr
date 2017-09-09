using System;
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
            ClearEvent();

            SetResult(ActionResultType.Success, null);
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
