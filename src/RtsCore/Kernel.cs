using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Utility;

namespace RtsCore
{
    public static class Kernel
    {
        public delegate void SystemMessageEventDelegate(SystemMessageInfo mi);

        public static event SystemMessageEventDelegate DebugMessageSetup;

        private static Stopwatch systick_base_ = new Stopwatch();


        static Kernel()
        {
            systick_base_.Start();
        }

        public static ulong SystemTick
        {
            get { return ((ulong)systick_base_.ElapsedTicks * 1000000UL / (ulong)Stopwatch.Frequency); }
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DebugMessage(string message, SystemMessageAttr attr = SystemMessageAttr.System)
        {
            DebugMessageSetup?.Invoke(new SystemMessageInfo(DateTime.Now, SystemTick, attr, message));
        }
    }
}
