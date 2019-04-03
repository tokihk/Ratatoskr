using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public class ProtocolFrameValue
    {
        public ProtocolFrameValue(uint bitlen)
        {
            BitLength = bitlen;
        }

        public uint BitLength { get; }

        public BitData GetBitData()
        {
            var bitdata = new BitData(BitLength);

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

        protected virtual void OnLoadBitData(BitData bitdata)
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
