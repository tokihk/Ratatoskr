//#define ASYNC_MODE

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RtsCore.Device.Null
{
    internal unsafe sealed class DeviceInstanceImpl : DeviceInstance
    {
        public DeviceInstanceImpl(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
        }

        protected override void OnConnectStart()
        {
        }

        protected override EventResult OnConnectBusy()
        {
            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
        }

        protected override PollState OnPoll()
        {
            var state = PollState.Idle;

            while (SendPoll()) {
                state = PollState.Busy;
            }

            return (state);
        }

        private bool SendPoll()
        {
            var send_data = GetSendData();

            if (send_data == null)return (false);

            NotifySendComplete("", "", "", send_data);

            return (true);
        }
    }
}
