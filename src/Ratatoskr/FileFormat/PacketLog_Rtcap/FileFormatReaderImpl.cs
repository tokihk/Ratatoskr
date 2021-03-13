using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.PacketConverter;

namespace Ratatoskr.FileFormat.PacketLog_Rtcap
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private BinaryReader reader_main_ = null;

        private MemoryStream block_stream_ = null;
        private BinaryReader block_reader_ = null;


        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            reader_main_ = new BinaryReader(stream);

            /* パターンコードチェック */
            if (!ReadPatternCode(reader_main_))return (false);

            return (true);
        }

        protected override void OnClose()
        {
            reader_main_?.Dispose();
        }

        protected override PacketObject OnReadPacket()
        {
            var packet = (PacketObject)null;

            do {
                /* ブロック読み込み */
                while ((block_reader_ == null) || (block_reader_.PeekChar() < 0)) {
                    /* ブロックが読み込めなかった場合は終了 */
                    if (!LoadCompressBlock(reader_main_))return (null);
                }

                /* 1パケット読込 */
                try {
                    var size = (UInt32)0;

                    size |= (uint)((uint)block_reader_.ReadByte() << 24);
                    size |= (uint)((uint)block_reader_.ReadByte() << 16);
                    size |= (uint)((uint)block_reader_.ReadByte() <<  8);
                    size |= (uint)((uint)block_reader_.ReadByte() <<  0);

                    packet = PacketSerializer.Deserialize(block_reader_.ReadBytes((int)size));

                } catch {
                    /* 読込が失敗した場合は繰り返す */
                }

            } while (packet == null);

            return (packet);
        }

        private bool ReadPatternCode(BinaryReader reader)
        {
            var code = reader.ReadBytes(FileFormatClassImpl.FORMATCODE.Length);

            if (!FileFormatClassImpl.FORMATCODE.SequenceEqual(code)) {
                return (false);
            }

            return (true);
        }

        private bool LoadCompressBlock(BinaryReader reader)
        {
            /* 終端検知 */
            if (reader.PeekChar() < 0)return (false);

            /* Size (4 Byte) */
            var size = (uint)0;

            size |= (uint)((uint)reader.ReadByte() << 24);
            size |= (uint)((uint)reader.ReadByte() << 16);
            size |= (uint)((uint)reader.ReadByte() <<  8);
            size |= (uint)((uint)reader.ReadByte() <<  0);

            /* Data */
            using (var stream_i = new MemoryStream(reader.ReadBytes((int)size)))
            using (var stream_c = new GZipStream(stream_i, CompressionMode.Decompress))
            {
                /* 解凍データを出力用ストリームに書き込み */
                block_stream_?.Dispose();
                block_stream_ = new MemoryStream();
                stream_c.CopyTo(block_stream_);

                /* 書込みでファイルポインタが移動しているので読込位置を初期化 */
                block_stream_.Position = 0;

                /* パケット読込用リーダー生成 */
                block_reader_?.Dispose();
                block_reader_ = new BinaryReader(block_stream_);
            }

            return (true);
        }
    }
}
