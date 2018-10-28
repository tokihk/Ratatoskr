using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Ratatoskr.Generic;
using Ratatoskr.Utility;

namespace Ratatoskr.Packet
{
    public enum PacketElementID : byte
    {
        Class,
        Facility,
        Alias,
        Priority,
        Attribute,
        DateTime_UTC_ISO8601,
        DateTime_UTC_Display,
        DateTime_Local_ISO8601,
        DateTime_Local_Display,
        Direction,
        Information,
        Source,
        Destination,
        Mark,
        Message,
        Data_BitString,
        Data_HexString,
        Data_TextAscii,
        Data_TextUTF8,
        Data_TextUTF16BE,
        Data_TextUTF16LE,
        Data_TextShiftJIS,
        Data_TextEucJp,
    }

    public enum PacketFacility : byte
    {
        System,
        View,
        Device,
        External,
    }

    public enum PacketPriority : byte
    {
        Debug,
        Standard,
        Notice,
        Warning,
        Error,
        Critical,
        Alert,
        Emergency,
    }

    public enum PacketAttribute : byte
    {
        Control,
        Message,
        Data,
    }

    public enum PacketDirection : byte
    {
        Recv,
        Send,
    }

    [Serializable]
    public class PacketObject
    {
        public const string DATETIME_FORMAT_UTC_DISPLAY   = "yyyy-MM-dd HH:mm:ss.fff";
        public const string DATETIME_FORMAT_LOCAL_DISPLAY = "yyyy-MM-dd HH:mm:ss.fff";

        private static string StringDataNormalize(string value)
        {
            return (((value != null) && (value.Length > 0)) ? (value) : (null));
        }

        private string class_;
        private string alias_;
        private string information_;

        private string source_;
        private string destination_;

        private string message_;

        private DateTime make_time_;
        private UInt16   packet_param_ = 0;
        private byte     user_mark_;

        private byte[]   data_;


        public PacketObject(
                    string class_name,
                    PacketFacility facility,
                    string alias,
                    PacketPriority prio,
                    PacketAttribute attr,
                    DateTime dt,
                    string info,
                    PacketDirection dir,
                    string src,
                    string dst,
                    byte mark,
                    string message,
                    byte[] data
        ) {
            Class = class_name;
            Facility = facility;
            Alias = alias;
            Priority = prio;
            Attribute = attr;
            MakeTime = dt;
            Information = info;
            Direction = dir;
            Source = src;
            Destination = dst;
            UserMark = mark;
            Message = message;
            Data = data;
        }

        public PacketObject(PacketObject packet)
            : this(
                packet.Class,
                packet.Facility,
                packet.Alias,
                packet.Priority,
                packet.Attribute,
                packet.MakeTime,
                packet.Information,
                packet.Direction,
                packet.Source,
                packet.Destination,
                packet.UserMark,
                packet.Message,
                packet.Data
        ) {
        }

        public PacketObject(PacketObject packet, byte[] data)
            : this(
                packet.Class,
                packet.Facility,
                packet.Alias,
                packet.Priority,
                packet.Attribute,
                packet.MakeTime,
                packet.Information,
                packet.Direction,
                packet.Source,
                packet.Destination,
                packet.UserMark,
                packet.Message,
                data
        ) {
        }

        public PacketObject() { }

        public string Class
        {
            get
            {
                return (class_ ?? "");
            }
            private set
            {
                class_ = StringDataNormalize(value);
            }
        }

        public DateTime MakeTime
        {
            get { return (make_time_); }
            private set { make_time_ = value; }
        }

        public PacketFacility  Facility
        {
            get
            {
                return ((PacketFacility)((packet_param_ & 0xF000u) >> 12));
            }
            private set
            {
                packet_param_ = (UInt16)((packet_param_ & 0x0FFFu) | (((UInt16)value << 12) & 0xF000u));
            }
        }

        public PacketPriority  Priority
        {
            get
            {
                return ((PacketPriority)((packet_param_ & 0x0F00u) >> 8));
            }
            private set
            {
                packet_param_ = (UInt16)((packet_param_ & 0xF0FFu) | (((UInt16)value << 8) & 0x0F00u));
            }
        }

