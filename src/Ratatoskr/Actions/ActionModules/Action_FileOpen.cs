using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_FileOpen : ActionObject
    {
        public Action_FileOpen()
        {
            InitParameter<Term_Text>("path");
        }

        protected override ExecState OnExecStart()
        {
            var value_path = GetParameter<Term_Text>("path");

            FileOpen((value_path != null) ? (value_path.Value) : (null));

            return (ExecState.Complete);
        }

        private delegate void FileOpenDelegate(string path);
        private void FileOpen(string path)
        {
            if (FormUiManager.InvokeRequired()) {
                FormUiManager.Invoke(new FileOpenDelegate(FileOpen), path);
                return;
            }

            if (path != null) {
                FormUiManager.FileOpen(new [] { path }, null);
            } else {
                FormUiManager.FileOpen();
            }
        }
    }
}
