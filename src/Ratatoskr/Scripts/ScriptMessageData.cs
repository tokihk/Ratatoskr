using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Scripts
{
    internal enum ScriptMessageType
    {
        Emergency,
        Alert,
        Error,
        Warning,
        Notice,
        Informational,
        Debug,
    }

    class ScriptMessageData
    {
        public ScriptMessageData(DateTime dt, ScriptMessageType type, string message)
        {
            CreateTime = dt;
            Type = type;
            Message = message;
        }

        public DateTime          CreateTime { get; }
        public ScriptMessageType Type       { get; }
        public string            Message    { get; }
    }
}