        public PacketAttribute Attribute
        {
            get
            {
                return ((PacketAttribute)((packet_param_ & 0x00F0u) >> 4));
            }
            private set
            {
                packet_param_ = (UInt16)((packet_param_ & 0xFF0Fu) | (((UInt16)value << 4) & 0x00F0u));
            }
        }

        public PacketDirection Direction
        {
            get
            {
                return ((PacketDirection)((packet_param_ & 0x000Fu) >> 0));
            }
            private set
            {
                packet_param_ = (UInt16)((packet_param_ & 0xFFF0u) | (((UInt16)value << 0) & 0x000Fu));
            }
        }

        public string Alias
        {
            get
            {
                return (alias_ ?? "");
            }
            set
            {
                alias_ = StringDataNormalize(value);
            }
        }

        public string Information
        {
            get
            {
                return (information_ ?? "");
            }
            set
            {
                information_ = StringDataNormalize(value);
            }
        }

        public string Source
        {
            get
            {
                return (source_ ?? "");
            }
            private set
            {
                source_ = StringDataNormalize(value);
            }
        }

        public string Destination
        {
            get
            {
                return (destination_ ?? "");
            }
            private set
            {
                destination_ = StringDataNormalize(value);
            }
        }

        public byte UserMark
        {
            get
            {
                return (user_mark_);
            }
            set
            {
                user_mark_ = value;
            }
        }

        public string Message
        {
            get
            {
                return (message_ ?? "");
            }
            private set
            {
                message_ = StringDataNormalize(value);
            }
        }

        public virtual byte[] Data
        {
            get
            {
                return (data_ ?? new byte[] { });
            }
            private set
            {
                data_ = ((value != null) && (value.Length > 0)) ? (value) : (null);
            }
        }

        public virtual int DataLength
        {
            get { return (Data.Length); }
        }


        public override bool Equals(object obj)
        {
            if (obj is PacketObject) {
                return (this == (PacketObject)obj);
            } else {
                return (base.Equals(obj));
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool AttributeCompare(PacketObject obj)
        {
            return (   (Class == obj.Class)
                    && (Facility == obj.Facility)
                    && (Alias == obj.Alias)
                    && (Priority == obj.Priority)
                    && (Attribute == obj.Attribute)
                    && (Direction == obj.Direction)
                    && (Source == obj.Source)
                    && (Destination == obj.Destination)
                    );
        }

        public virtual string GetElementText(PacketElementID id)
        {
            switch (id) {
                case PacketElementID.Class:
                {
                    return (Class);
                }

                case PacketElementID.Facility:
                {
                    return (Facility.ToString());
                }

                case PacketElementID.Alias:
                {
                    return (Alias);
                }

                case PacketElementID.Priority:
                {
                    return (Priority.ToString());
                }

                case PacketElementID.Attribute:
                {
                    return (Attribute.ToString());
                }

                case PacketElementID.DateTime_UTC_ISO8601:
                {
                    return (MakeTime.ToUniversalTime().ToString("o"));
                }

                case PacketElementID.DateTime_UTC_Display:
                {
                    return (MakeTime.ToUniversalTime().ToString(DATETIME_FORMAT_UTC_DISPLAY));
                }

                case PacketElementID.DateTime_Local_ISO8601:
                {
                    return (MakeTime.ToLocalTime().ToString("o"));
                }

                case PacketElementID.DateTime_Local_Display:
                {
                    return (MakeTime.ToLocalTime().ToString(DATETIME_FORMAT_LOCAL_DISPLAY));
                }

                case PacketElementID.Information:
                {
                    return (Information);
                }

                case PacketElementID.Direction:
                {
                    return (Direction.ToString());
                }

                case PacketElementID.Source:
                {
                    return (Source);
                }

                case PacketElementID.Destination:
                {
                    return (Destination);
                }

                case PacketElementID.Mark:
                {
                    return (UserMark.ToString());
                }

                case PacketElementID.Data_BitString:
                {
                    return (DataToBitString());
                }

                case PacketElementID.Data_HexString:
                {
                    return (DataToHexString());
                }

                case PacketElementID.Data_TextAscii:
                {
                    return (DataToText(Encoding.ASCII));
                }

                case PacketElementID.Data_TextUTF8:
                {
                    return (DataToText(Encoding.UTF8));
                }

                case PacketElementID.Data_TextUTF16BE:
                {
                    return (DataToText(Encoding.BigEndianUnicode));
                }

                case PacketElementID.Data_TextUTF16LE:
                {
                    return (DataToText(Encoding.Unicode));
                }

                case PacketElementID.Data_TextShiftJIS:
                {
                    return (DataToText(Encoding.GetEncoding(932)));
                }

                case PacketElementID.Data_TextEucJp:
                {
                    return (DataToText(Encoding.GetEncoding(20932)));
                }

                default:
                    return ("");
            }
        }

        public string DataToBitString(string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToBitText(raw_data, 0, raw_data.Length, separator));
        }

        public string DataToBitString(int offset, int size, string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToBitText(raw_data, offset, size, separator));
        }

