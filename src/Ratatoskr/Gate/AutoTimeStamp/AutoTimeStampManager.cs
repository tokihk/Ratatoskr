using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.System;
using Ratatoskr.General.Packet;

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

            enable =  (ConfigManager.System.AutoTimeStamp.Enable.Value)
                   && (ConfigManager.System.AutoTimeStamp.Trigger.Value.HasFlag(
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
                || (sw_recv_period_.ElapsedMilliseconds < ConfigManager.System.AutoTimeStamp.Value_LastRecvPeriod.Value)
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
