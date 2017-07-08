using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.FileFormats.PacketLog_Csv
{
    internal sealed class FileFormatWriterImpl : FileFormatWriter
    {
        public FileFormatWriterImpl() : base()
        {
        }

        protected override bool OnWrite(object obj, FileFormatOption option, Stream stream)
        {
            var packets = obj as IEnumerable<PacketObject>;

            if (packets == null)return (false);

            var option_i = option as FileFormatOptionImpl;

            if (option_i == null)return (false);

            /* エンコーディング判定 */
            var encoding = Encoding.UTF8;

            switch (option_i.CharCode) {
                case TextCharCode.UTF8:     encoding = Encoding.UTF8;             break;
                case TextCharCode.ShiftJIS: encoding = Encoding.GetEncoding(932); break;
                case TextCharCode.UnicodeB: encoding = Encoding.BigEndianUnicode; break;
                case TextCharCode.UnicodeL: encoding = Encoding.Unicode;          break;
                default:                    encoding = Encoding.UTF8;             break;
            }

            /* ファイル書き込み */
            using (var writer = new StreamWriter(stream, encoding)) {
                /* ヘッダー出力 */
                if (stream.Position == 0) {
                    if (!WriteHeader(option_i, writer))return (false);
                }

                /* 内容出力 */
                return (WriteContents(packets, option_i, writer));
            }
        }

        private bool WriteHeader(FileFormatOptionImpl option, StreamWriter writer)
        {
            var items = new List<string>();

            foreach (var order in option.ItemOrder) {
                items.Add(order.ToString());
            }

            writer.WriteLine(TextUtil.WriteCsvLine(items));
            writer.Flush();

            return (true);
        }

        private bool WriteContents(IEnumerable<PacketObject> packets, FileFormatOptionImpl option, StreamWriter writer)
        {
            var count = (ulong)0;

            foreach (var packet in packets) {
                WriteContentsRecord(packet, option, writer);

                /* 進捗更新 */
                Progress = (double)(++count) / packets.LongCount() * 100;
            }

            return (true);
        }

        private bool WriteContentsRecord(PacketObject packet, FileFormatOptionImpl option, StreamWriter writer)
        {
            try {
                var items = new List<string>();

                foreach (var order in option.ItemOrder) {
                    switch (order) {
                        case PacketElementID.Facility:
                        {
                            items.Add(packet.Facility.ToString());
                        }
                            break;
                        case PacketElementID.Alias:
                        {
                            items.Add(packet.Alias);
                        }
                            break;
                        case PacketElementID.Priority:
                        {
                            items.Add(packet.Priority.ToString());
                        }
                            break;
                        case PacketElementID.Attribute:
                        {
                            items.Add(packet.Attribute.ToString());
                        }
                            break;
                        case PacketElementID.DateTime:
                        {
                            items.Add(packet.MakeTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        }
                            break;
                        case PacketElementID.Information:
                        {
                            items.Add(packet.Information);
                        }
                            break;
                        case PacketElementID.Direction:
                        {
                            items.Add(packet.Direction.ToString());
                        }
                            break;
                        case PacketElementID.Source:
                        {
                            items.Add(packet.Source);
                        }
                            break;
                        case PacketElementID.Destination:
                        {
                            items.Add(packet.Destination);
                        }
                            break;
                        case PacketElementID.Mark:
                        {
                            items.Add(packet.UserMark.ToString());
                        }
                            break;
                        case PacketElementID.Data:
                        {
                            switch (packet.Attribute) {
                                case PacketAttribute.Control:
                                {
                                    var packet_i = packet as ControlPacketObject;

                                    items.Add(string.Format("{0}-{1}", packet_i.ControlCommand, HexTextEncoder.ToHexText(packet_i.ControlData)));
                                }
                                    break;
                                case PacketAttribute.Message:
                                {
                                    var packet_i = packet as MessagePacketObject;

                                    items.Add(packet_i.Message);
                                }
                                    break;
                                case PacketAttribute.Data:
                                {
                                    var packet_i = packet as DataPacketObject;

                                    items.Add(packet_i.GetHexText());
                                }
                                    break;
                            }
                        }
                            break;
                    }
                }

                /* 出力 */
                writer.WriteLine(TextUtil.WriteCsvLine(items));
                writer.Flush();

                return (true);

            } catch {
                return (false);
            }
        }
    }
}
