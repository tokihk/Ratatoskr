using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratatoskr.General.Packet.Filter
{
    public sealed class PacketFilterController
    {
        public static PacketFilterController Build(string exp_text)
        {
            /* 式オブジェクトに変換 */
            var obj = PacketFilterObject.Compile(exp_text);

            if (obj == null)return (null);

            return (new PacketFilterController(obj));
        }


        private PacketFilterObject exp_ = null;


        private PacketFilterController(PacketFilterObject exp)
        {
            exp_ = exp;
        }

        public string ExpressionText
        {
            get { return (exp_.ExpressionText); }
        }

        public PacketFilterCallStack CallStack { get; set; } = new PacketFilterCallStack();

        public bool Input(PacketObject packet)
        {
            /* 式が無効の場合は有効パケットとして扱う */
            if (exp_ == null)return (true);

            /* カレントパケット更新 */
            CallStack.LastPacket = packet;

            /* ヒットテスト */
            var hitstate = false;

            if (!exp_.HitTest(CallStack, out hitstate)) {
                /* --- 解析失敗時は有効パケットとして扱う --- */
                hitstate = true;
            }

            return (hitstate);
        }
    }
}
