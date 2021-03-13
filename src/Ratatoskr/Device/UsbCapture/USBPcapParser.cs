using Ratatoskr.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Device.UsbCapture
{
	internal class USBPcapParser
	{
		public enum ParseStatus
		{
			Busy,
			Success,
			Error,
		}

		public enum USBPCAP_TRANSFER : Byte
		{
			USBPCAP_TRANSFER_ISOCHRONOUS = 0,
			USBPCAP_TRANSFER_INTERRUPT   = 1,
			USBPCAP_TRANSFER_CONTROL     = 2,
			USBPCAP_TRANSFER_BULK        = 3,
		}

		public enum USBPCAP_CONTROL_STAGE : Byte
		{
			USBPCAP_CONTROL_STAGE_SETUP    = 0,
			USBPCAP_CONTROL_STAGE_DATA     = 1,
			USBPCAP_CONTROL_STAGE_STATUS   = 2,
			USBPCAP_CONTROL_STAGE_COMPLETE = 3
		}

		private class PCAP_FILE_HEADER_Parser
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

			private byte[]		magic_raw_ = new byte[4];
			private int			magic_raw_len_ = 0;
			private UInt32		magic_;

			private byte[]		version_major_raw_ = new byte[2];
			private int			version_major_raw_len_ = 0;
			private UInt16		version_major_;

			private byte[]		version_minor_raw_ = new byte[2];
			private int			version_minor_raw_len_ = 0;
			private UInt16		version_minor_;

			private byte[]		thiszone_raw_ = new byte[4];
			private int			thiszone_raw_len_ = 0;
			private UInt32		thiszone_;

			private byte[]		sigfigs_raw_ = new byte[4];
			private int			sigfigs_raw_len_ = 0;
			private UInt32		sigfigs_;

			private byte[]		snaplen_raw_ = new byte[4];
			private int			snaplen_raw_len_ = 0;
			private UInt32		snaplen_;

			private byte[]		linktype_raw_ = new byte[4];
			private int			linktype_raw_len_ = 0;
			private UInt32		linktype_;


			public ParseStatus	Status			{ get; private set; } = ParseStatus.Busy;
			public bool			LittleEndian	{ get; private set; } = false;


			public void Reset()
			{
				magic_raw_len_ = 0;
				version_major_raw_len_ = 0;
				version_minor_raw_len_ = 0;
				thiszone_raw_len_ = 0;
				sigfigs_raw_len_ = 0;
				snaplen_raw_len_ = 0;
				linktype_raw_len_ = 0;
			}

			public bool Parse(byte data)
			{
				/* 解析済みのときは何もしない */
				if (linktype_raw_len_ >= linktype_raw_.Length) {
					return (false);
				}

				if (magic_raw_len_ < magic_raw_.Length) {
					magic_raw_[magic_raw_len_++] = data;

					if (magic_raw_len_ >= magic_raw_.Length) {
						if (magic_raw_.SequenceEqual(new byte[] { 0xA1, 0xB2, 0xC3, 0xD4 })) {
							LittleEndian = false;
						} else if (magic_raw_.SequenceEqual(new byte[] { 0xA1, 0xB2, 0xC3, 0xD4 })) {
							LittleEndian = true;
						} else {
							Reset();
						}
					}
				} else if (version_major_raw_len_ < version_major_raw_.Length) {
					version_major_raw_[version_major_raw_len_++] = data;

				} else if (version_minor_raw_len_ < version_minor_raw_.Length) {
					version_minor_raw_[version_minor_raw_len_++] = data;

				} else if (thiszone_raw_len_ < thiszone_raw_.Length) {
					thiszone_raw_[thiszone_raw_len_++] = data;

				} else if (sigfigs_raw_len_ < sigfigs_raw_.Length) {
					sigfigs_raw_[sigfigs_raw_len_++] = data;

				} else if (snaplen_raw_len_ < snaplen_raw_.Length) {
					snaplen_raw_[snaplen_raw_len_++] = data;

				} else if (linktype_raw_len_ < linktype_raw_.Length) {
					linktype_raw_[linktype_raw_len_++] = data;
				}

				return (true);
			}
		}

		private class USBPCAP_BUFFER_PACKET_HEADER_Parser
		{
			/*
			typedef struct
			{
				USHORT       headerLen; // This header length
				UINT64       irpId;     // I/O Request packet ID
				USBD_STATUS  status;    // USB status code
											(on return from host controller)
				USHORT       function;  // URB Function
				UCHAR        info;      // I/O Request info

				USHORT       bus;       // bus (RootHub) number
				USHORT       device;    // device address
				UCHAR        endpoint;  // endpoint number and transfer direction
				UCHAR        transfer;  // transfer type

				UINT32       dataLength;/* Data length
			} USBPCAP_BUFFER_PACKET_HEADER, *PUSBPCAP_BUFFER_PACKET_HEADER;
			*/

			private UInt16			 headerLen; /* This header length */

			private UInt64			 irpId;     /* I/O Request packet ID */

			private UInt32			 status;    /* USB status code */

			private UInt16			 function;  /* URB Function */

			private Byte			 info;      /* I/O Request info */

			private UInt16			 bus;       /* bus (RootHub) number */

			private UInt16			 device;    /* device address */

			private Byte			 endpoint;  /* endpoint number and transfer direction */

			private USBPCAP_TRANSFER transfer;  /* transfer type */

			private UInt32			 dataLength;/* Data length */


			private byte[]		magic_raw_ = new byte[4];
			private int			magic_raw_len_ = 0;
			private UInt32		magic_;

		}


		private bool		little_endian_ = false;

		private DateTime	packet_datetime_;

		private byte[]		pcap_file_header_ = new byte[24];
		private int			pcap_file_header_len_;

		private byte[]		packet_header_ = new byte[16];
		private int			packet_header_len_;

		private byte[]		packet_data_;
		private int			packet_data_len_;


		public USBPcapParser()
		{
		}

		public IEnumerable<USBPcapPacket> InputData(byte[] input_data, int input_data_size)
		{
			var packets = new Queue<USBPcapPacket>();

			for (var index = 0; index < input_data_size; index++) {
				InputData(ref packets, input_data[index]);
			}

			return (packets);
		}

		private void InputData(ref Queue<USBPcapPacket> packets, byte input_data)
		{
			if (pcap_file_header_len_ < pcap_file_header_.Length) {
				pcap_file_header_[pcap_file_header_len_++] = input_data;

				if (pcap_file_header_len_ >= pcap_file_header_.Length) {
					UpdateFromPcapFileHeader(pcap_file_header_);
				}

			} else if (packet_header_len_ < packet_header_.Length) {
				packet_header_[packet_header_len_++] = input_data;

				if (packet_header_len_ >= packet_header_.Length) {
					UpdateFromPacketHeader(packet_header_);
				}

			} else if (packet_data_len_ < packet_data_.Length) {
				packet_data_[packet_data_len_++] = input_data;

				if (packet_data_len_ >= packet_data_.Length) {
					/* === パケット受信完了 === */
					UpdateFromPacketData(packet_data_);

					packets.Enqueue(BuildPacket());

					/* パケットヘッダーの受信から再開 */
					packet_header_len_ = 0;
				}
			}
		}

		private void UpdateFromPcapFileHeader(byte[] data)
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

			using (var reader = new BinaryReader(new MemoryStream(data))) {

				/* magic (4 byte) */
				var magic = reader.ReadBytes(4);

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

				/* エンディアン判定 */
				little_endian_ = magic.SequenceEqual(new byte[] { 0xD4, 0xC3, 0xB2, 0xA1 });
			}
		}

		private void UpdateFromPacketHeader(byte[] data)
		{
            /*
            struct pcap_pkthdr{
                 struct timeval ts;   // time stamp
                 bpf_u_int32 caplen;  // length of portion present
                 bpf_u_int32 len;     // length this packet (off wire)
            };
            */

			using (var reader = new BinaryReader(new MemoryStream(data))) {
				/* ts-sec (4 byte) */
				var ts_sec_raw = reader.ReadBytes(4);

				/* ts-usec (4 byte) */
				var ts_usec_raw = reader.ReadBytes(4);

				/* caplen (4 byte) */
				var caplen_raw = reader.ReadBytes(4);

				/* len (4 byte) */
				var len_raw = reader.ReadBytes(4);

				/* エンディアン補正 */
				if (little_endian_ != BitConverter.IsLittleEndian) {
					ts_sec_raw  = ts_sec_raw.Reverse().ToArray();
					ts_usec_raw = ts_usec_raw.Reverse().ToArray();
					caplen_raw  = caplen_raw.Reverse().ToArray();
				}

				var ts_sec  = BitConverter.ToUInt32(ts_sec_raw, 0);
				var ts_usec = BitConverter.ToUInt32(ts_usec_raw, 0);
				var caplen  = BitConverter.ToUInt32(caplen_raw, 0);

				/* パケット時刻 */
				/* DateTime <- Unix Epoch */
				packet_datetime_ = DateTimeOffset.FromUnixTimeSeconds(ts_sec).UtcDateTime.AddTicks(ts_usec * 10);

				/* ペイロードバッファ */
				packet_data_ = new byte[caplen];
				packet_data_len_ = 0;
			}
		}

		private void UpdateFromPacketData(byte[] data)
		{

		}

		private USBPcapPacket BuildPacket()
		{
			return (new USBPcapPacket(packet_datetime_, packet_data_, little_endian_));
		}
	}
}
