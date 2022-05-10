using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollectModule2
    {
        public delegate void SampledEventHandler(object sender, decimal[] data);


        private bool      sampling_init_ = false;
        private TimeSpan  sampling_ival_;
        private DateTime  sampling_start_time_ = DateTime.MinValue;

		private byte[]		data_block_;
		private DateTime	data_block_time_ = DateTime.MinValue;
		private TimeSpan	data_block_span_ = TimeSpan.Zero;
		private int			data_block_ch_num_ = 0;


		private DateTime  value_datetime_ = DateTime.MinValue;
		private TimeSpan  value_span_ = TimeSpan.Zero;
		private decimal[] value_;
		private int       value_index_;

        private bool      realtime_mode_ = true;

        
        public event SampledEventHandler Sampled;


        public DataCollectModule2(PacketViewPropertyImpl prop)
        {
			ChannelNum = (uint)prop.InputDataChannelNum.Value;

			switch (prop.SamplingIntervalUnit.Value) {
				case SamplingIntervalUnitType.Hz:
					sampling_ival_ = TimeSpan.FromTicks((long)(10000000 / prop.SamplingInterval.Value));
					break;
				case SamplingIntervalUnitType.kHz:
					sampling_ival_ = TimeSpan.FromTicks((long)(10000 / prop.SamplingInterval.Value));
					break;
				case SamplingIntervalUnitType.sec:
					sampling_ival_ = TimeSpan.FromTicks((long)(prop.SamplingInterval.Value * 10000000));
					break;
				case SamplingIntervalUnitType.msec:
					sampling_ival_ = TimeSpan.FromTicks((long)(prop.SamplingInterval.Value * 10000));
					break;
			}

			value_span_ = TimeSpan.FromTicks((long)(prop.InputDataSpan.Value * 10));

			value_ = new decimal[ChannelNum];
			value_index_ = 0;
        }

		public uint ChannelNum { get; }

        public bool RealtimeMode
        {
            get { return (realtime_mode_); }
            set
            {
                if (realtime_mode_ == value)return;

				Debugger.DebugManager.MessageOut(realtime_mode_);

                realtime_mode_ = value;

                OnModeChanged();

                if (realtime_mode_) {
					InitSampling(DateTime.Now);
                }
            }
        }

		public void SetValueDatetime(DateTime value_dt)
		{
			value_datetime_ = value_dt;
		}

		public void InputPacket(PacketObject packet)
		{

		}

		private void InitSampling(DateTime start_time)
		{
            sampling_init_ = true;
            sampling_start_time_ = start_time;

            OnSamplingStart();
		}

        private void TrySampling(DateTime cur_time)
        {
            if (sampling_ival_.TotalMilliseconds == 0)return;

			var sampling_time = sampling_start_time_ + sampling_ival_;

            /* サンプリング時間が設定されている場合は入力時間に達するまでサンプリングを行う */
            if (cur_time >= sampling_time) {
				/* サンプリング中の場合のみサンプリングイベントを実行 */
				if (sampling_init_) {
					Sampled?.Invoke(this, OnSamplingEnd());
				}

				/* 次のサンプリングを開始 */
				InitSampling(sampling_time);
            }
        }

        public void InputValue(decimal value)
        {
            /* 最初のデータ入力でサンプリング開始 */
            if (!sampling_init_) {
				InitSampling(value_datetime_);
            }

			/* チャンネルバッファにデータを収集 */
			if (value_index_ < value_.Length) {
				value_[value_index_++] = value;
			}

			/* チャンネル数のデータが集まったらイベント通知して再収集 */
			if (value_index_ >= value_.Length) {
				OnSamplingValueIn(value_);

				value_datetime_ += value_span_;
				value_ = new decimal[ChannelNum];
				value_index_ = 0;
			}

#if false
			/* サンプリング設定がOFFの場合はデータが集まるたびにサンプリング判定 */
			if (sampling_ival_.TotalMilliseconds == 0) {
                Sampling(channel_data_dt);
            }
#endif

			TrySampling(value_datetime_);
        }

        public void Poll()
        {
            if (realtime_mode_) {
                TrySampling(DateTime.Now);
            }

            OnPoll();
        }

        protected virtual void OnPoll()
        {
        }

        protected virtual void OnModeChanged()
        {
        }

        protected virtual void OnSamplingStart()
        {
        }

		/* チャンネル数のデータが入力されるたびに発生。
		 * 発生済みのデータと差し替えたり、結合を行う。
		 */
		protected virtual void OnSamplingValueIn(decimal[] value)
		{
		}

        /* サンプリング条件が成立したときに発生
         * sampling_dataに格納するデータをセットする。
         * セットできるサイズはsampling_dataのサイズまで。
         */
        protected virtual decimal[] OnSamplingEnd()
        {
			return (null);
        }
    }
}
