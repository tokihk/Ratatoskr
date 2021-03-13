using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public static class TextUtil
    {
        const string UNIT_CODE_INT = " kMGTPEZY";
        const string UNIT_CODE_DEC = "munpfazy";

        public static string[] ReadCsvLine(StreamReader reader)
        {
            var csv_line = new List<string>();
            var line_end = false;

            var block_text = new StringBuilder();
            var block_mode = false;
            var escape = false;
            var code_i = 0;
            var code = (char)0;

            while ((!line_end) && ((code_i = reader.Read()) >= 0)) {
                code = (char)code_i;

                /* ダブルクォテーションで始まった場合はブロックモード */
                if ((!block_mode) && (block_text.Length == 0) && (code == '"')) {
                    block_mode = true;
                    continue;
                }

                /* ブロックモード */
                if (block_mode) {
                    /* ブロックモード中にダブルクォテーションを検出した場合はエスケープモード */
                    if ((!escape) && (code == '"')) {
                        escape = true;
                        continue;
                    }

                    if (escape) {
                        /* ブロックモードのエスケープモード中にカンマを検出した場合はブロック終了 */
                        if (code == ',') {
                            csv_line.Add(block_text.ToString());
                            block_text.Clear();
                            block_mode = false;
                        }
                        escape = false;
                        continue;
                    }

                    /* テキスト収集 */
                    block_text.Append(code);

                } else {
                    switch (code) {
                        /* 通常モード中にカンマを検出した場合はブロック終了 */
                        case ',':
                        {
                            csv_line.Add(block_text.ToString());
                            block_text.Clear();
                        }
                            break;
                        
                        /* 通常モード中に改行コードを検出した場合は行終了 */
                        case '\n':
                        {
                            csv_line.Add(block_text.ToString());
                            block_text.Clear();
                            line_end = true;
                        }
                            break;

                        default:
                        {
                            block_text.Append(code);
                        }
                            break;
                    }
                }
            }

            /* 解析途中のデータを追加 */
            if (block_text.Length > 0) {
                csv_line.Add(block_text.ToString());
            }

            return (csv_line.ToArray());
        }

        public static string WriteCsvLine(IEnumerable<string> items)
        {
            var str = new StringBuilder();

            foreach (var item in items) {
                /* ダブルクォテーションを2重化してダブルクォテーションで囲う */
                str.AppendFormat("\"{0}\",", item.Replace("\"", "\"\""));
            }

            /* ダブルクォテーションで囲っているので終端には必ずカンマを付ける */

            return (str.ToString());
        }

        public static string DecToText(ulong value)
        {
            var value_int = value;
            var value_dec = "";
            var unit_code = "";

            /* 整数部分と最上位桁に合わせた単位を取得 */
            foreach (var code in UNIT_CODE_INT) {
                if (value_int < 1000) {
                    unit_code = code.ToString();
                    break;
                }
                value_dec = string.Format(".{0:D2}", (value_int % 1000) / 10);
                value_int /= 1000;
            }

            return ((value_int.ToString() + value_dec + unit_code).TrimEnd());
        }
    }
}
