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
    internal sealed class FileFormatWriterImpl : PacketLogWriter
    {
        private BinaryWriter               writer_ = null;
        private FileFormatWriterOptionImpl option_;


        public FileFormatWriterImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            writer_ = new BinaryWriter(stream);
            option_ = option as FileFormatWriterOptionImpl;

            return (true);
        }

        protected override void OnWritePacket(PacketObject packet)
        {
            WriteContentsRecord(packet, option_, writer_);
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
