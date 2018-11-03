using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using RtsCore.Packet;

namespace Ratatoskr.FileFormats.UserConfig_Rtcfg
{
    internal sealed class FileFormatWriterImpl : UserConfigWriter
    {
        private UserConfigWriterOption option_;

        private BinaryWriter writer_ = null;


        public FileFormatWriterImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            option_ = option as UserConfigWriterOption;
            if (option_ == null) {
                option_ = new UserConfigWriterOption();
            }

            /* プロファイルが存在しないときは失敗 */
            if (!ConfigManager.ProfileIsExist(option_.TargetProfileID)) {
                return (false);
            }

            writer_ = new BinaryWriter(stream);

            return (true);
        }

        protected override bool OnSave()
        {
            /* パターンコード書込み */
            if (writer_.BaseStream.Position == 0) {
                WritePatternCode(writer_);
            }

            /* ヘッダー書き込み */
            WriteHeader(writer_);

            /* 内容書き込み */
            WriteProfile(writer_);

            return (true);
        }

        private void WritePatternCode(BinaryWriter writer)
        {
            writer.Write(FileFormatClassImpl.FORMATCODE);
        }

        private bool WriteHeader(BinaryWriter writer)
        {
            try {
                /* Format Version (4 Byte) */
                writer.Write(FileFormatClassImpl.FORMATVERSION);

                /* Profile ID (1 + xx Byte) */
                var profile_id = option_.TargetProfileID.ToString("D");

                writer.Write((byte)profile_id.Length);
                if (profile_id.Length > 0) {
                    writer.Write(Encoding.UTF8.GetBytes(profile_id));
                }

                return (true);

            } catch {
                return (false);
            }
        }

        private void WriteProfile(BinaryWriter writer)
        {
            using (var stream = new MemoryStream()) {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    /* ディレクトリ内コンテンツを全て圧縮 */
                    foreach (var path in Directory.EnumerateFiles(ConfigManager.GetProfilePath(option_.TargetProfileID))) {
                        archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    }
                }

                /* Archive Data Size (4 bytes) */
                writer.Write((byte)(((UInt32)stream.Length >> 24) & 0xFF));
                writer.Write((byte)(((UInt32)stream.Length >> 16) & 0xFF));
                writer.Write((byte)(((UInt32)stream.Length >>  8) & 0xFF));
                writer.Write((byte)(((UInt32)stream.Length >>  0) & 0xFF));

                /* Archive Data (x bytes) */
                stream.WriteTo(writer.BaseStream);
            }
        }
    }
}
