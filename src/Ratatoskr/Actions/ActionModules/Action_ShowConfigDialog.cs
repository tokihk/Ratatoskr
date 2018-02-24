using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_ShowConfigDialog : ActionObject
    {
        public Action_ShowConfigDialog()
        {
        }

        protected override void OnExecStart()
        {
            FormUiManager.ShowOptionDialog();

            SetResult(ActionResultType.Success, null);
        }
    }
}
