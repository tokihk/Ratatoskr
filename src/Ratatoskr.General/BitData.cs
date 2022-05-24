using System;
using System.Collections.Generic;
using System.Text;

namespace Ratatoskr.General
{
    public class BitData
    {
        public static readonly string[] HexCode =
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5A", "5B", "5C", "5D", "5E", "5F",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6A", "6B", "6C", "6D", "6E", "6F",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7A", "7B", "7C", "7D", "7E", "7F",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8A", "8B", "8C", "8D", "8E", "8F",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9A", "9B", "9C", "9D", "9E", "9F",
            "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF",
            "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF",
            "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF",
            "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF",
            "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF",
            "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF"
        };


        public byte[] Data   { get; private set; }
        public uint   Length { get; private set; }


        public BitData()
        {
            Data = new byte[1] { 0x00 };
            Length = 0;
        }

        public BitData(byte bitdata, uint bitlen)
        {
            Data = new byte[] { bitdata };
            Length = bitlen;
        }

        public BitData(byte[] bitdata, uint bitlen)
        {
            Data = new byte[(bitlen + 7) / 8];
            Array.Copy(bitdata, Data, Math.Min(bitdata.Length, Data.Length));
            Length = bitlen;
        }

        public BitData(uint bitlen)
        {
            Data = (bitlen > 0) ? (new byte[(bitlen + 7) / 8]) : (new byte[] {});
            Length = bitlen;
        }

        public BitData(UInt64 value, uint bitlen) : this(bitlen)
        {
            SetInteger(0, bitlen, value);
        }

        public BitData(BitData obj) : this(obj.Data, obj.Length)
        {
        }

        public override string ToString()
        {
            return (ToHexText());
        }

        public void ReverseByteEndian()
        {
            Array.Reverse(Data);
            Length = (uint)Data.Length * 8;
        }

        /* Byte単位でBit位置を入れ替え */
        public void ReverseBitEndian()
        {
            var work_data = (byte)0;

            for (var byte_index = 0; byte_index < Data.Length; byte_index++) {
                work_data = Data[byte_index];

                /* Bit反転 */
                work_data = (byte)(((work_data & 0xaa) >> 1) | ((work_data & 0x55) << 1));
                work_data = (byte)(((work_data & 0xcc) >> 2) | ((work_data & 0x33) << 2));
                work_data = (byte)(((work_data & 0xf0) >> 4) | ((work_data & 0x0f) << 4));

                /* 8bitではないブロックは詰める */
                work_data <<= (8 - Math.Min(8, (int)Length - byte_index * 8));

                Data[byte_index] = work_data;
            }
        }

        public BitData GetBitData(uint offset, uint bitlen)
        {
            offset = Math.Min(offset, Length);
            bitlen = Math.Min(bitlen, Length - offset);

            if (bitlen <= 0)return (new BitData(0));

            var dst_data = new byte[(bitlen + 7) / 8];
            var dst_offset_byte = 0;
            var dst_offset_bit  = 7;
            var dst_bitlen = bitlen;

            var src_offset_byte = (int)(offset / 8);
            var src_offset_bit  = (int)(7 - offset % 8);

            while (bitlen > 0) {
                if ((Data[src_offset_byte] & (1u << src_offset_bit)) != 0) {
                    dst_data[dst_offset_byte] |= (byte)(1u << dst_offset_bit);
                }

                if ((--dst_offset_bit) < 0) {
                    dst_offset_bit = 7;
                    dst_offset_byte++;
                }

                if ((--src_offset_bit) < 0) {
                    src_offset_bit = 7;
                    src_offset_byte++;
                }

                bitlen--;
            }

            return (new BitData(dst_data, dst_bitlen));
        }

        private UInt64 GetIntegerBit(uint bit_offset, uint bitlen)
        {
            var work_data = (UInt64)0;

            /* 最左ビットからスキャン */
            var src_offset_byte = (int)(bit_offset / 8);
            var src_offset_bit  = (int)(7 - bit_offset % 8);

            while (bitlen > 0) {
                work_data <<= 1;
                if ((Data[src_offset_byte] & (1u << src_offset_bit)) != 0) {
                    work_data |= 1;
                }

                if ((--src_offset_bit) < 0) {
                    src_offset_bit = 7;
                    src_offset_byte++;
                }

                bitlen--;
            }

            return (work_data);
        }

        private UInt64 GetIntegerByte(uint byte_offset, uint bitlen)
        {
            var dst_data = (UInt64)0;
            var work_bitlen = (int)bitlen;

            /* Byte単位で結合 */
            while (work_bitlen > 0) {
                dst_data <<= 8;
                dst_data |= Data[byte_offset++];
                work_bitlen -= 8;
			}

            /* 処理しすぎた場合はシフトで消す */
            if (work_bitlen < 0) {
                dst_data >>= (-work_bitlen);
			}

            return (dst_data);
        }

