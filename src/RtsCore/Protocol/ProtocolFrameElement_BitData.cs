using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameElement_BitData : ProtocolFrameElement
    {
        public ProtocolFrameElement_BitData(ProtocolFrameElement parent, string name, uint bitlen)
            : base(parent, name, bitlen)
        {
            SetUnpackValue(new BitData(bitlen));
        }

        public ProtocolFrameElement_BitData(ProtocolFrameElement parent, string name, uint bitlen, BitData value)
            : this(parent, name, bitlen)
        {
            SetUnpackValue(value);
        }

        public ProtocolFrameElement_BitData(ProtocolFrameElement parent, string name, BitData value)
            : this(parent, name, value.Length, value)
        {
            SetUnpackValue(value);
        }

        protected override bool OnUnpackDataToUnpackValue(BitData unpack_data, ref object unpack_value)
        {
            unpack_value = unpack_data;

            return (true);
        }

        protected override bool OnUnpackValueToUnpackData(object unpack_value, ref BitData unpack_data)
        {
            unpack_data = unpack_value as BitData;

            return (unpack_data != null);
        }
    }
}
