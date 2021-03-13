using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.PacketFilter;
using RtsCore.Packet;

namespace RtsCore.Framework.PacketFilter.Terms
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
            Packet_Class,
            Packet_Information,
            Packet_Mark,
            Packet_Data_IsSend,
            Packet_Data_IsRecv,
            Packet_Data_Source,
            Packet_Data_Destination,
            Packet_Data_Length,
            Packet_Data_BitString,
            Packet_Data_HexString,
            Packet_Data_AsciiText,
            Packet_Data_Utf8Text,
            Packet_Data_Utf16BeText,
            Packet_Data_Utf16LeText,
            Packet_Data_ShiftJisText,
            Packet_Data_EucJpText,
        }


        private StatusType value_ = StatusType.PacketCount;


        public Term_Status()
        {
        }

        public Term_Status(StatusType value)
        {
            value_ = value;
        }

        private Term GetStatusTerm(PacketFilterCallStack cs, StatusType type)
        {
            switch (type) {
                case StatusType.PacketCount: {
                    return (new Term_Number(cs.PacketCount));
                }
                case StatusType.LastPacketDelta: {
                    return (new Term_DateTimeOffset(cs.LastPacket.MakeTime - cs.PrevPacket.MakeTime));
                }
                case StatusType.Packet_IsControl: {
                    return (new Term_Bool(cs.LastPacket.Attribute == PacketAttribute.Control));
                }
                case StatusType.Packet_IsMessage: {
                    return (new Term_Bool(cs.LastPacket.Attribute == PacketAttribute.Message));
                }
                case StatusType.Packet_IsData: {
                    return (new Term_Bool(cs.LastPacket.Attribute == PacketAttribute.Data));
                }
                case StatusType.Packet_Alias: {
                    return (new Term_Text(cs.LastPacket.Alias));
                }
                case StatusType.Packet_MakeTime: {
                    return (new Term_DateTime(cs.LastPacket.MakeTime));
                }
                case StatusType.Packet_Class: {
                    return (new Term_Text(cs.LastPacket.Class));
                }
                case StatusType.Packet_Information: {
                    return (new Term_Text(cs.LastPacket.Information));
                }
                case StatusType.Packet_Mark: {
                    return (new Term_Number(cs.LastPacket.UserMark));
                }
                case StatusType.Packet_Data_IsSend: {
                    return (new Term_Bool(cs.LastPacket.Direction == PacketDirection.Send));
                }
                case StatusType.Packet_Data_IsRecv: {
                    return (new Term_Bool(cs.LastPacket.Direction == PacketDirection.Recv));
                }
                case StatusType.Packet_Data_Source: {
                    return (new Term_Text(cs.LastPacket.Source));
                }
                case StatusType.Packet_Data_Destination: {
                    return (new Term_Text(cs.LastPacket.Destination));
                }
                case StatusType.Packet_Data_Length: {
                    return (new Term_Number(cs.LastPacket.DataLength));
                }
                case StatusType.Packet_Data_BitString: {
                    return (new Term_Text(cs.LastPacket.DataToBitString()));
                }
                case StatusType.Packet_Data_HexString: {
                    return (new Term_Text(cs.LastPacket.DataToHexString()));
                }
                case StatusType.Packet_Data_AsciiText: {
                    return (new Term_Text(cs.LastPacket.DataToText(Encoding.ASCII)));
                }
                case StatusType.Packet_Data_Utf8Text: {
                    return (new Term_Text(cs.LastPacket.DataToText(Encoding.UTF8)));
                }
                case StatusType.Packet_Data_Utf16BeText: {
                    return (new Term_Text(cs.LastPacket.DataToText(Encoding.BigEndianUnicode)));
                }
                case StatusType.Packet_Data_Utf16LeText: {
                    return (new Term_Text(cs.LastPacket.DataToText(Encoding.Unicode)));
                }
                case StatusType.Packet_Data_ShiftJisText: {
                    return (new Term_Text(cs.LastPacket.DataToText(Encoding.GetEncoding(932))));
                }
                case StatusType.Packet_Data_EucJpText: {
                    return (new Term_Text(cs.LastPacket.DataToText(Encoding.GetEncoding(20932))));
                }
                default: return (null);
            }
        }

        public override bool ToBool(PacketFilterCallStack cs)
        {
            var term = GetStatusTerm(cs, value_);

            return ((term != null) ? (term.ToBool(cs)) : (false));
        }

        public override Term Exec(PacketFilterCallStack cs, Tokens token, Term term_sub)
        {
            var term_new = GetStatusTerm(cs, value_);

            if (term_new == null)return (null);

            return (term_new.Exec(cs, token, term_sub));
        }
    }
}
