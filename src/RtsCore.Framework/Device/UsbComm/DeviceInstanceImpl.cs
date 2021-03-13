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
using LibUsbDotNet.DeviceNotify;
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.UsbComm
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private readonly DevicePropertyImpl devp_;

        private UsbDeviceFinder usb_finder_;
        private IDeviceNotifier usb_notifier_;
        private UsbDevice       usb_device_;

        private UsbEndpointWriter usb_writer_;
        private UsbEndpointReader usb_reader_;


        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
            devp_ = devp as DevicePropertyImpl;
        }

        protected override void OnConnectStart()
        {
        }

        protected override EventResult OnConnectBusy()
        {
            try {
                if (usb_finder_ == null) {
                    usb_finder_ = new UsbDeviceFinder((int)devp_.CommVendorID.Value, (int)devp_.CommProductID.Value);
                }

                if ((devp_.DeviceEventCapture.Value) && (usb_notifier_ == null)) {
                    usb_notifier_ = DeviceNotifier.OpenDeviceNotifier();
                    usb_notifier_.Enabled = false;
                }

                if (usb_notifier_ != null) {
                    usb_notifier_.Enabled = false;
                    usb_notifier_.OnDeviceNotify -= OnUsbDeviceNotify;
                    usb_notifier_.OnDeviceNotify += OnUsbDeviceNotify;
                    usb_notifier_.Enabled = true;
                }

                if ((devp_.DeviceComm.Value) && (usb_device_ == null) && (usb_finder_ != null)) {
                    usb_device_ = UsbDevice.OpenUsbDevice(usb_finder_);
                }

                if (   ((devp_.DeviceEventCapture.Value) && (usb_notifier_ == null))
                    || ((devp_.DeviceComm.Value) && (usb_device_ == null))
                ) {
                    return (EventResult.Busy);
                }

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

            if (usb_device_ != null) {
                if (usb_device_.IsOpen) {
                    if (usb_device_ is IUsbDevice usb_device_i) {
                        usb_device_i.ReleaseInterface(0);
                    }

                    usb_device_.Close();
                }
                usb_device_ = null;
            }

            if (usb_notifier_ != null) {
                usb_notifier_.Enabled = false;
                usb_notifier_.OnDeviceNotify -= OnUsbDeviceNotify;
                usb_notifier_ = null;
            }

            if (usb_finder_ != null) {
                usb_finder_ = null;
            }

            UsbDevice.Exit();
        }

        protected override PollState OnPoll()
        {
            return (PollState.Idle);
        }

        private void OnUsbDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
        }
    }
}
