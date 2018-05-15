using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeChannel
    {
        public ProtocolDecodeChannel(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return (Name);
        }
    }
}
