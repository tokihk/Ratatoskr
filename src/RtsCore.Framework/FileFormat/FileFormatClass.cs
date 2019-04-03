using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Framework.FileFormat
{
    public abstract class FileFormatClass
    {
        public abstract string   Name { get; }

        public abstract string[] FileExtension { get; }

        public virtual string Detail { get { return (Name); } }
        public virtual Image  Icon   { get { return (null); } }

        public virtual bool CanRead  { get; } = true;
        public virtual bool CanWrite { get; } = true;

        public virtual FileFormatOption CreateReaderOption() { return (null); }
        public virtual FileFormatReader CreateReader() { return (null); }

        public virtual FileFormatOption CreateWriterOption() { return (null); }
        public virtual FileFormatWriter CreateWriter() { return (null); }
    }
}
