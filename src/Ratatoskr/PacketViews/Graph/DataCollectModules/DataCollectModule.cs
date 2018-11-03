using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.PacketViews.Graph.DataCollectModules
{
    internal class DataCollectModule
    {
        public delegate void SampledEventHandler(object sender, decimal[] data);


        private decimal[] layer_data_;
        private uint      layer_data_index_ = 0;

        private bool      sampling_init_ = false;
        private TimeSpan  sampling_ival_;
        private DateTime  sampling_start_time_ = DateTime.MinValue;


        private bool auto_update_ = true;

        
        public event SampledEventHandler Sampled;


        public DataCollectModule(uint layer_count, uint sampling_ival)
        {
            LayerCount = Math.Max(1, layer_count);
            layer_data_ = new decimal[LayerCount];

            sampling_ival_ = TimeSpan.FromMilliseconds(sampling_ival);
        }

        public uint LayerCount { get; }

        public bool AutoUpdate
        {
            get { return (auto_update_); }
            set
            {
                if (auto_update_ == value)return;

                auto_update_ = value;

                OnAutoUpdateChanged();

                if (auto_update_) {
                    sampling_init_ = true;
                    sampling_start_time_ = DateTime.Now;
                    OnSamplingStart();
                }
            }
        }

        private void Sampling(DateTime next_start_time)
        {
            if (sampling_init_) {
                var data = new decimal[layer_data_.Length];

                OnSamplingEnd(data);

                Sampled?.Invoke(this, data);
            }

            sampling_init_ = true;
            sampling_start_time_ = next_start_time;
            OnSamplingStart();
        }

        private void SamplingProc(DateTime sampling_time)
        {
            if (sampling_ival_.TotalMilliseconds == 0)return;

            /* サンプリング時間が設定されている場合は入力時間に達するまでサンプリングを行う */
            while (sampling_time > (sampling_start_time_ + sampling_ival_)) {
                Sampling(sampling_start_time_ + sampling_ival_);
            }
        }

        public void InputData(PacketObject base_packet, decimal data)
        {
            /* 最初のデータ入力でサンプリング開始 */
            if (!sampling_init_) {
                sampling_init_ = true;
                sampling_start_time_ = base_packet.MakeTime;
                OnSamplingStart();
            }

            /* データをチャンネルバッファに格納 */
            if (layer_data_index_ < layer_data_.Length) {
                layer_data_[layer_data_index_++] = data;

                /* データが集まったら上位層に通知 */
                if (layer_data_index_ >= layer_data_.Length) {
                    OnAssignData(base_packet, layer_data_.Clone() as decimal[]);
                    layer_data_index_ = 0;

                    /* サンプリング設定がOFFの場合はデータが集まるたびにサンプリング処理 */
                    if (sampling_ival_.TotalMilliseconds == 0) {
                        Sampling(base_packet.MakeTime);
                    }
                }
            }

            SamplingProc(base_packet.MakeTime);
        }

        public void Poll()
        {
            if (auto_update_) {
                SamplingProc(DateTime.Now);
            }

            OnPoll();
        }

        protected virtual void OnPoll()
        {
        }

        protected virtual void OnAutoUpdateChanged()
        {
        }

        /* レイヤー数分のデータが集まった時に発生
         * 通知されたデータは継承先が管理する
         */
        protected virtual void OnAssignData(PacketObject base_packet, decimal[] assign_data)
        {
        }

        protected virtual void OnSamplingStart()
        {
        }

        /* サンプリング条件が成立したときに発生
         * sampling_dataに格納するデータをセットする。
         * セットできるサイズはsampling_dataのサイズまで。
         */
        protected virtual void OnSamplingEnd(decimal[] sampling_data)
        {
        }
    }
}
