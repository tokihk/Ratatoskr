using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal abstract class FileFormatWriter
    {
        public FileFormatClass Class { get; }

        protected string           FilePath   { get; private set; } = "";
        protected FileFormatOption Option     { get; private set; } = null;
        protected FileStream       BaseStream { get; private set; } = null;


        public FileFormatWriter(FileFormatClass fmtc)
        {
            Class = fmtc;
        }

        public virtual ulong ProgressMax { get; protected set; } = 1;
        public virtual ulong ProgressNow { get; protected set; } = 1;

        public bool IsOpen { get; private set; } = false;

        public bool Open(FileFormatOption option, string path, bool is_append = false)
        {
            Close();

            Option = option;

            IsOpen = OnOpenPath(option, path, is_append);

            return (IsOpen);
        }

        public virtual void Close()
        {
            if (IsOpen) {
                OnClose();
            }

            IsOpen = false;
            FilePath = "";

            if (BaseStream != null) {
                BaseStream.Close();
                BaseStream = null;
            }
        }

        protected virtual bool OnOpenPath(FileFormatOption option, string path, bool is_append)
        {
            try {
                /* 親フォルダ生成 */
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                BaseStream = new FileStream(path, (is_append) ? (FileMode.Append) : (FileMode.Create));

                if (!OnOpenStream(Option, BaseStream)) {
                    Close();
                    return (false);
                }
            
                FilePath = path;

                return (true);

            } catch {
                Close();
                return (false);
            }
        }

        protected virtual bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            return (true);
        }

        protected virtual void OnClose()
        {
        }
    }
}
