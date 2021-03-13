using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Ratatoskr.Forms
{
	public partial class DnsAddressSelectBox : UserControl
	{
		private class IPAddressListItem
		{
			public IPAddressListItem(IPAddress ipaddr)
			{
				Object = ipaddr;
			}

			public IPAddress Object { get; }

			public override bool Equals(object obj)
			{
				if (obj is IPAddress ipaddr) {
					return (Object.Equals(ipaddr));
				}

				return (base.Equals(obj));
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


		private const int		IP_ADDRESS_UPDATE_DELAY = 400;
		private readonly string	IP_ADDRESS_UPDATE_STATUS_TEXT = "Now updating";

		private Timer	ipaddr_update_timer_ = new Timer();
		private bool	ipaddr_update_req_   = false;

		private bool	text_blink_flag_ = false;

		private AddressFamily addr_family_		= AddressFamily.InterNetwork;
		private string[]	  hostnames_		= new string[] { };
		private IPAddress	  select_ipaddr_	= IPAddress.None;

		private bool		 get_hostaddr_busy_ = false;
		private int          get_hostaddr_index_ = 0;
		private IAsyncResult get_hostaddr_task_ar_ = null;


		public DnsAddressSelectBox()
		{
			InitializeComponent();

			ipaddr_update_timer_.Interval = IP_ADDRESS_UPDATE_DELAY;
			ipaddr_update_timer_.Tick += ipaddr_update_timer_on_tick;
		}

		public string HostName
		{
			get
			{
				return ((hostnames_.Length > 0) ? (hostnames_[0]) : (""));
			}

			set
			{
				HostNames = new string[] { value };
			}
		}

		public string[] HostNames
		{
			get
			{
				return (hostnames_);
			}

			set
			{
				hostnames_ = (value != null) ? (value) : (new string[] { });

				UpdateIpAddressListRequest();
			}
		}

		public AddressFamily AddressFamily
		{
			get
			{
				return (addr_family_);
			}

			set
			{
				if (addr_family_ != value) {
					addr_family_ = value;

					UpdateIpAddressListRequest();
				}
			}
		}

		public IPAddress SelectedIPAddress
		{
			get
			{
				/* 取得前にキャッシュを更新 */
				if (RBtnList_IpAddress.SelectedItem is IPAddressListItem item) {
					select_ipaddr_ = item.Object;
				} else {
					select_ipaddr_ = IPAddress.None;
				}

				return (select_ipaddr_);
			}

			set
			{
				select_ipaddr_ = value;

				if ((!ipaddr_update_req_) && (!get_hostaddr_busy_)) {
					RBtnList_IpAddress.SelectedItem = select_ipaddr_;
				}
			}
		}

		private void UpdateView()
		{
			if ((ipaddr_update_req_) || (get_hostaddr_busy_)) {
				/* アドレス収集中のみステータスラベルを表示 */
				Label_Status.Visible = true;
				Label_Status.Text = (text_blink_flag_) ? ("") : (IP_ADDRESS_UPDATE_STATUS_TEXT);

				/* アドレス収集中はアドレスリストは非表示 */
				RBtnList_IpAddress.Visible = false;

			} else {
				Label_Status.Visible = false;

				RBtnList_IpAddress.BackColor = (RBtnList_IpAddress.SelectedItemIndex != -1)
											 ? (Ratatoskr.Resource.AppColors.Ok)
											 : (Ratatoskr.Resource.AppColors.Ng);
				RBtnList_IpAddress.Visible = true;
			}
		}

		private void UpdateIpAddressListRequest()
		{
			ipaddr_update_req_ = true;

			if (ipaddr_update_timer_.Enabled) {
				ipaddr_update_timer_.Stop();
			}
			ipaddr_update_timer_.Start();

			text_blink_flag_ = false;

			/* 即座にIPアドレスリストをクリアする */
			RBtnList_IpAddress.ClearItems();

			UpdateView();
		}

		private void UpdateIpAddressList()
		{
			if (get_hostaddr_busy_) {
				return;
			}

			ipaddr_update_req_  = false;

			RBtnList_IpAddress.ClearItems();

			get_hostaddr_index_ = 0;

			GetHostAddressesSetup();

			UpdateView();
		}

		private void GetHostAddressesSetup()
		{
			if ((!ipaddr_update_req_) && (hostnames_ != null) && (get_hostaddr_index_ < hostnames_.Length)) {
				get_hostaddr_busy_ = true;

				get_hostaddr_task_ar_ = Dns.BeginGetHostAddresses(hostnames_[get_hostaddr_index_], GetHostAddressesTaskComplete, null);
				get_hostaddr_index_++;

			} else {
				GetHostAddressesComplete();
			}
		}

		private delegate void GetHostAddressesCompleteDelegate();
		private void GetHostAddressesComplete()
		{
			if (InvokeRequired) {
				Invoke((MethodInvoker)GetHostAddressesComplete);
				return;
			}

			/* 全ホスト名の変換が完了 */
			get_hostaddr_busy_ = false;

			/* 新しい更新要求が存在しないときのみ更新 */
			if (!ipaddr_update_req_) {
				/* 最後に選択していたデータを選択する */
				RBtnList_IpAddress.SelectedItem = select_ipaddr_;

				/* どれも選択していないときは最初のデータを選択する */
				if (RBtnList_IpAddress.SelectedItemIndex < 0) {
					RBtnList_IpAddress.SelectedItemIndex = 0;
				}

				UpdateView();
			}
		}

		private void GetHostAddressesTaskComplete(IAsyncResult ar)
		{
			try {
				UpdateIpAddressList(Dns.EndGetHostAddresses(ar));
			} catch {
			}

			GetHostAddressesSetup();
		}

		private delegate void UpdateIpAddressListDelegate(IEnumerable<IPAddress> ipaddr_list);
		private void UpdateIpAddressList(IEnumerable<IPAddress> ipaddr_list)
		{
			if (InvokeRequired) {
				Invoke(new UpdateIpAddressListDelegate(UpdateIpAddressList), ipaddr_list);
				return;
			}

			/* IPアドレスリストを更新 */
			foreach (var ipaddr in ipaddr_list) {
				if (ipaddr.AddressFamily == addr_family_) {
					RBtnList_IpAddress.AddItem(new IPAddressListItem(ipaddr));
				}
			}
		}

		private void ipaddr_update_timer_on_tick(object sender, EventArgs e)
		{
			if (ipaddr_update_req_) {
				UpdateIpAddressList();
			}

			UpdateView();

			text_blink_flag_ = !text_blink_flag_;

			if ((!ipaddr_update_req_) && (!get_hostaddr_busy_)) {
				ipaddr_update_timer_.Stop();
			}
		}

		private void RBtnList_IpAddress_RadioButtonClick(object sender, EventArgs e)
		{
			if (sender is RadioButtonListBox control) {
				if (control.SelectedItem is IPAddressListItem item) {
					select_ipaddr_ = item.Object;
				}
				UpdateView();
			}
		}
	}
}
