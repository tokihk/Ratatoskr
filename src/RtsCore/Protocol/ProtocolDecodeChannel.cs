using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeChannel
    {
        internal ProtocolDecodeChannel(ProtocolDecoderInstance prdi, string name, int channel_no)
        {
            Instance = prdi;
            Name = name;
            ChannelBit = 1ul << channel_no;
        }

        public ProtocolDecoderInstance Instance
        {
            get;
        }

        public string Name
        {
            get;
        }

        public ulong ChannelBit
        {
            get;
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
