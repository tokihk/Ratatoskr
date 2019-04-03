using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameEnumElement<EnumT> : ProtocolFrameElement
        where EnumT : struct
    {
        public ProtocolFrameEnumElement(ProtocolFrameElement parent, string name, uint bitlen)
            : base(parent, name, bitlen, bitlen)
        {
            SetUnpackValue(new ProtocolFrameEnumValue<EnumT>(UnpackDataBitLength));
        }

        public ProtocolFrameEnumElement(ProtocolFrameElement parent, string name, uint bitlen, EnumT value)
            : base(parent, name, bitlen, bitlen)
        {
            SetUnpackValue(new ProtocolFrameEnumValue<EnumT>(UnpackDataBitLength, value));
        }

        public ProtocolFrameEnumElement(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value)
            : base(parent, name, bitlen, bitlen)
        {
            SetUnpackValue(new ProtocolFrameEnumValue<EnumT>(UnpackDataBitLength, value));
        }

        protected override void OnPackDataToUnpackData(BitData pack_data, BitData unpack_data)
        {
            base.OnPackDataToUnpackData(pack_data, unpack_data);
        }

        protected override ProtocolFrameValue OnUnpackDataToUnpackValue(BitData unpack_data)
        {
            var value = new ProtocolFrameEnumValue<EnumT>(UnpackDataBitLength);

            value.SetValue(unpack_data);

            return (value);
        }

        protected override void OnUnpackValueToUnpackData(ProtocolFrameValue unpack_value, BitData unpack_data)
        {
            unpack_data.SetBitData(0, unpack_value.GetBitData());
        }

        public override string ToString()
        {
            return (GetUnpackValue().ToString());
        }
    }
}
