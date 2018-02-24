//#define ASYNC_MODE

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Generic;

namespace Ratatoskr.Devices.Null
{
    internal unsafe sealed class DeviceInstanceImpl : DeviceInstance
    {
        private object send_sync_ = new object();


        public DeviceInstanceImpl(DeviceManager devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
        }

        protected override EventResult OnConnectStart()
        {
            return (EventResult.Success);
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

        protected override void OnSendRequest()
        {
            SendPoll();
        }

        private bool SendPoll()
        {
            lock (send_sync_) {
                var send_data = GetSendData();

                if (send_data == null)return (false);

                NotifySendComplete("", "", "", send_data);

                return (true);
            }
        }
    }
}
