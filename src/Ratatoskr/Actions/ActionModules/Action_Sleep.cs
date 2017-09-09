using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_Sleep : ActionObject
    {
        public enum Argument
        {
            TimeMS,
        }

        
        private Stopwatch timeout_timer_ = new Stopwatch();
        private int       timeout_value_ = 0;


        public Action_Sleep()
        {
            RegisterArgument(Argument.TimeMS.ToString(), typeof(int), null);
        }

        public Action_Sleep(int time_ms) : this()
        {
            SetArgumentValue(Argument.TimeMS.ToString(), time_ms);
        }

        protected override void OnExecStart()
        {
            /* 停止時間取得 */
            timeout_value_ = (int)GetArgumentValue(Argument.TimeMS.ToString());

            /* 計測開始 */
            timeout_timer_.Start();

            if (timeout_timer_.ElapsedMilliseconds > timeout_value_) {
                SetResult(ActionResultType.Success, null);
            }
        }

        protected override void OnExecPoll()
        {
            System.Threading.Thread.Sleep(1);

            if (timeout_timer_.ElapsedMilliseconds > timeout_value_) {
                SetResult(ActionResultType.Success, null);
            }
        }
    }
}
