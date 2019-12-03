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
    public class P_EtherType : ProtocolFrameElement_Enum<EthernetType>
    {
        public P_EtherType(ProtocolFrameElement parent, string name, EthernetType type) : base(parent, name, 16)
        {
            SetUnpackValue(type);
        }
    }
}
