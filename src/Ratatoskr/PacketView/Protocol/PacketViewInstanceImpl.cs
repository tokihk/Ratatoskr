using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.General.Packet;
using Ratatoskr.ProtocolParser;

namespace Ratatoskr.PacketView.Protocol
{
	internal partial class PacketViewInstanceImpl : PacketViewInstance
	{
        private const ulong PACKET_NO_MIN = 1;
        private const ulong PACKET_NO_MAX = ulong.MaxValue;


		private class ProtocolTypeItem
		{
			public ProtocolTypeItem(ProtocolParserClass protocol_class)
			{
				ProtocolClass = protocol_class;
			}

			public ProtocolParserClass ProtocolClass { get; }

			public override bool Equals(object obj)
			{
				if (obj is Guid obj_id) {
					return (ProtocolClass.ID == obj_id);
				}

				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return (ProtocolClass.Name);
			}
		}

        private class PacketListViewItem
        {
            public PacketListViewItem(ulong no, PacketObject base_packet)
            {
                No = no;
                BasePacket = base_packet;
            }

            public ulong			No			 { get; }
            public PacketObject		BasePacket	 { get; }

			public ProtocolParsePacketInfo	ParsePacketInfo { get; set; } = null;
			public bool						IsParsedPacket  { get; set; } = false;

			public ProtocolParseStreamInfo	ParseStreamInfo { get; set; } = null;
			public bool						IsParsedStream  { get; set; } = false;
        }


		private PacketViewPropertyImpl		prop_;


		private List<PacketListViewItem>	packet_list_temp_;
		private ulong						packet_next_no_ = 0;

		private ProtocolParserInstance		parser_ = null;


		private PacketViewInstanceImpl()
		{
			InitializeComponent();
		}

        public PacketViewInstanceImpl(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
			prop_ = viewp as PacketViewPropertyImpl;

			InitializeComponent();
			InitializeProtocolType();

			BuildPacketListHeader();

			CBox_ProtocolType.SelectedItem = prop_.ProtocolType.Value;
		}

		private void InitializeProtocolType()
		{
			CBox_ProtocolType.BeginUpdate();
			{
				CBox_ProtocolType.Items.Clear();
				foreach (var protocol_class in ProtocolParserManager.Instance.GetClassList()) {
					CBox_ProtocolType.Items.Add(new ProtocolTypeItem(protocol_class));
				}
			}
			CBox_ProtocolType.EndUpdate();
		}

		protected override void OnBackupProperty()
		{
			if (CBox_ProtocolType.SelectedItem is ProtocolTypeItem item) {
				prop_.ProtocolType.Value = item.ProtocolClass.ID;
			}
		}

		private void BuildPacketListHeader()
		{
			LView_PacketList.BeginUpdate();
			{
                /* 先にデータをすべて削除してからヘッダーを削除する */
                LView_PacketList.ItemClear();
                LView_PacketList.Columns.Clear();

                /* メインヘッダー */
                LView_PacketList.Columns.Add(
                    new ColumnHeader()
                    {
                        Text = "No.",
                        Width = 50,
                    }
                );

				/* サブヘッダー */
                foreach (var info in prop_.PacketListColumn.Value) {
                    LView_PacketList.Columns.Add(
                        new ColumnHeader()
                        {
                            Text  = info.Key.ToString(),
                            Width = info.Value,
                        }
                    );
                }
			}
			LView_PacketList.EndUpdate();
		}

		private ListViewItem PacketListObjectToListViewItem(PacketListViewItem obj)
		{
			/* 未解析の場合のみ解析 */
			if ((!obj.IsParsedPacket) && (parser_ != null)) {
				obj.ParsePacketInfo = parser_.ParsePacket(obj.BasePacket);
				obj.IsParsedPacket = true;
			}

			var item = new ListViewItem(obj.No.ToString());

			item.Tag = obj;

			foreach (var info in prop_.PacketListColumn.Value) {
				switch (info.Key) {
					case PacketListColumnID.Alias:
						item.SubItems.Add(obj.BasePacket.Alias);
						break;
					case PacketListColumnID.Datetime_UTC:
						item.SubItems.Add(obj.BasePacket.GetElementText(PacketElementID.DateTime_UTC_Display));
						break;
					case PacketListColumnID.Datetime_Local:
						item.SubItems.Add(obj.BasePacket.GetElementText(PacketElementID.DateTime_Local_Display));
						break;
					case PacketListColumnID.PacketLength:
						item.SubItems.Add(obj.BasePacket.DataLength.ToString());
						break;
					default:
						item.SubItems.Add(GetPacketObjectText(info.Key, obj));
						break;
				}
			}

			return (item);
		}

		private string GetPacketObjectText(PacketListColumnID column_id, PacketListViewItem obj)
		{
			switch (obj.BasePacket.Attribute) {
				case PacketAttribute.Message:	return (GetMessagePacketObjectText(column_id, obj));
				case PacketAttribute.Data:		return (GetDataPacketObjectText(column_id, obj));
				default:						return ("");
			}
		}

		private string GetMessagePacketObjectText(PacketListColumnID column_id, PacketListViewItem obj)
		{
			switch (column_id) {
				case PacketListColumnID.PacketInformation:	return (obj.BasePacket.Message);
				default:									return ("");
			}
		}

