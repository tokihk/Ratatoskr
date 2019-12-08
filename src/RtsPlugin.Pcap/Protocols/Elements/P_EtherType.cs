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

        public override string ToString()
        {
            var value = GetUnpackData();

            /* Enum型に一致するものがあればEnum型で表示する */
            try {
                var value_i = value.GetInteger(0);
                var value_e = Enum.ToObject(typeof(EthernetType), value_i);

                return (string.Format("{0} (0x{1,4:X})", value_e.ToString(), value_i));
            } catch {
                return ((value != null) ? (value.GetInteger(0).ToString()) : ("Unknown"));
            }
        }
    }
}
