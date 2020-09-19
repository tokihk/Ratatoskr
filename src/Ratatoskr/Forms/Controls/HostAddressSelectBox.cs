using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Ratatoskr.Forms.Controls
{
	public partial class HostAddressSelectBox : UserControl
	{
		public HostAddressSelectBox()
		{
			InitializeComponent();
		}

		public string HostName
		{
			get
			{
				return (TBox_SearchAddress.Text);
			}

			set
			{
				TBox_SearchAddress.Text = value;
				DnsAddrList_Select.HostName = value;
			}
		}

		public AddressFamily AddressFamily
		{
			get
			{
				return (DnsAddrList_Select.AddressFamily);
			}

			set
			{
				DnsAddrList_Select.AddressFamily = value;
			}
		}

		public IPAddress SelectedIPAddress
		{
			get
			{
				return (DnsAddrList_Select.SelectedIPAddress);
			}

			set
			{
				DnsAddrList_Select.SelectedIPAddress = value;
			}
		}

		private void TBox_SearchAddress_TextChanged(object sender, EventArgs e)
		{
			DnsAddrList_Select.HostName = TBox_SearchAddress.Text;
		}
	}
}
