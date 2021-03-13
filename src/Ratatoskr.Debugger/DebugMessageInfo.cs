using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ratatoskr.Debugger
{
    internal class DebugMessageInfo
    {
        public DebugMessageInfo(DateTime date, ulong tick_us, DebugMessageAttr attr, string msg)
        {
            ThreadID = Thread.CurrentThread.ManagedThreadId;
            DateTime = date;
            TickTime = tick_us;
		    Attr     = attr;
            Message  = msg;
        }

        public int			        ThreadID { get; }
        public DateTime		        DateTime { get; }
        public ulong		        TickTime { get; }
	    public DebugMessageAttr     Attr     { get; }
        public string		        Message  { get; }
    }
}
