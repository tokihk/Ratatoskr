using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Scripts.Expression
{
    internal sealed class ExpressionCommand
    {
        public static ExpressionCommand Build(string exp_text, ExpressionParameter param)
        {
            /* 式オブジェクトに変換 */
            var exp = ExpressionCompiler.Compile(null, exp_text, param);

            if (exp == null)return (null);

            return (new ExpressionCommand(exp));
        }


        private ExpressionObject exp_ = null;


        private ExpressionCommand(ExpressionObject exp)
        {
            exp_ = exp;
        }

        public bool Exec()
        {
            return (Exec(new Parser.ExpressionCallStack()));
        }

        public bool Exec(Parser.ExpressionCallStack cs)
        {
            if (   (cs == null)
                || (exp_ == null)
            ) {
                return (true);
            }

            var hittest = false;

            if (exp_.HitTest(cs, out hittest)) {
                return (hittest);
            } else {
                return (false);
            }
        }
    }
}
