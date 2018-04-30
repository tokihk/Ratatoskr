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
    internal sealed class FileFormatWriterImpl : PacketLogWriter
    {
        private const int COMPLESSION_BLOCK_SIZE = 1024 * 128;

        private BinaryWriter writer_ = null;

        private MemoryStream block_stream_ = null;


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

        protected override void OnClose()
        {
            /* 残っているブロックを書き込み */
            if (block_stream_ != null) {
                WriteContentsBlock(writer_, block_stream_.ToArray());
                block_stream_ = null;
            }
        }

        protected override void OnWritePacket(PacketObject packet)
        {
            /* パケットをバイナリに変換 */
            var packet_b = PacketConverter.Serialize(packet);

            if (packet_b == null)return;
            if (packet_b.Length == 0)return;

            /* ブロックバッファ生成 */
            if (block_stream_ == null) {
                block_stream_ = new MemoryStream();
            }

            /* ブロックバッファに追加 */
            /* Size(4 byte) + Data(xx byte) */
            block_stream_.WriteByte((byte)(((uint)packet_b.Length >> 24) & 0xFF));
            block_stream_.WriteByte((byte)(((uint)packet_b.Length >> 16) & 0xFF));
            block_stream_.WriteByte((byte)(((uint)packet_b.Length >>  8) & 0xFF));
            block_stream_.WriteByte((byte)(((uint)packet_b.Length >>  0) & 0xFF));
            block_stream_.Write(packet_b, 0, packet_b.Length);

            /* ブロックバッファのデータ量が一定以上の場合はブロックをファイルへ出力 */
            if (block_stream_.Length >= COMPLESSION_BLOCK_SIZE) {
                WriteContentsBlock(writer_, block_stream_.ToArray());
                
                /* バッファ解放 */
                block_stream_ = null;
            }
        }

        private void WritePatternCode(FileFormatOption option, BinaryWriter writer)
        {
            writer.Write(FileFormatClassImpl.FORMATCODE);
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
