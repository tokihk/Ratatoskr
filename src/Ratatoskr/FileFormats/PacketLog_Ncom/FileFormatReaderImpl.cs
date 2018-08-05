using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Devices.Ethernet;
using Ratatoskr.Packet;
using Ratatoskr.Drivers.WinPcap;
using Ratatoskr.Utility;

namespace Ratatoskr.FileFormats.PacketLog_Ncom
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private static readonly string HEXCODE = "0123456789ABCDEF";
        private static readonly Regex  REGPTN_TIME = new Regex(@"[\[](?<time>.*)[\]]", RegexOptions.Compiled);


        private StreamReader reader_;
        private DateTime     dt_last_;


        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            try {
                reader_ = new StreamReader(stream, Encoding.GetEncoding(932));

                return (true);
            } catch {
                return (false);
            }
        }

        protected override PacketObject OnReadPacket()
        {
            var record = "";
            var packet = (PacketObject)null;
                
            do {
                record = reader_.ReadLine();

                if (record == null)return (null);

                packet = RecordToPacket(record);

            }  while (packet == null);

            return (packet);
        }

        private PacketObject RecordToPacket(string record)
        {
            if (record == null)return (null);
            if (record.Length == 0)return (null);

            if (record[0] == '-') {
                /* --- Time Data --- */
                var match = REGPTN_TIME.Match(record);

                if (match.Success) {
                    dt_last_ = DateTime.ParseExact(match.Groups["time"].Value, "yyyy/MM/dd HH:mm:ss:fff", null);
                }

                return (null);

            } else if (HEXCODE.Contains(record[0])) {
                /* --- Frame Data ---*/
                return (CreatePacket(record, dt_last_));

            } else {
                return (null);
            }
        }

        private PacketObject CreatePacket(string record, DateTime time)
        {
            var str_data = new StringBuilder(1 + 1 + 2 + record.Length + 1);

            str_data.AppendFormat(
                "FEFE{0:X2}{1:X2}{2:G}FD",
                (((uint)record.Length / 2) >> 8) & 0x00FF,
                (((uint)record.Length / 2) >> 0) & 0x00FF,
                record);

            var data_bin = HexTextEncoder.ToByteArray(str_data.ToString());

            if (data_bin == null)return (null);

            return (new PacketObject(
                            "NCOM",
                            PacketFacility.Device,
                            "",
                            PacketPriority.Standard,
                            PacketAttribute.Data,
                            time,
                            "",
                            PacketDirection.Recv,
                            "",
                            "",
                            0,
                            "",
                            data_bin));
        }
    }
}
