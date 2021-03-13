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
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Net.Sockets;

namespace RtsCore.Device.UdpClient
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
		private class MulticastGroupAddressItem
		{
			public MulticastGroupAddressItem(IPAddress ipaddr)
			{
				Object = ipaddr;
			}

			public IPAddress Object { get; }

			public override bool Equals(object obj)
			{
				if (obj is IPAddress ipaddr) {
					return (Object.Equals(ipaddr));
				}

				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return (Object.ToString());
			}
		}

		private class MulticastInterfaceListItem
		{
			public MulticastInterfaceListItem(NetworkInterface nic)
			{
				Object = nic;
			}

			public NetworkInterface Object { get; }

			public override bool Equals(object obj)
			{
				if (obj is NetworkInterface nic) {
					return (Object.Equals(nic));
				} else if (obj is string id) {
					return (Object.Id == id);
				}

				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return (string.Format("{0} ({1})", Object.Name, Object.Id));
			}
		}


        private DevicePropertyImpl devp_;

		private bool initialize_ = true;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
			InitializeAddressFamily();
			InitializeLocalBindMode();
			InitializeRemoteAddressType();
			InitializeLocalBindAddressList();
			InitializeMulticastInterfaceList();
        }

        public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

			initialize_ = true;
			{
				/* Address Family Type */
				CBox_AddressFamily.SelectedItem = devp_.AddressFamily.Value;

				/* Local - Bind Mode */
				CBox_LocalBindMode.SelectedItem = devp_.LocalBindMode.Value;

				/* Local - IP Address */
				DnsAddrList_Local.SelectedIPAddress = devp_.LocalIpAddress.Value;

				/* Local - Port No. */
				Num_LocalPortNo.Value = devp_.LocalPortNo.Value;

				/* Remote - Address Type */
				CBox_RemoteAddressType.SelectedItem = devp_.RemoteAddressType.Value;

				/* Remote - Search Name */
				IPAddrList_Remote.HostName = devp_.RemoteAddress.Value.Trim();

				/* Remote - Select IP Address */
				IPAddrList_Remote.SelectedIPAddress = devp_.RemoteIpAddress.Value;

				/* Remote - Port No. */
				Num_RemotePortNo.Value = devp_.RemotePortNo.Value;

				/* Unicast - TTL */
				ChkBox_Unicast_TTL.Checked = devp_.Unicast_TTL.Value;
				Num_Unicast_TTL.Value = devp_.Unicast_TTL_Value.Value;

				/* Multicast - TTL */
				ChkBox_Multicast_TTL.Checked = devp_.Multicast_TTL.Value;
				Num_Multicast_TTL.Value = devp_.Multicast_TTL_Value.Value;

				/* Multiast - Loopback */
				ChkBox_Multicast_Loopback.Checked = devp_.Multicast_Loopback.Value;

				/* Multicast - Group Address */
				ChkBox_MulticastGroupAddress.Checked = devp_.Multicast_GroupAddress.Value;
				LBox_MulticastGroupAddress.BeginUpdate();
				{
					LBox_MulticastGroupAddress.Items.Clear();
					foreach (var address in devp_.Multicast_GroupAddressList.Value) {
						LBox_MulticastGroupAddress.Items.Add(address.Trim());
					}
				}
				LBox_MulticastGroupAddress.EndUpdate();

				/* Multicast - Interface */
				ChkBox_MulticastInterface.Checked = devp_.Multicast_Interface.Value;
				RBtnList_MulticastInterface.SelectedItem = devp_.Multicast_Interface_Value.Value;
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

        private void InitializeLocalBindMode()
        {
            CBox_LocalBindMode.BeginUpdate();
            {
                CBox_LocalBindMode.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(BindModeType))) {
                    CBox_LocalBindMode.Items.Add(value);
                }
				CBox_LocalBindMode.SelectedIndex = 0;
            }
            CBox_LocalBindMode.EndUpdate();
        }

		private void InitializeRemoteAddressType()
		{
			CBox_RemoteAddressType.BeginUpdate();
			{
				CBox_RemoteAddressType.Items.Clear();
				foreach (var value in Enum.GetValues(typeof(AddressType))) {
					CBox_RemoteAddressType.Items.Add(value);
				}
				CBox_RemoteAddressType.SelectedIndex = 0;
			}
			CBox_RemoteAddressType.EndUpdate();
		}

		private void InitializeLocalBindAddressList()
        {
			DnsAddrList_Local.HostNames = new string[] { Dns.GetHostName(), "localhost" };
        }

        private void InitializeMulticastInterfaceList()
        {
			RBtnList_MulticastInterface.ClearItems();

			/* 全てのNICを取得 */
			var nics = NetworkInterface.GetAllNetworkInterfaces();

			if ((nics == null) || (nics.Length == 0)) {
				return;
			}

			/* 名前順で表示する */
			foreach (var nic in nics.OrderBy(nic => nic.Name)) {
				RBtnList_MulticastInterface.AddItem(new MulticastInterfaceListItem(nic));
			}
        }

        private void UpdateView()
        {
			if (initialize_)return;

			var bind_mode = (BindModeType)CBox_LocalBindMode.SelectedItem;

			GBox_LocalIPAddress.Enabled = (bind_mode == BindModeType.SelectAddress);
			GBox_LocalPortNo.Enabled  = (bind_mode != BindModeType.NotBind);

			Num_Multicast_TTL.Enabled = ChkBox_Multicast_TTL.Checked;

			GBox_MulticastGroupAddress.Enabled = ChkBox_MulticastGroupAddress.Checked;

			GBox_MulticastInterface.Enabled = ChkBox_MulticastInterface.Checked;
        }

		private void UpdateAddressFamily()
		{
			var addr_family = AddressFamily.InterNetwork;

			if ((AddressFamilyType)CBox_AddressFamily.SelectedItem == AddressFamilyType.IPv6) {
				addr_family = AddressFamily.InterNetworkV6;
			}

			DnsAddrList_Local.AddressFamily = addr_family;
			IPAddrList_Remote.AddressFamily = addr_family;
		}

        public override void Flush()
        {
			/* Address Family Type */
			devp_.AddressFamily.Value = (AddressFamilyType)CBox_AddressFamily.SelectedItem;

			/* Local - Bind Mode */
			devp_.LocalBindMode.Value = (BindModeType)CBox_LocalBindMode.SelectedItem;

			/* Local - IP Address */
			devp_.LocalIpAddress.Value = DnsAddrList_Local.SelectedIPAddress;

			/* Local - Port No. */
			devp_.LocalPortNo.Value = Num_LocalPortNo.Value;

			/* Remote - Address Type */
			devp_.RemoteAddressType.Value = (AddressType)CBox_RemoteAddressType.SelectedItem;

			/* Remote - Search Name */
			devp_.RemoteAddress.Value = IPAddrList_Remote.HostName;

			/* Remote - Select IP Address */
			devp_.RemoteIpAddress.Value = IPAddrList_Remote.SelectedIPAddress;

			/* Remote - Port No. */
			devp_.RemotePortNo.Value = Num_RemotePortNo.Value;

			/* Unicast - TTL */
			devp_.Unicast_TTL.Value = ChkBox_Unicast_TTL.Checked;
			devp_.Unicast_TTL_Value.Value = Num_Unicast_TTL.Value;

			/* Multicast - TTL */
			devp_.Multicast_TTL.Value = ChkBox_Multicast_TTL.Checked;
			devp_.Multicast_TTL_Value.Value = Num_Multicast_TTL.Value;

			/* Multiast - Loopback */
			devp_.Multicast_Loopback.Value = ChkBox_Multicast_Loopback.Checked;

			/* Multicast - Group Address */
			devp_.Multicast_GroupAddress.Value = ChkBox_MulticastGroupAddress.Checked;
			devp_.Multicast_GroupAddressList.Value.Clear();
			foreach (MulticastGroupAddressItem item in LBox_MulticastGroupAddress.Items) {
				devp_.Multicast_GroupAddressList.Value.Add(item.ToString());
			}

			/* Multicast - Interface */
			devp_.Multicast_Interface.Value = ChkBox_MulticastInterface.Checked;

			var mif_item = RBtnList_MulticastInterface.SelectedItem as MulticastInterfaceListItem;

			devp_.Multicast_Interface_Value.Value = (mif_item != null) ? (mif_item.Object.Id) : ("");
        }

		private void CBox_AddressFamily_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateAddressFamily();
		}

		private void ChkBox_Multicast_TTL_CheckedChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void CBox_BindMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void ChkBox_MulticastInterface_CheckedChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void ChkBox_MulticastGroupAddress_CheckedChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void Btn_MulticastGroupAddress_Add_Click(object sender, EventArgs e)
		{
			IPAddress ipaddr;

			if (!IPAddress.TryParse(TBox_MulticastGroupAddress.Text, out ipaddr)) {
				return;
			}

			/* 既に同じ設定が存在する場合は追加しない */
			if (LBox_MulticastGroupAddress.Items.Contains(ipaddr)) {
				return;
			}

			/* IPv6の場合はMulticastアドレスでなければ追加しない */
			if (ipaddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) {
				if (!ipaddr.IsIPv6Multicast) {
					return;
				}
			}

			LBox_MulticastGroupAddress.Items.Add(ipaddr);
		}

		private void Btn_MulticastGroupAddress_Remove_Click(object sender, EventArgs e)
		{
			var selected_index = LBox_MulticastGroupAddress.SelectedIndex;

			/* 選択されていなければ無視 */
			if (selected_index < 0) {
				return;
			}

			LBox_MulticastGroupAddress.Items.RemoveAt(selected_index);
		}
	}
}
