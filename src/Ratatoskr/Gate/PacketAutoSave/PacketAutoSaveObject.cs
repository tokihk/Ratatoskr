using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.FileFormats;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.PacketAutoSave
{
    internal abstract class PacketAutoSaveObject
    {
        private FileFormatClass  output_format_ = null;
        private FileFormatOption output_option_ = null;
        private PacketLogWriter  output_writer_ = null;
        private string           output_path_ = null;


        public PacketAutoSaveObject()
        {
        }

        private PacketLogWriter LoadOutputWriter()
        {
            if ((output_format_ == null) || (output_writer_ == null)) {
                switch (ConfigManager.User.Option.AutoSaveFormat.Value) {
                    case AutoSaveFormatType.Ratatoskr:
                        output_format_ = new FileFormats.PacketLog_Rtcap.FileFormatClassImpl();
                        break;
                    case AutoSaveFormatType.CSV:
                        output_format_ = new FileFormats.PacketLog_Csv.FileFormatClassImpl();
                        break;
                    case AutoSaveFormatType.Binary:
                        output_format_ = new FileFormats.PacketLog_Binary.FileFormatClassImpl();
                        break;
                }

                if (output_format_ != null) {
                    output_writer_ = output_format_.CreateWriter() as PacketLogWriter;
                    output_option_ = output_format_.CreateWriterOption();
                }
            }

            return (output_writer_);
        }

        private string LoadOutputPath()
        {
            /* 出力先が存在しない場合は新規作成 */
            if ((output_path_ == null) && (output_format_ != null)) {
                /* ディレクトリパス設定 */
                if (   (ConfigManager.User.Option.AutoSaveDirectory.Value != "")
                    && (Path.IsPathRooted(ConfigManager.User.Option.AutoSaveDirectory.Value))
                ) {
                    /* === 絶対パス === */
                    output_path_ = ConfigManager.User.Option.AutoSaveDirectory.Value;
                } else {
                    /* === 相対パス === */
                    output_path_ = string.Format("{0}/{1}", Program.GetWorkspaceDirectory("autolog"), ConfigManager.User.Option.AutoSaveDirectory.Value);
                }

                /* ファイル名設定 */
                output_path_ = (new Uri(
                    new Uri(output_path_),
                    string.Format(
                        "{0}_{1}.{2}",
                        ConfigManager.User.Option.AutoSavePrefix.Value,
                        DateTime.Now.ToString("yyyyMMddHHmmss"),
                        output_format_.FileExtension[0]))).LocalPath;
            }

            return (output_path_);
        }

        protected void WritePacket(IEnumerable<PacketObject> packets)
        {
            var writer = LoadOutputWriter();

            if (writer == null)return;

            var path = LoadOutputPath();

            if (path == null)return;

            if (writer.Open(output_option_, path, true)) {
                writer.WritePacket(packets);
                writer.Close();
            }
        }

        protected long GetOutputFileSize()
        {
            return ((new FileInfo(output_path_)).Length);
        }

        protected void ChangeNewFile()
        {
            output_path_ = null;
        }

        public void Output(IEnumerable<PacketObject> packets)
        {
            OnOutput(packets);
        }

        protected virtual void OnOutput(IEnumerable<PacketObject> packets)
        {
            WritePacket(packets);
        }
    }
}
