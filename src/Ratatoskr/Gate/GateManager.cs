using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Device;

namespace Ratatoskr.Gate
{
    internal static class GateManager
    {
        private static List<GateObject> gates_ = new List<GateObject>();

        
        public static GateObject[] GetGateList()
        {
            lock (gates_) {
                return (gates_.ToArray());
            }
        }

        private static Regex GetWildcardAliasModule(string alias)
        {
            /* aliasをワイルドカードとして扱うために正規表現に変換する */
            alias = alias.Replace("?", ".");
            alias = alias.Replace("*", ".*?");

            /* 正規表現オブジェクトを生成 */
            return (new Regex("^" + alias + "$"));
        }

        public static GateObject[] FindGateObjectFromWildcardAlias(string alias)
        {
            /* 正規表現オブジェクトを生成 */
            var regex = GetWildcardAliasModule(alias);

            /* スキャン実行 */
            lock (gates_) {
                return (gates_.FindAll(gate => (gate.DeviceClassID != Guid.Empty) && (regex.IsMatch(gate.Alias))).ToArray());
            }
        }

        public static GateObject[] FindGateObjectFromAlias(string alias)
        {
            lock (gates_) {
                return (gates_.FindAll(gate => (gate.DeviceClassID != Guid.Empty) && (gate.Alias == alias)).ToArray());
            }
        }

        public static GateObject CreateGateObject(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            var gate = new GateObject(gatep, devconf, devc_id, devp);

            lock (gates_) {
                gates_.Add(gate);
            }

            return (gate);
        }
    }
}
