using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_PrintOut : ActionObject
    {
        public Action_PrintOut()
        {
            InitParameter<Term_Void>("output");
            InitResult<Term_Void>("output");
        }

        protected override ExecState OnExecStart()
        {
            var output = (string)null;

            /* Term_Double */
            if (output == null) {
                var param = GetParameter<Term_Double>("output");

                if (param != null) {
                    output = param.Value.ToString();
                    SetResult("output", param);
                }
            }

            /* Term_Text */
            if (output == null) {
                var param = GetParameter<Term_Text>("output");

                if (param != null) {
                    output = param.Value.ToString();
                    SetResult("output", param);
                }
            }

            /* Term_Bool */
            if (output == null) {
                var param = GetParameter<Term_Bool>("output");

                if (param != null) {
                    output = param.Value.ToString();
                    SetResult("output", param);
                }
            }

            /* Term_DateTime */
            if (output == null) {
                var param = GetParameter<Term_DateTime>("output");

                if (param != null) {
                    output = param.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    SetResult("output", param);
                }
            }

            /* Term_TimeSpan */
            if (output == null) {
                var param = GetParameter<Term_TimeSpan>("output");

                if (param != null) {
                    output = param.Value.TotalMilliseconds.ToString();
                    SetResult("output", param);
                }
            }

            /* コメント出力 */
            if (output != null) {
                GatePacketManager.SetComment(output);
            }

            return (ExecState.Complete);
        }
    }
}
