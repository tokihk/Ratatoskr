#define BINARY_WRITER_MODE

using RtsCore.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsPlugin.Pcap.ProtocolAnalyzer
{
    internal class ProtocolAnalyzeManager
    {
        private readonly DateTime UNIX_EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        private readonly string WIRESHARK_PATH         = "C:\\Program Files\\Wireshark\\Wireshark.exe";
        private readonly string WIRESHARK_STARTUP_ARGS = "-k -i -";

        private const uint PCAP_SNAPLEN = 0x0000FFFF;

        private const uint PCAP_LINKTYPE = 147;


        private Process         wireshark_process_;

#if BINARY_WRITER_MODE
        private BinaryWriter    wireshark_stdin_;
#else
        private StreamWriter    wireshark_stdin_;
#endif

        private bool            wireshark_running_ = false;

        private Task            analyzer_startup_task_ = null;


        public ProtocolAnalyzeManager()
        {
            wireshark_process_ = new Process();
            wireshark_process_.StartInfo.FileName = WIRESHARK_PATH;
            wireshark_process_.StartInfo.Arguments = WIRESHARK_STARTUP_ARGS;
            wireshark_process_.StartInfo.UseShellExecute = false;
            wireshark_process_.StartInfo.RedirectStandardInput = true;
            wireshark_process_.StartInfo.RedirectStandardOutput = false;

        }

        public void AnlyzerStartup()
        {
            if ((analyzer_startup_task_ != null) && (!analyzer_startup_task_.IsCompleted)) {
                return;
            }

            analyzer_startup_task_ = Task.Run(() =>
            {
                wireshark_running_ = false;

                /* Wireshark起動 */
                wireshark_process_.Start();

                /* 標準入力をフック */
#if BINARY_WRITER_MODE
                wireshark_stdin_ = new BinaryWriter(wireshark_process_.StandardInput.BaseStream);
#else
                wireshark_stdin_ = wireshark_process_.StandardInput;
#endif

                /* Wiresharkの準備待ち */
                System.Threading.Thread.Sleep(3000);

                wireshark_running_ = true;

                WritePcapFileHeader(wireshark_stdin_);

                wireshark_process_.WaitForExit();                
            });
        }

        private bool IsAnalyzerRunning
        {
            get
            {
                return ((wireshark_running_) && (!wireshark_process_.HasExited));
            }
        }

        public void InputPacket(IEnumerable<PacketObject> packets)
        {
            try {
                if (IsAnalyzerRunning) {
                    foreach (var packet in packets) {
                        if (packet.Attribute == PacketAttribute.Data) {
                            WritePcapData(wireshark_stdin_, packet);
                        }
                    }
                    wireshark_stdin_.Flush();
                }
            } catch (Exception) {
                wireshark_running_ = !wireshark_process_.HasExited;
            }
        }

#if BINARY_WRITER_MODE
        private void WritePcapFileHeader(BinaryWriter writer)
        {
            /*
            struct pcap_file_header{
                 bpf_u_int32  magic;
                 u_short  version_major;
                 u_short  version_minor;
                 bpf_int32  thiszone;      // gmt to local correction
                 bpf_u_int32  sigfigs;     // accuracy of timestamps
                 bpf_u_int32  snaplen;     // max length saved portion of each pkt
                 bpf_u_int32  linktype;    // data link type (LINKTYPE_*)
            };
            */

            /* magic (4 byte) */
            writer.Write(new byte[] { 0xA1, 0xB2, 0xC3, 0xD4 });

            /* version_major (2 byte) */
            writer.Write(new byte[] { 0x00, 0x02 });

            /* version_minor (2 byte) */
            writer.Write(new byte[] { 0x00, 0x04 });

            /* thiszone (4 byte) */
            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            /* sigfigs (4 byte) */
            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            /* sigfigs (4 byte) */
            writer.Write((byte)((PCAP_SNAPLEN >> 24) & 0xFFu));
            writer.Write((byte)((PCAP_SNAPLEN >> 16) & 0xFFu));
            writer.Write((byte)((PCAP_SNAPLEN >>  8) & 0xFFu));
            writer.Write((byte)((PCAP_SNAPLEN >>  0) & 0xFFu));

            /* linktype (4 byte) */
            writer.Write((byte)((PCAP_LINKTYPE >> 24) & 0xFFu));
            writer.Write((byte)((PCAP_LINKTYPE >> 16) & 0xFFu));
            writer.Write((byte)((PCAP_LINKTYPE >>  8) & 0xFFu));
            writer.Write((byte)((PCAP_LINKTYPE >>  0) & 0xFFu));

            writer.Flush();
        }

        private void WritePcapData(BinaryWriter writer, PacketObject packet)
        {
            WritePcapData(writer, packet.MakeTime, packet.Data);
        }

        private void WritePcapData(BinaryWriter writer, DateTime dt, byte[] data)
        {
            /*
            struct pcap_pkthdr{
                 struct timeval ts;   // time stamp
                 bpf_u_int32 caplen;  // length of portion present
                 bpf_u_int32 len;     // length this packet (off wire)
            };
            */

            /* DateTime -> Unix Epoch */
            var dt_epoch = dt - UNIX_EPOCH_TIME;

            /* ts-sec (4 byte) */
            var ts_sec = (UInt32)(dt_epoch.Ticks / 10000000);

            writer.Write((byte)((ts_sec >> 24) & 0xFFu));
            writer.Write((byte)((ts_sec >> 16) & 0xFFu));
            writer.Write((byte)((ts_sec >>  8) & 0xFFu));
            writer.Write((byte)((ts_sec >>  0) & 0xFFu));

            /* ts-usec (4 byte) */
            var ts_usec = (UInt32)((dt_epoch.Ticks % 10000000) / 10);

            writer.Write((byte)((ts_usec >> 24) & 0xFFu));
            writer.Write((byte)((ts_usec >> 16) & 0xFFu));
            writer.Write((byte)((ts_usec >>  8) & 0xFFu));
            writer.Write((byte)((ts_usec >>  0) & 0xFFu));

            /* caplen (4 byte) */
            writer.Write((byte)(((uint)data.Length >> 24) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >> 16) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  8) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  0) & 0xFFu));
            
            /* len (4 byte) */
            writer.Write((byte)(((uint)data.Length >> 24) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >> 16) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  8) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  0) & 0xFFu));

            /* Data (x byte) */
            writer.Write(data);
        }
