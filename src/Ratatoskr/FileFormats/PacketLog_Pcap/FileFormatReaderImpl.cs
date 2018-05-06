using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Devices.Ethernet;
using Ratatoskr.Packet;
using SharpPcap.LibPcap;

namespace Ratatoskr.FileFormats.PacketLog_Pcap
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private FileFormatOptionImpl      option_ = null;
        private WinPcapPacketParserOption option_parser_ = null;
        private CaptureFileReaderDevice   device_ = null;


        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
            option_ = option as FileFormatOptionImpl;

            if (option_ == null)return (false);

            option_parser_ = new WinPcapPacketParserOption(option_.ViewSourceType, option_.ViewDestinationType, option_.ViewDataType);

            device_ = new CaptureFileReaderDevice(path);

            device_.Open();

            device_.Filter = option_.Filter;

            return (true);
        }

        protected override PacketObject OnReadPacket()
        {
            return (WinPcapPacketParser.Convert(device_.GetNextPacket(), option_parser_));
        }
    }
}
