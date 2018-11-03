using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using RtsCore.Generic;
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.AudioDevice
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl devp_;

        private WaveIn wave_in_ = null;

        private MMDevice             play_device_ = null;
        private IWavePlayer          wave_player_ = null;
        private BufferedWaveProvider play_provider_ = null;
        private byte[]               play_data_next_ = null;


        public DeviceInstanceImpl(DeviceManager devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
            devp_ = devp as DevicePropertyImpl;
        }

        protected override EventResult OnConnectStart()
        {
            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            InputDeviceStart();

            OutputPlayerClose();
//            OutputDeviceStart();

            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
            InputDeviceStop();
            OutputDeviceStop();
        }

        protected override PollState OnPoll()
        {
            OutputPoll();

            return (PollState.Idle);
        }

        private void InputDeviceStart()
        {
            InputDeviceStop();

            if (!devp_.InputEnable.Value)return;

            try {
                var dev_no = MicDeviceInfo.GetDeviceNo(devp_.InputDeviceId.Value);

                if (dev_no < 0)return;

                var dev_info = WaveIn.GetCapabilities(dev_no);

                wave_in_ = new WaveIn(WaveCallbackInfo.FunctionCallback());
                wave_in_.DataAvailable += OnWaveIn_DataAvailable;
                wave_in_.DeviceNumber = dev_no;
                wave_in_.WaveFormat = new WaveFormat(
                                            (int)devp_.InputSamplingRate.Value,
                                            (int)devp_.InputBitsPerSample.Value,
                                            Math.Min((int)devp_.InputChannelNum.Value, dev_info.Channels));

                /* 入力開始 */
                wave_in_.StartRecording();

            } catch {
                InputDeviceStop();
            }
        }

        private void InputDeviceStop()
        {
            if (wave_in_ == null)return;

            wave_in_.Dispose();
            wave_in_ = null;
        }

        private void OnWaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            NotifyRecvComplete("", "", "", ClassUtil.CloneCopy(e.Buffer));
        }

        private void OutputPoll()
        {
            if (play_device_ == null)return;
            if (!devp_.OutputEnable.Value)return;

            /* 送信データ読み込み */
            if (play_data_next_ == null) {
                play_data_next_ = GetSendData();
            }

            /* 再生オブジェクト初期化 */
            if (   (wave_player_ == null)
                && (play_data_next_ != null)
            ) {
                OutputPlayerOpen();
            }

            /* 再生オブジェクトが存在しない場合は終了 */
            if (wave_player_ == null)return;

            OutputPlayerPoll();
        }

        private void OutputDeviceStart()
        {
            OutputDeviceStop();

            if (!devp_.OutputEnable.Value)return;

            try {
                play_device_ = (new MMDeviceEnumerator()).GetDevice(devp_.OutputDeviceId.Value);
            } catch {
                OutputDeviceStop();
            }
        }

        private void OutputDeviceStop()
        {
            if (play_device_ == null)return;

            play_device_.Dispose();
            play_device_ = null;
        }

        private void OutputPlayerOpen()
        {
            OutputPlayerClose();

            /* 再生オブジェクトを作成 */
            wave_player_ = new WasapiOut(play_device_, AudioClientShareMode.Shared, false, 200);

            /* 再生データ格納用プロバイダを作成 */
            play_provider_ = new BufferedWaveProvider(
                                    new WaveFormat(
                                            (int)devp_.OutputSamplingRate.Value,
                                            (int)devp_.OutputBitsPerSample.Value,
                                            (int)devp_.OutputChannelNum.Value));

            /* 音量制御用プロバイダを経由させる */
            var provider = new VolumeWaveProvider16(play_provider_)
            {
//                Volume = 0.1f,
//                Volume = (float)((double)devp_.OutputVolume.Value / 100)
            };

            /* 再生オブジェクトにプロバイダを設定 */
            wave_player_.Init(provider);
        }

        private void OutputPlayerClose()
        {
            if (wave_player_ != null) {
                wave_player_.Stop();
                wave_player_.Dispose();
                wave_player_ = null;
            }

            if (play_provider_ != null) {
                play_provider_.ClearBuffer();
                play_provider_ = null;
            }
        }

        private void OutputPlayerPoll()
        {
            if (wave_player_ == null)return;

            OutputPlayerBufferSetup();

            if (play_provider_.BufferedBytes == 0) {
                OutputPlayerClose();
            }
        }

        private void OutputPlayerBufferSetup()
        {
            if (play_data_next_ != null) {
                var send_size = Math.Min(play_data_next_.Length, play_provider_.BufferLength - play_provider_.BufferedBytes);

                if (send_size == 0)return;
                
                var send_data = (byte[])null;

                if (play_data_next_.Length <= send_size) {
                    send_data = play_data_next_;
                }

                play_provider_.AddSamples(play_data_next_, 0, play_data_next_.Length);
                
                play_data_next_ = null;
            }
        }
    }
}
