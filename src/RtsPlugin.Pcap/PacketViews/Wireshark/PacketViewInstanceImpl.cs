using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.Native;
using RtsCore.Framework.PacketView;
using RtsCore.Packet;

namespace RtsPlugin.Pcap.PacketViews.Wireshark
{
    public partial class PacketViewInstanceImpl : PacketViewInstance
    {
        private readonly string WIRESHARK_PATH         = "C:\\Program Files\\Wireshark\\Wireshark.exe";
        private readonly string WIRESHARK_STARTUP_ARGS = "-k -i -";

        private readonly DateTime UNIX_EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        private const uint PCAP_SNAPLEN = 0x0000FFFF;

        private const uint PCAP_LINKTYPE = 147;

        private object      wireshark_sync_ = new object();
        private Task        wireshark_startup_task_ = null;

        private Process         wireshark_process_;
        private BinaryWriter    wireshark_stdin_;
        private StreamReader    wireshark_stdout_;
        private StreamReader    wireshark_stderr_;
        private bool            wireshark_ready_ = false;


        public PacketViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketViewInstanceImpl(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            InitializeComponent();
            InitializeWireshark();

            Disposed += OnDisposed;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            WiresharkStop();
        }

        private void InitializeWireshark()
        {
            WiresharkStart();
        }

        private void WiresharkStart()
        {
            WiresharkStop();

            lock (wireshark_sync_) {
                /* Setup */
                wireshark_process_ = new Process();
                wireshark_process_.StartInfo.FileName = WIRESHARK_PATH;
                wireshark_process_.StartInfo.Arguments = WIRESHARK_STARTUP_ARGS;
                wireshark_process_.StartInfo.UseShellExecute = false;
                wireshark_process_.StartInfo.RedirectStandardInput = true;
                wireshark_process_.StartInfo.RedirectStandardOutput = true;
                wireshark_process_.StartInfo.RedirectStandardError = true;

                /* Wireshark起動 */
                wireshark_process_.Start();

                /* 標準入力をフック */
                wireshark_stdin_ = new BinaryWriter(wireshark_process_.StandardInput.BaseStream);

                wireshark_process_.OutputDataReceived += WiresharkOutputDataReceived;
                wireshark_process_.ErrorDataReceived += WiresharkErrorDataReceived;
                wireshark_stdout_ = wireshark_process_.StandardOutput;
                wireshark_stderr_ = wireshark_process_.StandardError;

                /* Wiresharkの準備待ち */
                while (wireshark_process_.MainWindowHandle == IntPtr.Zero) { }
//                    wireshark_process_.WaitForInputIdle();

                /* pcap file headerをセット */
                WritePcapFileHeader(wireshark_stdin_);

                /* Wiresharkのウィンドウスタイルと親ウィンドウの変更 */
                var ws_style = WinAPI.GetWindowLongPtr(wireshark_process_.MainWindowHandle, WinAPI.GWL_STYLE);

                ws_style = 0;
//                ws_style &= (~(WinAPI.WS_CAPTION | WinAPI.WS_SYSMENU | WinAPI.WS_MINIMIZEBOX | WinAPI.WS_MAXIMIZEBOX | WinAPI.WS_DLGFRAME));
//                ws_style |= WinAPI.WS_VISIBLE | WinAPI.WS_POPUPWINDOW;
                ws_style |= WinAPI.WS_VISIBLE | WinAPI.WS_POPUP;

                WinAPI.SetWindowLongPtr(wireshark_process_.MainWindowHandle, WinAPI.GWL_STYLE, ws_style);

                WinAPI.SetParent(wireshark_process_.MainWindowHandle, Panel_Wireshark.Handle);

                wireshark_ready_ = true;
            }

            WiresharkSizeAdjust();
        }

        private void WiresharkErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Data);
        }

        private void WiresharkOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Data);
        }

        private void WiresharkStop()
        {
            lock (wireshark_sync_) {
                wireshark_ready_ = false;

                if (wireshark_process_ == null) {
                    return;
                }

                if (!wireshark_process_.HasExited) {
                    wireshark_process_.Kill();
                }

                wireshark_process_.WaitForExit();

                wireshark_process_ = null;
            }
        }

        private void WiresharkRestart()
        {
            WiresharkStop();
            WiresharkStart();
        }

        private void WiresharkSizeAdjust()
        {
            lock (wireshark_sync_) {
                var client_size = Panel_Wireshark.ClientSize;

                WinAPI.MoveWindow(wireshark_process_.MainWindowHandle, 0, 0, client_size.Width, client_size.Height, true);
            }
        }

        protected override void OnClearPacket()
        {
            WiresharkRestart();
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
            base.OnDrawPacketBegin(auto_scroll);
        }

        protected override void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist)
        {
            if (!wireshark_ready_)return;

            wireshark_stdin_.Flush();
        }

        protected override void OnDrawPacket(PacketObject packet)
        {
            if (!wireshark_ready_)return;

            WritePcapData(wireshark_stdin_, packet);
        }

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
            if (packet.Attribute != PacketAttribute.Data)return;

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

        private void Panel_Wireshark_Resize(object sender, EventArgs e)
        {
            WiresharkSizeAdjust();
        }
    }
}
