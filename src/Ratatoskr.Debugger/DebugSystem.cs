using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Debugger
{
	[Flags]
    public enum DebugMessageAttr
    {
        System			= 1 << 0,
        Device			= 1 << 1,
        View			= 1 << 2,
        User			= 1 << 3,

		SendAction		= 1 << 8,
		RecvAction		= 1 << 9,
    }

    public static class DebugSystem
    {
        private static Stopwatch systick_base_ = new Stopwatch();

        private static DebugMessageMonitor message_monitor_ = null;


        [Conditional("DEBUG")]
        public static void Start()
        {
            systick_base_.Start();

            message_monitor_ = new DebugMessageMonitor();
            message_monitor_.Visible = true;
        }

        public static ulong SystemTick
        {
            get { return ((ulong)systick_base_.ElapsedTicks * 1000000UL / (ulong)Stopwatch.Frequency); }
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MessageOut(string message, DebugMessageAttr attr = DebugMessageAttr.System)
        {
            message_monitor_.MessageOut(new DebugMessageInfo(DateTime.Now, SystemTick, attr, message));
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MessageOut(object obj, DebugMessageAttr attr = DebugMessageAttr.System)
        {
            message_monitor_.MessageOut(new DebugMessageInfo(DateTime.Now, SystemTick, attr, obj.ToString()));
        }
    }
}
