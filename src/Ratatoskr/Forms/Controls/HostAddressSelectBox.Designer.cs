namespace Ratatoskr.Forms.Controls
{
	partial class HostAddressSelectBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostAddressSelectBox));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TBox_SearchAddress = new System.Windows.Forms.TextBox();
            this.GBox_SelectIpAddress = new System.Windows.Forms.GroupBox();
            this.DnsAddrList_Select = new Ratatoskr.Forms.Controls.DnsAddressSelectBox();
            this.groupBox1.SuspendLayout();
            this.GBox_SelectIpAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TBox_SearchAddress);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Host name or IP Address";
            // 
            // TBox_SearchAddress
            // 
            this.TBox_SearchAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_SearchAddress.Location = new System.Drawing.Point(3, 15);
            this.TBox_SearchAddress.Name = "TBox_SearchAddress";
            this.TBox_SearchAddress.Size = new System.Drawing.Size(315, 19);
            this.TBox_SearchAddress.TabIndex = 1;
            this.TBox_SearchAddress.TextChanged += new System.EventHandler(this.TBox_SearchAddress_TextChanged);
            // 
            // GBox_SelectIpAddress
            // 
            this.GBox_SelectIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_SelectIpAddress.Controls.Add(this.DnsAddrList_Select);
            this.GBox_SelectIpAddress.Location = new System.Drawing.Point(4, 57);
            this.GBox_SelectIpAddress.Name = "GBox_SelectIpAddress";
            this.GBox_SelectIpAddress.Size = new System.Drawing.Size(321, 97);
            this.GBox_SelectIpAddress.TabIndex = 3;
            this.GBox_SelectIpAddress.TabStop = false;
            this.GBox_SelectIpAddress.Text = "Select IP Address";
            // 
            // DnsAddrList_Select
            // 
            this.DnsAddrList_Select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DnsAddrList_Select.HostName = "";
            this.DnsAddrList_Select.HostNames = new string[] {
        ""};
            this.DnsAddrList_Select.Location = new System.Drawing.Point(3, 15);
            this.DnsAddrList_Select.Name = "DnsAddrList_Select";
            this.DnsAddrList_Select.SelectedIPAddress = ((System.Net.IPAddress)(resources.GetObject("DnsAddrList_Select.SelectedIPAddress")));
            this.DnsAddrList_Select.Size = new System.Drawing.Size(315, 79);
            this.DnsAddrList_Select.TabIndex = 0;
            // 
            // HostAddressSelectBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_SelectIpAddress);
            this.Controls.Add(this.groupBox1);
            this.Name = "HostAddressSelectBox";
            this.Size = new System.Drawing.Size(328, 157);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GBox_SelectIpAddress.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox TBox_SearchAddress;
		private System.Windows.Forms.GroupBox GBox_SelectIpAddress;
		private DnsAddressSelectBox DnsAddrList_Select;
	}
}
