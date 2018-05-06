using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts.PacketFilterExp
{
    internal static class ExpressionCompiler
    {
        public static ExpressionObject Compile(string exp_text)
        {
            /* 式オブジェクトに変換 */
            return (ExpressionParser.Parse(exp_text));
        }
    }
}
