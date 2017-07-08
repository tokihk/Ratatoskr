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

        public bool Read(object obj, FileFormatOption option, string path)
        {
            var success = false;

            progress_value_ = 0;

            try {
                success = OnReadPath(obj, option, path);
            } catch {
            }

            if (success) {
                progress_value_ = 100;
            }

            return (success);
        }

        protected virtual bool OnReadPath(object obj, FileFormatOption option, string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                return (OnReadStream(obj, option, stream));
            }
        }

        protected virtual bool OnReadStream(object obj, FileFormatOption option, Stream stream)
        {
            return (true);
        }
    }
}
