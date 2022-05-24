using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.General;
using NAudio.Wave;

namespace Ratatoskr.Device.AudioFile
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
		private class AudioSamplingInfo
		{
			public int SamplingRate		{ get; set; }
			public int BitsPerSample	{ get; set; }
			public int Channels			{ get; set; }
		}


        private const int VOICE_OUTPUT_IVAL = 100;

        private DevicePropertyImpl devp_;

        private bool play_complete_ = false;
        private int repeat_count_ = 0;

        private MediaFoundationReader    reader_ = null;
        private MediaFoundationResampler resampler_ = null;

        private object              sampling_sync_ = new object();
        private System.Timers.Timer sampling_timer_ = new System.Timers.Timer();
        private long                sampling_counter_ = 0;
        private byte[]              sampling_buffer_;


        public DeviceInstanceImpl(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
            devp_ = devp as DevicePropertyImpl;

            sampling_timer_.Elapsed += OnSampling;
            sampling_timer_.Interval = VOICE_OUTPUT_IVAL;
        }

		private AudioSamplingInfo GetAudioFileInfo(string path)
		{
			try {
				using (var reader = new MediaFoundationReader(devp_.InputFilePath.Value)) {
					return (new AudioSamplingInfo()
					{
						SamplingRate = reader.WaveFormat.SampleRate,
						BitsPerSample = reader.WaveFormat.BitsPerSample,
						Channels = reader.WaveFormat.Channels,
					});
				}
			} catch {
				return (null);
			}
		}

		private AudioSamplingInfo GetAudioSamplingSetting(string path)
		{
			var info = GetAudioFileInfo(devp_.InputFilePath.Value);

			if (info == null)return (null);

			switch (devp_.InputSamplingRate.Value) {
				case SamplingRateType.Preset_8kHz:		info.SamplingRate = 8000;	break;
				case SamplingRateType.Preset_44_1kHz:	info.SamplingRate = 44100;	break;
				case SamplingRateType.Preset_48kHz:		info.SamplingRate = 48000;	break;
			}

			switch (devp_.InputBitsPerSample.Value) {
				case BitPerSampleType.Preset_8bit:		info.BitsPerSample = 8;		break;
				case BitPerSampleType.Preset_16bit:		info.BitsPerSample = 16;	break;
			}

			switch (devp_.InputChannelNum.Value) {
				case ChannelNumberType.Preset_Monoral:	info.Channels = 1;		break;
				case ChannelNumberType.Preset_Stereo:	info.Channels = 2;		break;
				case ChannelNumberType.Preset_5_1ch:	info.Channels = 6;		break;
				case ChannelNumberType.Preset_6_1ch:	info.Channels = 7;		break;
				case ChannelNumberType.Preset_7_1ch:	info.Channels = 8;		break;
			}

			return (info);
		}

        private void DataOutputExec()
        {
            var read_size = 0;

            while ((!play_complete_) && (read_size < sampling_buffer_.Length)) {
                read_size += resampler_.Read(sampling_buffer_, read_size, sampling_buffer_.Length - read_size);

                /* データバッファが一杯にならないときは最初から読み込む */
                if (read_size < sampling_buffer_.Length) {
                    if ((devp_.RepeatCount.Value == 0) || ((repeat_count_ + 1) < devp_.RepeatCount.Value)) {
                        /* 繰り返し設定が有効かつ繰り返しが可能のとき */
                        reader_.Position = 0;
                        repeat_count_++;
                    } else {
                        /* 再生終了 */
                        play_complete_ = true;
                    }
                }
            }

            if (read_size > 0) {
                /* データ出力 */
                NotifyRecvComplete(
                    "",
                    "",
                    "",
                    (read_size < sampling_buffer_.Length) ? (ClassUtil.CloneCopy(sampling_buffer_, 0, read_size)) : (sampling_buffer_));
            }
        }

        protected override void OnDispose()
        {
            if (sampling_timer_ != null) {
                sampling_timer_.Dispose();
            }

            if (reader_ != null) {
                reader_.Dispose();
            }

            if (resampler_ != null) {
                resampler_.Dispose();
            }
        }

		protected override EventResult OnConnectBusy()
		{
            try {
                /* 一度でも開いていないときのみ初期化 */
                if (   (reader_ == null)
                    || (resampler_ == null)
                ) {
					var sampling_info = GetAudioSamplingSetting(devp_.InputFilePath.Value);

                    reader_ = new MediaFoundationReader(devp_.InputFilePath.Value);

                    resampler_ = new MediaFoundationResampler(
                                        reader_,
                                        new WaveFormat(
                                            sampling_info.SamplingRate,
                                            sampling_info.BitsPerSample,
                                            sampling_info.Channels));

                    sampling_buffer_ = new byte[resampler_.WaveFormat.AverageBytesPerSecond / 10];
                }

                return (EventResult.Success);
            } catch {
                return (EventResult.Error);
            }
        }

        protected override void OnConnected()
        {
			/* 接続のたびに初期化する設定の場合 */
			if (devp_.ConnectAction.Value == ConnectActionType.Restart) {
				reader_.Position = 0;
				repeat_count_ = 0;
			}

			play_complete_ = false;

			sampling_timer_.Start();
        }

        protected override void OnDisconnectStart()
        {
            sampling_timer_.Stop();
        }

        protected override PollState OnPoll()
        {
            if (sampling_counter_ > 0) {
                lock (sampling_sync_) {
                    sampling_counter_--;
                    DataOutputExec();
                }
            }

            return (PollState.Active);
        }

        private void OnSampling(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (sampling_sync_) {
                sampling_counter_++;
            }
        }
    }
}