#else
        private void WritePcapFileHeader(StreamWriter writer)
        {
            /*
            struct pcap_file_header{
                 bpf_u_int32  magic;
                 u_short  version_major;
                 u_short  version_minor;
                 bpf_int32  thiszone;      // gmt to local correction
                 bpf_u_int32  sigfigs;     // accuracy of timestamps
                 bpf_u_int32  snaplen;     // max length saved portion of each pkt
                 bpf_u_int32  linktype;    // data link type (LINKTYPE_*)
            };
            */

            /* magic (4 byte) */
            writer.Write(new byte[] { 0xA1, 0xB2, 0xC3, 0xD4 });

            /* version_major (2 byte) */
            writer.Write(new byte[] { 0x00, 0x02 });

            /* version_minor (2 byte) */
            writer.Write(new byte[] { 0x00, 0x04 });

            /* thiszone (4 byte) */
            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            /* sigfigs (4 byte) */
            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            /* sigfigs (4 byte) */
            writer.Write((byte)((PCAP_SNAPLEN >> 24) & 0xFFu));
            writer.Write((byte)((PCAP_SNAPLEN >> 16) & 0xFFu));
            writer.Write((byte)((PCAP_SNAPLEN >>  8) & 0xFFu));
            writer.Write((byte)((PCAP_SNAPLEN >>  0) & 0xFFu));

            /* linktype (4 byte) */
            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x01 });
        }

        private void WritePcapData(StreamWriter writer, PacketObject packet)
        {
            WritePcapData(writer, packet.MakeTime, packet.Data);
        }

        private void WritePcapData(StreamWriter writer, DateTime dt, byte[] data)
        {
            /*
            struct pcap_pkthdr{
                 struct timeval ts;   // time stamp
                 bpf_u_int32 caplen;  // length of portion present
                 bpf_u_int32 len;     // length this packet (off wire)
            };
            */

            /* ts-sec (4 byte) */
            var ts_sec = (UInt32)(dt.Ticks / 10000000);

            writer.Write((byte)((ts_sec >> 24) & 0xFFu));
            writer.Write((byte)((ts_sec >> 16) & 0xFFu));
            writer.Write((byte)((ts_sec >>  8) & 0xFFu));
            writer.Write((byte)((ts_sec >>  0) & 0xFFu));

            /* ts-usec (4 byte) */
            var ts_usec = (UInt32)(dt.Ticks / 10);

            writer.Write((byte)((ts_usec >> 24) & 0xFFu));
            writer.Write((byte)((ts_usec >> 16) & 0xFFu));
            writer.Write((byte)((ts_usec >>  8) & 0xFFu));
            writer.Write((byte)((ts_usec >>  0) & 0xFFu));

            /* caplen (4 byte) */
            writer.Write((byte)(((uint)data.Length >> 24) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >> 16) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  8) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  0) & 0xFFu));
            
            /* len (4 byte) */
            writer.Write((byte)(((uint)data.Length >> 24) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >> 16) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  8) & 0xFFu));
            writer.Write((byte)(((uint)data.Length >>  0) & 0xFFu));

            /* Data (x byte) */
            writer.Write(data);
        }
#endif
    }
}
