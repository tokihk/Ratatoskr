using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ratatoskr.Debugger
{
    internal class DebugMessageInfo
    {
        public DebugMessageInfo(DateTime date, ulong tick_us, DebugMessageSender sender, DebugMessageType type, string msg)
        {
            ThreadID = Thread.CurrentThread.ManagedThreadId;
            DateTime = date;
            TickTime = tick_us;
            Sender   = sender;
		    Type     = type;
            Message  = msg;
        }

        public int			        ThreadID { get; }
        public DateTime		        DateTime { get; }
        public ulong		        TickTime { get; }

	    public DebugMessageSender   Sender   { get; }
	    public DebugMessageType     Type     { get; }

        public string		        Message  { get; }
    }
}
