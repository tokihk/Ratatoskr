using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal class FileReadTargetInfo
    {
        public string            FilePath { get; set; }
        public FileFormatReader  Reader   { get; set; }
        public FileFormatOption  Option   { get; set; }
    }
}
