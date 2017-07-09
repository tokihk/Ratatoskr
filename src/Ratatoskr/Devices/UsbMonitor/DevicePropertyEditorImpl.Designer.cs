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
            this.GBox_DeviceTree = new System.Windows.Forms.GroupBox();
            this.TBox_DeviceTree = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkBox_Filter_InterruptTransfer = new System.Windows.Forms.CheckBox();
            this.ChkBox_Filter_BulkTransfer = new System.Windows.Forms.CheckBox();
            this.ChkBox_Filter_ControlTransfer = new System.Windows.Forms.CheckBox();
            this.ChkBox_Filter_IsochronousTransfer = new System.Windows.Forms.CheckBox();
            this.GBox_DeviceList.SuspendLayout();
            this.GBox_DeviceTree.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // 
            // GBox_DeviceTree
            // 
            this.GBox_DeviceTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_DeviceTree.Controls.Add(this.TBox_DeviceTree);
            this.GBox_DeviceTree.Location = new System.Drawing.Point(159, 52);
            this.GBox_DeviceTree.Name = "GBox_DeviceTree";
            this.GBox_DeviceTree.Size = new System.Drawing.Size(252, 111);
            this.GBox_DeviceTree.TabIndex = 6;
            this.GBox_DeviceTree.TabStop = false;
            this.GBox_DeviceTree.Text = "Device tree";
            // 
            // TBox_DeviceTree
            // 
            this.TBox_DeviceTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBox_DeviceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_DeviceTree.Location = new System.Drawing.Point(3, 15);
            this.TBox_DeviceTree.Multiline = true;
            this.TBox_DeviceTree.Name = "TBox_DeviceTree";
            this.TBox_DeviceTree.ReadOnly = true;
            this.TBox_DeviceTree.Size = new System.Drawing.Size(246, 93);
            this.TBox_DeviceTree.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkBox_Filter_InterruptTransfer);
            this.groupBox1.Controls.Add(this.ChkBox_Filter_BulkTransfer);
            this.groupBox1.Controls.Add(this.ChkBox_Filter_ControlTransfer);
            this.groupBox1.Controls.Add(this.ChkBox_Filter_IsochronousTransfer);
            this.groupBox1.Location = new System.Drawing.Point(4, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 111);
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
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GBox_DeviceTree);
            this.Controls.Add(this.GBox_DeviceList);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(418, 177);
            this.GBox_DeviceList.ResumeLayout(false);
            this.GBox_DeviceTree.ResumeLayout(false);
            this.GBox_DeviceTree.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_DeviceList;
        private System.Windows.Forms.ComboBox CBox_DeviceList;
        private System.Windows.Forms.GroupBox GBox_DeviceTree;
        private System.Windows.Forms.TextBox TBox_DeviceTree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkBox_Filter_IsochronousTransfer;
        private System.Windows.Forms.CheckBox ChkBox_Filter_ControlTransfer;
        private System.Windows.Forms.CheckBox ChkBox_Filter_InterruptTransfer;
        private System.Windows.Forms.CheckBox ChkBox_Filter_BulkTransfer;
    }
}
