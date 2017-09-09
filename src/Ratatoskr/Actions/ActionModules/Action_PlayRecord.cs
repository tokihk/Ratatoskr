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
    internal sealed class Action_PlayRecord : ActionObject
    {
        public enum Argument
        {
            Path,
            Target,
        }

        public enum ArgumentTarget
        {
            RecvDataOnly,
            SendDataOnly,
            Both,
        }

        public enum Result
        {
            State,
        }

        public Action_PlayRecord()
        {
            RegisterArgument(Argument.Path.ToString(), typeof(string), null);
            RegisterArgument(Argument.Target.ToString(), typeof(string), ArgumentTarget.RecvDataOnly.ToString());
        }

        public Action_PlayRecord(string path, string target) : this()
        {
            SetArgumentValue(Argument.Path.ToString(), path);
            SetArgumentValue(Argument.Target.ToString(), target);
        }

        protected override void OnExecPoll()
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
