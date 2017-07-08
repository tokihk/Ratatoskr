using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.FileFormats.PacketLog_Rtcap
{
    internal sealed class FileFormatReaderImpl : FileFormatReader
    {
        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnReadStream(object obj, FileFormatOption option, Stream stream)
        {
            var packets = obj as PacketContainer;

            if (packets == null)return (false);

            using (var reader = new BinaryReader(stream)) {
                /* パターンコードチェック */
                if (!ReadPatternCode(reader))return (false);

                /* 内容読み込み */
                return (ReadContents(packets, reader));
            }
        }

        private bool ReadPatternCode(BinaryReader reader)
        {
            var code = reader.ReadBytes(FileFormatClassImpl.FORMATCODE.Length);

            if (!FileFormatClassImpl.FORMATCODE.SequenceEqual(code)) {
                return (false);
            }

            return (true);
        }

        private bool ReadContents(PacketContainer packets, BinaryReader reader)
        {
            try {
                while (reader.PeekChar() != -1) {
                    ReadContentsCompressBlock(packets, reader);

                    /* 進捗更新 */
                    Progress = (double)reader.BaseStream.Position / reader.BaseStream.Length * 100;
                }

                return (true);

            } catch {
                return (false);
            }
        }

        private void ReadContentsCompressBlock(PacketContainer packets, BinaryReader reader)
        {
            /* Size (4 Byte) */
            var size = (uint)0;

            size |= (uint)((uint)reader.ReadByte() << 24);
            size |= (uint)((uint)reader.ReadByte() << 16);
            size |= (uint)((uint)reader.ReadByte() <<  8);
            size |= (uint)((uint)reader.ReadByte() <<  0);

            /* Data */
            using (var stream_i = new MemoryStream(reader.ReadBytes((int)size))) {
                using (var stream_c = new GZipStream(stream_i, CompressionMode.Decompress)) {
                    using (var stream_o = new MemoryStream()) {
                        stream_c.CopyTo(stream_o);
                        ReadContentsBlock(packets, stream_o.ToArray());
                    }
                }
            }
        }

        private void ReadContentsBlock(PacketContainer packets, byte[] data)
        {
            var size = (uint)0;

            using (var stream = new MemoryStream(data)) {
                using (var reader = new BinaryReader(stream)) {
                    while (reader.PeekChar() >= 0) {
                        /* Size (4 Byte) */
                        size = 0;
                        size |= (uint)((uint)reader.ReadByte() << 24);
                        size |= (uint)((uint)reader.ReadByte() << 16);
                        size |= (uint)((uint)reader.ReadByte() <<  8);
                        size |= (uint)((uint)reader.ReadByte() <<  0);

                        packets.Add(PacketConverter.Deserialize(reader.ReadBytes((int)size)));
                    }
                }
            }
        }
    }
}
