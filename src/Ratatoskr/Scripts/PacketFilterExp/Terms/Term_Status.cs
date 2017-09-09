using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Scripts.PacketFilterExp.Terms
{
    internal sealed class Term_Status : Term
    {
        public enum StatusType
        {
            PacketCount,
            LastPacketDelta,

            Packet_IsControl,
            Packet_IsMessage,
            Packet_IsData,
            Packet_Alias,
            Packet_MakeTime,
            Packet_Information,
            Packet_Mark,
            Packet_Data_IsSend,
            Packet_Data_IsRecv,
            Packet_Data_Source,
            Packet_Data_Destination,
            Packet_Data_Length,
            Packet_Data_BitText,
            Packet_Data_HexText,
            Packet_Data_AsciiText,
            Packet_Data_Utf8Text,
            Packet_Data_UnicodeLText,
            Packet_Data_UnicodeBText,
        }


        private StatusType value_ = StatusType.PacketCount;


        public Term_Status()
        {
        }

        public Term_Status(StatusType value)
        {
            value_ = value;
        }

        private Term GetStatusTerm(ExpressionCallStack cs, StatusType type)
        {
            switch (type) {
                case StatusType.PacketCount: {
                    return (new Term_Number(cs.PacketCount));
                }
                case StatusType.LastPacketDelta: {
                    return (new Term_DateTimeOffset(cs.LastPacket.MakeTime - cs.PrevPacket.MakeTime));
                }
                case StatusType.Packet_IsControl: {
                    return (new Term_Bool(cs.LastPacket.Attribute == Generic.Packet.PacketAttribute.Control));
                }
                case StatusType.Packet_IsMessage: {
                    return (new Term_Bool(cs.LastPacket.Attribute == Generic.Packet.PacketAttribute.Message));
                }
                case StatusType.Packet_IsData: {
                    return (new Term_Bool(cs.LastPacket.Attribute == Generic.Packet.PacketAttribute.Data));
                }
                case StatusType.Packet_Alias: {
                    return (new Term_Text(cs.LastPacket.Alias));
                }
                case StatusType.Packet_MakeTime: {
                    return (new Term_DateTime(cs.LastPacket.MakeTime));
                }
                case StatusType.Packet_Information: {
                    return (new Term_Text(cs.LastPacket.Information));
                }
                case StatusType.Packet_Mark: {
                    return (new Term_Number(cs.LastPacket.UserMark));
                }
                case StatusType.Packet_Data_IsSend: {
                    return (new Term_Bool(cs.LastPacket.Direction == Generic.Packet.PacketDirection.Send));
                }
                case StatusType.Packet_Data_IsRecv: {
                    return (new Term_Bool(cs.LastPacket.Direction == Generic.Packet.PacketDirection.Recv));
                }
                case StatusType.Packet_Data_Source: {
                    return (new Term_Text(cs.LastPacket.Source));
                }
                case StatusType.Packet_Data_Destination: {
                    return (new Term_Text(cs.LastPacket.Destination));
                }
                case StatusType.Packet_Data_Length: {
                    return (new Term_Number(cs.LastPacket.GetDataSize()));
                }
                case StatusType.Packet_Data_BitText: {
                    var packet_d = cs.LastPacket as DataPacketObject;

                    return (new Term_Text((packet_d != null) ? (packet_d.GetBitText()) : ""));
                }
                case StatusType.Packet_Data_HexText: {
                    var packet_d = cs.LastPacket as DataPacketObject;

                    return (new Term_Text((packet_d != null) ? (packet_d.GetHexText()) : ""));
                }
                case StatusType.Packet_Data_AsciiText: {
                    var packet_d = cs.LastPacket as DataPacketObject;

                    return (new Term_Text((packet_d != null) ? (packet_d.GetAsciiText()) : ""));
                }
                case StatusType.Packet_Data_Utf8Text: {
                    var packet_d = cs.LastPacket as DataPacketObject;

                    return (new Term_Text((packet_d != null) ? (packet_d.GetUtf8Text()) : ""));
                }
                case StatusType.Packet_Data_UnicodeLText: {
                    var packet_d = cs.LastPacket as DataPacketObject;

                    return (new Term_Text((packet_d != null) ? (packet_d.GetUnicodeText(true)) : ""));
                }
                case StatusType.Packet_Data_UnicodeBText: {
                    var packet_d = cs.LastPacket as DataPacketObject;

                    return (new Term_Text((packet_d != null) ? (packet_d.GetUnicodeText(false)) : ""));
                }
                default: return (null);
            }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return (true);
        }

        public override Term Exec(ExpressionCallStack cs, Tokens token, Term term_sub)
        {
            var term_new = GetStatusTerm(cs, value_);

            if (term_new == null)return (null);

            return (term_new.Exec(cs, token, term_sub));
        }
    }
}
