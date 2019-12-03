using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.Elements
{
    public class P_IpAddressV4 : ProtocolFrameElement_BitData
    {
        public P_IpAddressV4(ProtocolFrameElement parent, string name, IPAddress addr) : base(parent, name, 32)
        {
            SetPackData(new BitData(addr.GetAddressBytes(), 32));
        }

        public override string ToString()
        {
            var value = GetUnpackData();
            var value_str = new StringBuilder();

            if ((value != null) && (value.Length >= 48)) {
                value_str.AppendFormat("{0}:{1}:{2}:{3}", value.Data[0], value.Data[1], value.Data[2], value.Data[3]);
            }

            return (value_str.ToString());
        }
    }
}
