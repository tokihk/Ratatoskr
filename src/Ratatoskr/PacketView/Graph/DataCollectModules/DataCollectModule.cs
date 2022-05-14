using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Forms;
using Ratatoskr.General;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollectModule
    {
		private const uint IVAL_SAMPLING_IDLE_LOOP_MAX = 10;


		private class ChannelValueInfo
		{
			public uint		BitSize;
			public bool		ReverseByteEndian;
			public bool		ReverseBitEndian;
			public bool		SignedValue;
		}


        public delegate void SampledEventHandler(object sender, decimal[] data);


		private SamplingTriggerType		sampling_trigger_;

        private bool		ival_sampling_init_ = false;
        private DateTime	ival_sampling_time_ = DateTime.MinValue;
        private TimeSpan	ival_sampling_span_;

		private byte[]		data_block_;
		private uint		data_block_size_ = 0;
		private uint		data_block_ch_num_ = 0;

		private ChannelValueInfo[] ch_value_infos_ = null;

        
        public event SampledEventHandler Sampled;


        public DataCollectModule(PacketViewPropertyImpl prop)
        {
			/* --- サンプリングトリガー --- */
			sampling_trigger_ = prop.SamplingTrigger.Value;

			if (sampling_trigger_ == SamplingTriggerType.TimeInterval) {
				switch (prop.SamplingIntervalUnit.Value) {
					case SamplingIntervalUnitType.Hz:
						ival_sampling_span_ = TimeSpan.FromTicks((long)(10000000 / prop.SamplingInterval.Value));
						break;
					case SamplingIntervalUnitType.kHz:
						ival_sampling_span_ = TimeSpan.FromTicks((long)(10000 / prop.SamplingInterval.Value));
						break;
					case SamplingIntervalUnitType.sec:
						ival_sampling_span_ = TimeSpan.FromTicks((long)(prop.SamplingInterval.Value * 10000000));
						break;
					case SamplingIntervalUnitType.msec:
						ival_sampling_span_ = TimeSpan.FromTicks((long)(prop.SamplingInterval.Value * 10000));
						break;
				}
			}

			/* --- データブロックサイズ --- */
			data_block_ = new byte[Math.Max(1, (int)prop.InputDataBlockSize.Value)];
			data_block_size_ = 0;

			/* --- データブロック内のチャンネル数 --- */
			if (prop.GraphTarget.Value == GraphTargetType.DataBlockCount) {
				data_block_ch_num_ = 1;
			} else {
				data_block_ch_num_ = (uint)prop.InputDataChannelNum.Value;
			}

			/* --- チャンネル情報 --- */
			var ch_value_info_list = new List<ChannelValueInfo>();
			var value_bit_size = 0;
			var value_bit_size_max = data_block_.Length * 8;

			foreach (var ch_config in prop.ChannelList.Value.Select((v, i) => (v, i))) {
				/* 設定チャンネル数を超えるときは終了 */
				if (ch_config.i >= data_block_ch_num_)break;

				/* チャンネルのデータサイズ加算後のサイズがデータブロックサイズを超えるときは終了 */
				if ((value_bit_size + ch_config.v.ValueBitSize) > value_bit_size_max)break;

				ch_value_info_list.Add(new ChannelValueInfo()
				{
					BitSize = ch_config.v.ValueBitSize,
					ReverseByteEndian = ch_config.v.ReverseByteEndian,
					ReverseBitEndian = ch_config.v.ReverseBitEndian,
					SignedValue = ch_config.v.SignedValue,
				});
			}

			ch_value_infos_ = ch_value_info_list.ToArray();
        }

		public uint ValueChannelNum
		{
			get { return (data_block_ch_num_); }
		}

        private void Sampling()
        {
			var values = OnSampling();

			if (values != null) {
				/* 取得したデータ値の数が設定チャンネル数と異なる場合は調整 */
				if (values.Length != data_block_ch_num_) {
					var values_new = new decimal[data_block_ch_num_];

					Array.Copy(values, values_new, Math.Min(values.Length, values_new.Length));
					values = values_new;
				}
			} else {
				values = new decimal[data_block_ch_num_];
			}

			Sampled?.Invoke(this, values);
        }

		private void TimeSamplingProc(DateTime cur_time)
		{
			/* サンプリング開始時刻より遡る場合は強制サンプリング */
			if (ival_sampling_time_ > cur_time) {
				ival_sampling_time_ = cur_time;
				Sampling();
			}

			/* 次のサンプリング時刻が入力時刻を超えるまで強制サンプリング */
			var count = (uint)0;

			while (((ival_sampling_time_ + ival_sampling_span_) <= cur_time) && (count < IVAL_SAMPLING_IDLE_LOOP_MAX)) {
				Sampling();
				ival_sampling_time_ += ival_sampling_span_;
				count++;
			}

			/* IDLEループを制限まで行ったときはサンプリング時刻を現在時刻に更新 */
			if (count >= IVAL_SAMPLING_IDLE_LOOP_MAX) {
				ival_sampling_time_ = cur_time;
			}
		}

		public void InputData(DateTime input_dt, IEnumerable<byte> data)
		{
			if (sampling_trigger_ == SamplingTriggerType.TimeInterval) {
				/* 時間サンプリング開始 */
				if (!ival_sampling_init_) {
					ival_sampling_init_ = true;
					ival_sampling_time_ = input_dt;
				}

				TimeSamplingProc(input_dt);
			}

			/* データブロック収集 */
			foreach (var data_one in data) {
				data_block_[data_block_size_++] = data_one;

				/* データブロックが集まったら解析 */
				if (data_block_size_ >= data_block_.Length) {
					ParseDataBlock(data_block_);
					data_block_size_ = 0;
				}
			}
		}

		private void ParseDataBlock(byte[] data_block)
		{
			var data_block_all = new BitData(data_block, (uint)data_block.Length * 8);
			var ch_value_list = new decimal[data_block_ch_num_];
			var bit_offset = (uint)0;
			var ch_value_info = (ChannelValueInfo)null;

			/* CH情報のデータ値を抽出 */
			for (var ch_index = 0; ch_index < ch_value_infos_.Length; ch_index++) {
				ch_value_info = ch_value_infos_[ch_index];

				if (ch_value_info.BitSize == 0)continue;

				/* データブロックからCH設定に合致する位置のデータを抽出 */
				var ch_block = data_block_all.GetBitData(bit_offset, ch_value_info.BitSize);

				if (ch_value_info.ReverseBitEndian) {
					ch_block.ReverseBitEndian();
				}
				if (ch_value_info.ReverseByteEndian) {
					ch_block.ReverseByteEndian();
				}

				ch_value_list[ch_index] = ch_block.GetInteger(ch_value_info.SignedValue);
			}

			/* 抽出したチャンネル値を上層へ通知 */
			OnExtractedValue(ch_value_list);

			if (sampling_trigger_ == SamplingTriggerType.DataBlockDetect) {
				Sampling();
			}
		}

        public void Poll()
        {
			if (   (sampling_trigger_ == SamplingTriggerType.TimeInterval)
				&& (!GatePacketManager.IsSaveBusy)
				&& (!GatePacketManager.IsLoadBusy)
				&& (!FormTaskManager.IsRedrawBusy)
			) {
				TimeSamplingProc(DateTime.Now);
			}

            OnPoll();
        }

        protected virtual void OnPoll()
        {
        }

		protected virtual void OnExtractedValue(decimal[] value)
		{
		}

        /* サンプリング条件が成立したときに発生
         * sampling_dataに格納するデータをセットする。
         * セットできるサイズはsampling_dataのサイズまで。
         */
        protected virtual decimal[] OnSampling()
        {
			return (null);
        }
    }
}