		private string GetDataPacketObjectText(PacketListColumnID column_id, PacketListViewItem obj)
		{
			switch (column_id) {
				case PacketListColumnID.Alias:				return (obj.BasePacket.Alias);
				case PacketListColumnID.Datetime_UTC:		return (obj.BasePacket.GetElementText(PacketElementID.DateTime_UTC_Display));
				case PacketListColumnID.Datetime_Local:		return (obj.BasePacket.GetElementText(PacketElementID.DateTime_Local_Display));
				case PacketListColumnID.Source:				return (GetDataPacketObjectText_Source(obj));
				case PacketListColumnID.Destination:		return (GetDataPacketObjectText_Destination(obj));
				case PacketListColumnID.PacketLength:		return (obj.BasePacket.DataLength.ToString());
				case PacketListColumnID.PacketInformation:	return (GetDataPacketObjectText_PacketInformation(obj));
				default:									return ("");
			}
		}

		private string GetDataPacketObjectText_Source(PacketListViewItem item)
		{
			return ("");
		}

		private string GetDataPacketObjectText_Destination(PacketListViewItem item)
		{
			return ("");
		}

		private string GetDataPacketObjectText_PacketInformation(PacketListViewItem item)
		{
			if (item.ParsePacketInfo == null) {
				return ("Unknown");
			}

			return (item.ParsePacketInfo.PacketInfo.Text);
		}

		private void UpdateProtocolType()
		{
			if (CBox_ProtocolType.SelectedItem is ProtocolTypeItem item) {
				UpdateProtocolType(item.ProtocolClass.ID);
			}
		}

		private void UpdateProtocolType(Guid protocol_class_id)
		{
			prop_.ProtocolType.Value = protocol_class_id;

			/* 変更前のパーサーを破棄 */
			if (parser_ != null) {
				parser_.Dispose();
				parser_ = null;
			}

			/* 指定IDのパーサーを生成 */
			parser_ = ProtocolParserManager.Instance.CreateInstance(prop_.ProtocolType.Value);
		}

		private void UpdatePacketDetails()
		{
            var item = LView_PacketList.FocusedItem;
            var obj  = (PacketListViewItem)null;

            if (item != null) {
				obj = item.Tag as PacketListViewItem;
            }

			UpdatePacketDetails(obj);
		}

		private void UpdatePacketDetails(PacketListViewItem obj)
		{
			TreeNode BuildTreeNode(ProtocolParseInfo.InformationObject info)
			{
				var node = new TreeNode(info.Text);

				foreach (var info_sub in info.SubItems) {
					node.Nodes.Add(BuildTreeNode(info_sub));
				}

				return (node);
			}


			TView_PacketDetails.BeginUpdate();
			{
				TView_PacketDetails.Nodes.Clear();

				var node = new TreeNode();

				if ((obj.ParsePacketInfo != null) && (obj.ParsePacketInfo.PacketInfo != null)) {
					foreach (var info in obj.ParsePacketInfo.PacketInfo.SubItems) {
						TView_PacketDetails.Nodes.Add(BuildTreeNode(info));
					}

				} else {
					node.Text = "Unknown";
				}
			}
			TView_PacketDetails.EndUpdate();
		}

		protected override void OnClearPacket()
		{
			LView_PacketList.ItemClear();

            /* リストビューの最大数を再セットアップ */
            LView_PacketList.ItemCountMax = (int)ConfigManager.System.ApplicationCore.Packet_ViewPacketCountLimit.Value;

			packet_next_no_ = PACKET_NO_MIN;
		}

		protected override void OnDrawPacketBegin(bool auto_scroll)
		{
            /* ちらつき防止用の一時バッファ */
			packet_list_temp_ = new List<PacketListViewItem>();

            /* リストビューの描画開始 */
            LView_PacketList.BeginUpdate();
		}

		protected override void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist)
		{
            /* 一時リストをリストビューに追加 */
            LView_PacketList.ItemAddRange(packet_list_temp_);
            packet_list_temp_ = null;

            /* 自動スクロール */
            if ((auto_scroll) && (LView_PacketList.ItemCount > 0)) {
                LView_PacketList.EnsureVisible(LView_PacketList.ItemCount - 1);
            }

            /* リストビューの描画完了 */
            LView_PacketList.EndUpdate();
		}

		protected override void OnDrawPacket(PacketObject packet)
		{
            packet_list_temp_.Add(new PacketListViewItem(packet_next_no_, packet));

            packet_next_no_++;
			if (packet_next_no_ >= PACKET_NO_MAX) {
				packet_next_no_++;
			}
		}

		private void LView_PacketList_ColumnClick(object sender, ColumnClickEventArgs e)
		{

		}

		private void LView_PacketList_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void LView_PacketList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			e.Item = PacketListObjectToListViewItem(LView_PacketList.ItemElementAt(e.ItemIndex) as PacketListViewItem);
		}

		private void LView_PacketList_ItemSelectBusyStatusChanged(object sender, EventArgs e)
		{

		}

		private void LView_PacketList_ItemSelectBusyStatusChanging(object sender, EventArgs e)
		{

		}

		private void LView_PacketList_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdatePacketDetails();
		}

		private void CBox_ProtocolType_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateProtocolType();
		}
	}
}
