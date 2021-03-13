using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ratatoskr.General.BinaryText
{
    public static class BinaryTextCompiler
    {
        public static byte[] Build(string exp_text)
        {
            /* 式オブジェクトに変換 */
            return (BinaryTextParser.Parse(exp_text));
        }
    }
}
