using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolFrameEnumValue<EnumT> : ProtocolFrameValue
        where EnumT : struct
    {
        private UInt32 value_;


        public ProtocolFrameEnumValue(uint bitlen)
            : base(bitlen)
        {
            /* Enum型の最も小さい値を取る */
            var value_e = (EnumT)Enum.GetValues(typeof(EnumT)).GetValue(0);

            /* 整数型で記憶 */
            value_ = Convert.ToUInt32(value_e);
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

        public UInt32 Value
        {
            get { return (value_); }
        }

        protected override void OnLoadBitData(ProtocolBitData bitdata)
        {
            bitdata.SetInteger(0, BitLength, value_);
        }

        protected override object OnLoadValue()
        {
            return (value_);
        }

        protected override void OnSaveValue(object value)
        {
            try {
                value_ = Convert.ToUInt32(value);
            } catch {
            }
        }

        public override string ToString()
        {
            /* Enum型に一致するものがあればEnum型で表示する */
            try {
                var value_e = Enum.ToObject(typeof(EnumT), value_);

                return (string.Format("{0} ({1})", value_e.ToString(), value_));
            } catch {
                return (string.Format("{0}", value_));
            }
        }
    }
}
