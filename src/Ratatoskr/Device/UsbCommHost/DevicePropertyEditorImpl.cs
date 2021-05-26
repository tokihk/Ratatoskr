using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Device.UsbCommHost
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
        private DevicePropertyImpl devp_;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
        }

        public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

            ChkBox_UsbEventCapture.Checked = devp_.DeviceEventCapture.Value;
            ChkBox_UsbDeviceComm.Checked = devp_.DeviceComm.Value;
            Num_UsbVendorID.Value = devp_.CommVendorID.Value;
            Num_UsbProductID.Value = devp_.CommProductID.Value;
        }

        private void UpdateView()
        {
        }

        public override void Flush()
        {
            devp_.DeviceEventCapture.Value = ChkBox_UsbEventCapture.Checked;
            devp_.DeviceComm.Value = ChkBox_UsbDeviceComm.Checked;
            devp_.CommVendorID.Value = Num_UsbVendorID.Value;
            devp_.CommProductID.Value = Num_UsbProductID.Value;
        }
    }
}
