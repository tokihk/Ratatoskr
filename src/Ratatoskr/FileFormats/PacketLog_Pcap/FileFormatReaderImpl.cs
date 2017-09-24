using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Devices.Ethernet;
using Ratatoskr.Generic.Packet;
using SharpPcap.LibPcap;

namespace Ratatoskr.FileFormats.PacketLog_Pcap
{
    internal sealed class FileFormatReaderImpl : FileFormatReader
    {
        private PacketContainer           packets_ = null;
        private FileFormatOptionImpl      option_ = null;
        private WinPcapPacketParserOption option_parse_ = null;
        private CaptureFileReaderDevice   device_ = null;


        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
            option_ = option as FileFormatOptionImpl;

            if (option_ == null)return (false);

            option_parse_ = new WinPcapPacketParserOption(option_.ViewSourceType, option_.ViewDestinationType, option_.ViewDataType);

            device_ = new CaptureFileReaderDevice(path);

            device_.Open();

            device_.Filter = option_.Filter;

            device_.OnPacketArrival += Device_OnPacketArrival;

            return (true);
        }

        protected override bool OnReadCustom(object obj, FileFormatOption option)
        {
            packets_ = obj as PacketContainer;

            if (packets_ == null)return (false);

            device_.Capture();

            device_.Close();

            return (true);
        }

        private void Device_OnPacketArrival(object sender, SharpPcap.CaptureEventArgs e)
        {
            packets_.Add(WinPcapPacketParser.Convert(e.Packet, option_parse_));
        }
    }
}
