using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameIntegerElement : ProtocolFrameElement
    {
        private UInt64 value_min_;
        private UInt64 value_max_;
        private UInt64 value_step_;


        public ProtocolFrameIntegerElement(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value_min, UInt64 value_max, UInt64 value_step)
            : base(parent, name, bitlen, bitlen)
        {
            value_min_ = value_min;
            value_max_ = value_max;
            value_step_ = value_step;

            SetUnpackValue(new ProtocolFrameIntegerValue(UnpackDataBitLength, value_min, value_max, value_step));
        }

        public ProtocolFrameIntegerElement(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value_min, UInt64 value_max, UInt64 value_step, UInt64 value)
            : this(parent, name, bitlen, value_min, value_max, value_step)
        {
            SetUnpackValue(new ProtocolFrameIntegerValue(UnpackDataBitLength, value_min, value_max, value_step, value));
        }

        public ProtocolFrameIntegerElement(ProtocolFrameElement parent, string name, uint bitlen)
            : this(parent, name, bitlen, 0, (UInt64)(UInt64.MaxValue >> (int)(64 - bitlen)), 1)
        {
        }

        public ProtocolFrameIntegerElement(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value)
            : this(parent, name, bitlen, 0, (UInt64)(UInt64.MaxValue >> (int)(64 - bitlen)), 1, value)
        {
        }

        protected override void OnPackDataToUnpackData(BitData pack_data, BitData unpack_data)
        {
            base.OnPackDataToUnpackData(pack_data, unpack_data);
        }

        protected override ProtocolFrameValue OnUnpackDataToUnpackValue(BitData unpack_data)
        {
            var value = new ProtocolFrameIntegerValue(UnpackDataBitLength, value_min_, value_max_, value_step_);

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
