using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormat_SignedWord : DataFormatModule
    {
        private const int DATA_FORMAT_SIZE = 2;

        DataEndianType endian_;
        private byte[] collect_buffer_;
        private int    collect_size_;


        public DataFormat_SignedWord(DataEndianType endian)
        {
            endian_ = endian;
            collect_buffer_ = new byte[DATA_FORMAT_SIZE];
        }

        protected override void OnAssignData(byte assign_data)
        {
            collect_buffer_[collect_size_++] = assign_data;

            if (collect_size_ < collect_buffer_.Length)return;

            var data = (Int16)0;

            if (endian_ == DataEndianType.BigEndian) {
                data = (Int16)(
                          ((UInt16)collect_buffer_[0] << 8)
                        | ((UInt16)collect_buffer_[1] << 0));
            } else {
                data = (Int16)(
                          ((UInt16)collect_buffer_[1] << 8)
                        | ((UInt16)collect_buffer_[0] << 0));
            }

            ExtractData(data);

            collect_size_ = 0;
        }
    }
}
