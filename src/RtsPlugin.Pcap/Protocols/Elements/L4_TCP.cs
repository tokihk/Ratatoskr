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
    public class L4_TCP : LayerPacket<TcpPacket>
    {
        public L4_TCP(ProtocolFrameElement parent, TcpPacket packet) : base(parent, "TCP", packet)
        {
        }

        protected override bool OnPacketToUnpackElements(TcpPacket packet)
        {
            /* Source Port */
            new ProtocolFrameElement_Integer(this, "Source Port", 16, packet.SourcePort);

            /* Destination Port */
            new ProtocolFrameElement_Integer(this, "Destination Port", 16, packet.DestinationPort);

            /* Sequence Number */
            new ProtocolFrameElement_Integer(this, "Sequence Number", 32, packet.SequenceNumber);

            /* Acknowledgment Number */
            new ProtocolFrameElement_Integer(this, "Acknowledgment Number", 32, packet.AcknowledgmentNumber);

            /* Data Offset */
            new P_TcpDataOffset(this, "Data Offset", packet.DataOffset);

            /* Flags */
            new P_TcpFlags(this, "Flags", packet.Flags);

            /* Window size */
            new ProtocolFrameElement_Integer(this, "Window Size", 16, packet.WindowSize);

            /* Window size */
            new ProtocolFrameElement_Integer(this, "Window Size", 16, packet.WindowSize);

            /* Check Sum */
            new P_CheckSum16(this, "Check Sum", packet.Checksum);

            /* Urgent Pointer */
            new ProtocolFrameElement_Integer(this, "Urgent Pointer", 16, (ulong)packet.UrgentPointer);

            return (true);
        }
    }
}
