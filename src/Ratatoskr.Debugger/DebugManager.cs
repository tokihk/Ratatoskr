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
    public enum DebugMessageSender : Int64
    {
        Application		= 1 << 0,

        Gate            = 1 << 1,

        Device			= 1 << 2,
        View			= 1 << 3,
        Form			= 1 << 4,
        User			= 1 << 5,

        Plugin          = 1 << 10,

        Unknown         = 1 << 24,
    }

	[Flags]
    public enum DebugMessageType : Int64
    {
        Startup          = 1 << 0,
        Shutdown         = 1 << 1,

        PollEvent        = 1 << 2,

		SendEvent		 = 1 << 3,
		RecvEvent		 = 1 << 4,

        ConfigEvent      = 1 << 5,
        ControlEvent     = 1 << 6,

        NoCategory       = 1 << 56,
    }

    public static class DebugManager
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
        public static void MessageOut(in DebugMessageSender sender, in DebugMessageType type, in object obj)
        {
            message_monitor_.MessageOut(new DebugMessageInfo(DateTime.Now, SystemTick, sender, type, obj.ToString()));
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MessageOut(in DebugMessageSender sender, in object obj)
        {
            MessageOut(sender, DebugMessageType.NoCategory, obj);
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MessageOut(in object obj)
        {
            MessageOut(DebugMessageSender.Unknown, DebugMessageType.NoCategory, obj);
        }
    }
}
