using Ratatoskr.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Device.UsbCapture
{
	internal class USBPcapPacket
	{
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

		public class USBPCAP_BUFFER_PACKET_HEADER
		{
			public UInt16           headerLen; /* This header length */
			public UInt64           irpId;     /* I/O Request packet ID */
			public UInt32           status;    /* USB status code
										          (on return from host controller) */
			public UInt16           function;  /* URB Function */
			public Byte             info;      /* I/O Request info */

			public UInt16           bus;       /* bus (RootHub) number */
			public UInt16           device;    /* device address */
			public Byte             endpoint;  /* endpoint number and transfer direction */
			public USBPCAP_TRANSFER transfer;  /* transfer type */

			public UInt32           dataLength;/* Data length */
		}

		public struct USBPCAP_BUFFER_ISO_PACKET
		{
			public UInt32        offset;
			public UInt32        length;
			public UInt32        status;
		};

		public class USBPCAP_BUFFER_ISOCH_HEADER
		{
			public UInt32                        startFrame;
			public UInt32                        numberOfPackets;
			public UInt32                        errorCount;
			public USBPCAP_BUFFER_ISO_PACKET     packet = new USBPCAP_BUFFER_ISO_PACKET();
		};

		public class USBPCAP_BUFFER_CONTROL_HEADER
		{
			public USBPCAP_CONTROL_STAGE         stage;
		};


		public USBPcapPacket(DateTime dt, byte[] raw_data, bool little_endian)
		{
			MakeTime = dt;
			RawData = raw_data;

			Parse(raw_data, little_endian);
		}

		public DateTime MakeTime { get; }
		public byte[]   RawData  { get; }

		public USBPCAP_BUFFER_PACKET_HEADER  PacketHeader      { get; private set; }
		public USBPCAP_BUFFER_ISOCH_HEADER   IsochronousHeader { get; private set; }
		public USBPCAP_BUFFER_CONTROL_HEADER ControlHeader     { get; private set; }
		public byte[]                        Payload           { get; private set; }


		private void Parse(byte[] data, bool little_endian)
		{
			using (var reader = new BinaryReader(new MemoryStream(data))) {
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

				var headerLen_raw   = reader.ReadBytes(2);
				var irpId_raw       = reader.ReadBytes(8);
				var status_raw      = reader.ReadBytes(4);
				var function_raw    = reader.ReadBytes(2);
				var info_raw        = reader.ReadBytes(1);
				var bus_raw         = reader.ReadBytes(2);
				var device_raw      = reader.ReadBytes(2);
				var endpoint_raw    = reader.ReadBytes(1);
				var transfer_raw    = reader.ReadBytes(1);
				var dataLength_raw  = reader.ReadBytes(4);

				if (little_endian != BitConverter.IsLittleEndian) {
					headerLen_raw   = headerLen_raw.Reverse().ToArray();
					irpId_raw       = irpId_raw.Reverse().ToArray();
					status_raw      = status_raw.Reverse().ToArray();
					function_raw    = function_raw.Reverse().ToArray();
					info_raw        = info_raw.Reverse().ToArray();
					bus_raw         = bus_raw.Reverse().ToArray();
					device_raw      = device_raw.Reverse().ToArray();
					endpoint_raw    = endpoint_raw.Reverse().ToArray();
					transfer_raw    = transfer_raw.Reverse().ToArray();
					dataLength_raw  = dataLength_raw.Reverse().ToArray();
				}

				PacketHeader = new USBPCAP_BUFFER_PACKET_HEADER();
				PacketHeader.headerLen   = BitConverter.ToUInt16(headerLen_raw, 0);
				PacketHeader.irpId       = BitConverter.ToUInt64(irpId_raw, 0);
				PacketHeader.status      = BitConverter.ToUInt32(status_raw, 0);
				PacketHeader.function    = BitConverter.ToUInt16(function_raw, 0);
				PacketHeader.info        = info_raw[0];
				PacketHeader.bus         = BitConverter.ToUInt16(bus_raw, 0);
				PacketHeader.device      = BitConverter.ToUInt16(device_raw, 0);
				PacketHeader.endpoint    = endpoint_raw[0];
				PacketHeader.transfer    = (USBPCAP_TRANSFER)transfer_raw[0];
				PacketHeader.dataLength  = BitConverter.ToUInt32(dataLength_raw, 0);

				Payload = ClassUtil.CloneCopy(data, PacketHeader.headerLen, data.Length - PacketHeader.headerLen);

				switch (PacketHeader.transfer) {
					case USBPCAP_TRANSFER.USBPCAP_TRANSFER_ISOCHRONOUS:
						ParseIsochronousHeader(reader, little_endian);
						break;
					case USBPCAP_TRANSFER.USBPCAP_TRANSFER_CONTROL:
						ParseControlHeader(reader, little_endian);
						break;
				}
			}
		}

		private void ParseIsochronousHeader(BinaryReader reader, bool little_endian)
		{
			/*
			public struct USBPCAP_BUFFER_ISO_PACKET
			{
				public UInt32        offset;
				public UInt32        length;
				public UInt32        status;
			};

			public class USBPCAP_BUFFER_ISOCH_HEADER
			{
				public UInt32                        startFrame;
				public UInt32                        numberOfPackets;
				public UInt32                        errorCount;
				public USBPCAP_BUFFER_ISO_PACKET     packet;
			};
			*/

			var startFrame_raw      = reader.ReadBytes(4);
			var numberOfPackets_raw = reader.ReadBytes(4);
			var errorCount_raw      = reader.ReadBytes(4);
			var offset_raw          = reader.ReadBytes(4);
			var length_raw          = reader.ReadBytes(4);
			var status_raw          = reader.ReadBytes(4);

			if (little_endian != BitConverter.IsLittleEndian) {
				startFrame_raw      = startFrame_raw.Reverse().ToArray();
				numberOfPackets_raw = numberOfPackets_raw.Reverse().ToArray();
				errorCount_raw      = errorCount_raw.Reverse().ToArray();
				offset_raw          = offset_raw.Reverse().ToArray();
				length_raw          = length_raw.Reverse().ToArray();
				status_raw          = status_raw.Reverse().ToArray();
			}

			IsochronousHeader = new USBPCAP_BUFFER_ISOCH_HEADER();
			IsochronousHeader.startFrame      = BitConverter.ToUInt32(startFrame_raw, 0);
			IsochronousHeader.numberOfPackets = BitConverter.ToUInt32(numberOfPackets_raw, 0);
			IsochronousHeader.errorCount      = BitConverter.ToUInt32(errorCount_raw, 0);
			IsochronousHeader.packet.offset   = BitConverter.ToUInt32(offset_raw, 0);
			IsochronousHeader.packet.length   = BitConverter.ToUInt32(length_raw, 0);
			IsochronousHeader.packet.status   = BitConverter.ToUInt32(status_raw, 0);
		}

		private void ParseControlHeader(BinaryReader reader, bool little_endian)
		{
			/*
			typedef struct
			{
				USBPCAP_BUFFER_PACKET_HEADER  header;
				UCHAR                         stage;
			} USBPCAP_BUFFER_CONTROL_HEADER, *PUSBPCAP_BUFFER_CONTROL_HEADER;
			*/

			var stage_raw = reader.ReadBytes(1);

			ControlHeader.stage = (USBPCAP_CONTROL_STAGE)stage_raw[0];
		}
	}
}
