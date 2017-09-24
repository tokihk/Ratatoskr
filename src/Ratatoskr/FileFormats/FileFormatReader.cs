using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    public abstract class FileFormatReader
    {
        private double progress_value_ = 0;


        public FileFormatReader()
        {
        }

        public double Progress
        {
            get { return (progress_value_); }
            set { progress_value_ = value;  }
        }

        public bool IsOpen { get; private set; } = false;

        protected string           FilePath     { get; private set; } = null;
        protected FileStream       BaseStream   { get; private set; } = null;
        protected FileFormatOption FormatOption { get; private set; } = null;

        public bool Open(FileFormatOption option, string path)
        {
            Close();

            FormatOption = option;

            IsOpen = OnOpenPath(option, path);

            return (IsOpen);
        }

        public void Close()
        {
            IsOpen = false;

            if (BaseStream != null) {
                BaseStream.Close();
                BaseStream = null;
            }

            progress_value_ = 0;
        }

        public bool Read(object obj, FileFormatOption option)
        {
            if (!IsOpen)return (false);

            if (BaseStream != null) {
                return (OnReadStream(obj, FormatOption, BaseStream));
            } else {
                return (OnReadCustom(obj, FormatOption));
            }
        }

        protected virtual bool OnOpenPath(FileFormatOption option, string path)
        {
            try {
                FilePath = path;

                BaseStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                if (!OnOpenStream(FormatOption, BaseStream))return (false);
            
                return (true);
            } catch {
                return (false);
            }
        }

        protected virtual bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            return (true);
        }

        protected virtual bool OnReadStream(object obj, FileFormatOption option, Stream stream)
        {
            return (true);
        }

        protected virtual bool OnReadCustom(object obj, FileFormatOption option)
        {
            return (true);
        }
    }
}
