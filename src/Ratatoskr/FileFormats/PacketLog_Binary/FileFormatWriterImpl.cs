using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.FileFormats.PacketLog_Binary
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

            var option_i = option as FileFormatWriterOptionImpl;

            if (option_i == null)return (false);

            /* ファイル書き込み */
            using (var writer = new BinaryWriter(stream)) {
                /* 内容出力 */
                return (WriteContents(packets, option_i, writer));
            }
        }

        private bool WriteContents(IEnumerable<PacketObject> packets, FileFormatWriterOptionImpl option, BinaryWriter writer)
        {
            var count = (ulong)0;

            foreach (var packet in packets) {
                WriteContentsRecord(packet, option, writer);

                /* 進捗更新 */
                Progress = (double)(++count) / packets.LongCount() * 100;
            }

            return (true);
        }

        private bool WriteContentsRecord(PacketObject packet, FileFormatWriterOptionImpl option, BinaryWriter writer)
        {
            if (packet.Attribute != PacketAttribute.Data)return (true);

            switch (option.SaveData) {
                case SaveDataType.RecvDataOnly:
                    if (packet.Direction != PacketDirection.Recv)return (true);
                    break;
                case SaveDataType.SendDataOnly:
                    if (packet.Direction != PacketDirection.Send)return (true);
                    break;
            }

            var packet_d = packet as DataPacketObject;

            if (packet_d == null)return (false);

            writer.Write(packet_d.GetData());

            return (true);
        }
    }
}
