using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions
{
    internal sealed class ActionParam
    {
        public ActionParam(string name, Type value_type, object value)
        {
            Name = name;
            ValueType = value_type;
            Value = value;
        }

        public string Name      { get; }
        public Type   ValueType { get; }
        public object Value     { get; set; }
    }
}
