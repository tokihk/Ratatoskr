
namespace Ratatoskr.FileFormat.PacketLog_Pcap
{
	partial class FileFormatReaderOptionEditorImpl
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
            this.GBox_RecvConfig = new System.Windows.Forms.GroupBox();
            this.GBox_PacketObjectItem = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CBox_PacketInfoType = new System.Windows.Forms.ComboBox();
            this.CBox_PacketDataType = new System.Windows.Forms.ComboBox();
            this.CBox_PacketDestinationType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBox_PacketSourceType = new System.Windows.Forms.ComboBox();
            this.GBox_RecvFilterPattern = new System.Windows.Forms.GroupBox();
            this.TBox_PcapFilter = new System.Windows.Forms.TextBox();
            this.GBox_RecvConfig.SuspendLayout();
            this.GBox_PacketObjectItem.SuspendLayout();
            this.GBox_RecvFilterPattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_RecvConfig
            // 
            this.GBox_RecvConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RecvConfig.Controls.Add(this.GBox_PacketObjectItem);
            this.GBox_RecvConfig.Controls.Add(this.GBox_RecvFilterPattern);
            this.GBox_RecvConfig.Location = new System.Drawing.Point(3, 3);
            this.GBox_RecvConfig.Name = "GBox_RecvConfig";
            this.GBox_RecvConfig.Size = new System.Drawing.Size(502, 201);
            this.GBox_RecvConfig.TabIndex = 7;
            this.GBox_RecvConfig.TabStop = false;
            this.GBox_RecvConfig.Text = "Capture setting";
            // 
            // GBox_PacketObjectItem
            // 
            this.GBox_PacketObjectItem.Controls.Add(this.label4);
            this.GBox_PacketObjectItem.Controls.Add(this.CBox_PacketInfoType);
            this.GBox_PacketObjectItem.Controls.Add(this.CBox_PacketDataType);
            this.GBox_PacketObjectItem.Controls.Add(this.CBox_PacketDestinationType);
            this.GBox_PacketObjectItem.Controls.Add(this.label3);
            this.GBox_PacketObjectItem.Controls.Add(this.label2);
            this.GBox_PacketObjectItem.Controls.Add(this.label1);
            this.GBox_PacketObjectItem.Controls.Add(this.CBox_PacketSourceType);
            this.GBox_PacketObjectItem.Location = new System.Drawing.Point(6, 66);
            this.GBox_PacketObjectItem.Name = "GBox_PacketObjectItem";
            this.GBox_PacketObjectItem.Size = new System.Drawing.Size(321, 127);
            this.GBox_PacketObjectItem.TabIndex = 9;
            this.GBox_PacketObjectItem.TabStop = false;
            this.GBox_PacketObjectItem.Text = "Packet object item select";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Information";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CBox_PacketInfoType
            // 
            this.CBox_PacketInfoType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_PacketInfoType.FormattingEnabled = true;
            this.CBox_PacketInfoType.Location = new System.Drawing.Point(113, 18);
            this.CBox_PacketInfoType.Name = "CBox_PacketInfoType";
            this.CBox_PacketInfoType.Size = new System.Drawing.Size(194, 20);
            this.CBox_PacketInfoType.TabIndex = 6;
            // 
            // CBox_PacketDataType
            // 
            this.CBox_PacketDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_PacketDataType.FormattingEnabled = true;
            this.CBox_PacketDataType.Location = new System.Drawing.Point(113, 96);
            this.CBox_PacketDataType.Name = "CBox_PacketDataType";
            this.CBox_PacketDataType.Size = new System.Drawing.Size(194, 20);
            this.CBox_PacketDataType.TabIndex = 5;
            // 
            // CBox_PacketDestinationType
            // 
            this.CBox_PacketDestinationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_PacketDestinationType.FormattingEnabled = true;
            this.CBox_PacketDestinationType.Location = new System.Drawing.Point(113, 70);
            this.CBox_PacketDestinationType.Name = "CBox_PacketDestinationType";
            this.CBox_PacketDestinationType.Size = new System.Drawing.Size(194, 20);
            this.CBox_PacketDestinationType.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data contents";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CBox_PacketSourceType
            // 
            this.CBox_PacketSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_PacketSourceType.FormattingEnabled = true;
            this.CBox_PacketSourceType.Location = new System.Drawing.Point(113, 44);
            this.CBox_PacketSourceType.Name = "CBox_PacketSourceType";
            this.CBox_PacketSourceType.Size = new System.Drawing.Size(194, 20);
            this.CBox_PacketSourceType.TabIndex = 0;
            // 
            // GBox_RecvFilterPattern
            // 
            this.GBox_RecvFilterPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RecvFilterPattern.Controls.Add(this.TBox_PcapFilter);
            this.GBox_RecvFilterPattern.Location = new System.Drawing.Point(6, 18);
            this.GBox_RecvFilterPattern.Name = "GBox_RecvFilterPattern";
            this.GBox_RecvFilterPattern.Size = new System.Drawing.Size(487, 42);
            this.GBox_RecvFilterPattern.TabIndex = 1;
            this.GBox_RecvFilterPattern.TabStop = false;
            this.GBox_RecvFilterPattern.Text = "Pcap filter";
            // 
            // TBox_PcapFilter
            // 
            this.TBox_PcapFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_PcapFilter.Location = new System.Drawing.Point(3, 15);
            this.TBox_PcapFilter.Name = "TBox_PcapFilter";
            this.TBox_PcapFilter.Size = new System.Drawing.Size(481, 19);
            this.TBox_PcapFilter.TabIndex = 1;
            // 
            // FileFormatReaderOptionEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_RecvConfig);
            this.Name = "FileFormatReaderOptionEditorImpl";
            this.Size = new System.Drawing.Size(508, 209);
            this.GBox_RecvConfig.ResumeLayout(false);
            this.GBox_PacketObjectItem.ResumeLayout(false);
            this.GBox_RecvFilterPattern.ResumeLayout(false);
            this.GBox_RecvFilterPattern.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GBox_RecvConfig;
		private System.Windows.Forms.GroupBox GBox_PacketObjectItem;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox CBox_PacketInfoType;
		private System.Windows.Forms.ComboBox CBox_PacketDataType;
		private System.Windows.Forms.ComboBox CBox_PacketDestinationType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CBox_PacketSourceType;
		private System.Windows.Forms.GroupBox GBox_RecvFilterPattern;
		private System.Windows.Forms.TextBox TBox_PcapFilter;
	}
}
