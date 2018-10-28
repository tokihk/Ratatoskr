using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;

namespace Ratatoskr.Utility
{
    internal static class BitConverterEx
    {
        public static byte[] GetBytesBigEndian(ulong value)
        {
            var bitdata = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian) {
                Array.Reverse(bitdata);
            }

            var zero_pos = Array.FindIndex(bitdata, data => data != 0);

            if (zero_pos > 0) {
                bitdata = ClassUtil.CloneCopy(bitdata, zero_pos, bitdata.Length - zero_pos);
            } else if (zero_pos < 0) {
                bitdata = new byte[] { 0x00 };
            }

            return (bitdata);
        }
    }
}
