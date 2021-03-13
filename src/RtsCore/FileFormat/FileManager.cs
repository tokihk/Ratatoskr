using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.FileFormat
{
    public static class FileManager
    {
        public static FileFormatManager FileOpen       { get; } = new FileFormatManager();
        public static FileFormatManager PacketLogOpen  { get; } = new FileFormatManager();
        public static FileFormatManager PacketLogSave  { get; } = new FileFormatManager();
        public static FileFormatManager UserConfigSave { get; } = new FileFormatManager();

        static FileManager()
        {
            var format_plog_rtcap = new PacketLog_Rtcap.FileFormatClassImpl();
            var format_plog_csv = new PacketLog_Csv.FileFormatClassImpl();
            var format_plog_bin = new PacketLog_Binary.FileFormatClassImpl();
            var format_uconf_rtcfg = new UserConfig_Rtcfg.FileFormatClassImpl();

            FileOpen.Formats.Add(format_plog_rtcap);
            FileOpen.Formats.Add(format_plog_csv);
            FileOpen.Formats.Add(format_plog_bin);
            FileOpen.Formats.Add(format_uconf_rtcfg);

            PacketLogOpen.Formats.Add(format_plog_rtcap);
            PacketLogOpen.Formats.Add(format_plog_csv);
            PacketLogOpen.Formats.Add(format_plog_bin);

            PacketLogSave.Formats.Add(format_plog_rtcap);
            PacketLogSave.Formats.Add(format_plog_csv);
            PacketLogSave.Formats.Add(format_plog_bin);

            UserConfigSave.Formats.Add(format_uconf_rtcfg);
        }
    }
}
