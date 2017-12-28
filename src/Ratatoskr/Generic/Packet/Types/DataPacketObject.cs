using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;

namespace Ratatoskr.Generic.Packet.Types
{
    [Serializable]
    internal abstract class DataPacketObject : PacketObject
    {
        public DataPacketObject(
                    PacketFacility facility,
                    string alias,
                    PacketPriority prio,
                    DateTime dt,
                    string info,
                    PacketDirection dir,
                    string src,
                    string dst,
                    byte mark
                    ) : base(
                        facility,
                        alias,
                        prio,
                        PacketAttribute.Data,
                        dt,
                        info,
                        dir,
                        src,
                        dst,
                        mark
                        )
        {
        }
        
        public DataPacketObject(PacketObject packet) : base(packet) { }

        public DataPacketObject() : base() { }

        public override string GetElementText(PacketElementID id)
        {
            switch (id) {
                case PacketElementID.Data:
                {
                    return (GetHexText());
                }

                default:
                {
                    return (base.GetElementText(id));
                }
            }
        }

        public string GetHexText(string separator = "")
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToHexText(raw_data, 0, raw_data.Length, separator));
        }

        public string GetHexText(int offset, int size, string separator = "")
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToHexText(raw_data, offset, size, separator));
        }

        public string GetBitText(string separator = "")
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToBitText(raw_data, 0, raw_data.Length, separator));
        }

        public string GetBinText(int offset, int size, string separator = "")
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return (HexTextEncoder.ToBitText(raw_data, offset, size, separator));
        }

        public string GetAsciiText()
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return (Encoding.ASCII.GetString(raw_data));
        }

        public string GetAsciiText(int offset, int size)
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            size = Math.Min(size, Math.Max(0, Encoding.ASCII.GetCharCount(raw_data) - offset));

            return (Encoding.ASCII.GetString(raw_data).Substring(offset, size));
        }

        public string GetUtf8Text()
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return (Encoding.UTF8.GetString(raw_data));
        }

        public string GetUtf8Text(int offset, int size)
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            size = Math.Min(size, Math.Max(0, Encoding.UTF8.GetCharCount(raw_data) - offset));

            return (Encoding.UTF8.GetString(raw_data).Substring(offset, size));
        }

        public string GetUnicodeText(bool little_endian)
        {
            var raw_data = GetData();

            if (raw_data == null)return ("");

            return ((little_endian) ?
                    (Encoding.Unicode.GetString(raw_data)) :
                    (Encoding.BigEndianUnicode.GetString(raw_data)));
        }

        public string GetUnicodeText(int offset, int size, bool little_endian)
        {
            var raw_data = GetData();

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
            var data_base = GetData();

            if (data_base == null)return (new byte[] { });

            size = Math.Min(size, Math.Max(0, data_base.Length - offset));

            if (size == 0)return (new byte[] { });

            var data_copy = new byte[size];

            Buffer.BlockCopy(data_base, offset, data_copy, 0, data_copy.Length);

            return (data_copy);
        }

        public byte[] GetBits(int offset, int size)
        {
            var src_data = GetData();

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
