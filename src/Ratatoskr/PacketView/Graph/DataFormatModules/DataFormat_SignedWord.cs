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

        private byte[] collect_buffer_;
        private int    collect_size_;


        public DataFormat_SignedWord(PacketViewPropertyImpl prop) : base(prop)
        {
            collect_buffer_ = new byte[DATA_FORMAT_SIZE];
        }

        protected override void OnInputData(byte data)
        {
            collect_buffer_[collect_size_++] = data;

            if (collect_size_ < collect_buffer_.Length)return;

            var value = (Int16)0;

            if (ByteEndian == DataEndianType.BigEndian) {
                value = (Int16)(
                          ((UInt16)collect_buffer_[0] << 8)
                        | ((UInt16)collect_buffer_[1] << 0));
            } else {
                value = (Int16)(
                          ((UInt16)collect_buffer_[1] << 8)
                        | ((UInt16)collect_buffer_[0] << 0));
            }

            ExtractValue(value);

            collect_size_ = 0;
        }
    }
}
