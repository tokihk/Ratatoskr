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
using SharpPcap.LibPcap;

namespace Ratatoskr.Devices.Ethernet
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
        private class PcapDeviceInfo
        {
            public LibPcapLiveDevice Device { get; }


            public PcapDeviceInfo(LibPcapLiveDevice dev)
            {
                Device = dev;
            }

            public override string ToString()
            {
                var str = new StringBuilder();

                if (Device.Description != null) {
                    str.Append(Device.Description);
                }

                if (Device.Interface.FriendlyName != null) {
                    str.AppendFormat("({0})", Device.Interface.FriendlyName);
                }

                return (str.ToString());
            }
        }


        private DevicePropertyImpl devp_;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
        }

        public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

            InitializeIfceList();
            InitializeViewSourceType();
            InitializeViewDestinationType();
            InitializeViewDataType();

            SelectInterface(devp_.Interface.Value);
            SetSendEnable(devp_.SendEnable.Value);
            SetRecvFilter(devp_.Filter.Value);
            SetViewSourceType(devp_.ViewSourceType.Value);
            SetViewDestinationType(devp_.ViewDestinationType.Value);
            SetViewDataType(devp_.ViewDataType.Value);
        }

        private void InitializeIfceList()
        {
            CBox_IfceList.BeginUpdate();
            {
                foreach (var ifce in LibPcapLiveDeviceList.Instance) {
                    CBox_IfceList.Items.Add(new PcapDeviceInfo(ifce));
                }
            }
            CBox_IfceList.EndUpdate();
        }

        private void InitializeViewSourceType()
        {
            CBox_ViewSourceType.BeginUpdate();
            {
                foreach (SourceInfoType type in Enum.GetValues(typeof(SourceInfoType))) {
                    CBox_ViewSourceType.Items.Add(type);
                }
            }
            CBox_ViewSourceType.EndUpdate();
        }

        private void InitializeViewDestinationType()
        {
            CBox_ViewDestinationType.BeginUpdate();
            {
                foreach (DestinationInfoType type in Enum.GetValues(typeof(DestinationInfoType))) {
                    CBox_ViewDestinationType.Items.Add(type);
                }
            }
            CBox_ViewDestinationType.EndUpdate();
        }

        private void InitializeViewDataType()
        {
            CBox_ViewDataType.BeginUpdate();
            {
                foreach (DataContentsType type in Enum.GetValues(typeof(DataContentsType))) {
                    CBox_ViewDataType.Items.Add(type);
                }
            }
            CBox_ViewDataType.EndUpdate();
        }

        private void SelectInterface(string value)
        {
            /* 選択項目を初期化 */
            if ((CBox_IfceList.SelectedIndex < 0) && (CBox_IfceList.Items.Count > 0)) {
                CBox_IfceList.SelectedIndex = 0;
            }

            /* デバイス名からインデックス取得 */
            foreach (PcapDeviceInfo info in CBox_IfceList.Items) {
                if (info.Device.Name == value) {
                    CBox_IfceList.SelectedItem = info;
                }
            }

            UpdateInterfaceName();
        }

        private void SetSendEnable(bool value)
        {
        }

        private void SetRecvFilter(string pattern)
        {
            TBox_RecvFilter.Text = pattern;
        }

        private void SetViewSourceType(SourceInfoType type)
        {
            CBox_ViewSourceType.SelectedItem = type;
            if (CBox_ViewSourceType.SelectedIndex < 0) {
                CBox_ViewSourceType.SelectedIndex = 0;
            }
        }

        private void SetViewDestinationType(DestinationInfoType type)
        {
            CBox_ViewDestinationType.SelectedItem = type;
            if (CBox_ViewDestinationType.SelectedIndex < 0) {
                CBox_ViewDestinationType.SelectedIndex = 0;
            }
        }

        private void SetViewDataType(DataContentsType type)
        {
            CBox_ViewDataType.SelectedItem = type;
            if (CBox_ViewDataType.SelectedIndex < 0) {
                CBox_ViewDataType.SelectedIndex = 0;
            }
        }

        private void UpdateInterfaceName()
        {
            var info = CBox_IfceList.SelectedItem as PcapDeviceInfo;

            Label_IfceName.Text = (info != null) ? (info.Device.Name) : ("");
        }

        public override void Flush()
        {
            devp_.Interface.Value = (CBox_IfceList.SelectedItem as PcapDeviceInfo).Device.Name;
            devp_.Filter.Value = TBox_RecvFilter.Text;
            devp_.ViewSourceType.Value = (SourceInfoType)CBox_ViewSourceType.SelectedItem;
            devp_.ViewDestinationType.Value = (DestinationInfoType)CBox_ViewDestinationType.SelectedItem;
            devp_.ViewDataType.Value = (DataContentsType)CBox_ViewDataType.SelectedItem;
        }

        private void CBox_IfceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterfaceName();
        }
    }
}
