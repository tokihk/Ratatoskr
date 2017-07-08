using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet.Types
{
    internal class MessagePacketObject : PacketObject
    {
        private static readonly Encoding ENCODER_MSG = Encoding.UTF8;


        public MessagePacketObject(
                    PacketFacility facility,
                    string alias,
                    PacketPriority prio,
                    DateTime dt,
                    string info,
                    byte mark,
                    string message
                    ) : base(
                        facility,
                        alias,
                        prio,
                        PacketAttribute.Message,
                        dt,
                        info,
                        PacketDirection.Recv,
                        "",
                        "",
                        mark
                        )
        {
            Message = message;
        }


        public string Message { get; }


        public override string GetElementText(PacketElementID id)
        {
            switch (id) {
                case PacketElementID.Data:
                {
                    return (Message);
                }

                default:
                {
                    return (base.GetElementText(id));
                }
            }
        }

        public override byte[] GetData()
        {
            return (ENCODER_MSG.GetBytes(Message));
        }

        public override int GetDataSize()
        {
            return (ENCODER_MSG.GetByteCount(Message));
        }
    }
}
