﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.PacketAutoSave
{
    internal sealed class PacketAutoSaveObject_PacketCount : PacketAutoSaveObject
    {
        private int count_ = 0;


        public PacketAutoSaveObject_PacketCount()
        {
        }

        public override void Output(IEnumerable<PacketObject> packets)
        {
            while (packets.Count() > 0) {
                var writer = GetWriter();

                if (writer == null)return;

                /* 現在のファイルに出力するパケット数を取得 */
                var copy_count = (int)Math.Min(
                                        packets.LongCount(),
                                        (long)(Math.Max(
                                                    0, ConfigManager.User.Option.AutoSaveValue_PacketCount.Value - count_)));

                count_ += copy_count;

                /* 分割出力 */
                if (copy_count < packets.Count()) {
                    /* パケット出力 */
                    writer.Write(packets.Take(copy_count));

                    /* パケットリスト更新 */
                    packets = packets.Skip(copy_count);

                    /* ファイル変更 */
                    ChangeNewFile();
                    count_ = 0;

                /* 全出力 */
                } else {
                    writer.Write(packets);
                }
            }
        }
    }
}