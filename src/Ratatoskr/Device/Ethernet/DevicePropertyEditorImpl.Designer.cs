namespace Ratatoskr.Device.Ethernet
{
    partial class DevicePropertyEditorImpl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.GBox_IfceList = new System.Windows.Forms.GroupBox();
            this.Label_IfceName = new System.Windows.Forms.Label();
            this.CBox_IfceList = new System.Windows.Forms.ComboBox();
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
            this.TBox_RecvFilter = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PGrid_SelectProtocolParameter = new System.Windows.Forms.PropertyGrid();
            this.Btn_AddProtocolItem = new System.Windows.Forms.Button();
            this.Btn_RemoveProtocolItem = new System.Windows.Forms.Button();
            this.LBox_FrameProtocolList = new System.Windows.Forms.ListBox();
            this.LBox_ProtocolItemList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.GBox_IfceList.SuspendLayout();
            this.GBox_RecvConfig.SuspendLayout();
            this.GBox_PacketObjectItem.SuspendLayout();
            this.GBox_RecvFilterPattern.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_IfceList
            // 
            this.GBox_IfceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_IfceList.Controls.Add(this.Label_IfceName);
            this.GBox_IfceList.Controls.Add(this.CBox_IfceList);
            this.GBox_IfceList.Location = new System.Drawing.Point(4, 4);
            this.GBox_IfceList.Name = "GBox_IfceList";
            this.GBox_IfceList.Size = new System.Drawing.Size(538, 65);
            this.GBox_IfceList.TabIndex = 5;
            this.GBox_IfceList.TabStop = false;
            this.GBox_IfceList.Text = "Device interface";
            // 
            // Label_IfceName
            // 
            this.Label_IfceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_IfceName.Location = new System.Drawing.Point(3, 38);
            this.Label_IfceName.Name = "Label_IfceName";
            this.Label_IfceName.Size = new System.Drawing.Size(532, 23);
            this.Label_IfceName.TabIndex = 1;
            this.Label_IfceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CBox_IfceList
            // 
            this.CBox_IfceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_IfceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_IfceList.FormattingEnabled = true;
            this.CBox_IfceList.Location = new System.Drawing.Point(3, 15);
            this.CBox_IfceList.Name = "CBox_IfceList";
            this.CBox_IfceList.Size = new System.Drawing.Size(532, 20);
            this.CBox_IfceList.TabIndex = 0;
            this.CBox_IfceList.SelectedIndexChanged += new System.EventHandler(this.CBox_IfceList_SelectedIndexChanged);
            // 
            // GBox_RecvConfig
            // 
            this.GBox_RecvConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RecvConfig.Controls.Add(this.GBox_PacketObjectItem);
            this.GBox_RecvConfig.Controls.Add(this.GBox_RecvFilterPattern);
            this.GBox_RecvConfig.Location = new System.Drawing.Point(4, 75);
            this.GBox_RecvConfig.Name = "GBox_RecvConfig";
            this.GBox_RecvConfig.Size = new System.Drawing.Size(538, 201);
            this.GBox_RecvConfig.TabIndex = 6;
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
            this.GBox_RecvFilterPattern.Controls.Add(this.TBox_RecvFilter);
            this.GBox_RecvFilterPattern.Location = new System.Drawing.Point(6, 18);
            this.GBox_RecvFilterPattern.Name = "GBox_RecvFilterPattern";
            this.GBox_RecvFilterPattern.Size = new System.Drawing.Size(523, 42);
            this.GBox_RecvFilterPattern.TabIndex = 1;
            this.GBox_RecvFilterPattern.TabStop = false;
            this.GBox_RecvFilterPattern.Text = "Pcap filter";
            // 
            // TBox_RecvFilter
            // 
            this.TBox_RecvFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_RecvFilter.Location = new System.Drawing.Point(3, 15);
            this.TBox_RecvFilter.Name = "TBox_RecvFilter";
            this.TBox_RecvFilter.Size = new System.Drawing.Size(517, 19);
            this.TBox_RecvFilter.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.Btn_RemoveProtocolItem);
            this.groupBox1.Controls.Add(this.Btn_AddProtocolItem);
            this.groupBox1.Location = new System.Drawing.Point(4, 282);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 457);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send setting";
            // 
            // PGrid_SelectProtocolParameter
            // 
            this.PGrid_SelectProtocolParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PGrid_SelectProtocolParameter.Location = new System.Drawing.Point(3, 15);
            this.PGrid_SelectProtocolParameter.Name = "PGrid_SelectProtocolParameter";
            this.PGrid_SelectProtocolParameter.Size = new System.Drawing.Size(274, 417);
            this.PGrid_SelectProtocolParameter.TabIndex = 1;
            // 
            // Btn_AddProtocolItem
            // 
            this.Btn_AddProtocolItem.Location = new System.Drawing.Point(50, 224);
            this.Btn_AddProtocolItem.Name = "Btn_AddProtocolItem";
            this.Btn_AddProtocolItem.Size = new System.Drawing.Size(75, 23);
            this.Btn_AddProtocolItem.TabIndex = 2;
            this.Btn_AddProtocolItem.Text = "Add";
            this.Btn_AddProtocolItem.UseVisualStyleBackColor = true;
            this.Btn_AddProtocolItem.Click += new System.EventHandler(this.Btn_AddProtocolItem_Click);
            // 
            // Btn_RemoveProtocolItem
            // 
            this.Btn_RemoveProtocolItem.Location = new System.Drawing.Point(131, 224);
            this.Btn_RemoveProtocolItem.Name = "Btn_RemoveProtocolItem";
            this.Btn_RemoveProtocolItem.Size = new System.Drawing.Size(75, 23);
            this.Btn_RemoveProtocolItem.TabIndex = 5;
            this.Btn_RemoveProtocolItem.Text = "Remove";
            this.Btn_RemoveProtocolItem.UseVisualStyleBackColor = true;
            this.Btn_RemoveProtocolItem.Click += new System.EventHandler(this.Btn_RemoveProtocolItem_Click);
            // 
            // LBox_FrameProtocolList
            // 
            this.LBox_FrameProtocolList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LBox_FrameProtocolList.FormattingEnabled = true;
            this.LBox_FrameProtocolList.ItemHeight = 12;
            this.LBox_FrameProtocolList.Location = new System.Drawing.Point(3, 15);
            this.LBox_FrameProtocolList.Name = "LBox_FrameProtocolList";
            this.LBox_FrameProtocolList.Size = new System.Drawing.Size(234, 182);
            this.LBox_FrameProtocolList.TabIndex = 6;
            this.LBox_FrameProtocolList.SelectedIndexChanged += new System.EventHandler(this.LBox_FrameProtocolList_SelectedIndexChanged);
            // 
            // LBox_ProtocolItemList
            // 
            this.LBox_ProtocolItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LBox_ProtocolItemList.FormattingEnabled = true;
            this.LBox_ProtocolItemList.ItemHeight = 12;
            this.LBox_ProtocolItemList.Location = new System.Drawing.Point(3, 15);
            this.LBox_ProtocolItemList.Name = "LBox_ProtocolItemList";
            this.LBox_ProtocolItemList.Size = new System.Drawing.Size(234, 182);
            this.LBox_ProtocolItemList.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LBox_ProtocolItemList);
            this.groupBox3.Location = new System.Drawing.Point(5, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 200);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Protocol item list";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PGrid_SelectProtocolParameter);
            this.groupBox4.Location = new System.Drawing.Point(252, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 435);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select protocol parameter";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.LBox_FrameProtocolList);
            this.groupBox5.Location = new System.Drawing.Point(6, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(240, 200);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Frame protocol llist";
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GBox_RecvConfig);
            this.Controls.Add(this.GBox_IfceList);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(549, 745);
            this.GBox_IfceList.ResumeLayout(false);
            this.GBox_RecvConfig.ResumeLayout(false);
            this.GBox_PacketObjectItem.ResumeLayout(false);
            this.GBox_RecvFilterPattern.ResumeLayout(false);
            this.GBox_RecvFilterPattern.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_IfceList;
        private System.Windows.Forms.ComboBox CBox_IfceList;
        private System.Windows.Forms.GroupBox GBox_RecvConfig;
        private System.Windows.Forms.Label Label_IfceName;
        private System.Windows.Forms.GroupBox GBox_RecvFilterPattern;
        private System.Windows.Forms.TextBox TBox_RecvFilter;
        private System.Windows.Forms.GroupBox GBox_PacketObjectItem;
        private System.Windows.Forms.ComboBox CBox_PacketSourceType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBox_PacketDataType;
        private System.Windows.Forms.ComboBox CBox_PacketDestinationType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox CBox_PacketInfoType;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.PropertyGrid PGrid_SelectProtocolParameter;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.ListBox LBox_FrameProtocolList;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ListBox LBox_ProtocolItemList;
		private System.Windows.Forms.Button Btn_RemoveProtocolItem;
		private System.Windows.Forms.Button Btn_AddProtocolItem;
	}
}
