using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.Elements
{
    public class P_IpHeaderLength : ProtocolFrameElement_Integer
    {
        public P_IpHeaderLength(ProtocolFrameElement parent, string name, int value) : base(parent, name, 4, (ulong)value)
        {
        }

        public override string ToString()
        {
            var value = Convert.ToUInt64(GetUnpackValue());

            return (string.Format("{0} byte [{1} x 4 byte]", value * 4, value));
        }
    }
}
