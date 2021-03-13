using Ratatoskr.General.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Device.UsbCapture
{
    internal sealed class USBPcapCMD : IDisposable
    {
		private readonly string USBPCAPCMD_FILEPATH = @"C:\Program Files\USBPcap\USBPcapCMD.exe";
		private readonly string USBPCAPCMD_STARTUP_ARGUMENT = @"-A -o - ";

        private readonly DateTime UNIX_EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, 0);


        private Process			process_ = null;
		private StreamWriter	process_stdin_;
		private MemoryStream	process_stdout_;

		private bool			process_open_ = false;
		private bool			process_error_ = false;

		private byte[]			recv_buffer_;

		private bool			pcap_little_endian_ = false;
		private uint			pcap_snaplen_ = 0;

		private byte[]			packet_ts_sec_raw_ = new byte[4];
		private int				packet_ts_sec_raw_len_ = 0;
		private UInt32			packet_ts_sec_;

		private byte[]			packet_ts_usec_raw_ = new byte[4];
		private int				packet_ts_usec_raw_len_ = 0;
		private UInt32			packet_ts_usec_;

		private byte[]			packet_caplen_raw_ = new byte[4];
		private int				packet_caplen_raw_len_ = 0;
		private UInt32			packet_caplen_;

		private byte[]			packet_len_raw_ = new byte[4];
		private int				packet_len_raw_len_ = 0;
		private UInt32			packet_len_;

		private byte[]			packet_data_raw_ = null;
		private int				packet_data_raw_len_ = 0;


        public USBPcapCMD(string devname)
        {
            /* Setup */
            process_ = new Process();
            process_.StartInfo.FileName = USBPCAPCMD_FILEPATH;
            process_.StartInfo.Arguments = USBPCAPCMD_STARTUP_ARGUMENT + "-d " + devname;
            process_.StartInfo.UseShellExecute = false;
            process_.StartInfo.RedirectStandardInput = true;
            process_.StartInfo.RedirectStandardOutput = true;
            process_.StartInfo.RedirectStandardError = true;

            /* Wireshark起動 */
            process_.Start();

            /* 標準入出力をフック */
            process_stdin_  = process_.StandardInput;
            process_stdout_ = new MemoryStream();
			process_.ErrorDataReceived += Usbpcapcmd__ErrorDataReceived;

			/* Pcapファイルヘッダーの解析 */
			process_error_ = !CheckPcapHeader(process_stdout_);

			process_open_ = true;
        }

		~USBPcapCMD()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (process_ != null) {
				try {
					process_.Kill();
				} catch {
				}
				process_ = null;
			}

			process_open_ = false;
		}

		public bool IsOpened
		{
			get
			{
				return (process_open_);
			}
		}

		public bool IsExited
		{
			get
			{
				return ((process_open_) && (process_.HasExited));
			}
		}

		public bool IsError
		{
			get
			{
				return ((process_open_) && (process_error_));
			}
		}

		private void Usbpcapcmd__ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
		}

		private bool CheckPcapHeader(BinaryReader reader)
		{
            /* Pcapファイルヘッダーパケットをチェック */
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
			var magic = reader.ReadBytes(4);

			if (magic.SequenceEqual(new byte[] { 0xA1, 0xB2, 0xC3, 0xD4 })) {
				/* ビッグエンディアン */
				pcap_little_endian_ = false;
			} else if (magic.SequenceEqual(new byte[] { 0xD4, 0xC3, 0xB2, 0xA1 })) {
				/* リトルエンディアン */
				pcap_little_endian_ = true;
			} else {
				return (false);
			}

            /* version_major (2 byte) */
			var version_major = reader.ReadBytes(2);

            /* version_minor (2 byte) */
			var version_minor = reader.ReadBytes(2);

            /* thiszone (4 byte) */
			var thiszone = reader.ReadBytes(4);

            /* sigfigs (4 byte) */
			var sigfigs = reader.ReadBytes(4);

            /* snaplen (4 byte) */
			var snaplen = reader.ReadBytes(4);

            /* linktype (4 byte) */
			var linktype = reader.ReadBytes(4);

			/* エンディアン調整 */
			if (BitConverter.IsLittleEndian != pcap_little_endian_) {
				version_major = version_major.Reverse().ToArray();
				version_minor = version_minor.Reverse().ToArray();
				thiszone = thiszone.Reverse().ToArray();
				sigfigs = thiszone.Reverse().ToArray();
				snaplen = thiszone.Reverse().ToArray();
				linktype = thiszone.Reverse().ToArray();
			}

			pcap_snaplen_ = BitConverter.ToUInt32(snaplen, 0);

			recv_buffer_ = new byte[pcap_snaplen_];

			return (true);
		}

		public IEnumerable<USBPcapPacket> GetPackets()
		{
			var packets = new Queue<USBPcapPacket>();

			using (var stream = new MemoryStream())
			{
				process_.StandardOutput.BaseStream.CopyTo(stream);

				int read_data;

				while ((read_data = stream.ReadByte()) > 0) {
					var packet = RecvProcedure((byte)read_data);

					if (packet != null) {
						packets.Enqueue(packet);
					}
				}
			}

#if false
			if (process_stdout_.BaseStream.) {
				var recv_data = process_stdout_.ReadBytes((int)pcap_snaplen_);

				if (recv_data != null) {
					foreach (var data in recv_data) {
						var packet = RecvProcedure(data);

						if (packet != null) {
							packets.Enqueue(packet);
						}
					}
				}
			}
#endif

			return (packets);
		}

		private USBPcapPacket RecvProcedure(byte recv_data)
		{
			var packet = (USBPcapPacket)null;

            /*
            struct pcap_pkthdr{
                 struct timeval ts;   // time stamp
                 bpf_u_int32 caplen;  // length of portion present
                 bpf_u_int32 len;     // length this packet (off wire)
            };
            */
			if (packet_ts_sec_raw_len_ < packet_ts_sec_raw_.Length) {
				packet_ts_sec_raw_[packet_ts_sec_raw_len_++] = recv_data;

			} else if (packet_ts_usec_raw_len_ < packet_ts_usec_raw_.Length) {
				packet_ts_usec_raw_[packet_ts_usec_raw_len_++] = recv_data;

			} else if (packet_caplen_raw_len_ < packet_caplen_raw_.Length) {
				packet_caplen_raw_[packet_caplen_raw_len_++] = recv_data;

			} else if (packet_len_raw_len_ < packet_len_raw_.Length) {
				packet_len_raw_[packet_len_raw_len_++] = recv_data;

				if (packet_len_raw_len_ >= packet_len_raw_.Length) {
					/* ヘッダー情報を受信完了 */
					UpdateHeaderInfoFromRawData();

					if (packet_caplen_ > 0) {
						/* データ収集に移行 */
						packet_data_raw_ = new byte[packet_caplen_];
						packet_data_raw_len_ = 0;

					} else {
						/* カウンタリセット */
						packet_ts_sec_raw_len_ = 0;
						packet_ts_usec_raw_len_ = 0;
						packet_caplen_raw_len_ = 0;
						packet_len_raw_len_ = 0;
					}
				}
			} else if (packet_data_raw_len_ < packet_data_raw_.Length) {
				packet_data_raw_[packet_data_raw_len_++] = recv_data;

				if (packet_data_raw_len_ >= packet_data_raw_.Length) {
					packet = BuildPacket();

					/* カウンタリセット */
					packet_ts_sec_raw_len_ = 0;
					packet_ts_usec_raw_len_ = 0;
					packet_caplen_raw_len_ = 0;
					packet_len_raw_len_ = 0;
				}
			}

			return (packet);
		}

		private void UpdateHeaderInfoFromRawData()
		{
			if (pcap_little_endian_) {
				packet_ts_sec_ = ((UInt32)packet_ts_sec_raw_[0])
					   | ((UInt32)packet_ts_sec_raw_[1] << 8)
					   | ((UInt32)packet_ts_sec_raw_[2] << 16)
					   | ((UInt32)packet_ts_sec_raw_[3] << 24);

				packet_ts_usec_ = ((UInt32)packet_ts_usec_raw_[0])
					    | ((UInt32)packet_ts_usec_raw_[1] << 8)
					    | ((UInt32)packet_ts_usec_raw_[2] << 16)
					    | ((UInt32)packet_ts_usec_raw_[3] << 24);

				packet_caplen_ = ((UInt32)packet_caplen_raw_[0])
					   | ((UInt32)packet_caplen_raw_[1] << 8)
					   | ((UInt32)packet_caplen_raw_[2] << 16)
					   | ((UInt32)packet_caplen_raw_[3] << 24);

				packet_len_ = ((UInt32)packet_len_raw_[0])
					| ((UInt32)packet_len_raw_[1] << 8)
					| ((UInt32)packet_len_raw_[2] << 16)
					| ((UInt32)packet_len_raw_[3] << 24);

			} else {
				packet_ts_sec_ = ((UInt32)packet_ts_sec_raw_[3])
					   | ((UInt32)packet_ts_sec_raw_[2] << 8)
					   | ((UInt32)packet_ts_sec_raw_[1] << 16)
					   | ((UInt32)packet_ts_sec_raw_[0] << 24);

				packet_ts_usec_ = ((UInt32)packet_ts_usec_raw_[3])
					    | ((UInt32)packet_ts_usec_raw_[2] << 8)
					    | ((UInt32)packet_ts_usec_raw_[1] << 16)
					    | ((UInt32)packet_ts_usec_raw_[0] << 24);

				packet_caplen_ = ((UInt32)packet_caplen_raw_[3])
					   | ((UInt32)packet_caplen_raw_[2] << 8)
					   | ((UInt32)packet_caplen_raw_[1] << 16)
					   | ((UInt32)packet_caplen_raw_[0] << 24);

				packet_len_ = ((UInt32)packet_len_raw_[3])
					| ((UInt32)packet_len_raw_[2] << 8)
					| ((UInt32)packet_len_raw_[1] << 16)
					| ((UInt32)packet_len_raw_[0] << 24);
			}
		}

		private USBPcapPacket BuildPacket()
		{
            /* DateTime <- Unix Epoch */
            var dt = DateTimeOffset.FromUnixTimeSeconds(packet_ts_sec_).UtcDateTime.AddTicks(packet_ts_usec_ * 10);

			return (new USBPcapPacket(dt, packet_data_raw_));
		}


		public USBPcapPacket ReadPacket()
		{
			if (process_stdout_.PeekChar() < 0) {
				return (null);
			}

            /*
            struct pcap_pkthdr{
                 struct timeval ts;   // time stamp
                 bpf_u_int32 caplen;  // length of portion present
                 bpf_u_int32 len;     // length this packet (off wire)
            };
            */

            /* ts-sec (4 byte) */
			var ts_sec_bytes = process_stdout_.ReadBytes(4);

            /* ts-usec (4 byte) */
			var ts_usec_bytes = process_stdout_.ReadBytes(4);

            /* caplen (4 byte) */
			var caplen_bytes = process_stdout_.ReadBytes(4);

            /* len (4 byte) */
			var len_bytes = process_stdout_.ReadBytes(4);

			UInt32 ts_sec;
			UInt32 ts_usec;
			UInt32 caplen;
			UInt32 len;

			if (pcap_little_endian_) {
				ts_sec = ((UInt32)ts_sec_bytes[0])
					   | ((UInt32)ts_sec_bytes[1] << 8)
					   | ((UInt32)ts_sec_bytes[2] << 16)
					   | ((UInt32)ts_sec_bytes[3] << 24);

				ts_usec = ((UInt32)ts_usec_bytes[0])
					    | ((UInt32)ts_usec_bytes[1] << 8)
					    | ((UInt32)ts_usec_bytes[2] << 16)
					    | ((UInt32)ts_usec_bytes[3] << 24);

				caplen = ((UInt32)caplen_bytes[0])
					   | ((UInt32)caplen_bytes[1] << 8)
					   | ((UInt32)caplen_bytes[2] << 16)
					   | ((UInt32)caplen_bytes[3] << 24);

				len = ((UInt32)len_bytes[0])
					| ((UInt32)len_bytes[1] << 8)
					| ((UInt32)len_bytes[2] << 16)
					| ((UInt32)len_bytes[3] << 24);

			} else {
				ts_sec = ((UInt32)ts_sec_bytes[3])
					   | ((UInt32)ts_sec_bytes[2] << 8)
					   | ((UInt32)ts_sec_bytes[1] << 16)
					   | ((UInt32)ts_sec_bytes[0] << 24);

				ts_usec = ((UInt32)ts_usec_bytes[3])
					    | ((UInt32)ts_usec_bytes[2] << 8)
					    | ((UInt32)ts_usec_bytes[1] << 16)
					    | ((UInt32)ts_usec_bytes[0] << 24);

				caplen = ((UInt32)caplen_bytes[3])
					   | ((UInt32)caplen_bytes[2] << 8)
					   | ((UInt32)caplen_bytes[1] << 16)
					   | ((UInt32)caplen_bytes[0] << 24);

				len = ((UInt32)len_bytes[3])
					| ((UInt32)len_bytes[2] << 8)
					| ((UInt32)len_bytes[1] << 16)
					| ((UInt32)len_bytes[0] << 24);
			}

            /* DateTime <- Unix Epoch */
            var dt = DateTimeOffset.FromUnixTimeSeconds(ts_sec).UtcDateTime.AddTicks(ts_usec * 10);

            /* Data (x byte) */
			var data = process_stdout_.ReadBytes((int)caplen);

			return (new USBPcapPacket(dt, data));
		}
    }
}
