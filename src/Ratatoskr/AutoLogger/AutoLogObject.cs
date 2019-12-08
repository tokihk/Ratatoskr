using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;
using RtsCore.Framework.FileFormat;
using RtsCore.Packet;

namespace Ratatoskr.Gate.AutoLogger
{
    internal abstract class AutoLogObject
    {
        private AutoPacketSaveFormatType config_format_type_ = 0;
        private string                   config_save_dir_ = "";

        private FileFormatClass  output_format_ = null;
        private FileFormatOption output_option_ = null;
        private PacketLogWriter  output_writer_ = null;
        private string           output_path_ = null;


        public AutoLogObject()
        {
        }

        private PacketLogWriter LoadOutputWriter()
        {
            if (   (config_format_type_ != ConfigManager.System.AutoPacketSave.SaveFormat.Value)
                || (output_format_ == null)
                || (output_writer_ == null)
            ) {
                config_format_type_ = ConfigManager.System.AutoPacketSave.SaveFormat.Value;

                switch (config_format_type_) {
                    case AutoPacketSaveFormatType.Ratatoskr:
                        output_format_ = new FileFormats.PacketLog_Rtcap.FileFormatClassImpl();
                        break;
                    case AutoPacketSaveFormatType.CSV:
                        output_format_ = new FileFormats.PacketLog_Csv.FileFormatClassImpl();
                        break;
                    case AutoPacketSaveFormatType.Binary:
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
            if (output_format_ != null) {
                var save_dir = ConfigManager.System.AutoPacketSave.SaveDirectory.Value.Trim();

                if (   (output_path_ == null)
                    || (config_save_dir_ != save_dir)
                ) {
                    config_save_dir_ = save_dir;

                    /* ディレクトリパス設定 */
                    if (   (config_save_dir_ != "")
                        && (Path.IsPathRooted(config_save_dir_))
                    ) {
                        /* === 絶対パス === */
                        output_path_ = config_save_dir_;
                    } else {
                        /* === 相対パス === */
                        output_path_ = string.Format("{0}/{1}", Program.GetWorkspaceDirectory("autolog"), config_save_dir_);
                    }

                    /* ファイル名設定 */
                    output_path_ = (new Uri(
                        new Uri(output_path_),
                        string.Format(
                            "{0}_{1}{2}",
                            ConfigManager.System.AutoPacketSave.SavePrefix.Value,
                            DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                            output_format_.FileExtension[0]))).LocalPath;
                }
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
