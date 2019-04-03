using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_ProtocolValue : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_ProtocolValue(DateTime event_dt, double value, ulong channel_bit)
            : base(event_dt, DecodeEventType.ProtocolValue, channel_bit)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
