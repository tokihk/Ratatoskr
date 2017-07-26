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
        private Stopwatch timeout_timer_ = new Stopwatch();
        private int       timeout_value_ = 0;


        public Action_Sleep()
        {
            InitParameter<Term_Double>("timeout");
        }

        protected override ExecState OnExecStart()
        {
            /* 停止時間取得 */
            timeout_value_ = (int)GetParameter<Term_Double>("timeout").Value;

            /* 計測開始 */
            timeout_timer_.Start();

            return ((timeout_timer_.ElapsedMilliseconds > (int)timeout_value_) ? (ExecState.Complete) : (ExecState.Busy));
        }

        protected override ExecState OnExecPoll()
        {
            System.Threading.Thread.Sleep(1);

            return ((timeout_timer_.ElapsedMilliseconds > (int)timeout_value_) ? (ExecState.Complete) : (ExecState.Busy));
        }
    }
}
