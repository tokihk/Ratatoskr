using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions
{
    internal sealed class ActionParam
    {
        public ActionParam(string name, Term value, Type value_type)
        {
            Name = name;
            Value = value;
            ValueType = value_type;
        }

        public string Name      { get; }
        public Term   Value     { get; set; }
        public Type   ValueType { get; }
    }
}
