using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate.PacketAutoSave;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_AutoPacketSave : ActionObject
    {
        public enum Argument
        {
            Value,
        }

        public enum Result
        {
            State,
        }

        public Action_AutoPacketSave()
        {
            RegisterArgument(Argument.Value.ToString(), typeof(bool), null);
        }

        public Action_AutoPacketSave(bool value) : this()
        {
            SetArgumentValue(Argument.Value.ToString(), value);
        }

        protected override void OnExecStart()
        {
            var value = GetArgumentValue(Argument.Value.ToString());

            /* 設定処理 */
            if (value != null) {
                /* 設定値更新 */
                ConfigManager.User.Option.AutoSave.Value = (bool)value;

                /* 自動保存状態更新 */
                PacketAutoSaveManager.Update();
            }

            SetResult(
                ActionResultType.Success,
                new [] {
                    new ActionParam(Result.State.ToString(), typeof(bool), ConfigManager.User.Option.AutoSave.Value) });
        }

        protected override void OnExecComplete()
        {
            FormUiManager.MainFrameMenuBarUpdate();
        }
    }
}
