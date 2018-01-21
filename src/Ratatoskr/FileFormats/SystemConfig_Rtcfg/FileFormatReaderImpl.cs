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
        private string profile_name_ = null;

        private BinaryReader reader_ = null;

        
        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
            /* ファイル名をプロファイル名として記憶する */
            profile_name_ = Path.GetFileNameWithoutExtension(path);

            return (base.OnOpenPath(option, path));
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            reader_ = new BinaryReader(stream);

            /* パターンコードチェック */
            if (!ReadPatternCode(reader_))return (false);

            return (true);
        }

        protected override bool OnLoad()
        {
            /* ヘッダー情報読み込み */
            if (!ReadHeader(reader_))return (false);

            /* 同名のプロファイルが存在する場合は失敗 */
            if (ConfigManager.ProfileIsExist(profile_name_)) {
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

                /* Profile Name (1 + xx Byte) */
                var profile_name_len = reader.ReadByte();
                var profile_name = Encoding.UTF8.GetString(reader.ReadBytes(profile_name_len));

                if (profile_name.Length > 0) {
                    profile_name_ = profile_name;
                }

                return (true);

            } catch {
                return (false);
            }
        }

        private bool ExtractProfileData(BinaryReader reader)
        {
            try {
                /* プロファイルディレクトリ名を取得 */
                var profile_path = ConfigManager.GetProfilePath(profile_name_);

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
