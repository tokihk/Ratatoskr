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
    public class P_MacAddress : ProtocolFrameElement_BitData
    {
        public P_MacAddress(ProtocolFrameElement parent, string name, PhysicalAddress addr) : base(parent, name, 48)
        {
            SetPackData(new BitData(addr.GetAddressBytes(), 48));
        }

        public override string ToString()
        {
            var value = GetUnpackData();
            var value_str = new StringBuilder();

            if ((value != null) && (value.Length >= 48)) {
                value_str.AppendFormat("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}", value.Data[0], value.Data[1], value.Data[2], value.Data[3], value.Data[4], value.Data[5]);
            }

            return (value_str.ToString());
        }
    }
}
