﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.PacketAutoSave
{
    internal sealed class PacketAutoSaveObject_Interval : PacketAutoSaveObject
    {
        private Stopwatch sw_change_ = new Stopwatch();


        public PacketAutoSaveObject_Interval()
        {
        }

        public override void Output(IEnumerable<PacketObject> packets)
        {
            /* 監視タイマーが動作していないときは受信時に開始 */
            if (!sw_change_.IsRunning) {
                sw_change_.Restart();
            }

            /* 指定時間が経過したときは新しいファイルに変更 */
            if (sw_change_.Elapsed.TotalMinutes >= (double)ConfigManager.User.Option.AutoSaveValue_Interval.Value) {
                ChangeNewFile();
            }

            /* 出力 */
            base.Output(packets);
        }
    }
}
