using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Utility;

namespace Ratatoskr.Scripts.API
{
    public static class API_Send
    {
        public static void Exec(string gate, string data_text)
        {
            /* データ取得 */
            var data_bin = HexTextEncoder.ToByteArray(data_text);

            if (data_bin == null)return;

            /* 送信先ゲート取得 */
            var gates = GateManager.FindGateObjectFromWildcardAlias(gate);

            if (gates == null)return;

            /* 送信実行 */
            foreach (var obj in gates) {
                obj.SendRequest(data_bin);
            }
        }
    }
}
