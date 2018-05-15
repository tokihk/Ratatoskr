using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Ratatoskr.Gate;

namespace Ratatoskr.Scripts.ScriptEngines
{
    public class ScriptSandbox : Api.ApiSandbox
    {
        private ScriptCodeRunner runner;
        private Thread thread_;

        private bool pause_req_ = false;
        private bool pause_state_ = false;
        private bool cancel_req_ = false;


        internal ScriptSandbox(ScriptCodeRunner runner, Thread script_thread)
        {
            this.runner = runner;
            thread_ = script_thread;
        }

        internal bool IsPause
        {
            get { return (pause_state_); }
        }

        internal void CancelRequest()
        {
            cancel_req_ = true;
        }

        internal void PauseRequest(bool pause)
        {
            pause_req_ = pause;
        }

        internal void InterruptProc()
        {
            do {
                /* 停止要求があるとき */
                if (cancel_req_) {
                    thread_.Abort();
                }

                /* 一時停止要求があるとき */
                if (pause_req_) {
                    pause_state_ = true;
                    Thread.Sleep(50);
                }
            } while (pause_req_);

            pause_state_ = false;
        }

        internal static IEnumerable<string> GetApiNames()
        {
            return (typeof(ScriptSandbox).GetMethods().Select(obj => obj.Name));
        }

        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
         *      API
         +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */

        [Flags]
        public enum PrintTargetType
        {
            PacketLog     = 1 << 0,
            EditorConsole = 1 << 1,
            EditorComment = 1 << 2,

            All = -1,
        }

        public void API_Pause()
        {
            while (true) {
                InterruptProc();
                Thread.Sleep(1);
            }
        }

        public override void API_Sleep(uint msec)
        {
            var sw = new System.Diagnostics.Stopwatch();

            sw.Restart();
            while (sw.ElapsedMilliseconds < msec) {
                InterruptProc();
                Thread.Sleep(1);
            }
        }

        public void API_Print(object obj, PrintTargetType target = PrintTargetType.All, [CallerLineNumber]int line_no = -1)
        {
            if (target.HasFlag(PrintTargetType.PacketLog)) {
                GatePacketManager.SetComment(obj.ToString());
            }

            if (target.HasFlag(PrintTargetType.EditorConsole)) {
                runner.AddScriptMessage(obj.ToString());
            }

            if (target.HasFlag(PrintTargetType.EditorComment)) {
                runner.SetScriptComment(line_no - 1, ScriptMessageType.Notice, obj.ToString());
            }
        }
    }
}
