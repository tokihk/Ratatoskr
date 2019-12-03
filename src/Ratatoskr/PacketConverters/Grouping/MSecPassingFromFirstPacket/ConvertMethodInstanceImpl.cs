using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketConverters.Grouping;
using RtsCore.Packet;
using RtsCore.Utility;

namespace Ratatoskr.PacketConverters.Grouping.MSecPassingFromFirstPacket
{
    internal class ConvertMethodInstanceImpl : ConvertMethodInstance
    {
        private PacketBuilder packet_busy_ = null;
        private PacketObject packet_last_ = null;

        private DateTime dt_base_ = DateTime.MaxValue;
        private int match_interval_ = -1;


        public ConvertMethodInstanceImpl(ConvertMethodProperty prop)
        {
            match_interval_ = (int)prop.Interval.Value;
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* パターンが正しくない場合はスルー */
            if (match_interval_ < 0) {
                output.Add(input);
            }

            /* 収集開始 */
            if (packet_busy_ == null) {
                packet_busy_ = new PacketBuilder(input);

                /* 受信開始時刻を記憶 */
                dt_base_ = input.MakeTime;
            }

            /* 最終受信パケットを記憶 */
            packet_last_ = input;

            /* データ収集 */
            packet_busy_.AddData(input.Data);

            /* 入力パケット時刻が基準時刻を超えていない場合は無視 */
            if (input.MakeTime < dt_base_) {
                return;
            }

            /* 時間が経過したかどうかを確認 */
            if ((input.MakeTime - dt_base_).TotalMilliseconds < match_interval_) {
                return;
            }

            /* 最新パケットで生成 */
            var packet_new = packet_busy_.Compile(packet_last_);

            if (packet_new != null) {
                output.Add(packet_new);
            }

            packet_busy_ = null;
        }

        protected override void OnInputBreak(ref List<PacketObject> output)
        {
            if (packet_busy_ != null) {
                if (packet_busy_.DataLength > 0) {
                    var packet_new = packet_busy_.Compile(packet_last_);

                    if (packet_new != null) {
                        output.Add(packet_new);
                    }
                }

                packet_busy_ = null;
            }
        }

        protected override void OnInputPoll(ref List<PacketObject> output)
        {
            if (packet_busy_ != null) {
                var dt_now = DateTime.UtcNow;

                if (   (dt_now < dt_base_)
                    || ((dt_now - dt_base_).TotalMilliseconds < match_interval_)
                ) {
                    return;
                }

                if (packet_busy_.DataLength > 0) {
                    var packet_new = packet_busy_.Compile(packet_last_);

                    if (packet_new != null) {
                        output.Add(packet_new);
                    }
                }

                packet_busy_ = null;
            }
        }
    }
}
