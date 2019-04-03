﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Utility
{
    public static class HexTextEncoder
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

        public static readonly char[] HexCode_4Bit =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        public static readonly string[] BinCode =
        {
            "00000000", "00000001", "00000010", "00000011", "00000100", "00000101", "00000110", "00000111",
            "00001000", "00001001", "00001010", "00001011", "00001100", "00001101", "00001110", "00001111",
            "00010000", "00010001", "00010010", "00010011", "00010100", "00010101", "00010110", "00010111",
            "00011000", "00011001", "00011010", "00011011", "00011100", "00011101", "00011110", "00011111",
            "00100000", "00100001", "00100010", "00100011", "00100100", "00100101", "00100110", "00100111",
            "00101000", "00101001", "00101010", "00101011", "00101100", "00101101", "00101110", "00101111",
            "00110000", "00110001", "00110010", "00110011", "00110100", "00110101", "00110110", "00110111",
            "00111000", "00111001", "00111010", "00111011", "00111100", "00111101", "00111110", "00111111",
            "01000000", "01000001", "01000010", "01000011", "01000100", "01000101", "01000110", "01000111",
            "01001000", "01001001", "01001010", "01001011", "01001100", "01001101", "01001110", "01001111",
            "01010000", "01010001", "01010010", "01010011", "01010100", "01010101", "01010110", "01010111",
            "01011000", "01011001", "01011010", "01011011", "01011100", "01011101", "01011110", "01011111",
            "01100000", "01100001", "01100010", "01100011", "01100100", "01100101", "01100110", "01100111",
            "01101000", "01101001", "01101010", "01101011", "01101100", "01101101", "01101110", "01101111",
            "01110000", "01110001", "01110010", "01110011", "01110100", "01110101", "01110110", "01110111",
            "01111000", "01111001", "01111010", "01111011", "01111100", "01111101", "01111110", "01111111",
            "10000000", "10000001", "10000010", "10000011", "10000100", "10000101", "10000110", "10000111",
            "10001000", "10001001", "10001010", "10001011", "10001100", "10001101", "10001110", "10001111",
            "10010000", "10010001", "10010010", "10010011", "10010100", "10010101", "10010110", "10010111",
            "10011000", "10011001", "10011010", "10011011", "10011100", "10011101", "10011110", "10011111",
            "10100000", "10100001", "10100010", "10100011", "10100100", "10100101", "10100110", "10100111",
            "10101000", "10101001", "10101010", "10101011", "10101100", "10101101", "10101110", "10101111",
            "10110000", "10110001", "10110010", "10110011", "10110100", "10110101", "10110110", "10110111",
            "10111000", "10111001", "10111010", "10111011", "10111100", "10111101", "10111110", "10111111",
            "11000000", "11000001", "11000010", "11000011", "11000100", "11000101", "11000110", "11000111",
            "11001000", "11001001", "11001010", "11001011", "11001100", "11001101", "11001110", "11001111",
            "11010000", "11010001", "11010010", "11010011", "11010100", "11010101", "11010110", "11010111",
            "11011000", "11011001", "11011010", "11011011", "11011100", "11011101", "11011110", "11011111",
            "11100000", "11100001", "11100010", "11100011", "11100100", "11100101", "11100110", "11100111",
            "11101000", "11101001", "11101010", "11101011", "11101100", "11101101", "11101110", "11101111",
            "11110000", "11110001", "11110010", "11110011", "11110100", "11110101", "11110110", "11110111",
            "11111000", "11111001", "11111010", "11111011", "11111100", "11111101", "11111110", "11111111",
        };
        
        private enum ParseMode { HexData, TextData, CharCode }

        public static string ParseCodeText(string text, bool parse_special_code)
        {
            const string CODELIST_DIG   = "0123456789ABCDEF";
            const char   CODELIST_SPACE = ' ';              // 
            const string CODELIST_SPECIAL = "$*";
            char[]       CODE_TEXTBLOCK = { '\'', '\'' };   // 'xxx'
            char[]       CODE_TYPEBLOCK = { '<', '>' };     // <xxx>

            var collector = new StringBuilder(text.Length * 2);

            var mode = ParseMode.HexData;
            var code_temp = char.MinValue;

            var hex_data_exist = false;

            var text_data_escape = false;
            var text_data_builder = new StringBuilder();
            var text_data_encoder = Encoding.UTF8;

            foreach (var code in text) {
                switch (mode) {
                    /* === デフォルトモード === */
                    case ParseMode.HexData:
                    {
                        if (code == CODE_TEXTBLOCK[0]) {
                            /* --- テキスト解析モードに移行 --- */ 
                            mode = ParseMode.TextData;

                            if (hex_data_exist) {
                                collector.Append(CODELIST_DIG[0]);
                                hex_data_exist = false;
                            }

                        } else if (code == CODE_TYPEBLOCK[0]) {
                            /* --- 文字コード指定モードに移行 --- */ 
                            mode = ParseMode.CharCode;

                            if (hex_data_exist) {
                                collector.Append(CODELIST_DIG[0]);
                                hex_data_exist = false;
                            }

                        } else {
                            /* --- 16進文字列を解析 --- */ 
                            /* 常に大文字で処理 */
                            code_temp = char.ToUpper(code);

                            /* 16進文字列かどうかをチェック */
                            if (CODELIST_DIG.Contains(code_temp)) {
                                hex_data_exist = !hex_data_exist;

                                collector.Append(code_temp);

                            } else if (code == CODELIST_SPACE) {
                                /* --- 空白文字 --- */
                                hex_data_exist = false;

                            } else if ((parse_special_code) && (CODELIST_SPECIAL.Contains(code_temp))) {
                                /* 特殊文字を許可するかどうか */
                                collector.Append(code_temp);

                            } else {
                                /* --- 解析不能コード --- */
                                return (null);
                            }
                        }
                    }
                        break;

                    case ParseMode.TextData:
                    {
                        if ((!text_data_escape) && (code == '\\')) {
                        /* --- エスケープシーケンス --- */
                            text_data_escape = true;

                        } else if ((!text_data_escape) && (code == CODE_TEXTBLOCK[1])) {
                        /* --- バイナリ解析モードへ移行 --- */
                            if (text_data_builder.Length > 0) {
                                
                                foreach (var byte_text in text_data_encoder.GetBytes(text_data_builder.ToString())) {
                                    collector.Append(byte_text.ToString("X2"));
                                }

                                text_data_builder.Clear();
                            }
                            mode = ParseMode.HexData;

                        /* --- 特殊文字収集 --- */
                        } else if (text_data_escape) {
                            switch (code) {
                                case 'n':   text_data_builder.Append('\n');     break;
                                default:    text_data_builder.Append(code);     break;
                            }
                            text_data_escape = false;

                        } else {
                        /* --- 通常文字収集 --- */
                            text_data_builder.Append(code);
                        }
                    }
                        break;

                    case ParseMode.CharCode:
                    {
                        if (code == CODE_TYPEBLOCK[1]) {
                            /* --- バイナリ解析モードへ移行 --- */
                            if (text_data_builder.Length > 0) {
                                try {
                                    text_data_encoder = Encoding.GetEncoding(text_data_builder.ToString());
                                } catch {
                                    return (null);
                                }
                                text_data_builder.Clear();
                            }
                            mode = ParseMode.HexData;

                        } else {
                        /* --- 文字収集 --- */
                            text_data_builder.Append(code);
                        }
                    }
                        break;
                }
            }

            /* 16進モード以外で終了した場合はエラー */
            if (mode != ParseMode.HexData)return (null);

            /* 最終データ収集 */
            if (hex_data_exist) {
                collector.Append(CODELIST_DIG[0]);
            }

            return (collector.ToString());
        }

        public static byte[] ToBinary(string hex_string)
        {
            const string CODELIST_DIG = "0123456789ABCDEF";

            var data = new byte[(hex_string.Length + 1) / 2];
            var data_size = 0;

            var hex_data_value = (byte)0;
            var hex_data_exist = false;

            hex_string = hex_string.ToUpper();

            foreach (var code in hex_string) {
                hex_data_value = (byte)((hex_data_value << 4) | CODELIST_DIG.IndexOf(code));

                if (hex_data_exist) {
                    data[data_size++] = hex_data_value;
                    hex_data_value = 0;
                }

                hex_data_exist = !hex_data_exist;
            }

            if (hex_data_exist) {
                data[data_size] = hex_data_value;
            }

            return (data);
        }

        public static byte[][] ToByteArrayMap(string text)
        {
            if (text == null)return (null);

            const string CODELIST_DIG      = "0123456789ABCDEF";
            const char   CODELIST_SPACE    = ' ';              // 
            const char   CODELIST_SPARATOR = '|';
            char[]       CODE_TEXTBLOCK = { '\'', '\'' };   // 'xxx'
            char[]       CODE_TYPEBLOCK = { '<', '>' };     // <xxx>

            var data_map = new Queue<byte[]>();
            var data_buffer = new List<byte>();

            var mode = ParseMode.HexData;
            var code_temp = char.MinValue;

            var hex_data_value = (byte)0;
            var hex_data_exist = false;

            var text_data_escape = false;
            var text_data_builder = new StringBuilder();
            var text_data_encoder = Encoding.UTF8;

            foreach (var code in text) {
                switch (mode) {
                    /* === デフォルトモード === */
                    case ParseMode.HexData:
                    {
                        if (code == CODELIST_SPARATOR) {
                            /* --- 新しいデータに移行 --- */ 
                            if (data_buffer.Count > 0) {
                                data_map.Enqueue(data_buffer.ToArray());
                                data_buffer = new List<byte>();
                            }

                        } else if (code == CODE_TEXTBLOCK[0]) {
                            /* --- テキスト解析モードに移行 --- */ 
                            mode = ParseMode.TextData;

                        } else if (code == CODE_TYPEBLOCK[0]) {
                            /* --- 文字コード指定モードに移行 --- */ 
                            mode = ParseMode.CharCode;

                        } else {
                            /* --- 16進文字列を解析 --- */ 
                            /* 常に大文字で処理 */
                            code_temp = char.ToUpper(code);

                            /* 16進文字列かどうかをチェック */
                            if (CODELIST_DIG.Contains(code_temp)) {
                                hex_data_value = (byte)(((hex_data_value << 4) & 0xF0) | (CODELIST_DIG.IndexOf(code_temp)));
                                hex_data_exist = !hex_data_exist;

                                /* 2バイト集まったら収集 */
                                if (!hex_data_exist) {
                                    data_buffer.Add(hex_data_value);
                                    hex_data_value = 0x00;
                                }

                            } else if (code == CODELIST_SPACE) {
                                /* --- 空白文字 --- */
                                /* データが存在する場合は強制適用 */
                                if (hex_data_exist) {
                                    data_buffer.Add(hex_data_value);
                                    hex_data_value = 0x00;
                                    hex_data_exist = false;
                                }

                            } else {
                                /* --- 解析不能コード --- */
                                return (null);
                            }
                        }
                    }
                        break;

                    case ParseMode.TextData:
                    {
                        if ((!text_data_escape) && (code == '\\')) {
                        /* --- エスケープシーケンス --- */
                            text_data_escape = true;

                        } else if ((!text_data_escape) && (code == CODE_TEXTBLOCK[1])) {
                        /* --- バイナリ解析モードへ移行 --- */
                            if (text_data_builder.Length > 0) {
                                data_buffer.AddRange(text_data_encoder.GetBytes(text_data_builder.ToString()));
                                text_data_builder.Clear();
                            }
                            mode = ParseMode.HexData;

                        /* --- 特殊文字収集 --- */
                        } else if (text_data_escape) {
                            switch (code) {
                                case 'n':   text_data_builder.Append('\n');     break;
                                default:    text_data_builder.Append(code);     break;
                            }
                            text_data_escape = false;

                        } else {
                        /* --- 文字収集 --- */
                            text_data_builder.Append(code);
                        }
                    }
                        break;

                    case ParseMode.CharCode:
                    {
                        if (code == CODE_TYPEBLOCK[1]) {
                            /* --- バイナリ解析モードへ移行 --- */
                            if (text_data_builder.Length > 0) {
                                try {
                                    text_data_encoder = Encoding.GetEncoding(text_data_builder.ToString());
                                } catch {
                                    return (null);
                                }
                                text_data_builder.Clear();
                            }
                            mode = ParseMode.HexData;

                        } else {
                        /* --- 文字収集 --- */
                            text_data_builder.Append(code);
                        }
                    }
                        break;
                }
            }

            /* 16進モード以外で終了した場合はエラー */
            if (mode != ParseMode.HexData)return (null);

            /* 最終データ収集 */
            if (hex_data_exist) {
                data_buffer.Add(hex_data_value);
            }

            /* 最後のデータをリストに追加 */
            if (data_buffer.Count > 0) {
                data_map.Enqueue(data_buffer.ToArray());
            }

            return (data_map.ToArray());
        }

        public static byte[] ToByteArray(string text)
        {
            var data_map = ToByteArrayMap(text);

            return (  ((data_map != null) && (data_map.Length > 0))
                    ? (data_map[0])
                    : (null));
        }

        public static string ToHexText(IEnumerable<byte> data, string separator = "")
        {
            return (ToHexText(data, 0, data.Count(), separator));
        }

        public static string ToHexText(IEnumerable<byte> data, int offset, int size, string separator = "")
        {
            var str = new StringBuilder(data.Count() * (3 + separator.Length));
            var end = Math.Min(data.Count(), offset + size);

            while (offset < end) {
                str.Append(HexCode[data.ElementAt(offset)]);
                str.Append(separator);
                offset++;
            }

            return (str.ToString());
        }

        public static string ToBitText(IEnumerable<byte> data, string separator = "")
        {
            return (ToBitText(data, 0, data.Count(), separator));
        }

        public static string ToBitText(IEnumerable<byte> data, int offset, int size, string separator = "")
        {
            var str = new StringBuilder(data.Count() * (8 + separator.Length));
            var end = Math.Min(data.Count(), offset + size);
            
            while (offset < end) {
                str.Append(BinCode[data.ElementAt(offset)]);
                str.Append(separator);
                offset++;
            }

            return (str.ToString());
        }
    }
}