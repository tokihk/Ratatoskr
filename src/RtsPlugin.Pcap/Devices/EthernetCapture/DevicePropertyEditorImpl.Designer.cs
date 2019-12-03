namespace RtsPlugin.Pcap.Devices.EthernetCapture
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
            this.GBox_ViewData = new System.Windows.Forms.GroupBox();
            this.CBox_ViewDataType = new System.Windows.Forms.ComboBox();
            this.CBox_ViewDestinationType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBox_ViewSourceType = new System.Windows.Forms.ComboBox();
            this.GBox_RecvFilterPattern = new System.Windows.Forms.GroupBox();
            this.TBox_RecvFilter = new System.Windows.Forms.TextBox();
            this.GBox_IfceList.SuspendLayout();
            this.GBox_RecvConfig.SuspendLayout();
            this.GBox_ViewData.SuspendLayout();
            this.GBox_RecvFilterPattern.SuspendLayout();
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
            this.GBox_IfceList.Size = new System.Drawing.Size(407, 65);
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
            this.Label_IfceName.Size = new System.Drawing.Size(401, 23);
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
            this.CBox_IfceList.Size = new System.Drawing.Size(401, 20);
            this.CBox_IfceList.TabIndex = 0;
            this.CBox_IfceList.SelectedIndexChanged += new System.EventHandler(this.CBox_IfceList_SelectedIndexChanged);
            // 
            // GBox_RecvConfig
            // 
            this.GBox_RecvConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RecvConfig.Controls.Add(this.GBox_ViewData);
            this.GBox_RecvConfig.Controls.Add(this.GBox_RecvFilterPattern);
            this.GBox_RecvConfig.Location = new System.Drawing.Point(4, 75);
            this.GBox_RecvConfig.Name = "GBox_RecvConfig";
            this.GBox_RecvConfig.Size = new System.Drawing.Size(407, 180);
            this.GBox_RecvConfig.TabIndex = 6;
            this.GBox_RecvConfig.TabStop = false;
            this.GBox_RecvConfig.Text = "Monitor setting";
            // 
            // GBox_ViewData
            // 
            this.GBox_ViewData.Controls.Add(this.CBox_ViewDataType);
            this.GBox_ViewData.Controls.Add(this.CBox_ViewDestinationType);
            this.GBox_ViewData.Controls.Add(this.label3);
            this.GBox_ViewData.Controls.Add(this.label2);
            this.GBox_ViewData.Controls.Add(this.label1);
            this.GBox_ViewData.Controls.Add(this.CBox_ViewSourceType);
            this.GBox_ViewData.Location = new System.Drawing.Point(3, 66);
            this.GBox_ViewData.Name = "GBox_ViewData";
            this.GBox_ViewData.Size = new System.Drawing.Size(321, 102);
            this.GBox_ViewData.TabIndex = 9;
            this.GBox_ViewData.TabStop = false;
            this.GBox_ViewData.Text = "Monitor data select";
            // 
            // CBox_ViewDataType
            // 
            this.CBox_ViewDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ViewDataType.FormattingEnabled = true;
            this.CBox_ViewDataType.Location = new System.Drawing.Point(113, 71);
            this.CBox_ViewDataType.Name = "CBox_ViewDataType";
            this.CBox_ViewDataType.Size = new System.Drawing.Size(194, 20);
            this.CBox_ViewDataType.TabIndex = 5;
            // 
            // CBox_ViewDestinationType
            // 
            this.CBox_ViewDestinationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ViewDestinationType.FormattingEnabled = true;
            this.CBox_ViewDestinationType.Location = new System.Drawing.Point(113, 45);
            this.CBox_ViewDestinationType.Name = "CBox_ViewDestinationType";
            this.CBox_ViewDestinationType.Size = new System.Drawing.Size(194, 20);
            this.CBox_ViewDestinationType.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data contents";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CBox_ViewSourceType
            // 
            this.CBox_ViewSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ViewSourceType.FormattingEnabled = true;
            this.CBox_ViewSourceType.Location = new System.Drawing.Point(113, 19);
            this.CBox_ViewSourceType.Name = "CBox_ViewSourceType";
            this.CBox_ViewSourceType.Size = new System.Drawing.Size(194, 20);
            this.CBox_ViewSourceType.TabIndex = 0;
            // 
            // GBox_RecvFilterPattern
            // 
            this.GBox_RecvFilterPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RecvFilterPattern.Controls.Add(this.TBox_RecvFilter);
            this.GBox_RecvFilterPattern.Location = new System.Drawing.Point(3, 18);
            this.GBox_RecvFilterPattern.Name = "GBox_RecvFilterPattern";
            this.GBox_RecvFilterPattern.Size = new System.Drawing.Size(395, 42);
            this.GBox_RecvFilterPattern.TabIndex = 1;
            this.GBox_RecvFilterPattern.TabStop = false;
            this.GBox_RecvFilterPattern.Text = "WinPcap filter";
            // 
            // TBox_RecvFilter
            // 
            this.TBox_RecvFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_RecvFilter.Location = new System.Drawing.Point(3, 15);
            this.TBox_RecvFilter.Name = "TBox_RecvFilter";
            this.TBox_RecvFilter.Size = new System.Drawing.Size(389, 19);
            this.TBox_RecvFilter.TabIndex = 1;
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_RecvConfig);
            this.Controls.Add(this.GBox_IfceList);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(418, 263);
            this.GBox_IfceList.ResumeLayout(false);
            this.GBox_RecvConfig.ResumeLayout(false);
            this.GBox_ViewData.ResumeLayout(false);
            this.GBox_RecvFilterPattern.ResumeLayout(false);
            this.GBox_RecvFilterPattern.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_IfceList;
        private System.Windows.Forms.ComboBox CBox_IfceList;
        private System.Windows.Forms.GroupBox GBox_RecvConfig;
        private System.Windows.Forms.Label Label_IfceName;
        private System.Windows.Forms.GroupBox GBox_RecvFilterPattern;
        private System.Windows.Forms.TextBox TBox_RecvFilter;
        private System.Windows.Forms.GroupBox GBox_ViewData;
        private System.Windows.Forms.ComboBox CBox_ViewSourceType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBox_ViewDataType;
        private System.Windows.Forms.ComboBox CBox_ViewDestinationType;
    }
}
