using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;

namespace Ratatoskr.Devices.UsbMonitor
{
    internal sealed class UsbPcapRecordParser
    {
        private const int ANALYZE_BUFFER_SIZE = 0x1FFFF;


        public enum UrbFunctionType
        {
            Isochronous,
            Interrupt,
            Control,
            Bulk,
        }

        public class PcapRecordHeader
        {
            public UInt32 ts_sec   = 0;
            public UInt32 ts_usec  = 0;
            public UInt32 incl_len = 0;
            public UInt32 orig_len = 0;

            public DateTime GetDateTime()
            {
                return (DateTimeOffset.FromUnixTimeSeconds(ts_sec).UtcDateTime.AddMilliseconds(ts_usec / 1000));
            }
        }

        public class UsbPcapPacketHeader
        {
            public UInt16  headerLen; /* This header length */
            public UInt64  irpId;     /* I/O Request packet ID */
            public Int32   status;    /* USB status code
                                    (on return from host controller) */
            public UInt16  function;  /* URB Function */
            public Byte    info;      /* I/O Request info */

            public UInt16  bus;       /* bus (RootHub) number */
            public UInt16  device;    /* device address */
            public Byte    endpoint;  /* endpoint number and transfer direction */
            public Byte    transfer;  /* transfer type */

            public UInt32  dataLength;/* Data length */
        }

        public class PacketInfo
        {
            public PcapRecordHeader    PcapHeader    { get; } = new PcapRecordHeader();
            public UsbPcapPacketHeader UsbPcapHeader { get; } = new UsbPcapPacketHeader();
            public byte[]              Data          { get; set; } = null;
        }


        private byte[] data_buffer_ = new byte[ANALYZE_BUFFER_SIZE];
        private int    data_size_ = 0;


        public UsbPcapRecordParser()
        {
            
        }

        public IEnumerable<PacketInfo> Parse(byte[] data, int offset, int size)
        {
            /* バッファの最後尾にデータを追加 */
            offset = Math.Min(offset, data.Length);
            size = Math.Min(size, data.Length - offset);

            Array.Copy(data, offset, data_buffer_, data_size_, size);
            data_size_ += size;

            /* 解析 */
            var infos = new Queue<PacketInfo>();
            var info = (PacketInfo)null;
            var read_size = (long)0;

            try {
                using (var stream = new MemoryStream(data_buffer_, 0, data_size_)) {
                    using (var reader = new BinaryReader(stream)) {
                        while (reader.BaseStream.Position < reader.BaseStream.Length) {
                            
                            ParsePcapRecord(reader, info = new PacketInfo());
                            infos.Enqueue(info);

                            /* 読込が成功した位置を記憶 */
                            read_size = reader.BaseStream.Position;
                        }
                    }
                }
            } catch {
            }

            if (read_size > 0) {
                /* 未解析データが存在するときはデータを詰める */
                if (read_size < data_size_) {
                    data_buffer_ = ClassUtil.ShiftCopy(data_buffer_, (int)read_size, (int)(data_size_ - read_size));
                }
                data_size_ -= (int)read_size;
            }

            return (infos);
        }

        private void ParsePcapRecord(BinaryReader reader, PacketInfo info)
        {
            var header = info.PcapHeader;

            /* ts_sec */
            header.ts_sec = (UInt32)(
                            (UInt32)((UInt32)reader.ReadByte() << 0)
                          | (UInt32)((UInt32)reader.ReadByte() << 8)
                          | (UInt32)((UInt32)reader.ReadByte() << 16)
                          | (UInt32)((UInt32)reader.ReadByte() << 24));

            /* ts_usec */
            header.ts_usec = (UInt32)(
                             ((UInt32)reader.ReadByte() << 0)
                           | ((UInt32)reader.ReadByte() << 8)
                           | ((UInt32)reader.ReadByte() << 16)
                           | ((UInt32)reader.ReadByte() << 24));

            /* incl_len */
            reader.ReadBytes(4);

            /* orig_len */
            reader.ReadBytes(4);

            ParsePcapRecord_UsbPcap(reader, info);
        }

        private static void ParsePcapRecord_UsbPcap(BinaryReader reader, PacketInfo info)
        {
            var header = info.UsbPcapHeader;
            var offset_top = reader.BaseStream.Position;

            /* headerLen */
            header.headerLen = (UInt16)(
                               (UInt16)((UInt16)reader.ReadByte() << 0)
                             | (UInt16)((UInt16)reader.ReadByte() << 8));

            /* irpId */
            reader.ReadBytes(8);

            /* status */
            reader.ReadBytes(4);

            /* function */
            header.function = (UInt16)(
                              (UInt16)((UInt16)reader.ReadByte() << 0)
                            | (UInt16)((UInt16)reader.ReadByte() << 8));

            /* info */
            header.info = reader.ReadByte();

            /* bus */
            reader.ReadBytes(2);

            /* device */
            header.device = (UInt16)(
                            (UInt16)((UInt16)reader.ReadByte() << 0)
                          | (UInt16)((UInt16)reader.ReadByte() << 8));

            /* endpoint */
            reader.ReadBytes(1);

            /* transfer */
            header.transfer = reader.ReadByte();

            /* dataLength */
            header.dataLength = (UInt32)(
                                ((UInt32)reader.ReadByte() << 0)
                              | ((UInt32)reader.ReadByte() << 8)
                              | ((UInt32)reader.ReadByte() << 16)
                              | ((UInt32)reader.ReadByte() << 24));

            /* 転送種別毎に処理 */
            switch ((UrbFunctionType)info.UsbPcapHeader.transfer) {
                case UrbFunctionType.Isochronous:   ParsePcapPacket_UsbPcap_Isochronous(reader, info);  break;
                case UrbFunctionType.Interrupt:     ParsePcapPacket_UsbPcap_Interrupt(reader, info);    break;
                case UrbFunctionType.Control:       ParsePcapPacket_UsbPcap_Control(reader, info);      break;
                case UrbFunctionType.Bulk:          ParsePcapPacket_UsbPcap_Bulk(reader, info);         break;
            }

            /* Data */
            reader.BaseStream.Position = offset_top + header.headerLen;
            info.Data = reader.ReadBytes((int)header.dataLength);
        }

        private static void ParsePcapPacket_UsbPcap_Isochronous(BinaryReader reader, PacketInfo info)
        {
        }

        private static void ParsePcapPacket_UsbPcap_Interrupt(BinaryReader reader, PacketInfo info)
        {
        }

        private static void ParsePcapPacket_UsbPcap_Control(BinaryReader reader, PacketInfo info)
        {
        }

        private static void ParsePcapPacket_UsbPcap_Bulk(BinaryReader reader, PacketInfo info)
        {
        }
    }
}
