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
        public enum Argument
        {
            Size,
        }

        public enum Result
        {
            Value,
        }

        public Action_RandomHexText()
        {
            RegisterArgument(Argument.Size.ToString(), typeof(int), null);
        }

        public Action_RandomHexText(int size) : this()
        {
            SetArgumentValue(Argument.Size.ToString(), size);
        }

        protected override void OnExecStart()
        {
            var size = (int)GetArgumentValue(Argument.Size.ToString());
            var data = new byte[size];

            (new Random()).NextBytes(data);

            SetResult(ActionResultType.Success, new [] {
                new ActionParam(Result.Value.ToString(), typeof(string), HexTextEncoder.ToHexText(data))
            });
        }
    }
}
