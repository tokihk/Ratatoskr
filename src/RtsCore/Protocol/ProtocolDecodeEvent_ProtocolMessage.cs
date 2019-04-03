using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_ProtocolMessage : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_ProtocolMessage(DateTime event_dt, string event_msg, ulong channel_bit)
            : base(event_dt, DecodeEventType.ProtocolMessage, channel_bit)
        {
            Value = event_msg;
        }

        public string Value { get; }
    }
}
