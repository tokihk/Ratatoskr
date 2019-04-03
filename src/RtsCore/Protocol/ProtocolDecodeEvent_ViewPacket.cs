using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_ViewPacket : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_ViewPacket(DateTime event_dt, byte[] data, ulong channel_bit)
            : base(event_dt, DecodeEventType.ViewPacket, channel_bit)
        {
        }
    }
}
