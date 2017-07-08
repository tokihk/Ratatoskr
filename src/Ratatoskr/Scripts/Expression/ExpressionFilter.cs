using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Scripts.Expression
{
    internal sealed class ExpressionFilter
    {
        public static ExpressionFilter Build(string exp_text, ExpressionParameter param)
        {
            /* 式オブジェクトに変換 */
            var exp = ExpressionCompiler.Compile(null, exp_text, param);

            if (exp == null)return (null);

            return (new ExpressionFilter(exp));
        }


        private ExpressionObject exp_ = null;


        private ExpressionFilter(ExpressionObject exp)
        {
            exp_ = exp;
        }

        public ExpressionCallStack CallStack { get; set; } = new ExpressionCallStack();

        public bool Input(PacketObject packet)
        {
            /* 式が無効の場合は有効パケットとして扱う */
            if (exp_ == null)return (true);

            /* カレントパケット更新 */
            CallStack.CurrentPacket = packet;

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
