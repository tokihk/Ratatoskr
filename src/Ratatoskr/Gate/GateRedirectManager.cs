using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate
{
    internal static class GateRedirectManager
    {
        private static Dictionary<string, HashSet<GateObject>> redirect_map_ = null;


        public static void Startup()
        {
            GatePacketManager.EventPacketEntried += GatePacketManager_EventPacketEntried;
        }

        public static void Shutdown()
        {
        }

        public static void Poll()
        {
        }

        public static void LoadConfig()
        {
            /* リダイレクトマップを初期化 */
            redirect_map_ = new Dictionary<string, HashSet<GateObject>>();

            /* 設定ファイルに従って読み込みを行う */
            foreach (var config in ConfigManager.User.GateRedirectList.Value) {
                /* 無効設定の場合は無視 */
                if (!config.Enable)continue;

                /* 入力ゲートパターンから実際のゲートに変換する */
                var gates_input = GateManager.FindGateObjectFromWildcardAlias(config.Input);
                var gates_output = GateManager.FindGateObjectFromWildcardAlias(config.Output);

                /* 該当するゲートがなければ無視 */
                if ((gates_input == null) || (gates_input.Length == 0))continue;
                if ((gates_output == null) || (gates_output.Length == 0))continue;

                foreach (var gate_input in gates_input) {
                    /* ノードが存在しない場合は新規作成 */
                    if (!redirect_map_.ContainsKey(gate_input.Alias)) {
                        redirect_map_[gate_input.Alias] = new HashSet<GateObject>();
                    }

                    /* ノードに追加 */
                    foreach (var gate_output in gates_output) {
                        redirect_map_[gate_input.Alias].Add(gate_output);
                    }
                }
            }
        }

        private static void RedirectPacket(PacketObject packet)
        {
            var gates = (HashSet<GateObject>)null;

            /* リダイレクトマップから転送情報を読込 */
            if (!redirect_map_.TryGetValue(packet.Alias, out gates)) {
                return;
            }

            /* リダイレクト実行 */
            foreach (var gate in gates) {
                gate.RedirectPacket(packet);
            }
        }

        private static void GatePacketManager_EventPacketEntried(IEnumerable<PacketObject> packets)
        {
            foreach (var packet in packets) {
                RedirectPacket(packet);
            }
        }
    }
}
