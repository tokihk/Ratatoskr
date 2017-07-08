using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet.Types
{
    internal sealed class StaticDataPacketObject : DataPacketObject
    {
        public StaticDataPacketObject(
                    PacketFacility facility,
                    string alias,
                    PacketPriority prio,
                    DateTime dt,
                    string info,
                    PacketDirection dir,
                    string src,
                    string dst,
                    byte mark,
                    byte[] data
                    ) : base(
                            facility,
                            alias,
                            prio,
                            dt,
                            info,
                            dir,
                            src,
                            dst,
                            mark
                            )
        {
            Data = data;
        }

        public StaticDataPacketObject(PacketObject base_packet, byte[] data) : base(base_packet)
        {
            Data = data;
        }

        public StaticDataPacketObject()
            : this(
                PacketFacility.Device,
                "",
                PacketPriority.Standard,
                DateTime.UtcNow,
                "",
                PacketDirection.Recv,
                "",
                "",
                0,
                new byte[] { 0x00 })
        {
        }

        public byte[] Data { get; }

        public override byte[] GetData()
        {
            return (Data);
        }

        public override int GetDataSize()
        {
            return (Data.Length);
        }
    }
}
