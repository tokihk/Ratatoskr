using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Debugger;
using Ratatoskr.Device;

namespace Ratatoskr.Gate
{
    internal static class GateManager
    {
        private static List<GateObject> gate_list_ = new List<GateObject>();

        
		public static uint GateCount
		{
			get { return ((uint)gate_list_.Count); }
		}

        public static GateObject[] GetGateList()
        {
            lock (gate_list_) {
                return (gate_list_.ToArray());
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
            lock (gate_list_) {
                return (gate_list_.FindAll(gate => (gate.DeviceClassID != Guid.Empty) && (regex.IsMatch(gate.Alias))).ToArray());
            }
        }

        public static GateObject[] FindGateObjectFromAlias(string alias)
        {
            lock (gate_list_) {
                return (gate_list_.FindAll(gate => (gate.DeviceClassID != Guid.Empty) && (gate.Alias == alias)).ToArray());
            }
        }

        public static GateObject CreateGateObject(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            var gate = new GateObject(gatep, devconf, devc_id, devp);

            lock (gate_list_) {
                gate_list_.Add(gate);
            }

			DebugManager.MessageOut(DebugEventSender.Gate, String.Format("Gate Added. count = {0}", gate_list_.Count));

            return (gate);
        }

        public static void RemoveAtGateObject(int index)
        {
			GateObject gate;

			lock (gate_list_) {
				if ((index < 0) || (index >= gate_list_.Count))return;

				gate = gate_list_[index];

				gate_list_.RemoveAt(index);
			}

			gate.Dispose();

			DebugManager.MessageOut(DebugEventSender.Gate, String.Format("Gate Removed. count = {0}", gate_list_.Count));
        }
    }
}
