using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolFrameBinaryValue : ProtocolFrameValue
    {
        private ProtocolBitData value_;


        public ProtocolFrameBinaryValue(uint bitlen)
            : base(bitlen)
        {
            value_ = new ProtocolBitData(bitlen);
        }

        public ProtocolFrameBinaryValue(uint bitlen, byte[] value)
            : this(bitlen)
        {
            SetValue(value);
        }

        public byte[] BitData   { get { return (value_.Data); } }

        protected override void OnLoadBitData(ProtocolBitData bitdata)
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

            } else if (value.GetType() == typeof(ProtocolBitData)) {
                value_.SetBitData(0, value as ProtocolBitData);
            }
        }

        public override string ToString()
        {
            return (value_.ToHexText());
        }
    }
}
