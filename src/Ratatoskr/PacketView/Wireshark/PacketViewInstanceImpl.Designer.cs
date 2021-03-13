namespace Ratatoskr.PacketView.Wireshark
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
            this.Panel_Wireshark = new System.Windows.Forms.Panel();
            this.Panel_Menu = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ChkBox_InnterWindowMode = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBox_LinkType = new System.Windows.Forms.ComboBox();
            this.Num_LinkType = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkBox_Capture_RecvPacket = new System.Windows.Forms.CheckBox();
            this.ChkBox_Capture_SendPacket = new System.Windows.Forms.CheckBox();
            this.Panel_Menu.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_LinkType)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Wireshark
            // 
            this.Panel_Wireshark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Wireshark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Wireshark.Location = new System.Drawing.Point(0, 76);
            this.Panel_Wireshark.Name = "Panel_Wireshark";
            this.Panel_Wireshark.Size = new System.Drawing.Size(888, 580);
            this.Panel_Wireshark.TabIndex = 0;
            this.Panel_Wireshark.Resize += new System.EventHandler(this.Panel_Wireshark_Resize);
            // 
            // Panel_Menu
            // 
            this.Panel_Menu.Controls.Add(this.groupBox4);
            this.Panel_Menu.Controls.Add(this.groupBox1);
            this.Panel_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.Panel_Menu.Name = "Panel_Menu";
            this.Panel_Menu.Size = new System.Drawing.Size(888, 76);
            this.Panel_Menu.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ChkBox_InnterWindowMode);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(411, 67);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Wireshark Config";
            // 
            // ChkBox_InnterWindowMode
            // 
            this.ChkBox_InnterWindowMode.AutoSize = true;
            this.ChkBox_InnterWindowMode.Location = new System.Drawing.Point(6, 28);
            this.ChkBox_InnterWindowMode.Name = "ChkBox_InnterWindowMode";
            this.ChkBox_InnterWindowMode.Size = new System.Drawing.Size(114, 16);
            this.ChkBox_InnterWindowMode.TabIndex = 4;
            this.ChkBox_InnterWindowMode.Text = "InnerWindowMode";
            this.ChkBox_InnterWindowMode.UseVisualStyleBackColor = true;
            this.ChkBox_InnterWindowMode.CheckedChanged += new System.EventHandler(this.ChkBox_InnterWindowMode_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.CBox_LinkType);
            this.groupBox3.Controls.Add(this.Num_LinkType);
            this.groupBox3.Location = new System.Drawing.Point(126, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 46);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wireshark - Link Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "<=>";
            // 
            // CBox_LinkType
            // 
            this.CBox_LinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_LinkType.FormattingEnabled = true;
            this.CBox_LinkType.Location = new System.Drawing.Point(121, 15);
            this.CBox_LinkType.Name = "CBox_LinkType";
            this.CBox_LinkType.Size = new System.Drawing.Size(140, 20);
            this.CBox_LinkType.TabIndex = 1;
            this.CBox_LinkType.SelectedIndexChanged += new System.EventHandler(this.CBox_LinkType_SelectedIndexChanged);
            // 
            // Num_LinkType
            // 
            this.Num_LinkType.Location = new System.Drawing.Point(6, 16);
            this.Num_LinkType.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.Num_LinkType.Name = "Num_LinkType";
            this.Num_LinkType.Size = new System.Drawing.Size(80, 19);
            this.Num_LinkType.TabIndex = 0;
            this.Num_LinkType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_LinkType.ValueChanged += new System.EventHandler(this.Num_LinkType_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkBox_Capture_RecvPacket);
            this.groupBox1.Controls.Add(this.ChkBox_Capture_SendPacket);
            this.groupBox1.Location = new System.Drawing.Point(420, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(128, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capture Target";
            // 
            // ChkBox_Capture_RecvPacket
            // 
            this.ChkBox_Capture_RecvPacket.AutoSize = true;
            this.ChkBox_Capture_RecvPacket.Location = new System.Drawing.Point(6, 42);
            this.ChkBox_Capture_RecvPacket.Name = "ChkBox_Capture_RecvPacket";
            this.ChkBox_Capture_RecvPacket.Size = new System.Drawing.Size(104, 16);
            this.ChkBox_Capture_RecvPacket.TabIndex = 1;
            this.ChkBox_Capture_RecvPacket.Text = "Receive Packet";
            this.ChkBox_Capture_RecvPacket.UseVisualStyleBackColor = true;
            this.ChkBox_Capture_RecvPacket.CheckedChanged += new System.EventHandler(this.ChkBox_Capture_RecvPacket_CheckedChanged);
            // 
            // ChkBox_Capture_SendPacket
            // 
            this.ChkBox_Capture_SendPacket.AutoSize = true;
            this.ChkBox_Capture_SendPacket.Location = new System.Drawing.Point(6, 20);
            this.ChkBox_Capture_SendPacket.Name = "ChkBox_Capture_SendPacket";
            this.ChkBox_Capture_SendPacket.Size = new System.Drawing.Size(88, 16);
            this.ChkBox_Capture_SendPacket.TabIndex = 0;
            this.ChkBox_Capture_SendPacket.Text = "Send Packet";
            this.ChkBox_Capture_SendPacket.UseVisualStyleBackColor = true;
            this.ChkBox_Capture_SendPacket.CheckedChanged += new System.EventHandler(this.ChkBox_Capture_SendPacket_CheckedChanged);
            // 
            // PacketViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Wireshark);
            this.Controls.Add(this.Panel_Menu);
            this.Name = "PacketViewInstanceImpl";
            this.Size = new System.Drawing.Size(888, 656);
            this.Panel_Menu.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_LinkType)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Wireshark;
        private System.Windows.Forms.Panel Panel_Menu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkBox_Capture_RecvPacket;
        private System.Windows.Forms.CheckBox ChkBox_Capture_SendPacket;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBox_LinkType;
        private System.Windows.Forms.NumericUpDown Num_LinkType;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox ChkBox_InnterWindowMode;
	}
}
