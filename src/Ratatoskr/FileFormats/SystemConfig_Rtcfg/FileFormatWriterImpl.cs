using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.FileFormats.SystemConfig_Rtcfg
{
    internal sealed class FileFormatWriterImpl : SystemConfigWriter
    {
        private SystemConfigWriterOption option_;

        private BinaryWriter writer_ = null;


        public FileFormatWriterImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            option_ = option as SystemConfigWriterOption;
            if (option_ == null) {
                option_ = new SystemConfigWriterOption();
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
                /* Format Version (0 Byte) */
                writer.Write((uint)0);

                /* Profile ID (1 + xx Byte) */
                writer.Write((byte)option_.TargetProfileID.Length);
                if (option_.TargetProfileID.Length > 0) {
                    writer.Write(Encoding.UTF8.GetBytes(option_.TargetProfileID));
                }

                return (true);

            } catch {
                return (false);
            }
        }

        private void WriteProfile(BinaryWriter writer)
        {
            using (var stream = new MemoryStream()) {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create)) {
                    foreach (var path in Directory.EnumerateFiles(ConfigManager.GetProfilePath(option_.TargetProfileID))) {
                        archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    }

                    stream.Position = 0;
                    stream.CopyTo(writer.BaseStream);
                }
            }
        }
    }
}
