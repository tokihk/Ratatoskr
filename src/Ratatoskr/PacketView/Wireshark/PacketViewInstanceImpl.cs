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
using Ratatoskr.Native.Windows;
using Ratatoskr.PacketView;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Wireshark
{
    internal partial class PacketViewInstanceImpl : PacketViewInstance
    {
		private class RefreshRateItem
		{
			public RefreshRateItem(int value, string text)
			{
				Value = value;
				Text = text;
			}

			public int    Value { get; }
			public string Text  { get; }

			public override bool Equals(object obj)
			{
				if (obj is Decimal objc) {
					return (objc == Value);
				}

				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return (Text);
			}
		}


        private readonly string WIRESHARK_PATH         = "C:\\Program Files\\Wireshark\\Wireshark.exe";
        private readonly string WIRESHARK_STARTUP_ARGS = "-k -i -";

        private readonly DateTime UNIX_EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        private const uint PCAP_SNAPLEN = 0x0000FFFF;

        private const uint PCAP_LINKTYPE = 147;

		private PacketViewPropertyImpl viewp_;

        private object			wireshark_sync_ = new object();

        private Process         wireshark_process_;
        private BinaryWriter    wireshark_stdin_;
        private bool            wireshark_ready_ = false;

		private bool			initialize_ = false;


        public PacketViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketViewInstanceImpl(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
			viewp_ = viewp as PacketViewPropertyImpl;

            InitializeComponent();
			InitializeLinkType();

            Disposed += OnDisposed;

			Num_LinkType.Value = viewp_.LibPcapLinkType.Value;
			ChkBox_TransferWithPcapHeader.Checked = viewp_.TransferWithPcapHeader.Value;
			ChkBox_Capture_SendPacket.Checked = viewp_.SendPacketCapture.Value;
			ChkBox_Capture_RecvPacket.Checked = viewp_.RecvPacketCapture.Value;

			initialize_ = true;

			WiresharkRestart();
        }

		private void OnDisposed(object sender, EventArgs e)
        {
            WiresharkStop();
        }

		private void InitializeLinkType()
		{
			CBox_LinkType.BeginUpdate();
			{
				CBox_LinkType.Items.Clear();
				foreach (WiresharkLinkType type in Enum.GetValues(typeof(WiresharkLinkType))) {
					CBox_LinkType.Items.Add(type);
				}
			}
			CBox_LinkType.EndUpdate();
		}

		protected override void OnBackupProperty()
		{
			viewp_.LibPcapLinkType.Value = Num_LinkType.Value;
			viewp_.SendPacketCapture.Value = ChkBox_Capture_SendPacket.Checked;
			viewp_.RecvPacketCapture.Value = ChkBox_Capture_RecvPacket.Checked;
		}

		private void WiresharkStart()
        {
            WiresharkStop();

			/* 初期化が完了するまでは起動させない */
			if (!initialize_)return;

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
				wireshark_process_.Exited += WiresharkExited;

                /* Wiresharkの準備待ち */
                while (wireshark_process_.MainWindowHandle == IntPtr.Zero) { }
//                    wireshark_process_.WaitForInputIdle();

                /* pcap file headerをセット */
                WritePcapFileHeader(wireshark_stdin_);

                wireshark_ready_ = true;
            }
        }

		private void WiresharkErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Data);
        }

        private void WiresharkOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Data);
        }

		private void WiresharkExited(object sender, EventArgs e)
		{
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

		private WiresharkLinkType WiresharkLinkType_ValueToType(int value)
		{
			var type = WiresharkLinkType.LINKTYPE_UNKNOWN;

			if (Enum.IsDefined(typeof(WiresharkLinkType), value)) {
				type = (WiresharkLinkType)Enum.ToObject(typeof(WiresharkLinkType), value);
			}

			return (type);
		}

		private int WiresharkLinkType_TypeToValue(WiresharkLinkType type)
		{
			var value = (int)WiresharkLinkType.LINKTYPE_UNKNOWN;

			if (Enum.IsDefined(typeof(WiresharkLinkType), (int)type)) {
				value = (int)type;
			}

			return (value);
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

            /* snaplen (4 byte) */
			var linktype = (uint)viewp_.LibPcapLinkType.Value;

            writer.Write((byte)((linktype >> 24) & 0xFFu));
            writer.Write((byte)((linktype >> 16) & 0xFFu));
            writer.Write((byte)((linktype >>  8) & 0xFFu));
            writer.Write((byte)((linktype >>  0) & 0xFFu));

            writer.Flush();
        }

        private void WritePcapData(BinaryWriter writer, PacketObject packet)
        {
            if (packet.Attribute != PacketAttribute.Data)return;

			if (   ((packet.Direction == PacketDirection.Send) && (!viewp_.SendPacketCapture.Value))
				|| ((packet.Direction == PacketDirection.Recv) && (!viewp_.RecvPacketCapture.Value))
			) {
				return;
			}

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

			if (viewp_.TransferWithPcapHeader.Value) {
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
			}

            /* Data (x byte) */
            writer.Write(data);
        }

		private void Num_LinkType_ValueChanged(object sender, EventArgs e)
		{
			CBox_LinkType.SelectedItem = WiresharkLinkType_ValueToType((int)Num_LinkType.Value);

			viewp_.LibPcapLinkType.Value = Num_LinkType.Value;

			RedrawPacket();
		}

		private void ChkBox_TransferWithPcapHeader_CheckedChanged(object sender, EventArgs e)
		{
			viewp_.TransferWithPcapHeader.Value = ChkBox_TransferWithPcapHeader.Checked;

			RedrawPacket();
		}

		private void CBox_LinkType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Num_LinkType.Value = WiresharkLinkType_TypeToValue((WiresharkLinkType)CBox_LinkType.SelectedItem);
		}

		private void ChkBox_Capture_SendPacket_CheckedChanged(object sender, EventArgs e)
		{
			viewp_.SendPacketCapture.Value = ChkBox_Capture_SendPacket.Checked;
		}

		private void ChkBox_Capture_RecvPacket_CheckedChanged(object sender, EventArgs e)
		{
			viewp_.RecvPacketCapture.Value = ChkBox_Capture_RecvPacket.Checked;
		}
	}
}
