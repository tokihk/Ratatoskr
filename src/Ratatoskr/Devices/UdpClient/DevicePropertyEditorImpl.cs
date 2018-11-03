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

namespace Ratatoskr.Devices.UdpClient
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

            InitializeBindMode();
            InitializeLocal();
            InitializeRemote();

            SetBindMode(devp_.BindMode.Value);
            SetLocalAddress(devp_.LocalAddress.Value);
            SetLocalPortNo((ushort)devp_.LocalPortNo.Value);
            SetRemoteAddress(devp_.RemoteAddress.Value);
            SetRemotePortNo((ushort)devp_.RemotePortNo.Value);
        }

        public void InitializeBindMode()
        {
            CBox_BindMode.BeginUpdate();
            {
                CBox_BindMode.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(BindModeType))) {
                    CBox_BindMode.Items.Add(value);
                }
            }
            CBox_BindMode.EndUpdate();
        }

        public void InitializeLocal()
        {
        }

        public void InitializeRemote()
        {
        }

        private void SetBindMode(BindModeType type)
        {
            CBox_BindMode.SelectedItem = type;
        }

        private void SetLocalAddress(string value)
        {
            TBox_LocalAddress.Text = value;
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

        private BindModeType GetBindMode()
        {
            return ((BindModeType)CBox_BindMode.SelectedItem);
        }

        private string GetLocalAddress()
        {
            return (TBox_LocalAddress.Text);
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

        private void UpdateView()
        {
            switch ((BindModeType)CBox_BindMode.SelectedItem) {
                case BindModeType.None:
                {
                    GBox_Local.Enabled = false;
                }
                    break;

                default:
                {
                    GBox_Local.Enabled = true;
                }
                    break;
            }
        }

        public override void Flush()
        {
            devp_.BindMode.Value = GetBindMode();
            devp_.LocalAddress.Value = GetLocalAddress();
            devp_.LocalPortNo.Value = GetLocalPortNo();
            devp_.RemoteAddress.Value = GetRemoteAddress();
            devp_.RemotePortNo.Value = GetRemotePortNo();
        }

        private void CBox_BindMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
