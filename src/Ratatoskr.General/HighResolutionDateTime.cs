//#define __HIGHRESOLUTION_MODE__

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ratatoskr.General
{
    public static class HighResolutionDateTime
    {
#if __HIGHRESOLUTION_MODE__
        private static DateTime  dt_base_;
        private static Stopwatch dt_tick_ = new Stopwatch();


        static HighResolutionDateTime()
        {
            Reset();
        }

        public static void Reset()
        {
            dt_base_ = DateTime.UtcNow;
            dt_tick_.Restart();
        }
#endif

        public static DateTime UtcNow
        {
#if __HIGHRESOLUTION_MODE__
            get { return (new DateTime(dt_base_.Ticks + dt_tick_.ElapsedTicks / (Stopwatch.Frequency / 10000000), DateTimeKind.Utc)); }
#else
            get { return (DateTime.UtcNow); }
#endif
        }

        public static DateTime Now
        {
            get { return (UtcNow.ToLocalTime()); }
        }
    }
}
