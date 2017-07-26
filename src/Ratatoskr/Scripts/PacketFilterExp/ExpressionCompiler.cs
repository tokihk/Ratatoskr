using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Scripts.PacketFilterExp
{
    internal static class ExpressionCompiler
    {
        private class Converter
        {
            private readonly Regex             regex_macro_ = new Regex(@"\$\{(?<name>[^\}]*)\}", RegexOptions.Compiled);
            private Dictionary<string, string> regex_param_;


            public Converter()
            {
            }

            public string Exec(string text, Dictionary<string, string> param)
            {
                regex_param_ = param;

                var text_prev = text;
                var text_new = text;
                var convert_count = 0;

                try {
                    do {
                        /* 変換前のテキストをバックアップ */
                        text_prev = text_new;

                        /* 変換ブロックを検索 */
                        text_new = regex_macro_.Replace(text_prev, ReplaceTask);

                        /* 変換回数更新 */
                        convert_count++;

                    } while ((text_new != text_prev) && (convert_count < 100));
                } catch { }

                return (text_new);
            }

            private string ReplaceTask(Match match)
            {
                /* 変数を値に変換 */
                var result = "";

                if (!regex_param_.TryGetValue(match.Groups["name"].Captures[0].Value, out result)) {
                    throw new FormatException();
                }

                return (result);
            }
        }

        public static ExpressionObject Compile(ExpressionCallStack cs, string exp_text, ExpressionParameter param)
        {
            /* 式を変換 */
            if (param != null) {
                exp_text = (new Converter()).Exec(exp_text, param.Params);
            }

            /* 式オブジェクトに変換 */
            var exp = ExpressionParser.Parse(exp_text);

            if (exp == null)return (null);

            if (cs == null) {
                cs = new ExpressionCallStack();
            }

            /* 実行モード */
            cs.ExecuteFlag = 0;

            /* ダミーパケット */
            cs.CurrentPacket = new StaticDataPacketObject();

            /* 実行確認 */
            var hittest = false;

            if (!exp.HitTest(cs, out hittest)) {
                return (null);
            }

            return (exp);
        }
    }
}
