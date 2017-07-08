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

        public bool Write(object obj, FileFormatOption option, string path, bool is_append = false)
        {
            var success = false;

            progress_value_ = 0;

            try {
                using (var stream = new FileStream(path, (is_append) ? (FileMode.Append) : (FileMode.Create))) {
                    success = OnWrite(obj, option, stream);
                }
            } catch {
            }

            if (success) {
                progress_value_ = 100;
            }

            return (success);
        }

        protected virtual bool OnWrite(object obj, FileFormatOption option, Stream stream)
        {
            return (true);
        }
    }
}
