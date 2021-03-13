using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
    internal class FileReadTargetInfo
    {
        public string            FilePath { get; set; }
        public FileFormatReader  Reader   { get; set; }
        public FileFormatOption  Option   { get; set; }
    }
}
