using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.DateFormatter;
using Ratatoskr.Device;
using Ratatoskr.Native.Windows;

namespace Ratatoskr.Device.UsbCapture
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private const int RECV_PACKET_MAX = 1000;

        private DevicePropertyImpl	prop_;

		private USBPcapCMD			usbpcapcmd_ = null;

        private byte[]              recv_buffer_;
		private int					recv_size_;


        public DeviceInstanceImpl(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
            prop_ = devp as DevicePropertyImpl;
        }

        protected override void OnConnectStart()
        {
        }

        protected override EventResult OnConnectBusy()
        {
			if (usbpcapcmd_ == null) {
				usbpcapcmd_ = new USBPcapCMD(prop_.DeviceName.Value);
			}

			/* プロセスが開けるまで待機 */
			if (!usbpcapcmd_.IsOpened) {
				return (EventResult.Busy);
			}

			/* プロセスが終了もしくはエラーが発生していたらやり直し */
			if ((usbpcapcmd_.IsExited) || (usbpcapcmd_.IsError)) {
				usbpcapcmd_.Dispose();
				usbpcapcmd_ = null;
				return (EventResult.Busy);
			}

            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
			if (usbpcapcmd_ != null) {
				usbpcapcmd_.Dispose();
				usbpcapcmd_ = null;
			}
        }

        protected override EventResult OnDisconnectBusy()
        {
            return (EventResult.Success);
        }

        protected override void OnDisconnected()
        {
        }

        protected override PollState OnPoll()
        {
			foreach (var packet in usbpcapcmd_.GetPackets()) {
				NotifyRecvComplete("", "", "", packet.RawData);
			}

            return (PollState.Idle);
        }
    }
}
