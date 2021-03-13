using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.General;

namespace Ratatoskr.FileFormat.PacketLog_Csv
{
    internal sealed class FileFormatWriterImpl : PacketLogWriter
    {
        private StreamWriter         writer_ = null;
        private FileFormatOptionImpl option_ = null;


        public FileFormatWriterImpl(FileFormatClass fmtc) : base(fmtc)
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

            writer_ = new StreamWriter(stream, encoding);

            /* ヘッダー出力 */
            if (stream.Position == 0) {
                if (!WriteHeader(option_, writer_))return (false);
            }

            return (true);
        }

        protected override void OnWritePacket(PacketObject packet)
        {
            WriteContentsRecord(packet, option_, writer_);
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

        private bool WriteContentsRecord(PacketObject packet, FileFormatOptionImpl option, StreamWriter writer)
        {
            try {
                var items = new List<string>();

                foreach (var order in option.ItemOrder) {
                    items.Add(packet.GetElementText(order));
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
