namespace Ratatoskr.Forms.ConfigEditor
{
    partial class ConfigEditorPage_System
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
            this.Num_RawPacketCountLimit = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Label_RawPacketCountLimit = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Num_Packet_ViewPacketCountLimit = new System.Windows.Forms.NumericUpDown();
            this.Label_Packet_ViewPacketCountLimit = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkBox_Sequential_WinApiMode = new System.Windows.Forms.CheckBox();
            this.ChkBox_Sequential_ViewCharCountLimit = new System.Windows.Forms.CheckBox();
            this.ChkBox_Sequential_LineNumberVisible = new System.Windows.Forms.CheckBox();
            this.Num_Sequential_ViewCharCountLimit = new System.Windows.Forms.NumericUpDown();
            this.Num_GateNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RawPacketCountLimit)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Packet_ViewPacketCountLimit)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Sequential_ViewCharCountLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_GateNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // Num_RawPacketCountLimit
            // 
            this.Num_RawPacketCountLimit.Location = new System.Drawing.Point(234, 38);
            this.Num_RawPacketCountLimit.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Num_RawPacketCountLimit.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Num_RawPacketCountLimit.Name = "Num_RawPacketCountLimit";
            this.Num_RawPacketCountLimit.Size = new System.Drawing.Size(100, 19);
            this.Num_RawPacketCountLimit.TabIndex = 0;
            this.Num_RawPacketCountLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RawPacketCountLimit.ThousandsSeparator = true;
            this.Num_RawPacketCountLimit.Value = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Num_GateNumber);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.Num_RawPacketCountLimit);
            this.groupBox3.Controls.Add(this.Label_RawPacketCountLimit);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 67);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Basic setting";
            // 
            // Label_RawPacketCountLimit
            // 
            this.Label_RawPacketCountLimit.Location = new System.Drawing.Point(9, 38);
            this.Label_RawPacketCountLimit.Name = "Label_RawPacketCountLimit";
            this.Label_RawPacketCountLimit.Size = new System.Drawing.Size(220, 23);
            this.Label_RawPacketCountLimit.TabIndex = 0;
            this.Label_RawPacketCountLimit.Text = "Raw packet count limit";
            this.Label_RawPacketCountLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Num_Packet_ViewPacketCountLimit);
            this.groupBox4.Controls.Add(this.Label_Packet_ViewPacketCountLimit);
            this.groupBox4.Location = new System.Drawing.Point(3, 76);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(340, 49);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Packet view setting - Packet";
            // 
            // Num_Packet_ViewPacketCountLimit
            // 
            this.Num_Packet_ViewPacketCountLimit.Location = new System.Drawing.Point(234, 18);
            this.Num_Packet_ViewPacketCountLimit.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Num_Packet_ViewPacketCountLimit.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Num_Packet_ViewPacketCountLimit.Name = "Num_Packet_ViewPacketCountLimit";
            this.Num_Packet_ViewPacketCountLimit.Size = new System.Drawing.Size(100, 19);
            this.Num_Packet_ViewPacketCountLimit.TabIndex = 1;
            this.Num_Packet_ViewPacketCountLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_Packet_ViewPacketCountLimit.ThousandsSeparator = true;
            this.Num_Packet_ViewPacketCountLimit.Value = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            // 
            // Label_Packet_ViewPacketCountLimit
            // 
            this.Label_Packet_ViewPacketCountLimit.Location = new System.Drawing.Point(9, 15);
            this.Label_Packet_ViewPacketCountLimit.Name = "Label_Packet_ViewPacketCountLimit";
            this.Label_Packet_ViewPacketCountLimit.Size = new System.Drawing.Size(220, 23);
            this.Label_Packet_ViewPacketCountLimit.TabIndex = 2;
            this.Label_Packet_ViewPacketCountLimit.Text = "View packet count limit";
            this.Label_Packet_ViewPacketCountLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkBox_Sequential_WinApiMode);
            this.groupBox1.Controls.Add(this.ChkBox_Sequential_ViewCharCountLimit);
            this.groupBox1.Controls.Add(this.ChkBox_Sequential_LineNumberVisible);
            this.groupBox1.Controls.Add(this.Num_Sequential_ViewCharCountLimit);
            this.groupBox1.Location = new System.Drawing.Point(3, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 89);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet view setting - Sequential";
            // 
            // ChkBox_Sequential_WinApiMode
            // 
            this.ChkBox_Sequential_WinApiMode.AutoSize = true;
            this.ChkBox_Sequential_WinApiMode.Location = new System.Drawing.Point(11, 18);
            this.ChkBox_Sequential_WinApiMode.Name = "ChkBox_Sequential_WinApiMode";
            this.ChkBox_Sequential_WinApiMode.Size = new System.Drawing.Size(91, 16);
            this.ChkBox_Sequential_WinApiMode.TabIndex = 5;
            this.ChkBox_Sequential_WinApiMode.Text = "WinAPI Mode";
            this.ChkBox_Sequential_WinApiMode.UseVisualStyleBackColor = true;
            this.ChkBox_Sequential_WinApiMode.CheckedChanged += new System.EventHandler(this.ChkBox_Sequential_WinApiMode_CheckedChanged);
            // 
            // ChkBox_Sequential_ViewCharCountLimit
            // 
            this.ChkBox_Sequential_ViewCharCountLimit.AutoSize = true;
            this.ChkBox_Sequential_ViewCharCountLimit.Location = new System.Drawing.Point(11, 41);
            this.ChkBox_Sequential_ViewCharCountLimit.Name = "ChkBox_Sequential_ViewCharCountLimit";
            this.ChkBox_Sequential_ViewCharCountLimit.Size = new System.Drawing.Size(159, 16);
            this.ChkBox_Sequential_ViewCharCountLimit.TabIndex = 4;
            this.ChkBox_Sequential_ViewCharCountLimit.Text = "View character count limit";
            this.ChkBox_Sequential_ViewCharCountLimit.UseVisualStyleBackColor = true;
            this.ChkBox_Sequential_ViewCharCountLimit.CheckedChanged += new System.EventHandler(this.ChkBox_Sequential_ViewCharCountLimit_CheckedChanged);
            // 
            // ChkBox_Sequential_LineNumberVisible
            // 
            this.ChkBox_Sequential_LineNumberVisible.AutoSize = true;
            this.ChkBox_Sequential_LineNumberVisible.Location = new System.Drawing.Point(11, 63);
            this.ChkBox_Sequential_LineNumberVisible.Name = "ChkBox_Sequential_LineNumberVisible";
            this.ChkBox_Sequential_LineNumberVisible.Size = new System.Drawing.Size(119, 16);
            this.ChkBox_Sequential_LineNumberVisible.TabIndex = 3;
            this.ChkBox_Sequential_LineNumberVisible.Text = "Show Line Number";
            this.ChkBox_Sequential_LineNumberVisible.UseVisualStyleBackColor = true;
            // 
            // Num_Sequential_ViewCharCountLimit
            // 
            this.Num_Sequential_ViewCharCountLimit.Location = new System.Drawing.Point(234, 40);
            this.Num_Sequential_ViewCharCountLimit.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Num_Sequential_ViewCharCountLimit.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Num_Sequential_ViewCharCountLimit.Name = "Num_Sequential_ViewCharCountLimit";
            this.Num_Sequential_ViewCharCountLimit.Size = new System.Drawing.Size(100, 19);
            this.Num_Sequential_ViewCharCountLimit.TabIndex = 1;
            this.Num_Sequential_ViewCharCountLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_Sequential_ViewCharCountLimit.ThousandsSeparator = true;
            this.Num_Sequential_ViewCharCountLimit.Value = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            // 
            // Num_GateNumber
            // 
            this.Num_GateNumber.Location = new System.Drawing.Point(234, 15);
            this.Num_GateNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Num_GateNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_GateNumber.Name = "Num_GateNumber";
            this.Num_GateNumber.Size = new System.Drawing.Size(100, 19);
            this.Num_GateNumber.TabIndex = 1;
            this.Num_GateNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_GateNumber.ThousandsSeparator = true;
            this.Num_GateNumber.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gate Number";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionEditPage_System
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "OptionEditPage_System";
            this.Size = new System.Drawing.Size(352, 228);
            ((System.ComponentModel.ISupportInitialize)(this.Num_RawPacketCountLimit)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_Packet_ViewPacketCountLimit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Sequential_ViewCharCountLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_GateNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown Num_RawPacketCountLimit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Label_RawPacketCountLimit;
        private System.Windows.Forms.NumericUpDown Num_Packet_ViewPacketCountLimit;
        private System.Windows.Forms.Label Label_Packet_ViewPacketCountLimit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown Num_Sequential_ViewCharCountLimit;
        private System.Windows.Forms.CheckBox ChkBox_Sequential_LineNumberVisible;
        private System.Windows.Forms.CheckBox ChkBox_Sequential_ViewCharCountLimit;
        private System.Windows.Forms.CheckBox ChkBox_Sequential_WinApiMode;
		private System.Windows.Forms.NumericUpDown Num_GateNumber;
		private System.Windows.Forms.Label label1;
	}
}
