using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;
using Ratatoskr.Utility;

namespace Ratatoskr.FileFormats.PacketLog_Csv
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private StreamReader         reader_ = null;
        private FileFormatOptionImpl option_ = null;

        private ulong line_count_ = 0;

        
        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            option_ = option as FileFormatOptionImpl;

            if (option_ == null)return (false);

            /* エンコーディング判定 */
            var encoding = Encoding.UTF8;

            switch (option_.CharCode) {
                case TextCharCode.UTF8:     encoding = Encoding.UTF8;             break;
                case TextCharCode.ShiftJIS: encoding = Encoding.GetEncoding(932); break;
                case TextCharCode.UnicodeB: encoding = Encoding.BigEndianUnicode; break;
                case TextCharCode.UnicodeL: encoding = Encoding.Unicode;          break;
                default:                    encoding = Encoding.UTF8;             break;
            }

            /* ファイル読み込み */
            reader_ = new StreamReader(stream, encoding);

            /* 状態初期化 */
            line_count_ = 0;

            return (true);
        }

        protected override PacketObject OnReadPacket()
        {
            var packet = (PacketObject)null;
            var csv_line = (string[])null;

            do {
                csv_line = TextUtil.ReadCsvLine(reader_);
                line_count_++;

                /* 最初の行をCSVヘッダーとして処理 */
                if (   (line_count_ == 1)
                    && (ReadHeader(option_, csv_line))
                ) {
                    /* ヘッダーとして認識したときは次の行を処理する */
                    continue;
                }

                /* パケット変換 */
                packet = ReadContentsRecord(option_, csv_line);
            } while (packet == null);

            return (packet);
        }

        private bool ReadHeader(FileFormatOptionImpl option, string[] items)
        {
            try {
                var order = new List<PacketElementID>();

                foreach (var item in items) {
                    order.Add((PacketElementID)Enum.Parse(typeof(PacketElementID), item));
                }    

                /* ヘッダーから取得したアイテム情報で解析アイテムを差し替える */
                option.ItemOrder.Clear();
                option.ItemOrder.AddRange(order);

                return (true);

            } catch {
                return (false);
            }    
        }

        private PacketObject ReadContentsRecord(FileFormatOptionImpl option, string[] items)
        {
            try {
                /* 要素解析 */
                var item = (string)null;

                var fac     = PacketFacility.Device;
                var alias   = "";
                var prio    = PacketPriority.Standard;
                var attr    = PacketAttribute.Data;
                var mt      = DateTime.UtcNow;
                var info    = "";
                var dir     = PacketDirection.Recv;
                var src     = "";
                var dst     = "";
                var mark    = (byte)0;
                var data_s  = "";

                foreach (var (order_value, order_index) in option.ItemOrder.Select((v, i) => (v, i))) {
                    item = items[order_index].TrimEnd();

                    switch (order_value) {
                        case PacketElementID.Facility:
                        {
                            fac = (PacketFacility)Enum.Parse(typeof(PacketFacility), item);
                        }
                            break;
                        case PacketElementID.Alias:
                        {
                            alias = item;
                        }
                            break;
                        case PacketElementID.Priority:
                        {
                            prio = (PacketPriority)Enum.Parse(typeof(PacketPriority), item);
                        }
                            break;
                        case PacketElementID.Attribute:
                        {
                            attr = (PacketAttribute)Enum.Parse(typeof(PacketAttribute), item);
                        }
                            break;
                        case PacketElementID.DateTime:
                        {
                            mt = DateTime.ParseExact(item, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                        }
                            break;
                        case PacketElementID.Information:
                        {
                            info = item;
                        }
                            break;
                        case PacketElementID.Direction:
                        {
                            dir = (PacketDirection)Enum.Parse(typeof(PacketDirection), item);
                        }
                            break;
                        case PacketElementID.Source:
                        {
                            src = item;
                        }
                            break;
                        case PacketElementID.Destination:
                        {
                            dst = item;
                        }
                            break;
                        case PacketElementID.Mark:
                        {
                            mark = byte.Parse(item);
                        }
                            break;
                        case PacketElementID.Data:
                        {
                            data_s = item;
                        }
                            break;
                    }
                }

                /* パケット生成 */
                switch (attr) {
                    case PacketAttribute.Control:
                    {
                        var blocks = data_s.Split('-');

                        return (new ControlPacketObject(
                                        fac,
                                        alias,
                                        prio,
                                        mt,
                                        mark,
                                        uint.Parse(blocks[0]),
                                        HexTextEncoder.ToByteArray(blocks[1])));
                    }

                    case PacketAttribute.Message:
                    {
                        return (new MessagePacketObject(
                                        fac,
                                        alias,
                                        prio,
                                        mt,
                                        info,
                                        mark,
                                        data_s));
                    }

                    case PacketAttribute.Data:
                    {
                        return (new StaticDataPacketObject(
                                        fac,
                                        alias,
                                        prio,
                                        mt,
                                        info,
                                        dir,
                                        src,
                                        dst,
                                        mark,
                                        HexTextEncoder.ToByteArray(data_s)));
                    }

                    default:
                        return (null);
                }

            } catch {
                return (null);
            }
        }
    }
}
