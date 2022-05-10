using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormat_IEEE754_Float : DataFormatModule
    {
        private const int DATA_FORMAT_SIZE = 4;

        private byte[] collect_buffer_;
        private int    collect_size_;


        public DataFormat_IEEE754_Float(PacketViewPropertyImpl prop) : base(prop)
        {
            collect_buffer_ = new byte[DATA_FORMAT_SIZE];
        }

        protected override void OnInputData(byte data)
        {
            collect_buffer_[collect_size_++] = data;

            if (collect_size_ < collect_buffer_.Length)return;

            var value = (float)0;

            if (ByteEndian == DataEndianType.BigEndian) {
                value = (float)(
                          ((UInt32)collect_buffer_[0] << 24)
                        | ((UInt32)collect_buffer_[1] << 16)
                        | ((UInt32)collect_buffer_[2] << 8)
                        | ((UInt32)collect_buffer_[3] << 0));
            } else {
                value = (float)(
                          ((UInt32)collect_buffer_[3] << 24)
                        | ((UInt32)collect_buffer_[2] << 16)
                        | ((UInt32)collect_buffer_[1] << 8)
                        | ((UInt32)collect_buffer_[0] << 0));
            }

            ExtractValue((decimal)value);

            collect_size_ = 0;
        }
    }
}
