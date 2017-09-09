using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_TextOut : ActionObject
    {
        public enum Argument
        {
            Text,
        }

        public Action_TextOut()
        {
            RegisterArgument(Argument.Text.ToString(), null, null);
        }

        public Action_TextOut(string text) : this()
        {
            SetArgumentValue(Argument.Text.ToString(), text);
        }

        protected override void OnExecStart()
        {
            var text_obj = (object)null;

            text_obj = GetArgumentValue(Argument.Text.ToString());

            if (text_obj == null) {
                SetResult(ActionResultType.Error_Argument, null);
                return;
            }

            var text_data = (string)null;

            if (text_obj.GetType() == typeof(DateTime)) {
                text_data = ((DateTime)text_obj).ToString("yyyy-MM-dd HH:mm:ss.fff");
            } else if (text_obj.GetType() == typeof(TimeSpan)) {
                text_data = ((TimeSpan)text_obj).TotalMilliseconds.ToString();
            } else {
                text_data = text_obj.ToString();
            }

            /* コメント出力 */
            GatePacketManager.SetComment(text_data);

            SetResult(ActionResultType.Success, null);
        }
    }
}
