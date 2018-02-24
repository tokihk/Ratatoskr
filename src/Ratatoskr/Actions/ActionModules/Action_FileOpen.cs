using System;
using System.Collections.Generic;
using System.IO;
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
        public enum Argument
        {
            Path,
        }

        public Action_FileOpen()
        {
            RegisterArgument(Argument.Path.ToString(), typeof(string), "");
        }

        public Action_FileOpen(string path) : this()
        {
            SetArgumentValue(Argument.Path.ToString(), path);
        }

        protected override void OnExecStart()
        {
            var value_path = GetArgumentValue(Argument.Path.ToString()) as string;

            if (File.Exists(value_path)) {
                FormUiManager.FileOpen(new [] { value_path }, null);
            } else {
                FormUiManager.FileOpen();
            }

            SetResult(ActionResultType.Success, null);
        }
    }
}
