using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.General;
using SharpPcap.LibPcap;
using PacketDotNet;

namespace Ratatoskr.FileFormat.PacketLog_Pcap
{
    internal sealed class FileFormatWriterImpl : PacketLogWriter
    {
        private FileFormatWriterOptionImpl	option_ = null;

		private CaptureFileWriterDevice		pcap_device_ = null;


        public FileFormatWriterImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

		protected override bool OnOpenPath(FileFormatOption option, string path, bool is_append)
		{
			try {
				pcap_device_ = new CaptureFileWriterDevice(path, (is_append) ? (FileMode.Append) : (FileMode.Create));

				return (true);
			} catch {
				return (false);
			}
		}

        protected override void OnWritePacket(PacketObject packet)
        {
			if (packet.Attribute != PacketAttribute.Data) {
				return;
			}

			pcap_device_.Write(new SharpPcap.RawCapture(LinkLayers.Ethernet, new SharpPcap.PosixTimeval(packet.MakeTime), packet.Data));
        }
    }
}
