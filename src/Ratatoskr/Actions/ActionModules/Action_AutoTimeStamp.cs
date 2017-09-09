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
    internal sealed class Action_AutoTimeStamp : ActionObject
    {
        public enum Argument
        {
            Value,
        }

        public enum Result
        {
            State,
        }

        public Action_AutoTimeStamp()
        {
            RegisterArgument(Argument.Value.ToString(), typeof(bool), null);
        }

        public Action_AutoTimeStamp(bool value) : this()
        {
            SetArgumentValue(Argument.Value.ToString(), value);
        }

        protected override void OnExecStart()
        {
            var value = GetArgumentValue(Argument.Value.ToString());

            /* 設定処理 */
            if (value != null) {
                ConfigManager.User.Option.AutoTimeStamp.Value = (bool)value;
            }

            SetResult(
                ActionResultType.Success,
                new [] {
                    new ActionParam(Result.State.ToString(), typeof(bool), ConfigManager.User.Option.AutoTimeStamp.Value) });
        }

        protected override void OnExecComplete()
        {
            FormUiManager.MainFrameMenuBarUpdate();
        }
    }
}
