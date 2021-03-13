namespace RtsCore.Device.UsbComm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkBox_UsbEventCapture = new System.Windows.Forms.CheckBox();
            this.GBox_UsbCommConfig = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Num_UsbProductID = new Ratatoskr.Forms.Controls.HexNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_UsbVendorID = new Ratatoskr.Forms.Controls.HexNumericUpDown();
            this.ChkBox_UsbDeviceComm = new System.Windows.Forms.CheckBox();
            this.GBox_UsbCommConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_UsbProductID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_UsbVendorID)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ChkBox_UsbEventCapture
            // 
            this.ChkBox_UsbEventCapture.AutoSize = true;
            this.ChkBox_UsbEventCapture.Location = new System.Drawing.Point(9, 0);
            this.ChkBox_UsbEventCapture.Name = "ChkBox_UsbEventCapture";
            this.ChkBox_UsbEventCapture.Size = new System.Drawing.Size(124, 16);
            this.ChkBox_UsbEventCapture.TabIndex = 0;
            this.ChkBox_UsbEventCapture.Text = "USB Event Capture";
            this.ChkBox_UsbEventCapture.UseVisualStyleBackColor = true;
            // 
            // GBox_UsbCommConfig
            // 
            this.GBox_UsbCommConfig.Controls.Add(this.label2);
            this.GBox_UsbCommConfig.Controls.Add(this.Num_UsbProductID);
            this.GBox_UsbCommConfig.Controls.Add(this.label1);
            this.GBox_UsbCommConfig.Controls.Add(this.Num_UsbVendorID);
            this.GBox_UsbCommConfig.Location = new System.Drawing.Point(3, 106);
            this.GBox_UsbCommConfig.Name = "GBox_UsbCommConfig";
            this.GBox_UsbCommConfig.Size = new System.Drawing.Size(234, 75);
            this.GBox_UsbCommConfig.TabIndex = 1;
            this.GBox_UsbCommConfig.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Product ID";
            // 
            // Num_UsbProductID
            // 
            this.Num_UsbProductID.Hexadecimal = true;
            this.Num_UsbProductID.Location = new System.Drawing.Point(128, 43);
            this.Num_UsbProductID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_UsbProductID.Name = "Num_UsbProductID";
            this.Num_UsbProductID.Size = new System.Drawing.Size(100, 19);
            this.Num_UsbProductID.TabIndex = 2;
            this.Num_UsbProductID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_UsbProductID.ZeroPadding = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vendor ID";
            // 
            // Num_UsbVendorID
            // 
            this.Num_UsbVendorID.Hexadecimal = true;
            this.Num_UsbVendorID.Location = new System.Drawing.Point(128, 18);
            this.Num_UsbVendorID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_UsbVendorID.Name = "Num_UsbVendorID";
            this.Num_UsbVendorID.Size = new System.Drawing.Size(100, 19);
            this.Num_UsbVendorID.TabIndex = 0;
            this.Num_UsbVendorID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_UsbVendorID.ZeroPadding = false;
            // 
            // ChkBox_UsbDeviceComm
            // 
            this.ChkBox_UsbDeviceComm.AutoSize = true;
            this.ChkBox_UsbDeviceComm.Location = new System.Drawing.Point(12, 103);
            this.ChkBox_UsbDeviceComm.Name = "ChkBox_UsbDeviceComm";
            this.ChkBox_UsbDeviceComm.Size = new System.Drawing.Size(168, 16);
            this.ChkBox_UsbDeviceComm.TabIndex = 2;
            this.ChkBox_UsbDeviceComm.Text = "Communication USB Device";
            this.ChkBox_UsbDeviceComm.UseVisualStyleBackColor = true;
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChkBox_UsbEventCapture);
            this.Controls.Add(this.ChkBox_UsbDeviceComm);
            this.Controls.Add(this.GBox_UsbCommConfig);
            this.Controls.Add(this.groupBox1);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(400, 263);
            this.GBox_UsbCommConfig.ResumeLayout(false);
            this.GBox_UsbCommConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_UsbProductID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_UsbVendorID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkBox_UsbEventCapture;
        private System.Windows.Forms.GroupBox GBox_UsbCommConfig;
        private System.Windows.Forms.Label label1;
        private Forms.Controls.HexNumericUpDown Num_UsbVendorID;
        private System.Windows.Forms.Label label2;
        private Forms.Controls.HexNumericUpDown Num_UsbProductID;
        private System.Windows.Forms.CheckBox ChkBox_UsbDeviceComm;
    }
}
