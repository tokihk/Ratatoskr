using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
    internal abstract class FileFormatReader
    {
        public FileFormatClass  Class      { get; }

        protected string           FilePath   { get; private set; } = "";
        protected FileFormatOption Option     { get; private set; } = null;
        protected FileStream       BaseStream { get; private set; } = null;


        public FileFormatReader(FileFormatClass fmtc)
        {
            Class = fmtc;
        }

        public virtual ulong ProgressMax
        {
            get { return ((BaseStream != null) ? ((ulong)BaseStream.Length) : (100)); }
        }

        public virtual ulong ProgressNow
        {
            get { return ((BaseStream != null) ? ((ulong)BaseStream.Position) : (0)); }
        }

        public bool IsOpen { get; private set; } = false;

        public bool Open(FileFormatOption option, string path)
        {
            Close();

            Option = option;

            IsOpen = OnOpenPath(option, path);

            return (IsOpen);
        }

        public void Close()
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

        protected virtual bool OnOpenPath(FileFormatOption option, string path)
        {
            try {
                BaseStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                /* 解析失敗のときはエラー */
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
