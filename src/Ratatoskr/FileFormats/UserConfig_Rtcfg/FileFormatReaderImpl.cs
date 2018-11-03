using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Forms;
using RtsCore.Packet;

namespace Ratatoskr.FileFormats.UserConfig_Rtcfg
{
    internal sealed class FileFormatReaderImpl : UserConfigReader
    {
        private BinaryReader reader_ = null;

        
        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            reader_ = new BinaryReader(stream);

            /* パターンコードチェック */
            if (!ReadPatternCode(reader_))return (false);

            return (true);
        }

        protected override (UserConfig config, Guid profile_id) OnLoad()
        {
            /* ヘッダー情報読み込み */
            var info = ReadHeader(reader_);

            if (info.version == FileFormatClassImpl.FormatVersion.Version_Unknown)return (null, Guid.Empty);

            /* 内容読み込み */
            var config = ReadArchiveData(reader_);

            if (config == null)return (null, Guid.Empty);

            return (config, info.profile_id);
        }

        private bool ReadPatternCode(BinaryReader reader)
        {
            var code = reader.ReadBytes(FileFormatClassImpl.FORMATCODE.Length);

            if (!FileFormatClassImpl.FORMATCODE.SequenceEqual(code)) {
                return (false);
            }

            return (true);
        }

        private (FileFormatClassImpl.FormatVersion version, Guid profile_id) ReadHeader(BinaryReader reader)
        {
            try {
                /* Format Version (4 Byte) */
                var version = (UInt32)0;
                
                version |= (UInt32)((UInt32)reader.ReadByte() << 24);
                version |= (UInt32)((UInt32)reader.ReadByte() << 16);
                version |= (UInt32)((UInt32)reader.ReadByte() <<  8);
                version |= (UInt32)((UInt32)reader.ReadByte() <<  0);

                /* Profile ID (1 + xx Byte) */
                var profile_id_len = reader.ReadByte();
                var profile_id_str = Encoding.UTF8.GetString(reader.ReadBytes(profile_id_len));
                
                if (profile_id_str.Length == 0)return (0, Guid.Empty);

                return ((FileFormatClassImpl.FormatVersion)version, Guid.Parse(profile_id_str));

            } catch {
                return (0, Guid.Empty);
            }
        }

        private UserConfig ReadArchiveData(BinaryReader reader)
        {
            try {
                /* Archive Data Size (4 byte) */
                var archive_data_size = (UInt32)0;

                archive_data_size |= (UInt32)((UInt32)reader.ReadByte() << 24);
                archive_data_size |= (UInt32)((UInt32)reader.ReadByte() << 16);
                archive_data_size |= (UInt32)((UInt32)reader.ReadByte() <<  8);
                archive_data_size |= (UInt32)((UInt32)reader.ReadByte() <<  0);

                /* Archive Data (x byte) */
                var archive_data = reader.ReadBytes((int)archive_data_size);

                return (ExtractData(archive_data));

            } catch {
                return (null);
            }
        }

        private UserConfig ExtractData(byte[] archive_data)
        {
            try {
                /* Archive Dataからデータを復元 */
                var config = new UserConfig();

                /* アーカイブ読込 */
                using (var archive = new ZipArchive(new MemoryStream(archive_data), ZipArchiveMode.Read, false))
                {
                    foreach (var entry in archive.Entries) {
                        if (entry.Name == UserConfig.CONFIG_FILE_NAME) {
                            /* === UserConfig === */
                            using (var entry_s = entry.Open()) {
                                config.LoadXml(entry_s);
                            }

                        } else {
                            /* === 不明なファイル === */
                            /* 無視 */
                        }
                    }
                }

                return (config);

            } catch {
                return (null);
            }
        }
    }
}
