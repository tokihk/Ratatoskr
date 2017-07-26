using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_RandomInt : ActionObject
    {
        public Action_RandomInt()
        {
            InitParameter<Term_Double>("max");

            InitResult<Term_Double>("value");
        }

        protected override ExecState OnExecStart()
        {
            var param_max = GetParameter<Term_Double>("max");

            SetResult("value", new Term_Double((new Random()).Next((int)param_max.Value)));

            return (ExecState.Complete);
        }
    }
}
