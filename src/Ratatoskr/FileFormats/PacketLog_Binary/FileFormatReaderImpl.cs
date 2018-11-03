using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Generic;
using RtsCore.Packet;

namespace Ratatoskr.FileFormats.PacketLog_Binary
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private BinaryReader               reader_ = null;
        private FileFormatReaderOptionImpl option_;

        private string alias_;

        private DateTime packet_time_ = DateTime.MinValue;

        
        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
            alias_ = Path.GetFileName(Path.GetDirectoryName(path));

            return base.OnOpenPath(option, path);
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            reader_ = new BinaryReader(stream);
            option_ = option as FileFormatReaderOptionImpl;

            if (option_ == null)return (false);

            return (true);
        }

        protected override PacketObject OnReadPacket()
        {
            var packet = (PacketObject)null;
            var data = (byte[])null;

            while ((reader_.BaseStream.Position < reader_.BaseStream.Length) && (packet == null)) {
                /* ブロック単位でデータを取得 */
                data = reader_.ReadBytes((int)Math.Min((long)option_.PacketDataSize, reader_.BaseStream.Length - reader_.BaseStream.Position));

                /* パケット変換 */
                packet = ReadContentsRecord(option_, data);
            }

            return (packet);
        }

        private PacketObject ReadContentsRecord(FileFormatReaderOptionImpl option, byte[] data)
        {
            try {
                if (packet_time_ == DateTime.MinValue) {
                    packet_time_ = DateTime.Now;
                }

                var packet = new PacketObject(
                                "Binary",
                                PacketFacility.External,
                                alias_,
                                PacketPriority.Standard,
                                PacketAttribute.Data,
                                packet_time_,
                                "", PacketDirection.Recv, "", "", 0, null, data);

                packet_time_ = packet_time_.AddMilliseconds(option.PacketInterval);

                return (packet);

            } catch {
                return (null);
            }
        }
    }
}
