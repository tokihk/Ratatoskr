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
    public class P_TcpFlags : ProtocolFrameElement_Integer
    {
        [Flags]
        private enum Flags
        {
            FIN = 1 << 0,
            SYN = 1 << 1,
            RST = 1 << 2,
            PSH = 1 << 3,
            ACK = 1 << 4,
            URG = 1 << 5,
            ECE = 1 << 6,
            CWR = 1 << 7,
            NS  = 1 << 8,
        }


        public P_TcpFlags(ProtocolFrameElement parent, string name, ushort flags) : base(parent, name, 12, flags)
        {
        }

        public override string ToString()
        {
            var value = (Flags)Convert.ToUInt64(GetUnpackValue());
            var str_list = new List<String>();

            foreach (Flags flag in Enum.GetValues(typeof(Flags))) {
                if (value.HasFlag(flag)) {
                    str_list.Add(flag.ToString());
                }
            }

            return (string.Format("{0} ({1,0000:X})", string.Join(", ", str_list.ToArray()), value));
        }
    }
}
