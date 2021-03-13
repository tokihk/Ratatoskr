using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormat_SignedDword : DataFormatModule
    {
        private const int DATA_FORMAT_SIZE = 4;

        DataEndianType endian_;
        private byte[] collect_buffer_;
        private int    collect_size_;


        public DataFormat_SignedDword(DataEndianType endian)
        {
            endian_ = endian;
            collect_buffer_ = new byte[DATA_FORMAT_SIZE];
        }

        protected override void OnAssignData(byte assign_data)
        {
            collect_buffer_[collect_size_++] = assign_data;

            if (collect_size_ < collect_buffer_.Length)return;

            var data = (Int32)0;

            if (endian_ == DataEndianType.BigEndian) {
                data = (Int32)(
                          ((UInt32)collect_buffer_[0] << 24)
                        | ((UInt32)collect_buffer_[1] << 16)
                        | ((UInt32)collect_buffer_[2] << 8)
                        | ((UInt32)collect_buffer_[3] << 0));
            } else {
                data = (Int32)(
                          ((UInt32)collect_buffer_[3] << 24)
                        | ((UInt32)collect_buffer_[2] << 16)
                        | ((UInt32)collect_buffer_[1] << 8)
                        | ((UInt32)collect_buffer_[0] << 0));
            }

            ExtractData(data);

            collect_size_ = 0;
        }
    }
}
