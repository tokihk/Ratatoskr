using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_ShowAppDocument : ActionObject
    {
        public Action_ShowAppDocument()
        {
        }

        protected override void OnExecStart()
        {
            FormUiManager.ShowAppDocument();

            SetResult(ActionResultType.Success, null);
        }
    }
}