        public string DataToHexString(string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToHexText(raw_data, 0, raw_data.Length, separator));
        }

        public string DataToHexString(int offset, int size, string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToHexText(raw_data, offset, size, separator));
        }

        public string DataToText(Encoding enc)
        {
            try {
                if (enc == null)return ("");

                var raw_data = Data;

                if (raw_data == null)return ("");

                return (enc.GetString(raw_data));

            } catch {
                return ("");
            }
        }

        public string DataToText(Encoding enc, int offset, int size)
        {
            try {
                if (enc == null)return ("");

                var raw_data = Data;

                if (raw_data == null)return ("");

                size = Math.Min(size, Math.Max(0, enc.GetCharCount(raw_data) - offset));

                return (enc.GetString(raw_data).Substring(offset, size));

            } catch {
                return ("");
            }
        }

        public byte[] TextToData(Encoding enc, int offset, int size)
        {
            try {
                if (enc == null)return (null);

                var text_data = DataToText(enc);

                size = Math.Min(size, Math.Max(0, text_data.Length - offset));

                return (enc.GetBytes(text_data.Substring(offset, size)));

            } catch {
                return (null);
            }
        }

        public byte[] GetBytes(int offset, int size)
        {
            var data_base = Data;

            if (data_base == null)return (new byte[] { });

            size = Math.Min(size, Math.Max(0, data_base.Length - offset));

            if (size == 0)return (new byte[] { });

            var data_copy = new byte[size];

            Buffer.BlockCopy(data_base, offset, data_copy, 0, data_copy.Length);

            return (data_copy);
        }

        public byte[] GetBits(int offset, int size)
        {
            var src_data = Data;

            if (src_data == null)return (new byte[] { });

            size = Math.Min(size, Math.Max(0, src_data.Length * 8 - offset));

            if (size == 0)return (new byte[] { });

            var src_offset_byte = offset / 8;
            var src_offset_bit  = 7 - (offset % 8);

            var dst_data = new byte[(size + 7) / 8];
            var dst_offset_byte = 0;
            var dst_offset_bit  = 7 - (size % 8);

            while (size > 0) {
                dst_data[dst_offset_byte] |= (byte)(((src_data[src_offset_byte] >> src_offset_bit) & 0x01) << dst_offset_bit);

                if (src_offset_bit >= 0) {
                    src_offset_bit--;
                }
                if (src_offset_bit < 0) {
                    src_offset_bit = 7;
                    src_offset_byte++;

                    if (src_offset_byte >= src_data.Length)break;
                }

                if (dst_offset_bit >= 0) {
                    dst_offset_bit--;
                }
                if (dst_offset_bit < 0) {
                    dst_offset_bit = 7;
                    dst_offset_byte++;
                }

                size--;
            }

            return (dst_data);
        }


        private enum FormatArgument { Format, Type, Offset, Size }

        public string GetFormatString(string pattern)
        {
            var blocks = pattern.Split(':');

            /* 引数取得 */
            var format = "HEXTEXT";
            var type   = "BYTE";
            var offset = 0;
            var size   = int.MaxValue;

            if ((blocks.Length > (int)FormatArgument.Format) && (blocks[(int)FormatArgument.Format].Length > 0)) {
                format = blocks[(int)FormatArgument.Format];
            }

            if ((blocks.Length > (int)FormatArgument.Type) && (blocks[(int)FormatArgument.Type].Length > 0)) {
                type = blocks[(int)FormatArgument.Type];
            }

            if ((blocks.Length > (int)FormatArgument.Offset) && (blocks[(int)FormatArgument.Offset].Length > 0)) {
                try { offset = int.Parse(blocks[(int)FormatArgument.Offset]); } catch {}
            }

            if ((blocks.Length > (int)FormatArgument.Size) && (blocks[(int)FormatArgument.Size].Length > 0)) {
                try { size = int.Parse(blocks[(int)FormatArgument.Size]); } catch {}
            }

            /* データ取得 */
            var data = (byte[])null;

            switch (type.ToUpper()) {
                case "BYTE":     data = GetBytes(offset, size);                                break;
                case "BIT":      data = GetBits(offset, size);                                 break;
                case "ASCII":    data = TextToData(Encoding.ASCII, offset, size);              break;
                case "UTF8":     data = TextToData(Encoding.UTF8, offset, size);               break;
                case "UTF16BE":  data = TextToData(Encoding.BigEndianUnicode, offset, size);   break;
                case "UTF16LE":  data = TextToData(Encoding.Unicode, offset, size);            break;
                case "SHIFTJIS": data = TextToData(Encoding.GetEncoding(932), offset, size);   break;
                case "EUCJP":    data = TextToData(Encoding.GetEncoding(20932), offset, size); break;
                default:         return (pattern);
            }

            /* データ変換 */
            var result = "";

            switch (format.ToUpper()) {
                case "HEXTEXT":   result = HexTextEncoder.ToHexText(data);                   break;
                case "BITTEXT":   result = HexTextEncoder.ToBitText(data);                   break;
                case "ASCII":     result = Encoding.UTF8.GetString(data);                    break;
                case "UTF8":      result = Encoding.UTF8.GetString(data);                    break;
                case "UTF16BE":   result = Encoding.BigEndianUnicode.GetString(data);        break;
                case "UTF16LE":   result = Encoding.Unicode.GetString(data);                 break;
                case "SHIFTJIS":  result = Encoding.GetEncoding(932).GetString(data);        break;
                case "EUCJP":     result = Encoding.GetEncoding(20932).GetString(data);      break;
                case "INT8":      result = StructEncoder.ToSByte(data).ToString();           break;
                case "UINT8":     result = StructEncoder.ToByte(data).ToString();            break;
                case "INT16LE":   result = StructEncoder.ToInt16(data, true).ToString();     break;
                case "INT32LE":   result = StructEncoder.ToInt32(data, true).ToString();     break;
                case "INT64LE":   result = StructEncoder.ToInt64(data, true).ToString();     break;
                case "UINT16LE":  result = StructEncoder.ToUInt16(data, true).ToString();    break;
                case "UINT32LE":  result = StructEncoder.ToUInt32(data, true).ToString();    break;
                case "UINT64LE":  result = StructEncoder.ToUInt64(data, true).ToString();    break;
                case "INT16BE":   result = StructEncoder.ToInt16(data, false).ToString();    break;
                case "INT32BE":   result = StructEncoder.ToInt32(data, false).ToString();    break;
                case "INT64BE":   result = StructEncoder.ToInt64(data, false).ToString();    break;
                case "UINT16BE":  result = StructEncoder.ToUInt16(data, false).ToString();   break;
                case "UINT32BE":  result = StructEncoder.ToUInt32(data, false).ToString();   break;
                case "UINT64BE":  result = StructEncoder.ToUInt64(data, false).ToString();   break;
                default:          return (pattern);
            }

            return (result);
        }
    }
}
