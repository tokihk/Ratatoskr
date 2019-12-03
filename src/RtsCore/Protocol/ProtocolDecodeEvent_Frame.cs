using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_Frame : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_Frame(ProtocolDecodeChannel channel, DateTime dt_block, DateTime dt_event, ProtocolFrameElement frame)
            : base(channel, dt_block, dt_event, DecodeEventType.Frame)
        {
            Frame = frame;
        }

        public override double EventTickMsec
        {
            get { return (Frame.GetFrameTickMsec()); }
        }

        public ProtocolFrameElement Frame { get; }

        public override string ToString()
        {
            return (Frame.ToString());
        }
    }
}
