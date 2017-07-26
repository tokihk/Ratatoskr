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
        public Action_AutoPacketSave()
        {
            InitParameter<Term_Bool>("value");
            InitResult<Term_Bool>("value");
        }

        public Action_AutoPacketSave(bool value) : this()
        {
            SetParameter("value", new Term_Bool(value));
        }

        protected override ExecState OnExecStart()
        {
            if (!ParameterCheck()) {
                return (ExecState.Complete);
            }

            var value = GetParameter<Term_Bool>("value");

            if (value != null) {
                /* 設定値更新 */
                ConfigManager.User.Option.AutoSave.Value = value.Value;

                /* 自動保存状態更新 */
                PacketAutoSaveManager.Update();
            }

            /* === 戻り値 === */
            SetResult("value", new Term_Bool(ConfigManager.User.Option.AutoSave.Value));

            return (ExecState.Complete);
        }

        protected override void OnExecComplete()
        {
            FormUiManager.MainFrameMenuBarUpdate();
        }
    }
}
