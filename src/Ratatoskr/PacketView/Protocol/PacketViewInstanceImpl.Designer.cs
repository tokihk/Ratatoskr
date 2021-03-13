namespace Ratatoskr.PacketView.Protocol
{
	partial class PacketViewInstanceImpl
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.Panel_Menu = new System.Windows.Forms.Panel();
            this.GBox_ProtocolType = new System.Windows.Forms.GroupBox();
            this.CBox_ProtocolType = new System.Windows.Forms.ComboBox();
            this.Split_Main = new System.Windows.Forms.SplitContainer();
            this.LView_PacketList = new Ratatoskr.Forms.ListViewEx();
            this.Split_Sub = new System.Windows.Forms.SplitContainer();
            this.TView_PacketDetails = new System.Windows.Forms.TreeView();
            this.BinEditBox_ElementData = new Ratatoskr.Forms.BinEditBox();
            this.Panel_Menu.SuspendLayout();
            this.GBox_ProtocolType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).BeginInit();
            this.Split_Main.Panel1.SuspendLayout();
            this.Split_Main.Panel2.SuspendLayout();
            this.Split_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Sub)).BeginInit();
            this.Split_Sub.Panel1.SuspendLayout();
            this.Split_Sub.Panel2.SuspendLayout();
            this.Split_Sub.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Menu
            // 
            this.Panel_Menu.Controls.Add(this.GBox_ProtocolType);
            this.Panel_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.Panel_Menu.Name = "Panel_Menu";
            this.Panel_Menu.Size = new System.Drawing.Size(981, 55);
            this.Panel_Menu.TabIndex = 2;
            // 
            // GBox_ProtocolType
            // 
            this.GBox_ProtocolType.Controls.Add(this.CBox_ProtocolType);
            this.GBox_ProtocolType.Location = new System.Drawing.Point(3, 3);
            this.GBox_ProtocolType.Name = "GBox_ProtocolType";
            this.GBox_ProtocolType.Size = new System.Drawing.Size(180, 46);
            this.GBox_ProtocolType.TabIndex = 3;
            this.GBox_ProtocolType.TabStop = false;
            this.GBox_ProtocolType.Text = "Protocol Type";
            // 
            // CBox_ProtocolType
            // 
            this.CBox_ProtocolType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ProtocolType.FormattingEnabled = true;
            this.CBox_ProtocolType.Location = new System.Drawing.Point(3, 15);
            this.CBox_ProtocolType.Name = "CBox_ProtocolType";
            this.CBox_ProtocolType.Size = new System.Drawing.Size(174, 20);
            this.CBox_ProtocolType.TabIndex = 1;
            this.CBox_ProtocolType.SelectedIndexChanged += new System.EventHandler(this.CBox_ProtocolType_SelectedIndexChanged);
            // 
            // Split_Main
            // 
            this.Split_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Split_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Split_Main.Location = new System.Drawing.Point(0, 55);
            this.Split_Main.Name = "Split_Main";
            this.Split_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Split_Main.Panel1
            // 
            this.Split_Main.Panel1.Controls.Add(this.LView_PacketList);
            // 
            // Split_Main.Panel2
            // 
            this.Split_Main.Panel2.Controls.Add(this.Split_Sub);
            this.Split_Main.Size = new System.Drawing.Size(981, 659);
            this.Split_Main.SplitterDistance = 219;
            this.Split_Main.TabIndex = 3;
            // 
            // LView_PacketList
            // 
            this.LView_PacketList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_PacketList.FullRowSelect = true;
            this.LView_PacketList.GridLines = true;
            this.LView_PacketList.HideSelection = false;
            this.LView_PacketList.ItemCountMax = 0;
            this.LView_PacketList.Location = new System.Drawing.Point(0, 0);
            this.LView_PacketList.Name = "LView_PacketList";
            this.LView_PacketList.ReadOnly = false;
            this.LView_PacketList.Size = new System.Drawing.Size(981, 219);
            this.LView_PacketList.TabIndex = 0;
            this.LView_PacketList.UseCompatibleStateImageBehavior = false;
            this.LView_PacketList.View = System.Windows.Forms.View.Details;
            this.LView_PacketList.VirtualMode = true;
            this.LView_PacketList.ItemSelectBusyStatusChanged += new System.EventHandler(this.LView_PacketList_ItemSelectBusyStatusChanged);
            this.LView_PacketList.ItemSelectBusyStatusChanging += new System.EventHandler(this.LView_PacketList_ItemSelectBusyStatusChanging);
            this.LView_PacketList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LView_PacketList_ColumnClick);
            this.LView_PacketList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.LView_PacketList_RetrieveVirtualItem);
            this.LView_PacketList.SelectedIndexChanged += new System.EventHandler(this.LView_PacketList_SelectedIndexChanged);
            this.LView_PacketList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LView_PacketList_MouseClick);
            // 
            // Split_Sub
            // 
            this.Split_Sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Split_Sub.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Split_Sub.IsSplitterFixed = true;
            this.Split_Sub.Location = new System.Drawing.Point(0, 0);
            this.Split_Sub.Name = "Split_Sub";
            // 
            // Split_Sub.Panel1
            // 
            this.Split_Sub.Panel1.Controls.Add(this.TView_PacketDetails);
            // 
            // Split_Sub.Panel2
            // 
            this.Split_Sub.Panel2.Controls.Add(this.BinEditBox_ElementData);
            this.Split_Sub.Panel2MinSize = 320;
            this.Split_Sub.Size = new System.Drawing.Size(981, 436);
            this.Split_Sub.SplitterDistance = 479;
            this.Split_Sub.TabIndex = 0;
            // 
            // TView_PacketDetails
            // 
            this.TView_PacketDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TView_PacketDetails.Location = new System.Drawing.Point(0, 0);
            this.TView_PacketDetails.Name = "TView_PacketDetails";
            this.TView_PacketDetails.Size = new System.Drawing.Size(479, 436);
            this.TView_PacketDetails.TabIndex = 0;
            // 
            // BinEditBox_ElementData
            // 
            this.BinEditBox_ElementData.AllowDrop = true;
            this.BinEditBox_ElementData.BackColor = System.Drawing.SystemColors.Window;
            this.BinEditBox_ElementData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BinEditBox_ElementData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BinEditBox_ElementData.EditEnable = false;
            this.BinEditBox_ElementData.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BinEditBox_ElementData.InsertEnable = false;
            this.BinEditBox_ElementData.Location = new System.Drawing.Point(0, 0);
            this.BinEditBox_ElementData.Name = "BinEditBox_ElementData";
            this.BinEditBox_ElementData.Size = new System.Drawing.Size(498, 436);
            this.BinEditBox_ElementData.TabIndex = 0;
            this.BinEditBox_ElementData.TextViewEnable = false;
            // 
            // PacketViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Split_Main);
            this.Controls.Add(this.Panel_Menu);
            this.Name = "PacketViewInstanceImpl";
            this.Size = new System.Drawing.Size(981, 714);
            this.Panel_Menu.ResumeLayout(false);
            this.GBox_ProtocolType.ResumeLayout(false);
            this.Split_Main.Panel1.ResumeLayout(false);
            this.Split_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).EndInit();
            this.Split_Main.ResumeLayout(false);
            this.Split_Sub.Panel1.ResumeLayout(false);
            this.Split_Sub.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Split_Sub)).EndInit();
            this.Split_Sub.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel_Menu;
		private System.Windows.Forms.GroupBox GBox_ProtocolType;
		private System.Windows.Forms.ComboBox CBox_ProtocolType;
		private System.Windows.Forms.SplitContainer Split_Main;
		private System.Windows.Forms.SplitContainer Split_Sub;
		private Forms.ListViewEx LView_PacketList;
		private System.Windows.Forms.TreeView TView_PacketDetails;
		private Forms.BinEditBox BinEditBox_ElementData;
	}
}
