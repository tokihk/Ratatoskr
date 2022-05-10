using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormat_SignedQword : DataFormatModule
    {
        private const int DATA_FORMAT_SIZE = 8;

        private byte[] collect_buffer_;
        private int    collect_size_;


        public DataFormat_SignedQword(PacketViewPropertyImpl prop) : base(prop)
        {
            collect_buffer_ = new byte[DATA_FORMAT_SIZE];
        }

        protected override void OnInputData(byte data)
        {
            collect_buffer_[collect_size_++] = data;

            if (collect_size_ < collect_buffer_.Length)return;

            var value = (Int64)0;

            if (ByteEndian == DataEndianType.BigEndian) {
                value = (Int64)(
                          ((UInt64)collect_buffer_[0] << 56)
                        | ((UInt64)collect_buffer_[1] << 48)
                        | ((UInt64)collect_buffer_[2] << 40)
                        | ((UInt64)collect_buffer_[3] << 32)
                        | ((UInt64)collect_buffer_[4] << 24)
                        | ((UInt64)collect_buffer_[5] << 16)
                        | ((UInt64)collect_buffer_[6] << 8)
                        | ((UInt64)collect_buffer_[7] << 0));
            } else {
                value = (Int64)(
                          ((UInt64)collect_buffer_[7] << 56)
                        | ((UInt64)collect_buffer_[6] << 48)
                        | ((UInt64)collect_buffer_[5] << 40)
                        | ((UInt64)collect_buffer_[4] << 32)
                        | ((UInt64)collect_buffer_[3] << 24)
                        | ((UInt64)collect_buffer_[2] << 16)
                        | ((UInt64)collect_buffer_[1] << 8)
                        | ((UInt64)collect_buffer_[0] << 0));
            }

            ExtractValue(value);

            collect_size_ = 0;
        }
    }
}
