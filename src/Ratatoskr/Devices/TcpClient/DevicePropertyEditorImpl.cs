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
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.TcpClient
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

            SetLocalPortNo((ushort)devp_.LocalPortNo.Value);
            SetRemoteAddress(devp_.RemoteAddress.Value);
            SetRemotePortNo((ushort)devp_.RemotePortNo.Value);
        }

        private void SetLocalPortNo(ushort value)
        {
            Num_LocalPortNo.Value = value;
        }

        private void SetRemoteAddress(string value)
        {
            TBox_RemoteAddress.Text = value;
        }

        private void SetRemotePortNo(ushort value)
        {
            Num_RemotePortNo.Value = value;
        }

        private ushort GetLocalPortNo()
        {
            return ((ushort)Num_LocalPortNo.Value);
        }

        private string GetRemoteAddress()
        {
            return (TBox_RemoteAddress.Text);
        }

        private ushort GetRemotePortNo()
        {
            return ((ushort)Num_RemotePortNo.Value);
        }

        public override void Flush()
        {
            devp_.LocalPortNo.Value = GetLocalPortNo();
            devp_.RemoteAddress.Value = GetRemoteAddress();
            devp_.RemotePortNo.Value = GetRemotePortNo();
        }
    }
}
