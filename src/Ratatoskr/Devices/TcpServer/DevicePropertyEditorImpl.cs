using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.TcpServer
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

            SetConnectMax((uint)devp_.Capacity.Value);
            SetLocalPortNo((ushort)devp_.LocalPortNo.Value);
        }

        private void SetConnectMax(uint value)
        {
            Num_ConnextMax.Value = value;
        }

        private void SetLocalPortNo(ushort value)
        {
            Num_LocalPortNo.Value = value;
        }

        private uint GetConnectMax()
        {
            return ((uint)Num_ConnextMax.Value);
        }

        private ushort GetLocalPortNo()
        {
            return ((ushort)Num_LocalPortNo.Value);
        }

        private void UpdateView()
        {
        }

        public override void Flush()
        {
            devp_.Capacity.Value = GetConnectMax();
            devp_.LocalPortNo.Value = GetLocalPortNo();
        }
    }
}
