using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Utility;

namespace RtsCore.Framework.BinaryText
{
    internal static class BinaryTextMacro
    {
        public static BinaryTextData Run(string macro_name, IEnumerable<BinaryTextData> args)
        {
            try {
                macro_name = macro_name.ToUpper();

                switch (macro_name) {
                    case "RANDOM-BIN":  return RandomBin(args.ElementAt(0), args.ElementAt(1));
                    case "RANDOM-INT":  return RandomInt(args.ElementAt(0), args.ElementAt(1));
                    case "REPEAT":      return Repeat(args.ElementAt(0), args.ElementAt(1));
                    default:            return (null);
                }
            } catch {
                return (null);
            }
        }

        private static BinaryTextData RandomBin(BinaryTextData bytelen_min, BinaryTextData bytelen_max)
        {
            var bytelen_min_i_temp = (bytelen_min != null) ? (bytelen_min.ToUint64()) : (1);
            var bytelen_max_i_temp = (bytelen_max != null) ? (bytelen_max.ToUint64()) : (0xFF);

            var bytelen_min_i = (int)Math.Min(bytelen_min_i_temp, bytelen_max_i_temp);
            var bytelen_max_i = (int)Math.Min(Math.Max(bytelen_min_i_temp, bytelen_max_i_temp), 0xFFFF);

            var random = new Random();
            var value = new byte[random.Next(bytelen_min_i, bytelen_max_i)];

            random.NextBytes(value);

            return (new BinaryTextData(value));
        }

        private static BinaryTextData RandomInt(BinaryTextData value_min, BinaryTextData value_max)
        {
            var value_min_i_temp = (value_min != null) ? (value_min.ToUint64()) : (0);
            var value_max_i_temp = (value_max != null) ? (value_max.ToUint64()) : (0xFF);

            var value_min_i = (int)Math.Min(value_min_i_temp, value_max_i_temp);
            var value_max_i = (int)Math.Min(Math.Max(value_min_i_temp, value_max_i_temp), 0xFFFFFFF);

            var random = new Random();

            return (new BinaryTextData(
                            BitConverterEx.GetBytesBigEndian(
                                (ulong)random.Next(value_min_i, value_max_i))));
        }

        private static BinaryTextData Repeat(BinaryTextData data, BinaryTextData repeat_count)
        {
            var data_bin = (data != null) ? (data.GetBytes()) : (new byte[0]);
            var repeat_count_i = (repeat_count != null) ? ((uint)repeat_count.ToUint64()) : (1);

            var value = new byte[data_bin.Length * repeat_count_i];

            for (var count = 0; count < repeat_count_i; count++) {
                Array.Copy(data_bin, 0, value, count * data_bin.Length, data_bin.Length);
            }

            return (new BinaryTextData(value));
        }
    }
}
