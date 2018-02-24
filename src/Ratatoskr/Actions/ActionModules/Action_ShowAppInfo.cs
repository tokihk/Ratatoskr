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
    internal sealed class Action_ShowAppInfo : ActionObject
    {
        public Action_ShowAppInfo()
        {
        }

        protected override void OnExecStart()
        {
            FormUiManager.ShowAppInfo();

            SetResult(ActionResultType.Success, null);
        }
    }
}
