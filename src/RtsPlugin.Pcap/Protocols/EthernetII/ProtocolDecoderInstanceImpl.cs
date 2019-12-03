using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Protocol;
using RtsCore.Utility;
using SharpPcap;
using SharpPcap.LibPcap;
using PacketDotNet;

namespace RtsPlugin.Pcap.Protocols.EthenetII
{
    public class ProtocolDecoderInstanceImpl : ProtocolDecoderInstance
    {
        private ProtocolDecodeChannel      prdch_frame_;


        public ProtocolDecoderInstanceImpl(ProtocolDecoderClass prdc) : base(prdc)
        {
            prdch_frame_ = CreateChannel("Frame");
        }

        protected override void OnInputData(DateTime input_dt, byte[] input_data)
        {
            var input_dt_offset = new DateTimeOffset(input_dt);

            var packet_raw = new RawCapture(
                LinkLayers.Ethernet,
                new PosixTimeval(
                    (ulong)input_dt_offset.ToUnixTimeSeconds(),
                    (ulong)input_dt_offset.ToUnixTimeMilliseconds()),
                input_data);

            var packet = PacketDotNet.Packet.ParsePacket(packet_raw.LinkLayerType, packet_raw.Data);

            var frame = Elements.PcapParser.ParsePacket(null, packet);

            if (frame == null)return;

            prdch_frame_.CreateFrameEvent(input_dt, input_dt, frame);
        }
    }
}
