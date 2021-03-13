using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public static class StructEncoder
    {
        public static Byte ToByte(IEnumerable<byte> data)
        {
            if (data.Count() < 1)return (0);

            return ((Byte)data.ElementAt(0));
        }

        public static UInt16 ToUInt16(IEnumerable<byte> data, bool little_endian)
        {
            if (data.Count() < 2)return (0);

            if (little_endian) {
                return ((UInt16)(
                              (((UInt16)data.ElementAt(0)) << 0)
                            | (((UInt16)data.ElementAt(1)) << 8)));
            } else {
                return ((UInt16)(
                              (((UInt16)data.ElementAt(0)) << 8)
                            | (((UInt16)data.ElementAt(1)) << 0)));
            }
        }

        public static UInt32 ToUInt32(IEnumerable<byte> data, bool little_endian)
        {
            if (data.Count() < 4)return (0);

            if (little_endian) {
                return ((UInt32)(
                              (((UInt32)data.ElementAt(0)) << 0)
                            | (((UInt32)data.ElementAt(1)) << 8)
                            | (((UInt32)data.ElementAt(2)) << 16)
                            | (((UInt32)data.ElementAt(3)) << 24)));
            } else {
                return ((UInt32)(
                              (((UInt32)data.ElementAt(0)) << 24)
                            | (((UInt32)data.ElementAt(1)) << 16)
                            | (((UInt32)data.ElementAt(2)) << 8)
                            | (((UInt32)data.ElementAt(3)) << 0)));
            }
        }

        public static UInt64 ToUInt64(IEnumerable<byte> data, bool little_endian)
        {
            if (data.Count() < 8)return (0);

            if (little_endian) {
                return ((UInt64)(
                              (((UInt64)data.ElementAt(0)) << 0)
                            | (((UInt64)data.ElementAt(1)) << 8)
                            | (((UInt64)data.ElementAt(2)) << 16)
                            | (((UInt64)data.ElementAt(3)) << 24)
                            | (((UInt64)data.ElementAt(4)) << 32)
                            | (((UInt64)data.ElementAt(5)) << 40)
                            | (((UInt64)data.ElementAt(6)) << 48)
                            | (((UInt64)data.ElementAt(7)) << 56)));
            } else {
                return ((UInt64)(
                              (((UInt64)data.ElementAt(0)) << 56)
                            | (((UInt64)data.ElementAt(1)) << 48)
                            | (((UInt64)data.ElementAt(2)) << 40)
                            | (((UInt64)data.ElementAt(3)) << 32)
                            | (((UInt64)data.ElementAt(4)) << 24)
                            | (((UInt64)data.ElementAt(5)) << 16)
                            | (((UInt64)data.ElementAt(6)) << 8)
                            | (((UInt64)data.ElementAt(7)) << 0)));
            }
        }

        public static SByte ToSByte(IEnumerable<byte> data)
        {
            return ((SByte)ToByte(data));
        }

        public static Int16 ToInt16(IEnumerable<byte> data, bool little_endian)
        {
            return ((Int16)ToUInt16(data, little_endian));
        }

        public static Int32 ToInt32(IEnumerable<byte> data, bool little_endian)
        {
            return ((Int32)ToUInt32(data, little_endian));
        }

        public static Int64 ToInt64(IEnumerable<byte> data, bool little_endian)
        {
            return ((Int64)ToUInt64(data, little_endian));
        }

        public static float ToFloat(IEnumerable<byte> data, bool little_endian)
        {
            return ((float)ToUInt32(data, little_endian));
        }

        public static double ToDouble(IEnumerable<byte> data, bool little_endian)
        {
            return ((double)ToUInt64(data, little_endian));
        }
    }
}
