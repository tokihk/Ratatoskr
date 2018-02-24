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
    internal sealed class Action_PacketSave : ActionObject
    {
        public enum Argument
        {
            OverWrite,
            ConvertEnable,
        }

        public enum Result
        {
            State,
        }

        public Action_PacketSave()
        {
            RegisterArgument(Argument.OverWrite.ToString(), typeof(bool), null);
            RegisterArgument(Argument.ConvertEnable.ToString(), typeof(bool), null);
        }

        public Action_PacketSave(bool over_write, bool filter) : this()
        {
            SetArgumentValue(Argument.OverWrite.ToString(), over_write);
            SetArgumentValue(Argument.ConvertEnable.ToString(), filter);
        }

        protected override void OnExecStart()
        {
            var over_write = (bool)GetArgumentValue(Argument.OverWrite.ToString());
            var convert_enable = (bool)GetArgumentValue(Argument.ConvertEnable.ToString());

            FormUiManager.PacketSave(over_write, convert_enable);

            SetResult(ActionResultType.Success, null);
        }
    }
}
