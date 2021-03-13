using System;
using System.Collections.Generic;
using System.Text;
using Ratatoskr.General;

namespace RtsCore.Protocol
{
    public class ProtocolFrameElement_Integer : ProtocolFrameElement
    {
        private UInt64 value_min_;
        private UInt64 value_max_;
        private UInt64 value_step_;


        public ProtocolFrameElement_Integer(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value_min, UInt64 value_max, UInt64 value_step)
            : base(parent, name, bitlen)
        {
            value_min_ = value_min;
            value_max_ = value_max;
            value_step_ = value_step;

            SetUnpackValue((UInt64)0);
        }

        public ProtocolFrameElement_Integer(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value_min, UInt64 value_max, UInt64 value_step, UInt64 value)
            : base(parent, name, bitlen)
        {
            value_min_ = value_min;
            value_max_ = value_max;
            value_step_ = value_step;

            SetUnpackValue(value);
        }

        public ProtocolFrameElement_Integer(ProtocolFrameElement parent, string name, uint bitlen)
            : this(parent, name, bitlen, 0, (UInt64)(UInt64.MaxValue >> (int)(64 - bitlen)), 1)
        {
        }

        public ProtocolFrameElement_Integer(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value)
            : this(parent, name, bitlen, 0, (UInt64)(UInt64.MaxValue >> (int)(64 - bitlen)), 1, value)
        {
        }

        protected override bool OnUnpackDataToUnpackValue(BitData unpack_data, ref object unpack_value)
        {
            try {
                unpack_value = unpack_data.GetInteger(0);

                return (true);
            } catch {
                return (false);
            }
        }

        protected override bool OnUnpackValueToUnpackData(object unpack_value, ref BitData unpack_data)
        {
            try {
                var unpack_value_i = Convert.ToUInt64(unpack_value);

                unpack_value_i = Math.Max(value_min_, unpack_value_i);
                unpack_value_i = Math.Min(value_max_, unpack_value_i);

                unpack_data = new BitData(unpack_value_i, PackDataBitLength);

                return (true);
            } catch {
                return (false);
            }
        }
    }
}
