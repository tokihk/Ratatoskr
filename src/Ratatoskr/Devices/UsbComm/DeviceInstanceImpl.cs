using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using Ratatoskr.Native;
using Ratatoskr.Generic;

namespace Ratatoskr.Devices.UsbComm
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl devp_;

        private UsbDevice usb_device_;

        private UsbEndpointWriter usb_writer_;
        private UsbEndpointReader usb_reader_;


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
            try {
                usb_device_ = UsbDevice.OpenUsbDevice(new UsbDeviceFinder(0x0C26, 0x0020));

                if (usb_device_ == null)return (EventResult.Busy);

                return (EventResult.Success);

            } catch {
                return (EventResult.Busy);
            }
        }

        protected override void OnConnected()
        {

        }

        protected override void OnDisconnectStart()
        {
            if (usb_reader_ != null) {
                usb_reader_.DataReceivedEnabled = false;
//                usb_reader_.DataReceived -= 
            }

            if (usb_device_.IsOpen) {
                var usb_device_i = usb_device_ as IUsbDevice;

                if (usb_device_i != null) {
                    usb_device_i.ReleaseInterface(0);
                }

                usb_device_.Close();
            }

            usb_device_ = null;

            UsbDevice.Exit();
        }

        protected override PollState OnPoll()
        {
            return (PollState.Idle);
        }
    }
}
