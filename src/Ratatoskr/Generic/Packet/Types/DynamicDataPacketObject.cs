using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet.Types
{
    [Serializable]
    internal sealed class DynamicDataPacketObject : DataPacketObject
    {
        private const int TOTAL_BUFFER_SIZE = 0xFFFFFFF;


        private sealed class DataBlock
        {
            private const int BUFFER_SIZE = 0x1FFFF;

            private byte[]      data_buffer_ = new byte[BUFFER_SIZE];
            private int         data_size_ = 0;

            public DataBlock()
            {
            }

            public int Add(byte[] data, int offset, int size)
            {
                var copy_size = Math.Min(data_buffer_.Length - data_size_, Math.Min(data.Length - offset, size));

                if (copy_size > 0) {
                    Array.Copy(data, offset, data_buffer_, data_size_, copy_size);
                    data_size_ += copy_size;
                }

                return (copy_size);
            }

            public int Add(byte data)
            {
                data_buffer_[data_size_++] = data;

                return (1);
            }

            public int Copy(byte[] dst, int offset)
            {
                var copy_size = Math.Min(dst.Length - offset, data_size_);

                if (copy_size > 0) {
                    Array.Copy(data_buffer_, 0, dst, offset, copy_size);
                    offset += copy_size;
                }

                return (Math.Min(offset, dst.Length));
            }
        }


        private Queue<DataBlock> blocks_ = new Queue<DataBlock>();
        private DataBlock block_last_ = null;

        private int data_size_ = 0;


        public DynamicDataPacketObject(PacketObject packet) : base(packet)
        {
        }

        public override byte[] GetData()
        {
            var copy_buffer = new byte[data_size_];
            var copy_offset = 0;

            foreach (var block in blocks_) {
                if (copy_offset >= copy_buffer.Length)break;

                copy_offset = block.Copy(copy_buffer, copy_offset);
            }

            return (copy_buffer);
        }

        public override int GetDataSize()
        {
            return (data_size_);
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
                offset += block_last_.Add(data, offset, data.Length);

                /* オフセットが終端に達していない場合はブロック満タン */
                if (offset < data.Length) {
                    block_last_ = null;
                }
            }

            data_size_ += data.Length;
        }

        public DataPacketObject Compile(PacketObject packet = null)
        {
            if (packet == null) {
                packet = this;
            }

            return (new StaticDataPacketObject(packet, GetData()));
        }
    }
}
