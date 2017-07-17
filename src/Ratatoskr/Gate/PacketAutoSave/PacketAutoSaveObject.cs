﻿using System;
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
        public sealed class Writer
        {
            private FileFormatWriter writer_;
            private FileFormatOption option_;
            private string path_;

            public Writer(FileFormatWriter writer, FileFormatOption option, string path)
            {
                writer_ = writer;
                option_ = option;
                path_ = path;
            }

            public ulong FileSize
            {
                get {
                    var info = new FileInfo(path_);

                    return ((ulong)((info.Exists) ? (info.Length) : (0)));
                }
            }

            public void Write(IEnumerable<PacketObject> packets)
            {
                writer_.Write(packets, option_, path_, true);
            }
        }


        private string output_path_ = null;


        public PacketAutoSaveObject()
        {
        }

        private FileFormatClass LoadFormatClass()
        {
            switch (ConfigManager.User.Option.AutoSaveFormat.Value) {
                case AutoSaveFormatType.Ratatoskr:  return (new FileFormats.PacketLog_Rtcap.FileFormatClassImpl());
                default:                            return (null);
            }
        }

        private string LoadOutputPath(FileFormatClass format)
        {
            /* 出力先が存在しない場合は新規作成 */
            if (output_path_ == null) {
                var path_base = new Uri(Program.GetWorkspaceDirectory("autolog"));
                var path_rel = new StringBuilder();
            
                path_rel.AppendFormat(
                    "{0}/{1}_{2}.{3}",
                    ConfigManager.User.Option.AutoSaveDirectory.Value,
                    ConfigManager.User.Option.AutoSavePrefix.Value,
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    format.FileExtension[0]);
            
                output_path_ = ((new Uri(path_base, path_rel.ToString())).LocalPath);
            }

            return (output_path_);
        }

        public void ChangeNewFile()
        {
            output_path_ = null;
        }

        protected Writer GetWriter()
        {
            var format = LoadFormatClass();

            if (format == null)return (null);

            var path = LoadOutputPath(format);

            if (path == null)return (null);

            var writer = format.CreateWriter();

            if (writer == null)return (null);

            return (new Writer(writer, format.CreateWriterOption(), path));
        }

        public virtual void Output(IEnumerable<PacketObject> packets)
        {
            var writer = GetWriter();

            if (writer == null)return;

            writer.Write(packets);
        }
    }
}