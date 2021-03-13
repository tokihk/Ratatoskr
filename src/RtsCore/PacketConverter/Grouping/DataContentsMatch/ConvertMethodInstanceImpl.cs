using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketConverters.Grouping;
using RtsCore.Packet;
using RtsCore.Utility;

namespace Ratatoskr.PacketConverters.Grouping.DataContentsMatch
{
    internal class ConvertMethodInstanceImpl : ConvertMethodInstance
    {
        private class PatternMatchObject
        {
            private byte[] match_data_ = null;
            private int match_size_ = 0;


            public PatternMatchObject(byte[] match_data)
            {
                match_data_ = match_data;
            }

            public void Reset()
            {
                match_size_ = 0;
            }

            public bool Input(byte data)
            {
                if (match_size_ >= match_data_.Length) {
                    match_size_ = 0;
                }

                if (data == match_data_[match_size_]) {
                    match_size_++;
                } else if (data == match_data_[0]) {
                    match_size_ = 1;
                } else {
                    match_size_ = 0;
                }

                return (match_size_ >= match_data_.Length);
            }
        }


        private PatternMatchObject[] match_objs_ = null;

        private PacketBuilder packet_busy_ = null;
        private PacketObject  packet_last_ = null;


        public ConvertMethodInstanceImpl(ConvertMethodProperty prop)
        {
            /* パターンコードを生成 */
            match_objs_ = CreateMatchObjects(prop.Pattern.Value);
        }

        private PatternMatchObject[] CreateMatchObjects(string pattern)
        {
            /* パターンコードからパターン一覧を作成 */
            var patterns = HexTextEncoder.ToByteArrayMap(pattern);

            if (patterns == null) return (null);

            /* マッチオブジェクトに変換 */
            var match_objs = new List<PatternMatchObject>();

            foreach (var pattern_data in patterns) {
                match_objs.Add(new PatternMatchObject(pattern_data));
            }

            return (match_objs.ToArray());
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* パターンが設定されていない場合はスルー */
            if ((match_objs_ == null) || (match_objs_.Length == 0)) {
                output.Add(input);
                return;
            }

            /* 収集開始 */
            if (packet_busy_ == null) {
                packet_busy_ = new PacketBuilder(input);

                foreach (var obj in match_objs_) {
                    obj.Reset();
                }
            }

            /* 最終受信パケットを記憶 */
            packet_last_ = input;

            var packet_new = (PacketObject)null;
            var match_ok = false;

            /* データ収集 */
            foreach (var data in input.Data) {
                /* 仮想パケットにデータを追加 */
                packet_busy_.AddData(data);

                /* 全マッチオブジェクトにデータセット */
                foreach (var obj in match_objs_) {
                    if (obj.Input(data)) {
                        match_ok = true;
                        break;
                    }
                }

                /* どれか１つでもパターンが一致すればOK */
                if (!match_ok) continue;

                /* 全マッチオブジェクトを初期化 */
                match_ok = false;
                foreach (var obj in match_objs_) {
                    obj.Reset();
                }

                /* パケット生成 */
                packet_new = packet_busy_.Compile(packet_last_);
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
