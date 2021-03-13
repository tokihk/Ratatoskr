using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtsCore.Framework.PacketFilter
{
    public sealed class PacketFilterObject
    {
        public static PacketFilterObject Compile(string exp_text)
        {
            /* 式オブジェクトに変換 */
            return (PacketFilterParser.Parse(exp_text));
        }


        private List<object> args_ = new List<object>();


        public PacketFilterObject(string exp)
        {
            ExpressionText = exp;
        }

        public string ExpressionText { get; }

        public void Add(object item)
        {
            args_.Add(item);
        }

        public void AddInterrupt(object item)
        {
            args_.Insert(Math.Max(0, args_.Count - 1), item);
        }

        public bool HitTest(PacketFilterCallStack cs, out bool hitstate)
        {
            hitstate = false;       // ヒット結果 初期値=非該当

            if (args_.Count == 0)return (false);

            if (cs.PrevPacket == null) {
                cs.PrevPacket = cs.LastPacket;
            }

            var term_stack = new Stack<Terms.Term>();

            foreach (var arg in args_) {
                /* --- 項 --- */
                if (arg is Terms.Term arg_term) {
                    term_stack.Push(arg_term);

                /* --- 演算子 --- */
                } else if (   (arg is Tokens arg_token)
                           && (term_stack.Count >= 1)    // 計算用パラメータが存在するかどうか
                ) {
                    var term_left = (Terms.Term)null;
                    var term_right = (Terms.Term)null;

                    /* 項が2個以上余ってるときのみ右辺を取得 */
                    if (term_stack.Count >= 2) {
                        term_right = term_stack.Pop();
                    }

                    /* 項が1個以上余ってるときのみ左辺を取得 */
                    if (term_stack.Count >= 1) {
                        term_left = term_stack.Pop();
                    }

                    /* 計算実行 */
                    var term_result = term_left.Exec(cs, arg_token, term_right);

                    /* 計算ができない場合は構文エラー */
                    if (term_result == null)return (false);

                    /* --- 計算結果を次の計算対象に含める --- */
                    term_stack.Push(term_result);
                }
            }

            /* --- 項が2個以上余っている場合は構文エラー --- */
            if (term_stack.Count > 1)return (false);

            /* 最終オブジェクトの真偽を結果として返す */
            hitstate = term_stack.First().ToBool(cs);

            /* === コールスタック更新 === */
            cs.PrevPacket = cs.LastPacket;

            return (true);
        }
    }
}
