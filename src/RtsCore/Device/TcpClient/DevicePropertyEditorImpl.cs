using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Resource;

namespace RtsCore.Device.TcpClient
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
        private DevicePropertyImpl devp_;

		private bool initialize_ = true;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
			InitializeAddressFamily();
		}

		public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

			initialize_ = true;
			{
				/* Address Family Type */
				CBox_AddressFamily.SelectedItem = devp_.AddressFamily.Value;

				/* Server - Search Name */
				IPAddrList_Server.HostName = devp_.RemoteAddress.Value;
			
				/* Server - Select IP Address */
				IPAddrList_Server.SelectedIPAddress = devp_.RemoteIpAddress.Value;

				/* Server - Port No. */
				Num_RemotePortNo.Value = (ushort)devp_.RemotePortNo.Value;

				/* Send Buffer Size */
				Num_SendBufferSize.Value = devp_.SendBufferSize.Value;

				/* Recv Buffer Size */
				Num_RecvBufferSize.Value = devp_.RecvBufferSize.Value;

				/* Reuse Address */
				ChkBox_ReuseAddr.Checked = devp_.ReuseAddress.Value;

				/* KeepAlive - OnOff */
				ChkBox_KeepAliveOnOff.Checked = devp_.KeepAliveOnOff.Value;

				/* KeepAlive - Time */
	//			ChkBox_KeepAliveTime.Checked = devp_.KeepAliveTime.Value;
				Num_KeepAliveTime.Value = devp_.KeepAliveTime_Value.Value;

				/* KeepAlive - Interval */
	//			ChkBox_KeepAliveInterval.Checked = devp_.KeepAliveInterval.Value;
	//			Num_KeepAliveInterval.Value = devp_.KeepAliveInterval_Value.Value;

				/* KeepAlive - Retry Count */
	//			ChkBox_KeepAliveRetryCount.Checked = devp_.KeepAliveRetryCount.Value;
	//			Num_KeepAliveRetryCount.Value = devp_.KeepAliveRetryCount_Value.Value;

				/* Unicast - TTL */
				ChkBox_Unicast_TTL.Checked = devp_.TTL_Unicast.Value;
				Num_Unicast_TTL.Value = devp_.TTL_Unicast_Value.Value;
			}
			initialize_ = false;

			UpdateView();
		}

        private void InitializeAddressFamily()
        {
            CBox_AddressFamily.BeginUpdate();
            {
                CBox_AddressFamily.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(AddressFamilyType))) {
                    CBox_AddressFamily.Items.Add(value);
                }
				CBox_AddressFamily.SelectedIndex = 0;
            }
            CBox_AddressFamily.EndUpdate();
        }

		private void UpdateView()
		{
			if (initialize_)return;

			Num_Unicast_TTL.Enabled = ChkBox_Unicast_TTL.Checked;

			GBox_KeepAlive.Enabled = ChkBox_KeepAliveOnOff.Checked;
		}

		private void UpdateAddressFamily()
		{
			var addr_family = AddressFamily.InterNetwork;

			if ((AddressFamilyType)CBox_AddressFamily.SelectedItem == AddressFamilyType.IPv6) {
				addr_family = AddressFamily.InterNetworkV6;
			}

			IPAddrList_Server.AddressFamily = addr_family;
		}

        public override void Flush()
        {
			/* Address Family Type */
			devp_.AddressFamily.Value = (AddressFamilyType)CBox_AddressFamily.SelectedItem;

			/* Server - Search Name */
			devp_.RemoteAddress.Value = IPAddrList_Server.HostName;
			
			/* Server - Select IP Address */
			devp_.RemoteIpAddress.Value = IPAddrList_Server.SelectedIPAddress;

			/* Server - Port No. */
			devp_.RemotePortNo.Value = (ushort)Num_RemotePortNo.Value;

			/* Send Buffer Size */
			devp_.SendBufferSize.Value = Num_SendBufferSize.Value;

			/* Recv Buffer Size */
			devp_.RecvBufferSize.Value = Num_RecvBufferSize.Value;

			/* Reuse Address */
			devp_.ReuseAddress.Value = ChkBox_ReuseAddr.Checked;

			/* KeepAlive - OnOff */
			devp_.KeepAliveOnOff.Value = ChkBox_KeepAliveOnOff.Checked;

			/* KeepAlive - Time */
//			ChkBox_KeepAliveTime.Checked = devp_.KeepAliveTime.Value;
			devp_.KeepAliveTime_Value.Value = Num_KeepAliveTime.Value;

			/* KeepAlive - Interval */
//			ChkBox_KeepAliveInterval.Checked = devp_.KeepAliveInterval.Value;
//			Num_KeepAliveInterval.Value = devp_.KeepAliveInterval_Value.Value;

			/* KeepAlive - Retry Count */
//			ChkBox_KeepAliveRetryCount.Checked = devp_.KeepAliveRetryCount.Value;
//			Num_KeepAliveRetryCount.Value = devp_.KeepAliveRetryCount_Value.Value;

			/* Unicast - TTL */
			devp_.TTL_Unicast.Value = ChkBox_Unicast_TTL.Checked;
			devp_.TTL_Unicast_Value.Value = Num_Unicast_TTL.Value;
		}

		private void CBox_AddressFamily_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateAddressFamily();
		}

		private void ChkBox_KeepAliveOnOff_CheckedChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void ChkBox_Unicast_TTL_CheckedChanged(object sender, EventArgs e)
		{
			UpdateView();
		}
	}
}
