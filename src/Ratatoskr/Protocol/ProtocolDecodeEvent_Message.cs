using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_Message : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_Message(ProtocolDecodeChannel channel, DateTime dt_block, DateTime dt_event, string event_msg)
            : base(channel, dt_block, dt_event, DecodeEventType.Message)
        {
            Message = event_msg;
        }

        public override double EventTickMsec
        {
            get { return (0.0f); }
        }

        public string Message { get; }

        public override string ToString()
        {
            return (Message);
        }
    }
}
