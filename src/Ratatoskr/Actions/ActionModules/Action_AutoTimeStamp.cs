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
        public Action_AutoTimeStamp()
        {
            InitParameter<Term_Bool>("value");
            InitResult<Term_Bool>("value");
        }

        public Action_AutoTimeStamp(bool value) : this()
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
                ConfigManager.User.Option.AutoTimeStamp.Value = value.Value;
            }

            /* === 戻り値 === */
            SetResult("value", new Term_Bool(ConfigManager.User.Option.AutoTimeStamp.Value));

            return (ExecState.Complete);
        }

        protected override void OnExecComplete()
        {
            FormUiManager.MainFrameMenuBarUpdate();
        }
    }
}
