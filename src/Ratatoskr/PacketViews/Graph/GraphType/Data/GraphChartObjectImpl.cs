using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;

namespace Ratatoskr.PacketViews.Graph.GraphType.Data
{
    internal class GraphChartObjectImpl : GraphChartObject
    {
        private class ChannelBuffer
        {
            public ChannelBuffer(int size)
            {
                Data = new byte[size];
            }

            public byte[] Data   { get; private set; }
            public int    Length { get; private set; } = 0;

            public void Clear()
            {
                Length = 0;
            }

            public bool Add(byte data)
            {
                Data[Length++] = data;

                return (Length >= Data.Length);
            }
        }


        private DataType input_type_;
        private int      input_bytes_;
        private bool     input_endian_is_little_;
        private int      input_cycle_;

        private byte[] busy_data_;
        private int    busy_size_ = 0;

        private double[] view_data_;
        private int      view_data_in_ = 0;
        private bool     view_update_ = false;


        public GraphChartObjectImpl(ViewPropertyImpl prop) : base(prop)
        {
            input_type_ = Property.InputDataType.Value;
            input_bytes_ = Property.GetInputDataTypeSize();
            input_endian_is_little_ = (Property.InputByteEndian.Value == EndianType.LittleEndian);
            input_cycle_ = (int)Property.InputCycle.Value;

            busy_data_ = new byte[Property.GetInputDataTypeSize()];
            view_data_ = new double[(int)Property.DrawPointNum.Value];
        }

        public override bool IsViewUpdate
        {
            get {
                return (view_update_);
            }
        }

        private void InputData(byte value)
        {
            /* 収集バッファにデータを追加 */
            busy_data_[busy_size_++] = value;
            
            /* データが集まっていない場合は抜ける */
            if (busy_size_ < busy_data_.Length)return;

            /* データ処理前に再収集可能な状態にしておく */
            busy_size_ = 0;

            /* データスキップ判定 */
            input_cycle_--;
            if (input_cycle_ > 0)return;
            input_cycle_ = (int)Property.InputCycle.Value;

            /* データを指定型に変換 */
            var value_d = (double)0;

            switch (input_type_) {
                case DataType.UnsignedByte:     value_d = ByteEncoder.ToByte(busy_data_);                            break;
                case DataType.UnsignedWord:     value_d = ByteEncoder.ToUInt16(busy_data_, input_endian_is_little_); break;
                case DataType.UnsignedDword:    value_d = ByteEncoder.ToUInt32(busy_data_, input_endian_is_little_); break;
                case DataType.UnsignedQword:    value_d = ByteEncoder.ToUInt64(busy_data_, input_endian_is_little_); break;
                case DataType.SignedByte:       value_d = ByteEncoder.ToSByte(busy_data_);                           break;
                case DataType.SignedWord:       value_d = ByteEncoder.ToInt16(busy_data_, input_endian_is_little_);  break;
                case DataType.SignedDword:      value_d = ByteEncoder.ToInt32(busy_data_, input_endian_is_little_);  break;
                case DataType.SignedQword:      value_d = ByteEncoder.ToInt64(busy_data_, input_endian_is_little_);  break;
                case DataType.IEEE754_Float:    value_d = ByteEncoder.ToFloat(busy_data_, input_endian_is_little_);  break;
                case DataType.IEEE754_Double:   value_d = ByteEncoder.ToDouble(busy_data_, input_endian_is_little_); break;
            }

            /* ビューバッファに格納 */
            view_data_[view_data_in_++] = value_d + (double)Property.DataValueOffset.Value;
            if (view_data_in_ >= view_data_.Length) {
                view_data_in_ = 0;
            }

            view_update_ = true;
        }

        protected override GraphViewData OnLoadViewData()
        {
            var draw_data = new double[view_data_.Length];

            var src_index = 0;
            var dst_index = 0;
            var data_now = double.NaN;
            var data_min = double.MaxValue;
            var data_max = double.MinValue;

            /* スキャン開始位置を取得(最も古いデータから表示) */
            src_index = view_data_in_;
            if (src_index >= view_data_.Length) {
                src_index = 0;
            }

            /* データをバッファにコピー */
            do {
                data_now = view_data_[src_index++];
                if (data_now < data_min)data_min = data_now;
                if (data_now > data_max)data_max = data_now;

                draw_data[dst_index++] = data_now;
                if (src_index >= view_data_.Length) {
                    src_index = 0;
                }
            } while (src_index != view_data_in_);

            view_update_ = false;

            return (new GraphViewData(data_min, data_max, draw_data));
        }

        protected override void OnInputData(byte[] value)
        {
            foreach (var data in value) {
                InputData(data);
            }
        }
    }
}
