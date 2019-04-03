using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameIntegerValue : ProtocolFrameValue
    {
        private UInt64 value_;


        public ProtocolFrameIntegerValue(uint bitlen, UInt64 value_min, UInt64 value_max, UInt64 value_step)
            : base(bitlen)
        {
            ValueMin = Math.Min(value_min, value_max);
            ValueMax = Math.Max(value_min, value_max);
            ValueStep = Math.Min(value_step, ValueMax - ValueMin);
        }

        public ProtocolFrameIntegerValue(uint bitlen, UInt64 value_min, UInt64 value_max, UInt64 value_step, UInt64 value)
            : this(bitlen, value_min, value_max, value_step)
        {
            SetValue(value);
        }

        public UInt64 ValueMin  { get; }
        public UInt64 ValueMax  { get; }
        public UInt64 ValueStep { get; }

        public UInt64 Value
        {
            get { return (value_); }
        }

        protected override void OnLoadBitData(BitData bitdata)
        {
            bitdata.SetInteger(0, BitLength, value_);
        }

        protected override object OnLoadValue()
        {
            return (value_);
        }

        protected override void OnSaveValue(object value)
        {
            if (   (value.GetType() == typeof(UInt64))
                || (value.GetType() == typeof(UInt32))
                || (value.GetType() == typeof(UInt16))
                || (value.GetType() == typeof(Byte))
            ) {
                value_ = (UInt64)value;
            }
        }

        public override string ToString()
        {
            return (value_.ToString());
        }
    }
}
