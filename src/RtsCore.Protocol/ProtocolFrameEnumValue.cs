using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolFrameEnumValue<EnumT> : ProtocolFrameValue
        where EnumT : struct
    {
        private EnumT value_;


        public ProtocolFrameEnumValue(uint bitlen)
            : base(bitlen)
        {
            value_ = (EnumT)Enum.GetValues(typeof(EnumT)).GetValue(0);
        }

        public ProtocolFrameEnumValue(uint bitlen, EnumT value)
            : this(bitlen)
        {
            SetValue(value);
        }

        public ProtocolFrameEnumValue(uint bitlen, UInt64 value)
            : this(bitlen)
        {
            SetValue(value);
        }

        public EnumT[] ValueList
        {
            get { return ((EnumT[])Enum.GetValues(typeof(EnumT))); }
        }

        public EnumT Value
        {
            get { return (value_); }
        }

        protected override void OnLoadBitData(ProtocolBitData bitdata)
        {
            bitdata.SetInteger(0, BitLength, (ulong)(int)(object)value_);
        }

        protected override object OnLoadValue()
        {
            return (value_);
        }

        protected override void OnSaveValue(object value)
        {
            try {
                if (value.GetType() == typeof(EnumT)) {
                    value_ = (EnumT)value;
                } else {
                    value_ = (EnumT)Enum.ToObject(typeof(EnumT), value);
                }
            } catch {
            }
        }

        public override string ToString()
        {
            return (value_.ToString());
        }
    }
}
