using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameElement_Enum<EnumT> : ProtocolFrameElement
        where EnumT : struct
    {
        public ProtocolFrameElement_Enum(ProtocolFrameElement parent, string name, uint bitlen)
            : base(parent, name, bitlen)
        {
            /* Enum型の先頭値を取る */
            SetUnpackValue(Enum.GetValues(typeof(EnumT)).GetValue(0));
        }

        public ProtocolFrameElement_Enum(ProtocolFrameElement parent, string name, uint bitlen, EnumT value)
            : base(parent, name, bitlen)
        {
            SetUnpackValue(value);
        }

        public ProtocolFrameElement_Enum(ProtocolFrameElement parent, string name, uint bitlen, UInt64 value)
            : base(parent, name, bitlen)
        {
            SetUnpackValue(value);
        }

        protected override bool OnUnpackDataToUnpackValue(BitData unpack_data, ref object unpack_value)
        {
            try {
                unpack_value = Enum.ToObject(typeof(EnumT), unpack_data.GetInteger(0));

                return (true);
            } catch {
                return (false);
            }
        }

        protected override bool OnUnpackValueToUnpackData(object unpack_value, ref BitData unpack_data)
        {
            try {
                unpack_data = new BitData(Convert.ToUInt64(unpack_value), PackDataBitLength);

                return (true);
            } catch {
                return (false);
            }
        }

        public override string ToString()
        {
            var value = GetUnpackData();

            /* Enum型に一致するものがあればEnum型で表示する */
            try {
                var value_i = value.GetInteger(0);
                var value_e = Enum.ToObject(typeof(EnumT), value_i);

                return (string.Format("{0} ({1})", value_e.ToString(), value_i));
            } catch {
                return ((value != null) ? (value.GetInteger(0).ToString()) : ("Unknown"));
            }
        }
    }
}
