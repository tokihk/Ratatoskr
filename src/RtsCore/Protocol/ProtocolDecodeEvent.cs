using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent
    {
        public enum DecodeEventType
        {
            ProtocolPacket   = 0x0001,
            ProtocolMessage  = 0x0002,
            ProtocolValue    = 0x0003,
            ViewPacket       = 0x1000,
        }

        public ProtocolDecodeEvent(DateTime event_dt, DecodeEventType event_type, ulong channel_bit)
        {
            EventDateTime = event_dt;
            EventType = event_type;
            ChannelBit = channel_bit;
        }

        public DateTime        EventDateTime { get; }
        public DecodeEventType EventType     { get; }
        public ulong           ChannelBit    { get; }
    }
}
