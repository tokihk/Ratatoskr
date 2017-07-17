﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_PlayRecord : ActionObject
    {
        public Action_PlayRecord()
        {
            InitParameter<Term_Text>("path");
            InitParameter<Term_Text>("");
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

            var dialog = new Forms.AboutForm.AboutForm();

            dialog.ShowDialog();
        }
    }
}