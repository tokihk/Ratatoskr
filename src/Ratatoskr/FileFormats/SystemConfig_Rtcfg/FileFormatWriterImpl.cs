using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.FileFormats.SystemConfig_Rtcfg
{
    internal sealed class FileFormatWriterImpl : SystemConfigWriter
    {
        private FileFormatWriterOptionImpl option_;

        private BinaryWriter writer_ = null;


        public FileFormatWriterImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            option_ = option as FileFormatWriterOptionImpl;
            if (option_ == null) {
                option_ = new FileFormatWriterOptionImpl();
            }

            /* プロファイルが存在しないときは失敗 */
            if (!ConfigManager.ProfileIsExist(option_.TargetProfileName)) {
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


            return (true);
        }

        private void WritePatternCode(BinaryWriter writer)
        {
            writer.Write(FileFormatClassImpl.FORMATCODE);
        }

        private bool WriteHeader(BinaryWriter writer)
        {
            try {
                /* Format Version */
                writer.Write((uint)0);

                /* Profile Name (1 + xx Byte) */
                writer.Write((byte)option_.OutputProfileName.Length);
                if (option_.OutputProfileName.Length > 0) {
                    writer.Write(Encoding.UTF8.GetBytes(option_.OutputProfileName));
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
                    foreach (var path in Directory.EnumerateFiles(ConfigManager.GetProfilePath(option_.TargetProfileName))) {
                        archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    }
                }
            }
        }
    }
}
