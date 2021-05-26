using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
    internal class FileControlParam
    {
        public string				FilePath { get; set; }
        public FileFormatClass		Format   { get; set; }
		public FileFormatOption		Option   { get; set; }
    }
}
