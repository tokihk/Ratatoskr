using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratatoskr.General.BinaryText
{
    public sealed class BinaryTextData
    {
        private const int DATA_BUFFER_MARGIN_SIZE = 128;


        public static BinaryTextData operator+ (BinaryTextData left, BinaryTextData right)
        {
            /* Leftに十分な空きがないときは拡張 */
            if ((left.data_size_ + right.data_size_) > left.data_buffer_.Length) {
                var data_new_buffer = new byte[left.data_size_ + right.data_size_ + DATA_BUFFER_MARGIN_SIZE];

                Array.Copy(left.data_buffer_, 0, data_new_buffer, 0, left.data_size_);

                left.data_buffer_ = data_new_buffer;
            }

            Array.Copy(right.data_buffer_, 0, left.data_buffer_, left.data_size_, right.data_size_);
            left.data_size_ += right.data_size_;

            return (left);
        }

        private static string TextDecodeES(string text)
        {
            var str = new StringBuilder(text.Length);
            var escape_state = false;

            foreach (var code in text) {
                if (escape_state) {
                    str.Append(code);
                    escape_state = false;
                } else if (code == '\\') {
                    escape_state = true;
                } else {
                    str.Append(code);
                }
            }

            return (str.ToString());
        }


        private byte[] data_buffer_;
        private int    data_size_;
        

        public BinaryTextData()
        {
            data_buffer_ = new byte[0];
            data_size_ = 0;
        }

        public BinaryTextData(byte[] data_buffer)
        {
            data_buffer_ = data_buffer;
            data_size_ = data_buffer.Length;
        }

        public BinaryTextData(byte[] data_buffer, int data_size)
        {
            data_buffer_ = data_buffer;
            data_size_ = data_size;
        }

        public BinaryTextData(ulong value)
        {
            data_buffer_ = BitConverterEx.GetBytesBigEndian(value);
            data_size_ = data_buffer_.Length;
        }

        public BinaryTextData(string hex_string)
        {
            data_buffer_ = HexTextEncoder.ToBinary(hex_string);
            data_size_ = data_buffer_.Length;
        }

        public BinaryTextData(Encoding encoder, string text)
        {
            data_buffer_ = encoder.GetBytes(TextDecodeES(text));
            data_size_ = data_buffer_.Length;
        }

        public override string ToString()
        {
            return (HexTextEncoder.ToHexText(GetBytes()));
        }

        public byte[] GetBytes()
        {
            if (data_buffer_.Length == data_size_) {
                return (data_buffer_);
            } else {
                return (ClassUtil.CloneCopy(data_buffer_, data_size_));
            }
        }

        public ulong ToUint64()
        {
            var value = new byte[8];
            var copy_size = Math.Min(8, data_size_);
            var src_offset = data_size_ - copy_size;
            var dst_offset = 8 - copy_size;

            Array.Copy(data_buffer_, src_offset, value, dst_offset, copy_size);

            return (  ((ulong)value[0] << 56)
                    | ((ulong)value[1] << 48)
                    | ((ulong)value[2] << 40)
                    | ((ulong)value[3] << 32)
                    | ((ulong)value[4] << 24)
                    | ((ulong)value[5] << 16)
                    | ((ulong)value[6] <<  8)
                    | ((ulong)value[7] <<  0));
        }
    }
}
