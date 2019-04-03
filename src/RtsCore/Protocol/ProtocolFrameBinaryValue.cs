using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameBinaryValue : ProtocolFrameValue
    {
        private BitData value_;


        public ProtocolFrameBinaryValue(uint bitlen)
            : base(bitlen)
        {
            value_ = new BitData(bitlen);
        }

        public ProtocolFrameBinaryValue(uint bitlen, byte[] value)
            : this(bitlen)
        {
            SetValue(value);
        }

        public byte[] BitData   { get { return (value_.Data); } }

        protected override void OnLoadBitData(BitData bitdata)
        {
            bitdata.SetBitData(0, value_);
        }

        protected override object OnLoadValue()
        {
            return (value_.Data);
        }

        protected override void OnSaveValue(object value)
        {
            if (value.GetType() == typeof(byte[])) {
                var value_b = value as byte[];

                value_.SetBitData(0, value_b, 0, (uint)value_b.Length * 8);

            } else if (value.GetType() == typeof(BitData)) {
                value_.SetBitData(0, value as BitData);
            }
        }

        public override string ToString()
        {
            return (value_.ToHexText());
        }
    }
}
