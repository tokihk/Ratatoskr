using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ratatoskr.Debugger
{
    internal class DebugEventInfo
    {
        public DebugEventInfo(DateTime date, ulong tick_us, DebugEventSender sender, DebugEventType type, string msg)
        {
            ThreadID = Thread.CurrentThread.ManagedThreadId;
            DateTime = date;
            TickTime = tick_us;
            Sender   = sender;
		    Type     = type;
            Value  = msg;
        }

        public int			        ThreadID { get; }
        public DateTime		        DateTime { get; }
        public ulong		        TickTime { get; }

	    public DebugEventSender   Sender   { get; }
	    public DebugEventType     Type     { get; }

        public string		        Value    { get; }
    }
}
