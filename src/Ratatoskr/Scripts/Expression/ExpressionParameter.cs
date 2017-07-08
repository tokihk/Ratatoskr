using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Scripts.Expression
{
    internal sealed class ExpressionParameter
    {
        public Dictionary<string, string> Params { get; } = new Dictionary<string, string>();
    }
}
