namespace Ratatoskr.Forms.Controls
{
	partial class DnsAddressSelectBox
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
            this.RBtnList_IpAddress = new Ratatoskr.Forms.Controls.RadioButtonListBox();
            this.Label_Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RBtnList_IpAddress
            // 
            this.RBtnList_IpAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RBtnList_IpAddress.Location = new System.Drawing.Point(0, 20);
            this.RBtnList_IpAddress.Name = "RBtnList_IpAddress";
            this.RBtnList_IpAddress.SelectedItem = null;
            this.RBtnList_IpAddress.SelectedItemIndex = -1;
            this.RBtnList_IpAddress.Size = new System.Drawing.Size(150, 130);
            this.RBtnList_IpAddress.TabIndex = 0;
            this.RBtnList_IpAddress.RadioButtonClick += new System.EventHandler(this.RBtnList_IpAddress_RadioButtonClick);
            // 
            // Label_Status
            // 
            this.Label_Status.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Status.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_Status.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Label_Status.Location = new System.Drawing.Point(0, 0);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(150, 20);
            this.Label_Status.TabIndex = 1;
            this.Label_Status.Text = "label1";
            this.Label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Status.Visible = false;
            // 
            // DnsAddressSelectBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RBtnList_IpAddress);
            this.Controls.Add(this.Label_Status);
            this.Name = "DnsAddressSelectBox";
            this.ResumeLayout(false);

		}

		#endregion

		private RadioButtonListBox RBtnList_IpAddress;
		private System.Windows.Forms.Label Label_Status;
	}
}
