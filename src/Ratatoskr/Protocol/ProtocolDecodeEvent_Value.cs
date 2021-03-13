using System;
using System.Collections.Generic;
using System.Text;
using Ratatoskr.General;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_Value : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_Value(ProtocolDecodeChannel channel, DateTime dt_block, DateTime dt_event, double value)
            : base(channel, dt_block, dt_event, DecodeEventType.Data)
        {
            Value = value;
        }

        public override double EventTickMsec
        {
            get { return (0.0f); }
        }

        public double Value { get; }

        public override string ToString()
        {
            return (Value.ToString());
        }
    }
}
