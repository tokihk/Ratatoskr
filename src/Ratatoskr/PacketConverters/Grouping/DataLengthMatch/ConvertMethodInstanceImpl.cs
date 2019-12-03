using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketConverters.Grouping;
using RtsCore.Packet;
using RtsCore.Utility;

namespace Ratatoskr.PacketConverters.Grouping.DataLengthMatch
{
    internal class ConvertMethodInstanceImpl : ConvertMethodInstance
    {
        private PacketBuilder packet_busy_ = null;
        private PacketObject  packet_last_ = null;

        private int match_length_ = -1;


        public ConvertMethodInstanceImpl(ConvertMethodProperty prop)
        {
            match_length_ = (int)prop.Length.Value;
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* パターンが正しくない場合は無視 */
            if (match_length_ < 0) {
                output.Add(input);
                return;
            }

            /* 収集開始 */
            if (packet_busy_ == null) {
                packet_busy_ = new PacketBuilder(input);
            }

            /* 最終受信パケットを記憶 */
            packet_last_ = input;

            var packets = new Queue<PacketObject>();
            var packet_new = (PacketObject)null;

            /* データ収集 */
            foreach (var data in input.Data) {
                /* 仮想パケットにデータを追加 */
                packet_busy_.AddData(data);

                /* データ長が指定長以上になるまで無視 */
                if (packet_busy_.DataLength < match_length_) continue;

                /* パケット生成 */
                packet_new = packet_busy_.Compile(input);

                if (packet_new != null) {
                    output.Add(packet_new);
                }

                /* 新しいパケットの収集を開始 */
                packet_busy_ = new PacketBuilder(input);
            }
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
    }
}
