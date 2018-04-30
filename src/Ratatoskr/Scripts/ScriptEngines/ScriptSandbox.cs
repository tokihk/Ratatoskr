using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.Gate;

namespace Ratatoskr.Scripts.ScriptEngines
{
    public class ScriptSandbox
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
            PacketLog = 1 << 0,
            Console   = 1 << 1,
            Editor    = 1 << 2,

            All       = -1,
        }

        [Flags]
        public enum WatchPacketType
        {
            RawPacket  = API.API_RecvWait.WatchPacketType.RawPacket,
            ViewPacket = API.API_RecvWait.WatchPacketType.ViewPacket,
        }

        public void API_Pause()
        {
            while (true) {
                InterruptProc();
                Thread.Sleep(1);
            }
        }

        public void API_Sleep(uint msec)
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

            if (target.HasFlag(PrintTargetType.Console)) {
                runner.AddScriptOutput(obj.ToString());
            }

            if (target.HasFlag(PrintTargetType.Editor)) {
                runner.SetScriptComment(line_no, ScriptMessageType.Notice, obj.ToString());
            }
        }

        public API.API_PlayRecord API_PlayRecordAsync(string gate, string file_path, string filter_exp)
        {
            var api_obj = new API.API_PlayRecord();

            api_obj.ExecAsync(gate, file_path, filter_exp, null);

            return (api_obj);
        }

        public bool API_PlayRecord(string gate, string file_path, string filter_exp)
        {
            var api_obj = API_PlayRecordAsync(gate, file_path, filter_exp);

            while (api_obj.IsBusy) {
                API_Sleep(10);
            }

            return (api_obj.Success);
        }

        public API.API_RecvWait API_RecvWaitAsync(string filter_exp, WatchPacketType watch_type, uint timeout)
        {
            var api_obj = new API.API_RecvWait();

            api_obj.ExecAsync(filter_exp, (API.API_RecvWait.WatchPacketType)watch_type, timeout, null);

            return (api_obj);
        }

        public bool API_RecvWait(string filter_exp, WatchPacketType watch_type, uint timeout)
        {
            var api_obj = API_RecvWaitAsync(filter_exp, watch_type, timeout);

            while (api_obj.IsBusy) {
                API_Sleep(10);
            }

            return (api_obj.Success);
        }

        public void API_Send(string gate, string data_text)
        {
            API.API_Send.Exec(gate, data_text);
        }

        public API.API_SendFile API_SendFileAsync(string gate, string file_path, uint block_size)
        {
            var api_obj = new API.API_SendFile();

            api_obj.ExecAsync(gate, file_path, block_size, null);

            return (api_obj);
        }

        public bool API_SendFile(string gate, string file_path, uint block_size)
        {
            var api_obj = API_SendFileAsync(gate, file_path, block_size);

            while (api_obj.IsBusy) {
                API_Sleep(10);
            }

            return (api_obj.Success);
        }
    }
}
