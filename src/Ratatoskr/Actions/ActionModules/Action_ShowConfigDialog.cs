using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.Expression.Terms;
using Ratatoskr.Generic;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_ShowConfigDialog : ActionObject
    {
        public Action_ShowConfigDialog()
        {
        }

        protected override ExecState OnExecPoll()
        {
            ShowDialog();

            return (ExecState.Complete);
        }

        private delegate void ShowDialogDelegate();
        private void ShowDialog()
        {
            if (FormUiManager.InvokeRequired()) {
                FormUiManager.Invoke(new ShowDialogDelegate(ShowDialog));
                return;
            }

            var config = ClassUtil.Clone(ConfigManager.User.Option);

            if (config == null)return;

            var dialog = new Forms.OptionForm.OptionForm(config);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ConfigManager.User.Option = config;
        }
    }
}
