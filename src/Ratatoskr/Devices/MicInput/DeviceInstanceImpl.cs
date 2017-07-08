using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Ratatoskr.Devices.MicInput
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl devp_;

        private WaveIn wave_in_ = null;


        public DeviceInstanceImpl(DeviceManager devm, DeviceClass devd, DeviceProperty devp, Guid id, string name) : base(devm, devd, devp, id, name)
        {
            devp_ = devp as DevicePropertyImpl;
        }

        protected override EventResult OnConnectStart()
        {
            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            try {
                var dev_no = MicDeviceInfo.GetDeviceNo(devp_.InputDeviceId.Value);

                if (dev_no < 0)return (EventResult.Error);

                var dev = WaveIn.GetCapabilities(dev_no);

                wave_in_ = new WaveIn(WaveCallbackInfo.FunctionCallback());
                wave_in_.DataAvailable += OnWaveIn_DataAvailable;
                wave_in_.DeviceNumber = dev_no;
                wave_in_.WaveFormat = new WaveFormat(
                                            (int)devp_.SamplingRate.Value,
                                            (int)devp_.BitsPerSample.Value,
                                            Math.Min((int)devp_.ChannelNum.Value, dev.Channels));
                wave_in_.StartRecording();

                return (EventResult.Success);

            } catch {
                return (EventResult.Error);
            }
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
            wave_in_.Dispose();
            wave_in_ = null;
        }

        protected override PollState OnPoll()
        {
            return (PollState.Idle);
        }

        private void OnWaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            NotifyRecvComplete("", "", "", e.Buffer);
        }

        private int GetDeviceNumber(Guid guid)
        {
            var index = 0;

            for (index = 0; index < WaveIn.DeviceCount; index++) {
                var device = WaveIn.GetCapabilities(index);

                if (device.NameGuid == guid)return (index);
            }

            return (-1);
        }
    }
}
