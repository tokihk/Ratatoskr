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
    internal sealed class FileFormatWriterImpl : FileFormatWriter
    {
        private const int COMPLESSION_BLOCK_SIZE = 1024 * 128;

        private BinaryWriter writer_ = null;


        public FileFormatWriterImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            writer_ = new BinaryWriter(stream);

            /* パターンコード書込み */
            if (stream.Position == 0) {
                WritePatternCode(option, writer_);
            }

            return (true);
        }

        protected override bool OnWriteStream(object obj, FileFormatOption option, Stream stream)
        {
            var packets = obj as IEnumerable<PacketObject>;

            if (packets == null)return (false);

            /* パケット書き込み */
            return (WriteContents(packets, writer_));
        }

        private void WritePatternCode(FileFormatOption option, BinaryWriter writer)
        {
            writer.Write(FileFormatClassImpl.FORMATCODE);
        }

        private bool WriteContents(IEnumerable<PacketObject> packets, BinaryWriter writer)
        {
            var stream_b = (MemoryStream)null;
            var packet_b = (byte[])null;
            var count = (ulong)0;
            var count_max = (ulong)packets.Count();

            foreach (var packet in packets) {
                /* パケットをバイナリに変換 */
                packet_b = PacketConverter.Serialize(packet);

                if (packet_b == null)continue;
                if (packet_b.Length == 0)continue;

                /* ブロックバッファ生成 */
                if (stream_b == null) {
                    stream_b = new MemoryStream();
                }

                /* ブロックバッファに追加 */
                /* Size(4 byte) + Data(xx byte) */
                stream_b.WriteByte((byte)(((uint)packet_b.Length >> 24) & 0xFF));
                stream_b.WriteByte((byte)(((uint)packet_b.Length >> 16) & 0xFF));
                stream_b.WriteByte((byte)(((uint)packet_b.Length >>  8) & 0xFF));
                stream_b.WriteByte((byte)(((uint)packet_b.Length >>  0) & 0xFF));
                stream_b.Write(packet_b, 0, packet_b.Length);

                /* データ量が一定以上の場合はブロック出力 */
                if (stream_b.Length < COMPLESSION_BLOCK_SIZE)continue;

                WriteContentsBlock(writer, stream_b.ToArray());

                /* バッファ解放 */
                stream_b = null;

                /* 進捗更新 */
                Progress = (double)(++count) / count_max * 100;
            }

            /* 残っているデータを書き込み */
            if (stream_b != null) {
                WriteContentsBlock(writer, stream_b.ToArray());

                /* バッファ解放 */
                stream_b = null;
            }

            return (true);
        }

        private void WriteContentsBlock(BinaryWriter writer, byte[] data)
        {
            using (var stream_m = new MemoryStream()) {
                using (var stream_c = new GZipStream(stream_m, CompressionMode.Compress, true)) {
                    /* 圧縮 */
                    stream_c.Write(data, 0, data.Length);
                }

                /* 出力(サイズ - 4バイト) */
                var data_c = stream_m.ToArray();
                var data_c_len = (uint)data_c.Length;

                writer.Write((byte)((data_c_len >> 24) & 0xFF));
                writer.Write((byte)((data_c_len >> 16) & 0xFF));
                writer.Write((byte)((data_c_len >>  8) & 0xFF));
                writer.Write((byte)((data_c_len >>  0) & 0xFF));

                /* 出力(データ) */
                writer.Write(data_c);
            }
        }
    }
}
