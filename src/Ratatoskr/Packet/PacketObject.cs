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
        Facility,
        Alias,
        Priority,
        Attribute,
        DateTime,
        Direction,
        Information,
        Source,
        Destination,
        Mark,
        Message,
        Data,
    }

    public enum PacketFacility : byte
    {
        System,
        View,
        Device,
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
        public PacketObject(
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
            Message = (message ?? "");
            Data = (data ?? new byte[] { });
        }

        public PacketObject(PacketObject packet)
            : this(
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


        public DateTime MakeTime { get; }

        public PacketFacility  Facility { get; }
        public PacketPriority  Priority { get; }
        public PacketAttribute Attribute { get; }

        public string Alias       { get; set; }
        public string Information { get; set; }

        public PacketDirection Direction { get; }

        public string Source      { get; }
        public string Destination { get; }

        public byte UserMark { get; }

        public string Message { get; }

        public virtual byte[] Data { get; }
        public virtual int    DataLength { get { return (Data.Length); } }


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
            return (   (Facility == obj.Facility)
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

                case PacketElementID.DateTime:
                {
                    return (MakeTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
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

                case PacketElementID.Data:
                {
                    return (GetHexText());
                }

                default:
                    return ("");
            }
        }

        public static string GetCsvHeaderString()
        {
            var str = new StringBuilder();

            foreach (var id in Enum.GetNames(typeof(PacketElementID))) {
                str.Append(id.ToString());
                str.Append(',');
            }

            if (str.Length > 0) {
                str.Remove(str.Length - 1, 1);
            }

            return (str.ToString());
        }

        public string GetCsvDataString()
        {
            var str = new StringBuilder();

            foreach (PacketElementID id in Enum.GetValues(typeof(PacketElementID))) {
                str.Append(GetElementText(id));
                str.Append(',');
            }

            if (str.Length > 0) {
                str.Remove(str.Length - 1, 1);
            }

            return (str.ToString());
        }

        public string GetHexText(string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToHexText(raw_data, 0, raw_data.Length, separator));
        }

        public string GetHexText(int offset, int size, string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToHexText(raw_data, offset, size, separator));
        }

        public string GetBitText(string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToBitText(raw_data, 0, raw_data.Length, separator));
        }

        public string GetBinText(int offset, int size, string separator = "")
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToBitText(raw_data, offset, size, separator));
        }

        public string GetAsciiText()
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (Encoding.ASCII.GetString(raw_data));
        }

        public string GetAsciiText(int offset, int size)
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            size = Math.Min(size, Math.Max(0, Encoding.ASCII.GetCharCount(raw_data) - offset));

            return (Encoding.ASCII.GetString(raw_data).Substring(offset, size));
        }

        public string GetUtf8Text()
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return (Encoding.UTF8.GetString(raw_data));
        }

        public string GetUtf8Text(int offset, int size)
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            size = Math.Min(size, Math.Max(0, Encoding.UTF8.GetCharCount(raw_data) - offset));

            return (Encoding.UTF8.GetString(raw_data).Substring(offset, size));
        }

        public string GetUnicodeText(bool little_endian)
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            return ((little_endian) ?
                    (Encoding.Unicode.GetString(raw_data)) :
                    (Encoding.BigEndianUnicode.GetString(raw_data)));
        }

        public string GetUnicodeText(int offset, int size, bool little_endian)
        {
            var raw_data = Data;

            if (raw_data == null)return ("");

            if (little_endian) {
                size = Math.Min(size, Math.Max(0, Encoding.Unicode.GetCharCount(raw_data) - offset));

                return (Encoding.Unicode.GetString(raw_data).Substring(offset, size));

            } else {
                size = Math.Min(size, Math.Max(0, Encoding.BigEndianUnicode.GetCharCount(raw_data) - offset));

                return (Encoding.BigEndianUnicode.GetString(raw_data).Substring(offset, size));
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
            var format = "HEX";
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
                case "BYTE":     data = GetBytes(offset, size);                                  break;
                case "BIT":      data = GetBits(offset, size);                                   break;
                case "ASCII":    data = Encoding.ASCII.GetBytes(GetAsciiText(offset, size));     break;
                case "UTF8":     data = Encoding.UTF8.GetBytes(GetUtf8Text(offset, size));       break;
                case "UNICODEL": data = Encoding.Unicode.GetBytes(GetUnicodeText(offset, size, true)); break;
                case "UNICODEB": data = Encoding.BigEndianUnicode.GetBytes(GetUnicodeText(offset, size, false)); break;
                default:         return (pattern);
            }

            /* データ変換 */
            var result = "";

            switch (format.ToUpper()) {
                case "HEXTEXT":   result = HexTextEncoder.ToHexText(data);                 break;
                case "BITTEXT":   result = HexTextEncoder.ToBitText(data);                 break;
                case "ASCII":     result = Encoding.UTF8.GetString(data);                  break;
                case "UTF8":      result = Encoding.UTF8.GetString(data);                  break;
                case "UNICODEL":  result = Encoding.Unicode.GetString(data);               break;
                case "UNICODEB":  result = Encoding.BigEndianUnicode.GetString(data);      break;
                case "INT8":      result = ByteEncoder.ToSByte(data).ToString();           break;
                case "UINT8":     result = ByteEncoder.ToByte(data).ToString();            break;
                case "INT16L":    result = ByteEncoder.ToInt16(data, true).ToString();     break;
                case "INT32L":    result = ByteEncoder.ToInt32(data, true).ToString();     break;
                case "INT64L":    result = ByteEncoder.ToInt64(data, true).ToString();     break;
                case "UINT16L":   result = ByteEncoder.ToUInt16(data, true).ToString();    break;
                case "UINT32L":   result = ByteEncoder.ToUInt32(data, true).ToString();    break;
                case "UINT64L":   result = ByteEncoder.ToUInt64(data, true).ToString();    break;
                case "INT16B":    result = ByteEncoder.ToInt16(data, false).ToString();    break;
                case "INT32B":    result = ByteEncoder.ToInt32(data, false).ToString();    break;
                case "INT64B":    result = ByteEncoder.ToInt64(data, false).ToString();    break;
                case "UINT16B":   result = ByteEncoder.ToUInt16(data, false).ToString();   break;
                case "UINT32B":   result = ByteEncoder.ToUInt32(data, false).ToString();   break;
                case "UINT64B":   result = ByteEncoder.ToUInt64(data, false).ToString();   break;
                default:          return (pattern);
            }

            return (result);
        }
    }
}