        public Int64 GetInteger(uint bit_offset, uint bitlen, bool signed = false)
        {
            bit_offset = Math.Min(bit_offset, Length);
            bitlen = Math.Min(bitlen, Length - bit_offset);

            var raw_data = (UInt64)0;

            if ((bit_offset & 0x07) == 0) {
                raw_data = GetIntegerByte(bit_offset / 8, bitlen);
			} else {
                raw_data = GetIntegerBit(bit_offset, bitlen);
			}

            var ret_data = (Int64)raw_data;

            if (signed) {
                if (bitlen < 64) {
                    /* 64bit未満の場合は計算で作成 */
                    bitlen = Math.Min(bitlen - 1, 63);

                    if (((raw_data >> (int)bitlen) & 1) != 0) {
                        /* 最上位ビットが1なら負数に変換 */
                        ret_data = (Int64)(((~raw_data) + 1) & (0xFFFFFFFFFFFFFFFFul >> (int)(63 - bitlen))) * -1;
                    } else {
                        ret_data = (Int64)raw_data;
					}
                } else {
                    /* 64bit以上のときはキャストで作成 */
                    ret_data = (Int64)raw_data;
				}
            }

            return (ret_data);
        }

        public Int64 GetInteger(uint bit_offset, bool signed = false)
        {
            return (GetInteger(bit_offset, Length, signed));
        }

        public Int64 GetInteger(bool signed = false)
        {
            return (GetInteger(0, signed));
        }

        public void SetBitData(uint dst_offset, byte[] src_data, uint src_offset, uint bitlen)
        {
            if (Length == 0)return;
            if (src_data == null)return;
            if (src_data.Length == 0)return;

            dst_offset = Math.Min(dst_offset, Length - 1);
            src_offset = Math.Min(src_offset, (uint)src_data.Length * 8 - 1);
            bitlen = Math.Min(Length - dst_offset, (uint)src_data.Length * 8 - src_offset);

            if (bitlen <= 0)return;

            var dst_offset_byte = (int)(dst_offset / 8);
            var dst_offset_bit  = (int)(7 - dst_offset % 8);

            var src_offset_byte = (int)(src_offset / 8);
            var src_offset_bit  = (int)(7 - src_offset % 8);

            while (bitlen > 0) {
                if ((src_data[src_offset_byte] & (1u << src_offset_bit)) != 0) {
                    Data[dst_offset_byte] |= (byte)(1u << dst_offset_bit);
                } else {
                    Data[dst_offset_byte] &= (byte)(~(1u << dst_offset_bit));
                }

                if ((--dst_offset_bit) < 0) {
                    dst_offset_bit = 7;
                    dst_offset_byte++;
                }

                if ((--src_offset_bit) < 0) {
                    src_offset_bit = 7;
                    src_offset_byte++;
                }

                bitlen--;
            }
        }

        public void SetBitData(uint dst_offset, BitData src_data, uint src_offset, uint bitlen)
        {
            if (src_data != null) {
                SetBitData(dst_offset, src_data.Data, src_offset, src_data.Length);
            }
        }

        public void SetBitData(uint dst_offset, BitData src_data, uint bitlen)
        {
            if (src_data != null) {
                SetBitData(dst_offset, src_data.Data, 0, bitlen);
            }
        }

        public void SetBitData(uint dst_offset, BitData src_data)
        {
            if (src_data != null) {
                SetBitData(dst_offset, src_data, src_data.Length);
            }
        }

        public void SetInteger(uint offset, uint bitlen, UInt64 value, bool little_endian = false)
        {
            var src_data = new byte[8];

            src_data[0] = (byte)((value >> 56) & 0xFF);
            src_data[1] = (byte)((value >> 48) & 0xFF);
            src_data[2] = (byte)((value >> 40) & 0xFF);
            src_data[3] = (byte)((value >> 32) & 0xFF);
            src_data[4] = (byte)((value >> 24) & 0xFF);
            src_data[5] = (byte)((value >> 16) & 0xFF);
            src_data[6] = (byte)((value >>  8) & 0xFF);
            src_data[7] = (byte)((value >>  0) & 0xFF);

            SetBitData(offset, src_data, 64 - bitlen, bitlen);
        }

        public string ToHexText(string separator = "")
        {
            return (ToHexText(0, Length, separator));
        }

        public string ToHexText(uint offset, uint bitlen, string separator = "")
        {
            var str = new StringBuilder(Data.Length * (2 + separator.Length) * 8);
            var end = Math.Min(Data.Length, (offset + bitlen + 7) / 8);

            offset /= 8;            
            while (offset < end) {
                str.Append(HexCode[Data[offset]]);
                str.Append(separator);
                offset++;
            }

            return (str.ToString());
        }
    }
}
