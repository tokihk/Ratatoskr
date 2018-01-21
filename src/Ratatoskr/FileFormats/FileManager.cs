using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal static class FileManager
    {
        public static FileFormatManager AllFormat => new FileFormatManager();

        static FileManager()
        {
            AllFormat.Formats.Add(new PacketLog_Rtcap.FileFormatClassImpl());
            AllFormat.Formats.Add(new PacketLog_Pcap.FileFormatClassImpl());
            AllFormat.Formats.Add(new PacketLog_Csv.FileFormatClassImpl());
            AllFormat.Formats.Add(new PacketLog_Binary.FileFormatClassImpl());
        }
    }
}
