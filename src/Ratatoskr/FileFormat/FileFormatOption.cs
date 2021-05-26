using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
	[Serializable]
    internal abstract class FileFormatOption
    {
        public abstract FileFormatOptionEditor GetEditor();

		public override string ToString()
		{
			return ("-");
		}
	}
}
