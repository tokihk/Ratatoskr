using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;
using RtsCore.Framework.FileFormat;
using RtsPlugin.Pcap.Utility;
using SharpPcap.LibPcap;

namespace RtsPlugin.Pcap.FileFormats.PacketLog_Pcap
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private FileFormatOptionImpl      option_ = null;
        private PcapPacketParserOption    option_parser_ = null;
        private CaptureFileReaderDevice   device_ = null;


        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
            option_ = option as FileFormatOptionImpl;

            if (option_ == null)return (false);

            option_parser_ = new PcapPacketParserOption(option_.ViewSourceType, option_.ViewDestinationType, option_.ViewDataType);

            device_ = new CaptureFileReaderDevice(path);

            device_.Filter = option_.Filter;

            device_.Open();

            return (true);
        }

        protected override PacketObject OnReadPacket()
        {
            return (SharpPcapPacketParser.Convert(device_, device_.GetNextPacket(), option_parser_));
        }
    }
}
