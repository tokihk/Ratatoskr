using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RtsCore.Utility
{
	[Flags]
    public enum SystemMessageAttr
    {
        System			= 1 << 0,
        Device			= 1 << 1,
        View			= 1 << 2,
        User			= 1 << 3,

		SendAction		= 1 << 8,
		RecvAction		= 1 << 9,
    }

    public class SystemMessageInfo
    {
        public SystemMessageInfo(DateTime date, ulong tick_us, SystemMessageAttr attr, string msg)
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
	    public SystemMessageAttr    Attr     { get; }
        public string		        Message  { get; }
    }
}
