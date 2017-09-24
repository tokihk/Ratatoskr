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
        private BinaryWriter writer_ = null;


        public FileFormatWriterImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            writer_ = new BinaryWriter(stream);

            return (true);
        }

        protected override bool OnWriteStream(object obj, FileFormatOption option, Stream stream)
        {
            var packets = obj as IEnumerable<PacketObject>;

            if (packets == null)return (false);

            /* 内容出力 */
            return (WriteContents(packets, null, writer_));
        }

        private bool WriteContents(IEnumerable<PacketObject> packets, FileFormatWriterOptionImpl option, BinaryWriter writer)
        {
            var count = (ulong)0;
            var count_max = packets.Count();

            foreach (var packet in packets) {
                WriteContentsRecord(packet, option, writer);

                /* 進捗更新 */
                Progress = (double)(++count) / count_max * 100;
            }

            return (true);
        }

        private bool WriteContentsRecord(PacketObject packet, FileFormatWriterOptionImpl option, BinaryWriter writer)
        {
            if (packet.Attribute != PacketAttribute.Data)return (true);

#if false
            switch (option.SaveData) {
                case SaveDataType.RecvDataOnly:
                    if (packet.Direction != PacketDirection.Recv)return (true);
                    break;
                case SaveDataType.SendDataOnly:
                    if (packet.Direction != PacketDirection.Send)return (true);
                    break;
            }
#endif

            var packet_d = packet as DataPacketObject;

            if (packet_d == null)return (false);

            writer.Write(packet_d.GetData());

            return (true);
        }
    }
}
