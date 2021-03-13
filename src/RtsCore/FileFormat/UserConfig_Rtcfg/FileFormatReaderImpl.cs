using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace RtsCore.FileFormat.UserConfig_Rtcfg
{
    public sealed class FileFormatReaderImpl : UserConfigReader
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

        protected override UserConfigData OnLoad()
        {
            var config = new UserConfigData();
            var version = (FileFormatClassImpl.FormatVersion)0;

            /* ヘッダー情報読み込み */
            if (!ReadHeader(reader_, config, ref version))return (null);

            if (version == FileFormatClassImpl.FormatVersion.Version_Unknown)return (null);

            /* 内容読み込み */
            if (!ReadArchiveData(reader_, config))return (null);

            return (config);
        }

        private bool ReadPatternCode(BinaryReader reader)
        {
            var code = reader.ReadBytes(FileFormatClassImpl.FORMATCODE.Length);

            if (!FileFormatClassImpl.FORMATCODE.SequenceEqual(code)) {
                return (false);
            }

            return (true);
        }

        private bool ReadHeader(BinaryReader reader, UserConfigData config, ref FileFormatClassImpl.FormatVersion version)
        {
            try {
                /* Format Version (4 Byte) */
                var version_i = (UInt32)0;
                
                version_i |= (UInt32)((UInt32)reader.ReadByte() << 24);
                version_i |= (UInt32)((UInt32)reader.ReadByte() << 16);
                version_i |= (UInt32)((UInt32)reader.ReadByte() <<  8);
                version_i |= (UInt32)((UInt32)reader.ReadByte() <<  0);

                /* Profile ID (1 + xx Byte) */
                var profile_id_len = reader.ReadByte();
                var profile_id_str = Encoding.UTF8.GetString(reader.ReadBytes(profile_id_len));
                
                if (profile_id_str.Length == 0)return (false);

                version = (FileFormatClassImpl.FormatVersion)version_i;
                config.ProfileID = Guid.Parse(profile_id_str);

                return (true);

            } catch {
                return (false);
            }
        }

        private bool ReadArchiveData(BinaryReader reader, UserConfigData config)
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

                return (ExtractData(archive_data, config));

            } catch {
                return (false);
            }
        }

        private bool ExtractData(byte[] archive_data, UserConfigData config)
        {
            try {
                /* Archive Dataからデータを復元 */
                config.Config = new UserConfig();

                /* アーカイブ読込 */
                using (var archive = new ZipArchive(new MemoryStream(archive_data), ZipArchiveMode.Read, false))
                {
                    foreach (var entry in archive.Entries) {
                        using (var entry_s = entry.Open()) {
                            if (entry.Name == UserConfig.CONFIG_FILE_NAME) {
                                /* === UserConfig === */
                                config.Config.LoadFromStream(entry_s);

                            } else {
                                /* === 不明なファイル === */
                                using (var reader = new BinaryReader(entry_s))
                                {
                                    config.ExtDataList.Add(entry.Name, reader.ReadBytes((int)entry.Length));
                                }
                            }
                        }
                    }
                }

                return (true);

            } catch {
                return (false);
            }
        }
    }
}
