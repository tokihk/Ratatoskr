using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolFrameValue
    {
        public ProtocolFrameValue(uint bitlen)
        {
            BitLength = bitlen;
        }

        public uint BitLength { get; }

        public ProtocolBitData GetBitData()
        {
            var bitdata = new ProtocolBitData(BitLength);

            OnLoadBitData(bitdata);

            return (bitdata);
        }

        public object GetValue()
        {
            return (OnLoadValue());
        }

        public void SetValue(object value)
        {
            OnSaveValue(value);
        }

        protected virtual void OnLoadBitData(ProtocolBitData bitdata)
        {
        }

        protected virtual object OnLoadValue()
        {
            return (null);
        }

        protected virtual void OnSaveValue(object value)
        {
        }
    }
}
