using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.General.Pcap.SharpPcap;

namespace Ratatoskr.General.Pcap
{
    public static class PcapPacketParser
    {
        public static PcapPacketInfo Parse(DateTime packet_time, in PcapLinkType packet_linktype, in byte[] packet_data)
        {
            PcapPacketInfo packet_info;

            /* SharpPcapで解析 */
            packet_info = SharpPcapParser.Parse(packet_time, packet_linktype, packet_data);
            if (packet_info != null) {
                return (packet_info);
            }

            /* --- ここに独自パーサーを追加 --- */

            return (packet_info);
        }
    }
}
