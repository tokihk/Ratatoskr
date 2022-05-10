#define __SHARPPCAP__

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
using Ratatoskr.General.Pcap;

#if __SHARPPCAP__
using SharpPcap.LibPcap;
#elif __PCAPDOTNET__
using PcapDotNet.Core;
using PcapDotNet.Packets;
#endif

namespace Ratatoskr.Device.Ethernet
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
        private class CaptureDeviceInfo
        {
#if __SHARPPCAP__
            public LibPcapLiveDevice Device { get; }
#elif __PCAPDOTNET__
            public LivePacketDevice Device { get; }
#endif

#if __SHARPPCAP__
            public CaptureDeviceInfo(LibPcapLiveDevice dev)
#elif __PCAPDOTNET__
            public CaptureDeviceInfo(LivePacketDevice dev)
#endif
            {
                Device = dev;
            }

            public override string ToString()
            {
                var str = new StringBuilder();

#if __SHARPPCAP__
                if (Device.Description != null) {
                    str.Append(Device.Description);
                }

                if (Device.Interface.FriendlyName != null) {
                    str.AppendFormat("({0})", Device.Interface.FriendlyName);
                }
#elif __PCAPDOTNET__
                if (Device.Description != null) {
                    str.Append(Device.Description);
                }

                if (Device.Name != null) {
                    str.AppendFormat("({0})", Device.Name);
                }
#endif

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
            InitializePacketInformationType();
            InitializePacketSourceType();
            InitializePacketDestinationType();
            InitializePacketDataType();
			InitializeSelectProtocolItemList();

            SelectInterface(devp_.Interface.Value);
			TBox_RecvFilter.Text = devp_.Filter.Value;
			CBox_PacketInfoType.SelectedItem = devp_.PacketInfo.Value;
			CBox_PacketSourceType.SelectedItem = devp_.PacketSource.Value;
			CBox_PacketDestinationType.SelectedItem = devp_.PacketDestination.Value;
			CBox_PacketDataType.SelectedItem = devp_.PacketData.Value;
        }

        private void InitializeIfceList()
        {
            CBox_IfceList.BeginUpdate();
            {
#if __SHARPPCAP__
                foreach (var dev in LibPcapLiveDeviceList.Instance) {
#elif __PCAPDOTNET__
                foreach (var dev in LivePacketDevice.AllLocalMachine) {
#endif
                    CBox_IfceList.Items.Add(new CaptureDeviceInfo(dev));
                }
            }
            CBox_IfceList.EndUpdate();
        }

        private void InitializePacketInformationType()
        {
            CBox_PacketInfoType.BeginUpdate();
            {
                foreach (PcapPacketInfoType type in Enum.GetValues(typeof(PcapPacketInfoType))) {
                    CBox_PacketInfoType.Items.Add(type);
                }
				CBox_PacketInfoType.SelectedIndex = 0;
            }
            CBox_PacketInfoType.EndUpdate();
        }

        private void InitializePacketSourceType()
        {
            CBox_PacketSourceType.BeginUpdate();
            {
                foreach (PcapPacketSourceType type in Enum.GetValues(typeof(PcapPacketSourceType))) {
                    CBox_PacketSourceType.Items.Add(type);
                }
				CBox_PacketSourceType.SelectedIndex = 0;
            }
            CBox_PacketSourceType.EndUpdate();
        }

        private void InitializePacketDestinationType()
        {
            CBox_PacketDestinationType.BeginUpdate();
            {
                foreach (PcapPacketDestinationType type in Enum.GetValues(typeof(PcapPacketDestinationType))) {
                    CBox_PacketDestinationType.Items.Add(type);
                }
				CBox_PacketDestinationType.SelectedIndex = 0;
            }
            CBox_PacketDestinationType.EndUpdate();
        }

        private void InitializePacketDataType()
        {
            CBox_PacketDataType.BeginUpdate();
            {
                foreach (PcapPacketDataType type in Enum.GetValues(typeof(PcapPacketDataType))) {
                    CBox_PacketDataType.Items.Add(type);
                }
				CBox_PacketDataType.SelectedIndex = 0;
            }
            CBox_PacketDataType.EndUpdate();
        }

		private void InitializeSelectProtocolItemList()
		{
			LBox_ProtocolItemList.BeginUpdate();
			{
				LBox_ProtocolItemList.Items.Clear();

			}
			LBox_ProtocolItemList.EndUpdate();
		}

        private void SelectInterface(string value)
        {
            /* 選択項目を初期化 */
            if ((CBox_IfceList.SelectedIndex < 0) && (CBox_IfceList.Items.Count > 0)) {
                CBox_IfceList.SelectedIndex = 0;
            }

            /* デバイス名からインデックス取得 */
            foreach (CaptureDeviceInfo info in CBox_IfceList.Items) {
                if (info.Device.Name == value) {
                    CBox_IfceList.SelectedItem = info;
                }
            }

            UpdateInterfaceName();
        }

        private void UpdateInterfaceName()
        {
            var info = CBox_IfceList.SelectedItem as CaptureDeviceInfo;

            Label_IfceName.Text = (info != null) ? (info.Device.Name) : ("");
        }

        public override void Flush()
        {
            if (CBox_IfceList.SelectedItem is CaptureDeviceInfo cdi) {
                devp_.Interface.Value = cdi.Device.Name;
            } else {
                devp_.Interface.Value = "";
            }

            devp_.Filter.Value = TBox_RecvFilter.Text;

            devp_.PacketInfo.Value = (PcapPacketInfoType)CBox_PacketInfoType.SelectedItem;
            devp_.PacketSource.Value = (PcapPacketSourceType)CBox_PacketSourceType.SelectedItem;
            devp_.PacketDestination.Value = (PcapPacketDestinationType)CBox_PacketDestinationType.SelectedItem;
            devp_.PacketData.Value = (PcapPacketDataType)CBox_PacketDataType.SelectedItem;
        }

        private void CBox_IfceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInterfaceName();
        }

		private void LBox_FrameProtocolList_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void Btn_AddProtocolItem_Click(object sender, EventArgs e)
		{

		}

		private void Btn_RemoveProtocolItem_Click(object sender, EventArgs e)
		{

		}
	}
}
