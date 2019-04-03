using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecoderParam
    {
        public List<ProtocolDecodeData2> DecodeDataList { get; internal set; }

        public DateTime InputDateTime { get; internal set; }
        public byte[]   InputData     { get; internal set; }
    }
}
