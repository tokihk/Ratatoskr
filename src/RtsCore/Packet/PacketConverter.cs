using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Packet
{
    public static class PacketConverter
    {
        private static class Binary
        {
            public enum SerializeFormat
            {
                V000,
                V001
            }

            private const SerializeFormat FORMAT_VERSION = SerializeFormat.V001;


            public static byte[] Serialize(PacketObject packet)
            {
                using (var stream = new MemoryStream()) {
                    using (var writer = new BinaryWriter(stream)) {
                        /* フォーマットバージョン */
                        writer.Write((byte)FORMAT_VERSION);

                        /* フォーマット別処理 */
                        Serialize_V001(writer, packet);
                    }

                    return (stream.ToArray());
                }
            }

            private static void Serialize_V000(BinaryWriter writer, PacketObject packet)
            {
                /* Facility (1 Byte) */
                writer.Write((byte)packet.Facility);

                /* Alias (1 + xx Byte) */
                var alias_data = Encoding.UTF8.GetBytes(packet.Alias);
                var alias_data_len = Math.Min(alias_data.Length, 255);

                writer.Write((byte)alias_data_len);
                writer.Write(alias_data);

                /* Priority (1 Byte) */
                writer.Write((byte)packet.Priority);

                /* Attribute (1 Byte) */
                writer.Write((byte)packet.Attribute);

                /* Make Time (2 + 1 + 1 + 1 + 1 + 1 + 2 Byte) */
                var dt_utc = packet.MakeTime.ToUniversalTime();

                writer.Write((byte)((dt_utc.Year >> 8) & 0xFF));
                writer.Write((byte)((dt_utc.Year >> 0) & 0xFF));
                writer.Write((byte)dt_utc.Month);
                writer.Write((byte)dt_utc.Day);
                writer.Write((byte)dt_utc.Hour);
                writer.Write((byte)dt_utc.Minute);
                writer.Write((byte)dt_utc.Second);
                writer.Write((byte)((dt_utc.Millisecond >> 8) & 0xFF));
                writer.Write((byte)((dt_utc.Millisecond >> 0) & 0xFF));

                /* Information (2 + xx Byte) */
                var info_data = Encoding.UTF8.GetBytes(packet.Information);
                var info_data_len = Math.Min(info_data.Length, 65535);

                writer.Write((byte)((info_data_len >> 8) & 0xFF));
                writer.Write((byte)((info_data_len >> 0) & 0xFF));
                writer.Write(info_data);

                /* Direction (1 Byte) */
                writer.Write((byte)packet.Direction);

                /* Source (1 + xx Byte) */
                var src_data = Encoding.UTF8.GetBytes(packet.Source);
                var src_data_len = (byte)Math.Min(src_data.Length, 255);

                writer.Write((byte)src_data_len);
                writer.Write(src_data);

                /* Destination (1 + xx Byte) */
                var dst_data = Encoding.UTF8.GetBytes(packet.Destination);
                var dst_data_len = (byte)Math.Min(dst_data.Length, 255);

                writer.Write((byte)dst_data_len);
                writer.Write(dst_data);

                /* User Mark (1 Byte) */
                writer.Write(packet.UserMark);

                /* パケット別 */
                switch (packet.Attribute) {
                    case PacketAttribute.Control:
                    {
                        /* Code (4 Byte) */
                        writer.Write((byte)(0));
                        writer.Write((byte)(0));
                        writer.Write((byte)(0));
                        writer.Write((byte)(0));

                        /* Data (4 + xx Byte) */
                        var data = packet.Data;
                        var data_len = (uint)data.Length;

                        writer.Write((byte)((data_len >> 24) & 0xFF));
                        writer.Write((byte)((data_len >> 16) & 0xFF));
                        writer.Write((byte)((data_len >>  8) & 0xFF));
                        writer.Write((byte)((data_len >>  0) & 0xFF));
                        writer.Write(data);
                        
                    }
                        break;

                    case PacketAttribute.Message:
                    {
                        /* Data (4 + xx Byte) */
                        var data = Encoding.UTF8.GetBytes(packet.Message);
                        var data_len = (uint)data.Length;

                        writer.Write((byte)((data_len >> 24) & 0xFF));
                        writer.Write((byte)((data_len >> 16) & 0xFF));
                        writer.Write((byte)((data_len >>  8) & 0xFF));
                        writer.Write((byte)((data_len >>  0) & 0xFF));
                        writer.Write(data);
                    }
                        break;

                    case PacketAttribute.Data:
                    {
                        /* Data (4 + xx Byte)*/
                        var data = packet.Data;
                        var data_len = (uint)data.Length;

                        writer.Write((byte)((data_len >> 24) & 0xFF));
                        writer.Write((byte)((data_len >> 16) & 0xFF));
                        writer.Write((byte)((data_len >>  8) & 0xFF));
                        writer.Write((byte)((data_len >>  0) & 0xFF));
                        writer.Write(data);
                    }
                        break;
                }
            }

            private static void Serialize_V001(BinaryWriter writer, PacketObject packet)
            {
                /* Facility (1 Byte) */
                writer.Write((byte)packet.Facility);

                /* Alias (1 + xx Byte) */
                var alias_data = Encoding.UTF8.GetBytes(packet.Alias);
                var alias_data_len = Math.Min(alias_data.Length, 255);

                writer.Write((byte)alias_data_len);
                writer.Write(alias_data);

                /* Priority (1 Byte) */
                writer.Write((byte)packet.Priority);

                /* Attribute (1 Byte) */
                writer.Write((byte)packet.Attribute);

                /* Make Time (2 + 1 + 1 + 1 + 1 + 1 + 2 Byte) */
                var dt_utc = packet.MakeTime.ToUniversalTime();

                writer.Write((byte)((dt_utc.Year >> 8) & 0xFF));
                writer.Write((byte)((dt_utc.Year >> 0) & 0xFF));
                writer.Write((byte)dt_utc.Month);
                writer.Write((byte)dt_utc.Day);
                writer.Write((byte)dt_utc.Hour);
                writer.Write((byte)dt_utc.Minute);
                writer.Write((byte)dt_utc.Second);
                writer.Write((byte)((dt_utc.Millisecond >> 8) & 0xFF));
                writer.Write((byte)((dt_utc.Millisecond >> 0) & 0xFF));

                /* Information (2 + xx Byte) */
                var info_data = Encoding.UTF8.GetBytes(packet.Information);
                var info_data_len = Math.Min(info_data.Length, 65535);

                writer.Write((byte)((info_data_len >> 8) & 0xFF));
                writer.Write((byte)((info_data_len >> 0) & 0xFF));
                writer.Write(info_data);

                /* Direction (1 Byte) */
                writer.Write((byte)packet.Direction);

                /* Source (1 + xx Byte) */
                var src_data = Encoding.UTF8.GetBytes(packet.Source);
                var src_data_len = (byte)Math.Min(src_data.Length, 255);

                writer.Write((byte)src_data_len);
                writer.Write(src_data);

                /* Destination (1 + xx Byte) */
                var dst_data = Encoding.UTF8.GetBytes(packet.Destination);
                var dst_data_len = (byte)Math.Min(dst_data.Length, 255);

                writer.Write((byte)dst_data_len);
                writer.Write(dst_data);

                /* User Mark (1 Byte) */
                writer.Write(packet.UserMark);

                /* Message (2 + xx Byte) */
                var msg_data = Encoding.UTF8.GetBytes(packet.Message);
                var msg_data_len = (byte)Math.Min(dst_data.Length, 0xFFFF);
                
                writer.Write((ushort)msg_data_len);
                writer.Write(msg_data);

                /* Data (4 + xx Byte) */
                var data = packet.Data;
                var data_len = (uint)data.Length;

                writer.Write((byte)((data_len >> 24) & 0xFF));
                writer.Write((byte)((data_len >> 16) & 0xFF));
                writer.Write((byte)((data_len >>  8) & 0xFF));
                writer.Write((byte)((data_len >>  0) & 0xFF));
                writer.Write(data);
            }

            public static PacketObject Deserialize(byte[] data)
            {
                using (var stream = new MemoryStream(data)) {
                    using (var reader = new BinaryReader(stream)) {
                        /* フォーマットバージョン */
                        var format = (SerializeFormat)reader.ReadByte();

                        /* フォーマット別処理 */
                        switch (format) {
                            case SerializeFormat.V000:  return (Deserialize_V000(reader));
                            case SerializeFormat.V001:  return (Deserialize_V001(reader));
                            default:                    return (null);
                        }
                    }
                }
            }

            private static PacketObject Deserialize_V000(BinaryReader reader)
            {
                /* Facility (1 Byte) */
                var facility = (Packet.PacketFacility)reader.ReadByte();

                /* Alias (1 + xx Byte) */
                var alias_len = (int)reader.ReadByte();
                var alias = Encoding.UTF8.GetString(reader.ReadBytes(alias_len));

                /* Priority (1 Byte) */
                var prio = (Packet.PacketPriority)reader.ReadByte();

                /* Attribute (1 Byte) */
                var attr = (Packet.PacketAttribute)reader.ReadByte();

                /* Make Time (2 + 1 + 1 + 1 + 1 + 1 + 2 Byte) */
                var dt_year = (int)0;
                var dt_month = (int)0;
                var dt_day = (int)0;
                var dt_hour = (int)0;
                var dt_min = (int)0;
                var dt_sec = (int)0;
                var dt_msec = (int)0;

                dt_year  |= (int)reader.ReadByte() << 8;
                dt_year  |= (int)reader.ReadByte() << 0;
                dt_month  = (int)reader.ReadByte();
                dt_day    = (int)reader.ReadByte();
                dt_hour   = (int)reader.ReadByte();
                dt_min    = (int)reader.ReadByte();
                dt_sec    = (int)reader.ReadByte();
                dt_msec  |= (int)reader.ReadByte() << 8;
                dt_msec  |= (int)reader.ReadByte() << 0;

                var dt = new DateTime(dt_year, dt_month, dt_day, dt_hour, dt_min, dt_sec, dt_msec, DateTimeKind.Utc);

                /* Information (2 + xx Byte) */
                var info_len = (ushort)0;
                var info = "";
                
                info_len |= (ushort)((ushort)reader.ReadByte() << 8);
                info_len |= (ushort)((ushort)reader.ReadByte() << 0);
                info = Encoding.UTF8.GetString(reader.ReadBytes(info_len));

                /* Direction (1 Byte) */
                var dir = (PacketDirection)reader.ReadByte();

                /* Source (1 + xx Byte) */
                var src_data_len = (int)reader.ReadByte();
                var src_data = Encoding.UTF8.GetString(reader.ReadBytes(src_data_len));

                /* Destination (1 + xx Byte) */
                var dst_data_len = (int)reader.ReadByte();
                var dst_data = Encoding.UTF8.GetString(reader.ReadBytes(dst_data_len));

                /* User Mark (1 Byte) */
                var mark = reader.ReadByte();

                /* === パケット生成 === */
                switch (attr) {
                    case PacketAttribute.Control:
                    {
                        /* Code (4 Byte) */
                        var code = (uint)0;

                        code |= (uint)((uint)reader.ReadByte() << 24);
                        code |= (uint)((uint)reader.ReadByte() << 16);
                        code |= (uint)((uint)reader.ReadByte() <<  8);
                        code |= (uint)((uint)reader.ReadByte() <<  0);

                        /* Data (4 + xx Byte) */
                        var data_len = (uint)0;

                        data_len |= (uint)((uint)reader.ReadByte() << 24);
                        data_len |= (uint)((uint)reader.ReadByte() << 16);
                        data_len |= (uint)((uint)reader.ReadByte() <<  8);
                        data_len |= (uint)((uint)reader.ReadByte() <<  0);

                        var data = reader.ReadBytes((int)data_len);

                        return (new PacketObject(
                                            "",
                                            facility,
                                            alias,
                                            prio,
                                            attr,
                                            dt,
                                            info,
                                            dir,
                                            src_data,
                                            dst_data,
                                            mark,
                                            "",
                                            data));
                    }

                    case PacketAttribute.Message:
                    {
                        /* Data (4 + xx Byte) */
                        var data_len = (uint)0;

                        data_len |= (uint)((uint)reader.ReadByte() << 24);
                        data_len |= (uint)((uint)reader.ReadByte() << 16);
                        data_len |= (uint)((uint)reader.ReadByte() <<  8);
                        data_len |= (uint)((uint)reader.ReadByte() <<  0);

                        var data = Encoding.UTF8.GetString(reader.ReadBytes((int)data_len));

                        return (new PacketObject(
                                            "",
                                            facility,
                                            alias,
                                            prio,
                                            attr,
                                            dt,
                                            info,
                                            dir,
                                            src_data,
                                            dst_data,
                                            mark,
                                            data,
                                            null));
                    }

                    case PacketAttribute.Data:
                    {
                        /* Data (4 + xx Byte) */
                        var data_len = (uint)0;

                        data_len |= (uint)((uint)reader.ReadByte() << 24);
                        data_len |= (uint)((uint)reader.ReadByte() << 16);
                        data_len |= (uint)((uint)reader.ReadByte() <<  8);
                        data_len |= (uint)((uint)reader.ReadByte() <<  0);

                        var data = reader.ReadBytes((int)data_len);

                        return (new PacketObject(
                                            "",
                                            facility,
                                            alias,
                                            prio,
                                            attr,
                                            dt,
                                            info,
                                            dir,
                                            src_data,
                                            dst_data,
                                            mark,
                                            null,
                                            data));
                    }

                    default:
                        return (null);
                }
            }

            private static PacketObject Deserialize_V001(BinaryReader reader)
            {
                /* Facility (1 Byte) */
                var facility = (Packet.PacketFacility)reader.ReadByte();

                /* Alias (1 + xx Byte) */
                var alias_len = (int)reader.ReadByte();
                var alias = Encoding.UTF8.GetString(reader.ReadBytes(alias_len));

                /* Priority (1 Byte) */
                var prio = (Packet.PacketPriority)reader.ReadByte();

                /* Attribute (1 Byte) */
                var attr = (Packet.PacketAttribute)reader.ReadByte();

                /* Make Time (2 + 1 + 1 + 1 + 1 + 1 + 2 Byte) */
                var dt_year = (int)0;
                var dt_month = (int)0;
                var dt_day = (int)0;
                var dt_hour = (int)0;
                var dt_min = (int)0;
                var dt_sec = (int)0;
                var dt_msec = (int)0;

                dt_year  |= (int)reader.ReadByte() << 8;
                dt_year  |= (int)reader.ReadByte() << 0;
                dt_month  = (int)reader.ReadByte();
                dt_day    = (int)reader.ReadByte();
                dt_hour   = (int)reader.ReadByte();
                dt_min    = (int)reader.ReadByte();
                dt_sec    = (int)reader.ReadByte();
                dt_msec  |= (int)reader.ReadByte() << 8;
                dt_msec  |= (int)reader.ReadByte() << 0;

                var dt = new DateTime(dt_year, dt_month, dt_day, dt_hour, dt_min, dt_sec, dt_msec, DateTimeKind.Utc);

                /* Information (2 + xx Byte) */
                var info_len = (ushort)0;
                var info = "";
                
                info_len |= (ushort)((ushort)reader.ReadByte() << 8);
                info_len |= (ushort)((ushort)reader.ReadByte() << 0);
                info = Encoding.UTF8.GetString(reader.ReadBytes(info_len));

                /* Direction (1 Byte) */
                var dir = (PacketDirection)reader.ReadByte();

                /* Source (1 + xx Byte) */
                var src_data_len = (int)reader.ReadByte();
                var src_data = Encoding.UTF8.GetString(reader.ReadBytes(src_data_len));

                /* Destination (1 + xx Byte) */
                var dst_data_len = (int)reader.ReadByte();
                var dst_data = Encoding.UTF8.GetString(reader.ReadBytes(dst_data_len));

                /* User Mark (1 Byte) */
                var mark = reader.ReadByte();

                /* Message (2 + xx Byte) */
                var msg_data_len = (ushort)0;

                msg_data_len |= (ushort)((ushort)reader.ReadByte() <<  8);
                msg_data_len |= (ushort)((ushort)reader.ReadByte() <<  0);

                var msg_data = Encoding.UTF8.GetString(reader.ReadBytes(msg_data_len));

                /* Data (4 + xx Byte) */
                var data_len = (uint)0;

                data_len |= (uint)((uint)reader.ReadByte() << 24);
                data_len |= (uint)((uint)reader.ReadByte() << 16);
                data_len |= (uint)((uint)reader.ReadByte() <<  8);
                data_len |= (uint)((uint)reader.ReadByte() <<  0);

                var data = reader.ReadBytes((int)data_len);

                /* Class (1 + xx Byte) [オプション情報なので有無判定が必要] */
                var class_n_len = (byte)0;
                var class_n = "";

                if (reader.PeekChar() >= 0) {
                    class_n_len = reader.ReadByte();
                    class_n = Encoding.UTF8.GetString(reader.ReadBytes(class_n_len));
                }

                /* === パケット生成 === */
                return (new PacketObject(
                                    class_n,
                                    facility,
                                    alias,
                                    prio,
                                    attr,
                                    dt,
                                    info,
                                    dir,
                                    src_data,
                                    dst_data,
                                    mark,
                                    null,
                                    data));
            }
        }

        public static byte[] Serialize(PacketObject packet, bool compless = true)
        {
            if (packet == null)return (null);

            /* 圧縮前データを作成 */
            var data = Binary.Serialize(packet);

            if (data == null)return (null);

            var data_type = false;

            /* 圧縮後データを作成 */
            if (compless) {
                var data_comp = (byte[])null;

                using (var mstream = new MemoryStream()) {
                    using (var cstream = new GZipStream(mstream, CompressionMode.Compress)) {
                        cstream.Write(data, 0, data.Length);
                        cstream.Close();

                        data_comp = mstream.ToArray();
                    }
                }

                /* 圧縮前と圧縮後のデータサイズを比較して書き込むデータを選定 */
                if (data_comp.Length < data.Length) {
                    data = data_comp;
                    data_type = true;
                }
            }

            /* 書込み */
            using (var stream = new MemoryStream()) {
                using (var writer = new BinaryWriter(stream)) {
                    /* データタイプ (1 Byte) */
                    writer.Write(data_type);

                    /* データ (xx Byte) */
                    writer.Write(data);

                    return (stream.ToArray());
                }
            }
        }

        public static PacketObject Deserialize(byte[] data)
        {
            if (data == null)return (null);

            var data_value = (byte[])null;

            using (var stream = new MemoryStream(data)) {
                using (var reader = new BinaryReader(stream)) {
                    /* データタイプ (1 Byte) */
                    var data_type = reader.ReadBoolean();

                    /* データ (xx Byte) */
                    if (data_type) {
                        /* === 圧縮データ === */
                        using (var cstream = new GZipStream(stream, CompressionMode.Decompress)) {
                            using (var tstream = new MemoryStream()) {
                                cstream.CopyTo(tstream);
                                data_value = tstream.ToArray();
                            }
                        }
                    } else {
                        /* === 未圧縮データ === */
                        data_value = reader.ReadBytes((int)(stream.Length - stream.Position));
                    }
                }
            }

            /* データ解析 */
            return (Binary.Deserialize(data_value));
        }
    }
}
