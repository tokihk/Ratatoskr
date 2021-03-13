namespace Ratatoskr.Devices.TcpServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevicePropertyEditorImpl));
            this.GBox_ConnectMax = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_ConnextMax = new System.Windows.Forms.NumericUpDown();
            this.GBox_LocalPortNo = new System.Windows.Forms.GroupBox();
            this.Num_LocalPortNo = new System.Windows.Forms.NumericUpDown();
            this.GBox_Local = new System.Windows.Forms.GroupBox();
            this.CBox_LocalBindMode = new System.Windows.Forms.ComboBox();
            this.GBox_LocalIPAddress = new System.Windows.Forms.GroupBox();
            this.DnsAddrList_Local = new Ratatoskr.Forms.Controls.DnsAddressSelectBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CBox_AddressFamily = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Num_RecvBufferSize = new System.Windows.Forms.NumericUpDown();
            this.Num_SendBufferSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChkBox_ReuseAddr = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ChkBox_Unicast_TTL = new System.Windows.Forms.CheckBox();
            this.Num_Unicast_TTL = new System.Windows.Forms.NumericUpDown();
            this.ChkBox_KeepAliveOnOff = new System.Windows.Forms.CheckBox();
            this.GBox_KeepAlive = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Num_KeepAliveTime = new System.Windows.Forms.NumericUpDown();
            this.GBox_ConnectMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ConnextMax)).BeginInit();
            this.GBox_LocalPortNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).BeginInit();
            this.GBox_Local.SuspendLayout();
            this.GBox_LocalIPAddress.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RecvBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendBufferSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Unicast_TTL)).BeginInit();
            this.GBox_KeepAlive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_KeepAliveTime)).BeginInit();
            this.SuspendLayout();
            // 
            // GBox_ConnectMax
            // 
            this.GBox_ConnectMax.Controls.Add(this.label1);
            this.GBox_ConnectMax.Controls.Add(this.Num_ConnextMax);
            this.GBox_ConnectMax.Location = new System.Drawing.Point(4, 268);
            this.GBox_ConnectMax.Name = "GBox_ConnectMax";
            this.GBox_ConnectMax.Size = new System.Drawing.Size(184, 41);
            this.GBox_ConnectMax.TabIndex = 7;
            this.GBox_ConnectMax.TabStop = false;
            this.GBox_ConnectMax.Text = "Connect limit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "(0=No limit)";
            // 
            // Num_ConnextMax
            // 
            this.Num_ConnextMax.Location = new System.Drawing.Point(3, 15);
            this.Num_ConnextMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_ConnextMax.Name = "Num_ConnextMax";
            this.Num_ConnextMax.Size = new System.Drawing.Size(94, 19);
            this.Num_ConnextMax.TabIndex = 0;
            this.Num_ConnextMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GBox_LocalPortNo
            // 
            this.GBox_LocalPortNo.Controls.Add(this.Num_LocalPortNo);
            this.GBox_LocalPortNo.Location = new System.Drawing.Point(372, 44);
            this.GBox_LocalPortNo.Name = "GBox_LocalPortNo";
            this.GBox_LocalPortNo.Size = new System.Drawing.Size(100, 47);
            this.GBox_LocalPortNo.TabIndex = 8;
            this.GBox_LocalPortNo.TabStop = false;
            this.GBox_LocalPortNo.Text = "Port number";
            // 
            // Num_LocalPortNo
            // 
            this.Num_LocalPortNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_LocalPortNo.Location = new System.Drawing.Point(3, 15);
            this.Num_LocalPortNo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_LocalPortNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_LocalPortNo.Name = "Num_LocalPortNo";
            this.Num_LocalPortNo.Size = new System.Drawing.Size(94, 19);
            this.Num_LocalPortNo.TabIndex = 0;
            this.Num_LocalPortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_LocalPortNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GBox_Local
            // 
            this.GBox_Local.Controls.Add(this.CBox_LocalBindMode);
            this.GBox_Local.Controls.Add(this.GBox_LocalIPAddress);
            this.GBox_Local.Controls.Add(this.GBox_LocalPortNo);
            this.GBox_Local.Location = new System.Drawing.Point(4, 56);
            this.GBox_Local.Name = "GBox_Local";
            this.GBox_Local.Size = new System.Drawing.Size(480, 206);
            this.GBox_Local.TabIndex = 9;
            this.GBox_Local.TabStop = false;
            this.GBox_Local.Text = "Local";
            // 
            // CBox_LocalBindMode
            // 
            this.CBox_LocalBindMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_LocalBindMode.FormattingEnabled = true;
            this.CBox_LocalBindMode.Location = new System.Drawing.Point(6, 18);
            this.CBox_LocalBindMode.Name = "CBox_LocalBindMode";
            this.CBox_LocalBindMode.Size = new System.Drawing.Size(150, 20);
            this.CBox_LocalBindMode.TabIndex = 3;
            // 
            // GBox_LocalIPAddress
            // 
            this.GBox_LocalIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_LocalIPAddress.Controls.Add(this.DnsAddrList_Local);
            this.GBox_LocalIPAddress.Location = new System.Drawing.Point(7, 44);
            this.GBox_LocalIPAddress.Name = "GBox_LocalIPAddress";
            this.GBox_LocalIPAddress.Size = new System.Drawing.Size(360, 156);
            this.GBox_LocalIPAddress.TabIndex = 0;
            this.GBox_LocalIPAddress.TabStop = false;
            this.GBox_LocalIPAddress.Text = "Bind IP Address";
            // 
            // DnsAddrList_Local
            // 
            this.DnsAddrList_Local.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            this.DnsAddrList_Local.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DnsAddrList_Local.HostName = "";
            this.DnsAddrList_Local.HostNames = new string[] {
        ""};
            this.DnsAddrList_Local.Location = new System.Drawing.Point(3, 15);
            this.DnsAddrList_Local.Name = "DnsAddrList_Local";
            this.DnsAddrList_Local.SelectedIPAddress = ((System.Net.IPAddress)(resources.GetObject("DnsAddrList_Local.SelectedIPAddress")));
            this.DnsAddrList_Local.Size = new System.Drawing.Size(354, 138);
            this.DnsAddrList_Local.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CBox_AddressFamily);
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 47);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Address Family";
            // 
            // CBox_AddressFamily
            // 
            this.CBox_AddressFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_AddressFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_AddressFamily.FormattingEnabled = true;
            this.CBox_AddressFamily.Location = new System.Drawing.Point(6, 18);
            this.CBox_AddressFamily.Name = "CBox_AddressFamily";
            this.CBox_AddressFamily.Size = new System.Drawing.Size(150, 20);
            this.CBox_AddressFamily.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.Num_RecvBufferSize);
            this.groupBox4.Controls.Add(this.Num_SendBufferSize);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(194, 268);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 71);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buffer Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "bytes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "bytes";
            // 
            // Num_RecvBufferSize
            // 
            this.Num_RecvBufferSize.Location = new System.Drawing.Point(148, 43);
            this.Num_RecvBufferSize.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.Num_RecvBufferSize.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.Num_RecvBufferSize.Name = "Num_RecvBufferSize";
            this.Num_RecvBufferSize.Size = new System.Drawing.Size(90, 19);
            this.Num_RecvBufferSize.TabIndex = 8;
            this.Num_RecvBufferSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RecvBufferSize.ThousandsSeparator = true;
            this.Num_RecvBufferSize.Value = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            // 
            // Num_SendBufferSize
            // 
            this.Num_SendBufferSize.Location = new System.Drawing.Point(148, 18);
            this.Num_SendBufferSize.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.Num_SendBufferSize.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.Num_SendBufferSize.Name = "Num_SendBufferSize";
            this.Num_SendBufferSize.Size = new System.Drawing.Size(90, 19);
            this.Num_SendBufferSize.TabIndex = 7;
            this.Num_SendBufferSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SendBufferSize.ThousandsSeparator = true;
            this.Num_SendBufferSize.Value = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Send Buffer Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Receive Buffer Size";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ChkBox_ReuseAddr);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.ChkBox_KeepAliveOnOff);
            this.groupBox3.Controls.Add(this.GBox_KeepAlive);
            this.groupBox3.Location = new System.Drawing.Point(4, 345);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(562, 84);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option";
            // 
            // ChkBox_ReuseAddr
            // 
            this.ChkBox_ReuseAddr.AutoSize = true;
            this.ChkBox_ReuseAddr.Location = new System.Drawing.Point(7, 18);
            this.ChkBox_ReuseAddr.Name = "ChkBox_ReuseAddr";
            this.ChkBox_ReuseAddr.Size = new System.Drawing.Size(102, 16);
            this.ChkBox_ReuseAddr.TabIndex = 4;
            this.ChkBox_ReuseAddr.Text = "Reuse Address";
            this.ChkBox_ReuseAddr.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ChkBox_Unicast_TTL);
            this.groupBox5.Controls.Add(this.Num_Unicast_TTL);
            this.groupBox5.Location = new System.Drawing.Point(390, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(159, 53);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Unicast";
            // 
            // ChkBox_Unicast_TTL
            // 
            this.ChkBox_Unicast_TTL.AutoSize = true;
            this.ChkBox_Unicast_TTL.Location = new System.Drawing.Point(6, 24);
            this.ChkBox_Unicast_TTL.Name = "ChkBox_Unicast_TTL";
            this.ChkBox_Unicast_TTL.Size = new System.Drawing.Size(44, 16);
            this.ChkBox_Unicast_TTL.TabIndex = 0;
            this.ChkBox_Unicast_TTL.Text = "TTL";
            this.ChkBox_Unicast_TTL.UseVisualStyleBackColor = true;
            this.ChkBox_Unicast_TTL.CheckedChanged += new System.EventHandler(this.ChkBox_Unicast_TTL_CheckedChanged);
            // 
            // Num_Unicast_TTL
            // 
            this.Num_Unicast_TTL.Location = new System.Drawing.Point(75, 23);
            this.Num_Unicast_TTL.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_Unicast_TTL.Name = "Num_Unicast_TTL";
            this.Num_Unicast_TTL.Size = new System.Drawing.Size(70, 19);
            this.Num_Unicast_TTL.TabIndex = 0;
            this.Num_Unicast_TTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ChkBox_KeepAliveOnOff
            // 
            this.ChkBox_KeepAliveOnOff.AutoSize = true;
            this.ChkBox_KeepAliveOnOff.Location = new System.Drawing.Point(137, 17);
            this.ChkBox_KeepAliveOnOff.Name = "ChkBox_KeepAliveOnOff";
            this.ChkBox_KeepAliveOnOff.Size = new System.Drawing.Size(79, 16);
            this.ChkBox_KeepAliveOnOff.TabIndex = 2;
            this.ChkBox_KeepAliveOnOff.Text = "Keep Alive";
            this.ChkBox_KeepAliveOnOff.UseVisualStyleBackColor = true;
            this.ChkBox_KeepAliveOnOff.CheckedChanged += new System.EventHandler(this.ChkBox_KeepAliveOnOff_CheckedChanged);
            // 
            // GBox_KeepAlive
            // 
            this.GBox_KeepAlive.Controls.Add(this.label2);
            this.GBox_KeepAlive.Controls.Add(this.label7);
            this.GBox_KeepAlive.Controls.Add(this.Num_KeepAliveTime);
            this.GBox_KeepAlive.Location = new System.Drawing.Point(128, 18);
            this.GBox_KeepAlive.Name = "GBox_KeepAlive";
            this.GBox_KeepAlive.Size = new System.Drawing.Size(256, 53);
            this.GBox_KeepAlive.TabIndex = 0;
            this.GBox_KeepAlive.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "KeepAliveTime";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(209, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "msec";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Num_KeepAliveTime
            // 
            this.Num_KeepAliveTime.Location = new System.Drawing.Point(113, 23);
            this.Num_KeepAliveTime.Maximum = new decimal(new int[] {
            86400000,
            0,
            0,
            0});
            this.Num_KeepAliveTime.Name = "Num_KeepAliveTime";
            this.Num_KeepAliveTime.Size = new System.Drawing.Size(90, 19);
            this.Num_KeepAliveTime.TabIndex = 2;
            this.Num_KeepAliveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_KeepAliveTime.ThousandsSeparator = true;
            this.Num_KeepAliveTime.Value = new decimal(new int[] {
            86400000,
            0,
            0,
            0});
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GBox_Local);
            this.Controls.Add(this.GBox_ConnectMax);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(578, 437);
            this.GBox_ConnectMax.ResumeLayout(false);
            this.GBox_ConnectMax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ConnextMax)).EndInit();
            this.GBox_LocalPortNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).EndInit();
            this.GBox_Local.ResumeLayout(false);
            this.GBox_LocalIPAddress.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RecvBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendBufferSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Unicast_TTL)).EndInit();
            this.GBox_KeepAlive.ResumeLayout(false);
            this.GBox_KeepAlive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_KeepAliveTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_ConnectMax;
        private System.Windows.Forms.NumericUpDown Num_ConnextMax;
        private System.Windows.Forms.GroupBox GBox_LocalPortNo;
        private System.Windows.Forms.NumericUpDown Num_LocalPortNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox GBox_Local;
		private System.Windows.Forms.ComboBox CBox_LocalBindMode;
		private System.Windows.Forms.GroupBox GBox_LocalIPAddress;
		private Forms.Controls.DnsAddressSelectBox DnsAddrList_Local;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox CBox_AddressFamily;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown Num_RecvBufferSize;
		private System.Windows.Forms.NumericUpDown Num_SendBufferSize;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox ChkBox_ReuseAddr;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.CheckBox ChkBox_Unicast_TTL;
		private System.Windows.Forms.NumericUpDown Num_Unicast_TTL;
		private System.Windows.Forms.CheckBox ChkBox_KeepAliveOnOff;
		private System.Windows.Forms.GroupBox GBox_KeepAlive;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown Num_KeepAliveTime;
	}
}
