using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public abstract class ProtocolDecodeEvent
    {
        public enum DecodeEventType
        {
            Frame,
            Message,
            Data,
        }

        internal ProtocolDecodeEvent(ProtocolDecodeChannel channel, DateTime dt_block, DateTime dt_event, DecodeEventType event_type)
        {
            Channel = channel;
            BlockDateTime = dt_block;
            EventDateTime = dt_event;
            EventType = event_type;
        }

        public ProtocolDecodeChannel Channel       { get; }
        public DateTime              BlockDateTime { get; }
        public DateTime              EventDateTime { get; }
        public DecodeEventType       EventType     { get; }
        public abstract double       EventTickMsec { get; }
    }
}
