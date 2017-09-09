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
            ShowDialog();

            SetResult(ActionResultType.Success, null);
        }

        private delegate void ShowDialogDelegate();
        private void ShowDialog()
        {
            if (FormUiManager.InvokeRequired()) {
                FormUiManager.Invoke(new ShowDialogDelegate(ShowDialog));
                return;
            }

            var dialog = new Forms.AboutForm.AboutForm();

            dialog.ShowDialog();
        }
    }
}
