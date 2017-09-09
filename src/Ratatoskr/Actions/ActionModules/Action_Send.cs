using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_Send : ActionObject
    {
        public enum Argument
        {
            Gate,
            Data,
        }

        public Action_Send()
        {
            RegisterArgument(Argument.Gate.ToString(), typeof(string), null);
            RegisterArgument(Argument.Data.ToString(), typeof(string), null);
        }

        public Action_Send(string gate, string data) : this()
        {
            SetArgumentValue(Argument.Gate.ToString(), gate);
            SetArgumentValue(Argument.Data.ToString(), data);
        }

        protected override bool OnArgumentCheck()
        {
            /* data */
            if (HexTextEncoder.ToByteArray(GetArgumentValue(Argument.Data.ToString()) as string) == null) {
                return (false);
            }
            
            return (true);
        }

        protected override void OnExecStart()
        {
            /* 宛先取得 */
            var gate = GetArgumentValue(Argument.Gate.ToString()) as string;

            /* データ取得 */
            var data_bin = HexTextEncoder.ToByteArray(GetArgumentValue(Argument.Data.ToString()) as string);

            /* 送信先ゲート取得 */
            var gates = GateManager.FindGateObjectFromWildcardAlias(gate);

            /* 送信実行 */
            foreach (var obj in gates) {
                obj.SendRequest(data_bin);
            }

            SetResult(ActionResultType.Success, null);
        }
    }
}
