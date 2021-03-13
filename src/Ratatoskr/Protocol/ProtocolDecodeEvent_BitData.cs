using System;
using System.Collections.Generic;
using System.Text;
using Ratatoskr.General;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeEvent_BitData : ProtocolDecodeEvent
    {
        public ProtocolDecodeEvent_BitData(ProtocolDecodeChannel channel, DateTime dt_block, DateTime dt_event, BitData bitdata)
            : base(channel, dt_block, dt_event, DecodeEventType.Data)
        {
            Data = bitdata;
        }

        public override double EventTickMsec
        {
            get { return (0.0f); }
        }

        public BitData Data { get; }

        public override string ToString()
        {
            return (Data.ToString());
        }
    }
}
