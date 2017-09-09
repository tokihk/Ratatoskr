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
        public enum Argument
        {
            Max,
        }

        public enum Result
        {
            Value,
        }

        public Action_RandomInt()
        {
            RegisterArgument(Argument.Max.ToString(), typeof(int), null);
        }

        public Action_RandomInt(int value_max) : this()
        {
            SetArgumentValue(Argument.Max.ToString(), value_max);
        }

        protected override void OnExecStart()
        {
            var value_max = (int)GetArgumentValue(Argument.Max.ToString());

            SetResult(ActionResultType.Success, new [] {
                new ActionParam(Result.Value.ToString(), typeof(int), (new Random()).Next(value_max))
            });
        }
    }
}
