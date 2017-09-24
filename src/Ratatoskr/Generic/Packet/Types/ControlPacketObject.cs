using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet.Types
{
    [Serializable]
    internal sealed class ControlPacketObject : PacketObject
    {
        public uint   ControlCommand { get; }
        public byte[] ControlData    { get; }


        public ControlPacketObject(
                    PacketFacility facility,
                    string alias,
                    PacketPriority prio,
                    DateTime dt,
                    byte mark,
                    uint ctrl_command,
                    byte[] ctrl_data
                    ) : base(
                        facility,
                        alias,
                        prio,
                        PacketAttribute.Control,
                        dt,
                        "",
                        PacketDirection.Recv,
                        "",
                        "",
                        mark)
        {
            ControlCommand = ctrl_command;
            ControlData = ctrl_data;
        }
        
        public ControlPacketObject(PacketObject packet) : base(packet) { }


        public override byte[] GetData()
        {
            var result = new Queue<byte>();

            /* Control Command */
            result.Enqueue((byte)((ControlCommand >> 24) & 0xFF));
            result.Enqueue((byte)((ControlCommand >> 16) & 0xFF));
            result.Enqueue((byte)((ControlCommand >>  8) & 0xFF));
            result.Enqueue((byte)((ControlCommand >>  0) & 0xFF));

            /* Control Data */
            if (ControlData != null) {
                foreach (var data in ControlData) {
                    result.Enqueue(data);
                }
            }

            return (result.ToArray());
        }

        public override int GetDataSize()
        {
            var size = 4;

            if (ControlData != null) {
                size += ControlData.Length;                
            }

            return (size);
        }
    }
}
