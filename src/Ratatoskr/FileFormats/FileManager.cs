using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal static class FileManager
    {
        public static FileFormatManager FileOpen      { get; } = new FileFormatManager();
        public static FileFormatManager PacketOpen    { get; } = new FileFormatManager();
        public static FileFormatManager PacketSave    { get; } = new FileFormatManager();
        public static FileFormatManager ProfileExport { get; } = new FileFormatManager();


        static FileManager()
        {
            var format_plog_rtcap = new PacketLog_Rtcap.FileFormatClassImpl();
            var format_plog_pcap = new PacketLog_Pcap.FileFormatClassImpl();
            var format_plog_csv = new PacketLog_Csv.FileFormatClassImpl();
            var format_plog_bin = new PacketLog_Binary.FileFormatClassImpl();
            var format_sysconf_rtcfg = new SystemConfig_Rtcfg.FileFormatClassImpl();

            FileOpen.Formats.Add(format_plog_rtcap);
            FileOpen.Formats.Add(format_plog_pcap);
            FileOpen.Formats.Add(format_plog_csv);
            FileOpen.Formats.Add(format_sysconf_rtcfg);

            PacketOpen.Formats.Add(format_plog_rtcap);
            PacketOpen.Formats.Add(format_plog_csv);

            PacketSave.Formats.Add(format_plog_rtcap);
            PacketSave.Formats.Add(format_plog_csv);
            PacketSave.Formats.Add(format_plog_bin);

            ProfileExport.Formats.Add(format_sysconf_rtcfg);
        }
    }
}
