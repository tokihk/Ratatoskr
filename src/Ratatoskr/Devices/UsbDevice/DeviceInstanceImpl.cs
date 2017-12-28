using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Generic;

namespace Ratatoskr.Devices.UsbDevice
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl devp_;


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
            return (PollState.Idle);
        }
    }
}
