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
    public class P_IpTypeOfService : ProtocolFrameElement_Integer
    {
        public P_IpTypeOfService(ProtocolFrameElement parent, string name, int value) : base(parent, name, 8, (ulong)value)
        {
        }

        public override string ToString()
        {
            var value = GetUnpackData();
            var value_prio = value.GetInteger(0, 3);

            return (string.Format(
                "Prio={0}, D={1}, T={2}, R={3} ({4})",
                value.GetInteger(0, 3),
                value.GetInteger(4, 1),
                value.GetInteger(5, 1),
                value.GetInteger(6, 1),
                value.ToHexText()));
        }
    }
}
