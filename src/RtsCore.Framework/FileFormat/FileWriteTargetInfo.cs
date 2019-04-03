using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Framework.FileFormat
{
    public class FileWriteTargetInfo
    {
        public string            FilePath { get; set; }
        public FileFormatWriter  Writer   { get; set; }
        public FileFormatOption  Option   { get; set; }
    }
}
