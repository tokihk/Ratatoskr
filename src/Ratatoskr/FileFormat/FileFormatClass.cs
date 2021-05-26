using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
    internal abstract class FileFormatClass
    {
        public abstract string   Name { get; }

        public abstract string[] FileExtension { get; }

        public virtual string Detail { get { return (Name); } }
        public virtual Image  Icon   { get { return (null); } }

        public virtual bool CanRead  { get; } = false;
        public virtual bool CanWrite { get; } = false;

        public virtual FileFormatReader CreateReader() { return (null); }
        public virtual FileFormatOption CreateReaderOption() { return (null); }

        public virtual FileFormatOption CreateWriterOption() { return (null); }
        public virtual FileFormatWriter CreateWriter() { return (null); }

		public override string ToString()
		{
			var str = new StringBuilder();

			str.Append(Name);

			if (FileExtension != null) {
				str.AppendFormat(" ({0})", String.Join(";", FileExtension.Select(ext => "*" + ext)));
			}

			return (str.ToString());
		}
	}
}
