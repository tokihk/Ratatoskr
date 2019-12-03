using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.Elements
{
    public class LayerPacket<PacketT> : ProtocolFrameElement, ILayerPacket
        where PacketT : Packet
    {
        private PacketT packet_;


        public LayerPacket(ProtocolFrameElement parent, string name, PacketT packet) : base(parent, name, (uint)packet.TotalPacketLength * 8)
        {
            packet_ = packet;

            SetPackData(new BitData(packet_.Bytes, (uint)packet_.TotalPacketLength * 8));
        }

        protected override bool OnUnpackDataToUnpackElements(BitData unpack_data)
        {
            if (!OnPacketToUnpackElements(packet_))return (false);

            PcapParser.ParsePacketPayload(this, packet_);

            return (true);
        }

        protected virtual bool OnPacketToUnpackElements(PacketT packet)
        {
            return (true);
        }

        public override string ToString()
        {
            var str = Name;
            var unpack_elems = GetUnpackElements();

            if (unpack_elems != null) {
                foreach (var elem in unpack_elems) {
                    if (elem is ILayerPacket packet) {
                        str = packet.ToString();
                    }
                }
            }

            return (str);
        }
    }
}
