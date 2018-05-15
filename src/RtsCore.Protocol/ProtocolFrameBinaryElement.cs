using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolFrameBinaryElement : ProtocolFrameElement
    {
        public ProtocolFrameBinaryElement(ProtocolFrameElement parent, string name, uint bitlen)
            : base(parent, name, bitlen, bitlen)
        {
            SetUnpackValue(new ProtocolFrameBinaryValue(UnpackDataBitLength));
        }

        public ProtocolFrameBinaryElement(ProtocolFrameElement parent, string name, uint bitlen, byte[] value)
            : this(parent, name, bitlen)
        {
            SetUnpackValue(new ProtocolFrameBinaryValue(UnpackDataBitLength, value));
        }

        protected override void OnPackDataToUnpackData(ProtocolBitData pack_data, ProtocolBitData unpack_data)
        {
            base.OnPackDataToUnpackData(pack_data, unpack_data);
        }

        protected override ProtocolFrameValue OnUnpackDataToUnpackValue(ProtocolBitData unpack_data)
        {
            var value = new ProtocolFrameBinaryValue(UnpackDataBitLength);

            value.SetValue(unpack_data);

            return (value);
        }

        protected override void OnUnpackValueToUnpackData(ProtocolFrameValue unpack_value, ProtocolBitData unpack_data)
        {
            base.OnUnpackValueToUnpackData(unpack_value, unpack_data);
        }

        public override string ToString()
        {
            return (base.ToString());
        }
    }
}
