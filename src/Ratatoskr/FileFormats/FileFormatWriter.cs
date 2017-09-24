using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    public abstract class FileFormatWriter
    {
        private double progress_value_ = 0;


        public FileFormatWriter()
        {
        }

        public double Progress
        {
            get { return (progress_value_); }
            set { progress_value_ = value;  }
        }

        public bool IsOpen { get; private set; } = false;

        protected FileStream       BaseStream { get; private set; } = null;
        protected FileFormatOption FormatOption { get; private set; } = null;


        public bool Open(FileFormatOption option, string path, bool is_append = false)
        {
            Close();

            FormatOption = option;

            IsOpen = OnOpenPath(option, path, is_append);

            return (IsOpen);
        }

        public virtual void Close()
        {
            IsOpen = false;

            if (BaseStream != null) {
                BaseStream.Close();
                BaseStream = null;
            }

            progress_value_ = 0;
        }

        public bool Write(object obj)
        {
            if (!IsOpen)return (false);

            if (BaseStream != null) {
                return (OnWriteStream(obj, FormatOption, BaseStream));
            } else {
                return (OnWriteCustom(obj, FormatOption));
            }
        }

        protected virtual bool OnOpenPath(FileFormatOption option, string path, bool is_append)
        {
            try {
                BaseStream = new FileStream(path, (is_append) ? (FileMode.Append) : (FileMode.Create));

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

        protected virtual bool OnWriteStream(object obj, FileFormatOption option, Stream stream)
        {
            return (true);
        }

        protected virtual bool OnWriteCustom(object obj, FileFormatOption option)
        {
            return (true);
        }
    }
}
