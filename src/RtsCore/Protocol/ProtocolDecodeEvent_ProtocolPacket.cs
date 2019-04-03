using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_ProtocolPacket : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_ProtocolPacket(DateTime event_dt, ulong channel_bit)
            : base(event_dt, DecodeEventType.ProtocolPacket, channel_bit)
        {
        }
    }
}
