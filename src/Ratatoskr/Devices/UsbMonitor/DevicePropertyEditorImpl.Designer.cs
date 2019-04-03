namespace Ratatoskr.Devices.UsbMonitor
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
            this.GBox_DeviceList = new System.Windows.Forms.GroupBox();
            this.CBox_DeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkBox_Filter_InterruptTransfer = new System.Windows.Forms.CheckBox();
            this.ChkBox_Filter_BulkTransfer = new System.Windows.Forms.CheckBox();
            this.ChkBox_Filter_ControlTransfer = new System.Windows.Forms.CheckBox();
            this.ChkBox_Filter_IsochronousTransfer = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChkBox_Info_FuncParam = new System.Windows.Forms.CheckBox();
            this.ChkBox_Info_IrpID = new System.Windows.Forms.CheckBox();
            this.ChkBox_Info_EndPoint = new System.Windows.Forms.CheckBox();
            this.ChkBox_Info_UsbDeviceID = new System.Windows.Forms.CheckBox();
            this.ChkBox_Info_FuncType = new System.Windows.Forms.CheckBox();
            this.GBox_DeviceList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_DeviceList
            // 
            this.GBox_DeviceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_DeviceList.Controls.Add(this.CBox_DeviceList);
            this.GBox_DeviceList.Location = new System.Drawing.Point(4, 4);
            this.GBox_DeviceList.Name = "GBox_DeviceList";
            this.GBox_DeviceList.Size = new System.Drawing.Size(407, 42);
            this.GBox_DeviceList.TabIndex = 5;
            this.GBox_DeviceList.TabStop = false;
            this.GBox_DeviceList.Text = "Monitor device";
            // 
            // CBox_DeviceList
            // 
            this.CBox_DeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DeviceList.FormattingEnabled = true;
            this.CBox_DeviceList.Location = new System.Drawing.Point(3, 15);
            this.CBox_DeviceList.Name = "CBox_DeviceList";
            this.CBox_DeviceList.Size = new System.Drawing.Size(401, 20);
            this.CBox_DeviceList.TabIndex = 0;
            this.CBox_DeviceList.SelectedIndexChanged += new System.EventHandler(this.CBox_DeviceList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkBox_Filter_InterruptTransfer);
            this.groupBox1.Controls.Add(this.ChkBox_Filter_BulkTransfer);
            this.groupBox1.Controls.Add(this.ChkBox_Filter_ControlTransfer);
            this.groupBox1.Controls.Add(this.ChkBox_Filter_IsochronousTransfer);
            this.groupBox1.Location = new System.Drawing.Point(4, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 106);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter setting";
            // 
            // ChkBox_Filter_InterruptTransfer
            // 
            this.ChkBox_Filter_InterruptTransfer.AutoSize = true;
            this.ChkBox_Filter_InterruptTransfer.Location = new System.Drawing.Point(6, 62);
            this.ChkBox_Filter_InterruptTransfer.Name = "ChkBox_Filter_InterruptTransfer";
            this.ChkBox_Filter_InterruptTransfer.Size = new System.Drawing.Size(111, 16);
            this.ChkBox_Filter_InterruptTransfer.TabIndex = 3;
            this.ChkBox_Filter_InterruptTransfer.Text = "Interrupt transfer";
            this.ChkBox_Filter_InterruptTransfer.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Filter_BulkTransfer
            // 
            this.ChkBox_Filter_BulkTransfer.AutoSize = true;
            this.ChkBox_Filter_BulkTransfer.Location = new System.Drawing.Point(6, 40);
            this.ChkBox_Filter_BulkTransfer.Name = "ChkBox_Filter_BulkTransfer";
            this.ChkBox_Filter_BulkTransfer.Size = new System.Drawing.Size(91, 16);
            this.ChkBox_Filter_BulkTransfer.TabIndex = 2;
            this.ChkBox_Filter_BulkTransfer.Text = "Bulk transfer";
            this.ChkBox_Filter_BulkTransfer.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Filter_ControlTransfer
            // 
            this.ChkBox_Filter_ControlTransfer.AutoSize = true;
            this.ChkBox_Filter_ControlTransfer.Location = new System.Drawing.Point(6, 18);
            this.ChkBox_Filter_ControlTransfer.Name = "ChkBox_Filter_ControlTransfer";
            this.ChkBox_Filter_ControlTransfer.Size = new System.Drawing.Size(105, 16);
            this.ChkBox_Filter_ControlTransfer.TabIndex = 1;
            this.ChkBox_Filter_ControlTransfer.Text = "Control transfer";
            this.ChkBox_Filter_ControlTransfer.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Filter_IsochronousTransfer
            // 
            this.ChkBox_Filter_IsochronousTransfer.AutoSize = true;
            this.ChkBox_Filter_IsochronousTransfer.Location = new System.Drawing.Point(6, 84);
            this.ChkBox_Filter_IsochronousTransfer.Name = "ChkBox_Filter_IsochronousTransfer";
            this.ChkBox_Filter_IsochronousTransfer.Size = new System.Drawing.Size(129, 16);
            this.ChkBox_Filter_IsochronousTransfer.TabIndex = 0;
            this.ChkBox_Filter_IsochronousTransfer.Text = "Isochronous transfer";
            this.ChkBox_Filter_IsochronousTransfer.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChkBox_Info_FuncParam);
            this.groupBox2.Controls.Add(this.ChkBox_Info_IrpID);
            this.groupBox2.Controls.Add(this.ChkBox_Info_EndPoint);
            this.groupBox2.Controls.Add(this.ChkBox_Info_UsbDeviceID);
            this.groupBox2.Controls.Add(this.ChkBox_Info_FuncType);
            this.groupBox2.Location = new System.Drawing.Point(210, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 131);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information Display setting";
            // 
            // ChkBox_Info_FuncParam
            // 
            this.ChkBox_Info_FuncParam.AutoSize = true;
            this.ChkBox_Info_FuncParam.Location = new System.Drawing.Point(6, 106);
            this.ChkBox_Info_FuncParam.Name = "ChkBox_Info_FuncParam";
            this.ChkBox_Info_FuncParam.Size = new System.Drawing.Size(124, 16);
            this.ChkBox_Info_FuncParam.TabIndex = 4;
            this.ChkBox_Info_FuncParam.Text = "Function Parameter";
            this.ChkBox_Info_FuncParam.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Info_IrpID
            // 
            this.ChkBox_Info_IrpID.AutoSize = true;
            this.ChkBox_Info_IrpID.Location = new System.Drawing.Point(6, 62);
            this.ChkBox_Info_IrpID.Name = "ChkBox_Info_IrpID";
            this.ChkBox_Info_IrpID.Size = new System.Drawing.Size(140, 16);
            this.ChkBox_Info_IrpID.TabIndex = 3;
            this.ChkBox_Info_IrpID.Text = "I/O Request packet ID";
            this.ChkBox_Info_IrpID.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Info_EndPoint
            // 
            this.ChkBox_Info_EndPoint.AutoSize = true;
            this.ChkBox_Info_EndPoint.Location = new System.Drawing.Point(6, 40);
            this.ChkBox_Info_EndPoint.Name = "ChkBox_Info_EndPoint";
            this.ChkBox_Info_EndPoint.Size = new System.Drawing.Size(73, 16);
            this.ChkBox_Info_EndPoint.TabIndex = 2;
            this.ChkBox_Info_EndPoint.Text = "End Point";
            this.ChkBox_Info_EndPoint.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Info_UsbDeviceID
            // 
            this.ChkBox_Info_UsbDeviceID.AutoSize = true;
            this.ChkBox_Info_UsbDeviceID.Location = new System.Drawing.Point(6, 18);
            this.ChkBox_Info_UsbDeviceID.Name = "ChkBox_Info_UsbDeviceID";
            this.ChkBox_Info_UsbDeviceID.Size = new System.Drawing.Size(101, 16);
            this.ChkBox_Info_UsbDeviceID.TabIndex = 1;
            this.ChkBox_Info_UsbDeviceID.Text = "USB Device ID";
            this.ChkBox_Info_UsbDeviceID.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Info_FuncType
            // 
            this.ChkBox_Info_FuncType.AutoSize = true;
            this.ChkBox_Info_FuncType.Location = new System.Drawing.Point(6, 84);
            this.ChkBox_Info_FuncType.Name = "ChkBox_Info_FuncType";
            this.ChkBox_Info_FuncType.Size = new System.Drawing.Size(97, 16);
            this.ChkBox_Info_FuncType.TabIndex = 0;
            this.ChkBox_Info_FuncType.Text = "Function Type";
            this.ChkBox_Info_FuncType.UseVisualStyleBackColor = true;
            this.ChkBox_Info_FuncType.CheckedChanged += new System.EventHandler(this.ChkBox_Info_FuncType_CheckedChanged);
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GBox_DeviceList);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(418, 332);
            this.GBox_DeviceList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_DeviceList;
        private System.Windows.Forms.ComboBox CBox_DeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkBox_Filter_IsochronousTransfer;
        private System.Windows.Forms.CheckBox ChkBox_Filter_ControlTransfer;
        private System.Windows.Forms.CheckBox ChkBox_Filter_InterruptTransfer;
        private System.Windows.Forms.CheckBox ChkBox_Filter_BulkTransfer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ChkBox_Info_IrpID;
        private System.Windows.Forms.CheckBox ChkBox_Info_EndPoint;
        private System.Windows.Forms.CheckBox ChkBox_Info_UsbDeviceID;
        private System.Windows.Forms.CheckBox ChkBox_Info_FuncType;
        private System.Windows.Forms.CheckBox ChkBox_Info_FuncParam;
    }
}
