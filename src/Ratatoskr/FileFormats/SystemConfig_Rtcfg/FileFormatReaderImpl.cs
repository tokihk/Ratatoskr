using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.FileFormats.SystemConfig_Rtcfg
{
    internal sealed class FileFormatReaderImpl : SystemConfigReader
    {
        private SystemConfigOption option_;

        private BinaryReader reader_ = null;

        private string profile_id_ = null;

        
        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            option_ = option as SystemConfigOption;
            if (option_ == null) {
                option_ = new SystemConfigOption();
            }

            reader_ = new BinaryReader(stream);

            /* パターンコードチェック */
            if (!ReadPatternCode(reader_))return (false);

            return (true);
        }

        protected override bool OnLoad()
        {
            /* ヘッダー情報読み込み */
            if (!ReadHeader(reader_))return (false);

            /* 同IDのプロファイルが存在する場合は上書き */
            if (ConfigManager.ProfileIsExist(profile_id_)) {
                return (false);
            }

            /* 内容読み込み */
            if (!ExtractProfileData(reader_))return (false);

            return (true);
        }

        private bool ReadPatternCode(BinaryReader reader)
        {
            var code = reader.ReadBytes(FileFormatClassImpl.FORMATCODE.Length);

            if (!FileFormatClassImpl.FORMATCODE.SequenceEqual(code)) {
                return (false);
            }

            return (true);
        }

        private bool ReadHeader(BinaryReader reader)
        {
            try {
                /* Format Version (4 Byte) */
                var version = reader.ReadBytes(4);

                /* Profile ID (1 + xx Byte) */
                var profile_id_len = reader.ReadByte();
                var profile_id = Encoding.UTF8.GetString(reader.ReadBytes(profile_id_len));
                
                if (profile_id.Length == 0)return (false);

                profile_id = profile_id_;

                return (true);

            } catch {
                return (false);
            }
        }

        private bool ExtractProfileData(BinaryReader reader)
        {
            try {
                /* プロファイルディレクトリ名を取得 */
                var profile_path = ConfigManager.GetProfilePath(profile_id_);

                /* 出力先ディレクトリを作成 */
                if (!Directory.Exists(profile_path)) {
                    Directory.CreateDirectory(profile_path);
                }

                /* アーカイブ内の全てのファイルをプロファイルディレクトリに出力 */
                using (var archive = new ZipArchive(reader.BaseStream, ZipArchiveMode.Read)) {
                    archive.ExtractToDirectory(profile_path);
                }

                return (true);

            } catch {
                return (false);
            }
        }
    }
}
