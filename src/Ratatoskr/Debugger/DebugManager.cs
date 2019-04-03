using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Debugger
{
    internal static class DebugManager
    {
        public enum Channel
        {
            System,
            Device_PacketRecv,
            PacketView_PacketRecv,
        }


        private static DebugForm debug_form_   = null;
        private static Stopwatch debug_timer_  = null;


        [Conditional("DEBUG")]
        public static void Startup()
        {
            debug_form_ = new DebugForm();
            debug_timer_ = new Stopwatch();

            debug_form_.Visible = true;
        }

        [Conditional("DEBUG")]
        public static void MessageOut(string message, Channel channel = Channel.System)
        {
            debug_form_.AddMessage(message);
        }

        [Conditional("DEBUG")]
        public static void MessageOut(object obj, Channel channel = Channel.System)
        {
            if (obj == null)return;

            MessageOut(obj.ToString());
        }

        [Conditional("DEBUG")]
        public static void StopWatchStart()
        {
            debug_timer_.Restart();
        }

        [Conditional("DEBUG")]
        public static void StopWatchStop()
        {
            if (!debug_timer_.IsRunning)return;

            debug_timer_.Stop();

            MessageOut(string.Format("Elapsed {0}ms / {1} tick", debug_timer_.ElapsedMilliseconds, debug_timer_.ElapsedTicks));
        }
    }
}
