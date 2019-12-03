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
    public class P_IpFragmentFlags : ProtocolFrameElement_Integer
    {
        public P_IpFragmentFlags(ProtocolFrameElement parent, string name, int flags) : base(parent, name, 3, (ulong)flags)
        {
        }

        public override string ToString()
        {
            var value = GetUnpackData();

            return (string.Format(
                "Continue={0}, Forbid={1} ({2})",
                value.GetInteger(0, 1),
                value.GetInteger(1, 1),
                value.ToHexText()));
        }
    }
}
