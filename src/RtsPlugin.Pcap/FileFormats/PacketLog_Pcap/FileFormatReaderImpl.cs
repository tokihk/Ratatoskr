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
        private const int READ_PACKET_BLOCK_NUMBER = 200;


        private FileFormatOptionImpl      option_ = null;
        private PcapPacketParserOption    option_parser_ = null;
        private CaptureFileReaderDevice   device_ = null;

        private Queue<SharpPcap.RawCapture>        packets_busy_in_ = new Queue<SharpPcap.RawCapture>();
        private Queue<SharpPcap.RawCapture>        packets_busy_out_ = null;
        private Queue<Queue<SharpPcap.RawCapture>> packets_list_ = new Queue<Queue<SharpPcap.RawCapture>>();

        private ulong packets_count_in_  = 0;
        private ulong packets_count_out_ = 0;

        private IAsyncResult ar_load_device_packet_ = null;


        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        public override ulong ProgressMax
        {
            get { return (packets_count_in_); }
        }

        public override ulong ProgressNow
        {
            get { return (packets_count_out_); }
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
            option_ = option as FileFormatOptionImpl;

            if (option_ == null)return (false);

            option_parser_ = new PcapPacketParserOption(option_.ViewSourceType, option_.ViewDestinationType, option_.ViewDataType);

            device_ = new CaptureFileReaderDevice(path);

            device_.Filter = option_.Filter;

            ar_load_device_packet_ = (new LoadDevicePacketTaskDelegate(LoadDevicePacketTask)).BeginInvoke(device_, null, null);

            return (true);
        }

        protected override PacketObject OnReadPacket()
        {
            var packet = (PacketObject)null;

            do {
                if (   (packets_busy_out_ == null)
                    && (packets_list_.Count > 0)
                ) {
                    lock (packets_list_) {
                        packets_busy_out_ = packets_list_.Dequeue();
                    }
                }

                if (packets_busy_out_ != null) {
                    packet = SharpPcapPacketParser.Convert(device_, packets_busy_out_.Dequeue(), option_parser_);
                    if (packets_busy_out_.Count == 0) {
                        packets_busy_out_ = null;
                        packets_count_out_++;
                    }
                }
            } while (
                   (packet == null)
                && (   (!ar_load_device_packet_.IsCompleted)
                    || (packets_busy_out_ != null)
                ));

            return (packet);
        }

        private delegate void LoadDevicePacketTaskDelegate(CaptureFileReaderDevice device);
        private void LoadDevicePacketTask(CaptureFileReaderDevice device)
        {
            device.OnPacketArrival += Device_OnPacketArrival;

            device.Open();

            /* 全パケットの読み込み開始(完了するまで戻ってこない) */
            device_.Capture();

            /* バッファにデータがあれば残りも登録 */
            if (packets_busy_in_.Count > 0) {
                lock (packets_list_) {
                    packets_list_.Enqueue(packets_busy_in_);
                }
                packets_count_in_++;
            }
        }

        private void Device_OnPacketArrival(object sender, SharpPcap.CaptureEventArgs e)
        {
            if (e.Packet != null) {
                packets_busy_in_.Enqueue(e.Packet);

                /* バッファがいっぱいになったらリストに登録してバッファをリロード */
                if (packets_busy_in_.Count >= READ_PACKET_BLOCK_NUMBER) {
                    lock (packets_list_) {
                        packets_list_.Enqueue(packets_busy_in_);
                    }
                    packets_count_in_++;
                    packets_busy_in_ = new Queue<SharpPcap.RawCapture>();
                }
            }
        }
    }
}
