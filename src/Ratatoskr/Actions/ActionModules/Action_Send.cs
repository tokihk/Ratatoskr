using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_Send : ActionObject
    {
        public Action_Send()
        {
            InitParameter<Term_Text>("gate");
            InitParameter<Term_Text>("data");
        }

        public override bool OnParameterCheck()
        {
            /* data */
            var data_bin = HexTextEncoder.ToByteArray(GetParameter<Term_Text>("data").Value);

            if (data_bin == null) {
                return (false);
            }
            
            return (true);
        }

        protected override ExecState OnExecStart()
        {
            if (!ParameterCheck()) {
                return (ExecState.Complete);
            }

            /* 宛先取得 */
            var gate = GetParameter<Term_Text>("gate");

            /* データ取得 */
            var data = GetParameter<Term_Text>("data");
            var data_bin = HexTextEncoder.ToByteArray(data.Value);

            /* 送信先ゲート取得 */
            var gates = GateManager.FindGateObjectFromWildcardAlias(gate.Value);

            /* 送信実行 */
            gates.AsParallel().ForAll(obj => obj.SendDataPush(data_bin));

            return (ExecState.Complete);
        }
    }
}
