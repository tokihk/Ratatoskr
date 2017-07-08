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


        public FileFormatReaderImpl() : base()
        {
        }

        protected override bool OnReadPath(object obj, FileFormatOption option, string path)
        {
            packets_ = obj as PacketContainer;

            if (packets_ == null)return (false);

            option_ = option as FileFormatOptionImpl;

            if (option_ == null)return (false);

            option_parse_ = new WinPcapPacketParserOption(option_.ViewSourceType, option_.ViewDestinationType, option_.ViewDataType);

            var device = new CaptureFileReaderDevice(path);

            device.Open();

            device.Filter = option_.Filter;

            device.OnPacketArrival += Device_OnPacketArrival;

            device.Capture();

            device.Close();

            return (true);
        }

        private void Device_OnPacketArrival(object sender, SharpPcap.CaptureEventArgs e)
        {
            packets_.Add(WinPcapPacketParser.Convert(e.Packet, option_parse_));
        }
    }
}
