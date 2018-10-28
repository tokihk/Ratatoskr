using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Scripts.BinaryCodeBuilder.Parser;

namespace Ratatoskr.Scripts.BinaryCodeBuilder
{
    internal static class BinaryCodeCompiler
    {
        internal static byte[] Run(string exp_text)
        {
            /* 式オブジェクトに変換 */
            return (BinaryCodeBuilderParser.Parse(exp_text));
        }
    }
}
