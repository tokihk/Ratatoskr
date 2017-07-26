using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_RandomHexText : ActionObject
    {
        public Action_RandomHexText()
        {
            InitParameter<Term_Double>("size");

            InitResult<Term_Text>("value");
        }

        protected override ExecState OnExecStart()
        {
            var param_size = GetParameter<Term_Double>("size");

            var data = new byte[(int)param_size.Value];

            (new Random()).NextBytes(data);

            SetResult("value", new Term_Text(HexTextEncoder.ToHexText(data)));

            return (ExecState.Complete);
        }
    }
}
