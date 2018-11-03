using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Packet
{
    [Serializable]
    public sealed class DynamicPacketObject : PacketObject
    {
        private const int TOTAL_BUFFER_SIZE = 0xFFFFFFF;


        private sealed class DataBlock
        {
            private const int BUFFER_SIZE = 1024;

            private byte[]      data_buffer_ = new byte[BUFFER_SIZE];
            private int         data_size_ = 0;

            public DataBlock()
            {
            }

            public int AddData(byte[] data, int offset, int size)
            {
                var copy_size = Math.Min(data_buffer_.Length - data_size_, Math.Min(data.Length - offset, size));

                if (copy_size > 0) {
                    Buffer.BlockCopy(data, offset, data_buffer_, data_size_, copy_size);
                    data_size_ += copy_size;
                }

                return (copy_size);
            }

            public int AddData(byte data)
            {
                data_buffer_[data_size_++] = data;

                return (1);
            }

            public int Copy(byte[] dst, int offset)
            {
                var copy_size = Math.Min(dst.Length - offset, data_size_);

                if (copy_size > 0) {
                    Buffer.BlockCopy(data_buffer_, 0, dst, offset, copy_size);
                    offset += copy_size;
                }

                return (Math.Min(offset, dst.Length));
            }
        }


        private Queue<DataBlock> blocks_ = new Queue<DataBlock>();
        private DataBlock block_last_ = null;

        private int data_size_ = 0;


        public DynamicPacketObject(PacketObject packet) : base(packet)
        {
        }

        public override byte[] Data
        {
            get
            {
                var copy_buffer = new byte[data_size_];
                var copy_offset = 0;

                foreach (var block in blocks_) {
                    if (copy_offset >= copy_buffer.Length)break;

                    copy_offset = block.Copy(copy_buffer, copy_offset);
                }

                return (copy_buffer);
            }
        }

        public override int DataLength
        {
            get { return (data_size_); }
        }

        public bool AddData(byte data)
        {
            /* 追加後のデータサイズが限度を超える場合は失敗 */
            if (data_size_ >= TOTAL_BUFFER_SIZE)return (false);

            AddDataCore(new byte[] { data });

            return (true);
        }

        public bool AddData(byte[] data)
        {
            /* 追加後のデータサイズが限度を超える場合は失敗 */
            if ((data_size_ + data.Length) >= TOTAL_BUFFER_SIZE)return (false);

            AddDataCore(data);

            return (true);
        }

        private void AddDataCore(byte[] data)
        {
            for (var offset = 0; offset < data.Length;) {
                /* 新規ブロック挿入 */
                if (block_last_ == null) {
                    blocks_.Enqueue(block_last_ = new DataBlock());
                }

                /* ブロックにデータを追加 */
                offset += block_last_.AddData(data, offset, data.Length);

                /* オフセットが終端に達していない場合はブロック満タン */
                if (offset < data.Length) {
                    block_last_ = null;
                }
            }

            data_size_ += data.Length;
        }

        public PacketObject Compile(PacketObject packet = null)
        {
            if (packet == null) {
                packet = this;
            }

            return (new PacketObject(
                            packet.Class,
                            packet.Facility,
                            packet.Alias,
                            packet.Priority,
                            packet.Attribute,
                            packet.MakeTime,
                            packet.Information,
                            packet.Direction,
                            packet.Source,
                            packet.Destination,
                            packet.UserMark,
                            packet.Message,
                            Data));
        }
    }
}
