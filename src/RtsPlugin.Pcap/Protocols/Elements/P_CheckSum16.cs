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
    public class P_CheckSum16 : ProtocolFrameElement_Integer
    {
        public P_CheckSum16(ProtocolFrameElement parent, string name, ushort value) : base(parent, name, 16, (ulong)value)
        {
        }

        public override string ToString()
        {
            var value = Convert.ToUInt64(GetUnpackValue());

            return (string.Format("0x{0,0000:X}", value));
        }
    }
}
