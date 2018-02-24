using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.AutoTimeStamp
{
    internal sealed class AutoTimeStampManager
    {
        private static bool enable_recv_period_ = false;

        private static Stopwatch sw_recv_period_ = new Stopwatch();


        public static void Poll()
        {
            UpdatePoll();

            if (enable_recv_period_) {
                AutoTimeStampPoll();
            }
        }

        private static void UpdatePoll()
        {
            var enable = false;

            enable =  (ConfigManager.User.Option.AutoTimeStamp.Value)
                   && (ConfigManager.User.Option.AutoTimeStampTrigger.Value.HasFlag(
                            AutoTimeStampTriggerType.LastRecvPeriod));

            if (enable_recv_period_ != enable) {
                enable_recv_period_ = enable;
                if (!enable) {
                    sw_recv_period_.Stop();
                }
            }
        }

        private static void AutoTimeStampPoll()
        {
            if (   (!sw_recv_period_.IsRunning)
                || (sw_recv_period_.ElapsedMilliseconds < ConfigManager.User.Option.AutoTimeStampValue_LastRecvPeriod.Value)
            ) {
                return;
            }

            GatePacketManager.SetTimeStamp(ConfigManager.Language.MainMessage.TimeStampAuto.Value);

            /* タイムスタンプの挿入でタイマーが開始してしまうので、必ずタイムスタンプ挿入の後に実行 */
            sw_recv_period_.Stop();
        }

        public static void ClearPacket()
        {
            sw_recv_period_.Stop();
        }

        public static void OnEntryPacket(PacketObject packet)
        {
            if (enable_recv_period_) {
                sw_recv_period_.Restart();
            }
        }
    }
}
