using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_LastActionResult : ActionObject
    {
        public Action_LastActionResult()
        {
            InitParameter<Term_Void>("target");

            InitResult<Term_Void>("value");
        }

        protected override ExecState OnExecStart()
        {
            if (   (CallStack == null)
                || (CallStack.LastAction == null)
            ) {
                return (ExecState.Complete);
            }

            /* 戻り値のインデックスでフィルタリング */
            var param_index = GetParameter<Term_Double>("target");

            if (param_index != null) {
                SetResult("value", CallStack.LastAction.GetResult((uint)param_index.Value));
                return (ExecState.Complete);
            }

            /* 戻り値名でフィルタリング */
            var param_name = GetParameter<Term_Text>("target");

            if (param_name != null) {
                SetResult("value", CallStack.LastAction.GetResult(param_name.Value));
                return (ExecState.Complete);
            }

            /* 全ての戻り値を返す */
            var array = new Term_Array();

            foreach (var result in CallStack.LastAction.GetAllResult()) {
                array.AddValue(result.Key, result.Value);
            }

            SetResult("value", array);

            return (ExecState.Complete);
        }
    }
}
