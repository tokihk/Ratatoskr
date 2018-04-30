using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.AutoPacketSave
{
    internal sealed class AutoPacketSaveObject_PacketCount : AutoPacketSaveObject
    {
        private int count_ = 0;


        public AutoPacketSaveObject_PacketCount()
        {
        }

        protected override void OnOutput(IEnumerable<PacketObject> packets)
        {
            while ((packets != null) && (packets.Count() > 0)) {
                /* 現在のファイルに出力するパケット数を取得 */
                var copy_count = (int)Math.Min(
                                        packets.Count(),
                                        (long)(Math.Max(
                                                    0, ConfigManager.System.AutoPacketSave.SaveValue_PacketCount.Value - count_)));

                count_ += copy_count;

                /* for Debug */
                System.Diagnostics.Debug.WriteLine(string.Format("In:{0}, CopyCount:{1}, TotalCount:{2}", packets.Count(), copy_count, count_));

                /* 分割出力 */
                if (copy_count < packets.Count()) {
                    /* パケット出力 */
                    WritePacket(packets.Take(copy_count));

                    /* パケットリスト更新 */
                    packets = packets.Skip(copy_count);

                    /* ファイル変更 */
                    ChangeNewFile();
                    count_ = 0;

                /* 全出力 */
                } else {
                    WritePacket(packets);
                    packets = null;
                }
            }
        }
    }
}
