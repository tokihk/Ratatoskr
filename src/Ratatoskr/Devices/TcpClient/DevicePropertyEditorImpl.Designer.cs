namespace Ratatoskr.Devices.TcpClient
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
            this.GBox_Local = new System.Windows.Forms.GroupBox();
            this.GBox_LocalPortNo = new System.Windows.Forms.GroupBox();
            this.Num_LocalPortNo = new System.Windows.Forms.NumericUpDown();
            this.GBox_Remote = new System.Windows.Forms.GroupBox();
            this.GBox_RemotePortNo = new System.Windows.Forms.GroupBox();
            this.Num_RemotePortNo = new System.Windows.Forms.NumericUpDown();
            this.GBox_RemoteAddress = new System.Windows.Forms.GroupBox();
            this.TBox_RemoteAddress = new System.Windows.Forms.TextBox();
            this.GBox_Local.SuspendLayout();
            this.GBox_LocalPortNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).BeginInit();
            this.GBox_Remote.SuspendLayout();
            this.GBox_RemotePortNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RemotePortNo)).BeginInit();
            this.GBox_RemoteAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_Local
            // 
            this.GBox_Local.Controls.Add(this.GBox_LocalPortNo);
            this.GBox_Local.Location = new System.Drawing.Point(4, 3);
            this.GBox_Local.Name = "GBox_Local";
            this.GBox_Local.Size = new System.Drawing.Size(113, 66);
            this.GBox_Local.TabIndex = 5;
            this.GBox_Local.TabStop = false;
            this.GBox_Local.Text = "ローカル";
            // 
            // GBox_LocalPortNo
            // 
            this.GBox_LocalPortNo.Controls.Add(this.Num_LocalPortNo);
            this.GBox_LocalPortNo.Location = new System.Drawing.Point(7, 18);
            this.GBox_LocalPortNo.Name = "GBox_LocalPortNo";
            this.GBox_LocalPortNo.Size = new System.Drawing.Size(100, 41);
            this.GBox_LocalPortNo.TabIndex = 6;
            this.GBox_LocalPortNo.TabStop = false;
            this.GBox_LocalPortNo.Text = "ポート番号";
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
            this.Num_LocalPortNo.Name = "Num_LocalPortNo";
            this.Num_LocalPortNo.Size = new System.Drawing.Size(94, 19);
            this.Num_LocalPortNo.TabIndex = 0;
            this.Num_LocalPortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GBox_Remote
            // 
            this.GBox_Remote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_Remote.Controls.Add(this.GBox_RemotePortNo);
            this.GBox_Remote.Controls.Add(this.GBox_RemoteAddress);
            this.GBox_Remote.Location = new System.Drawing.Point(123, 3);
            this.GBox_Remote.Name = "GBox_Remote";
            this.GBox_Remote.Size = new System.Drawing.Size(338, 66);
            this.GBox_Remote.TabIndex = 6;
            this.GBox_Remote.TabStop = false;
            this.GBox_Remote.Text = "サーバー";
            // 
            // GBox_RemotePortNo
            // 
            this.GBox_RemotePortNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RemotePortNo.Controls.Add(this.Num_RemotePortNo);
            this.GBox_RemotePortNo.Location = new System.Drawing.Point(231, 18);
            this.GBox_RemotePortNo.Name = "GBox_RemotePortNo";
            this.GBox_RemotePortNo.Size = new System.Drawing.Size(100, 41);
            this.GBox_RemotePortNo.TabIndex = 6;
            this.GBox_RemotePortNo.TabStop = false;
            this.GBox_RemotePortNo.Text = "ポート番号";
            // 
            // Num_RemotePortNo
            // 
            this.Num_RemotePortNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_RemotePortNo.Location = new System.Drawing.Point(3, 15);
            this.Num_RemotePortNo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_RemotePortNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_RemotePortNo.Name = "Num_RemotePortNo";
            this.Num_RemotePortNo.Size = new System.Drawing.Size(94, 19);
            this.Num_RemotePortNo.TabIndex = 0;
            this.Num_RemotePortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RemotePortNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GBox_RemoteAddress
            // 
            this.GBox_RemoteAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RemoteAddress.Controls.Add(this.TBox_RemoteAddress);
            this.GBox_RemoteAddress.Location = new System.Drawing.Point(7, 18);
            this.GBox_RemoteAddress.Name = "GBox_RemoteAddress";
            this.GBox_RemoteAddress.Size = new System.Drawing.Size(219, 41);
            this.GBox_RemoteAddress.TabIndex = 0;
            this.GBox_RemoteAddress.TabStop = false;
            this.GBox_RemoteAddress.Text = "アドレス";
            // 
            // TBox_RemoteAddress
            // 
            this.TBox_RemoteAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_RemoteAddress.Location = new System.Drawing.Point(3, 15);
            this.TBox_RemoteAddress.Name = "TBox_RemoteAddress";
            this.TBox_RemoteAddress.Size = new System.Drawing.Size(213, 19);
            this.TBox_RemoteAddress.TabIndex = 0;
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_Remote);
            this.Controls.Add(this.GBox_Local);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(464, 74);
            this.GBox_Local.ResumeLayout(false);
            this.GBox_LocalPortNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).EndInit();
            this.GBox_Remote.ResumeLayout(false);
            this.GBox_RemotePortNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_RemotePortNo)).EndInit();
            this.GBox_RemoteAddress.ResumeLayout(false);
            this.GBox_RemoteAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_Local;
        private System.Windows.Forms.GroupBox GBox_LocalPortNo;
        private System.Windows.Forms.GroupBox GBox_Remote;
        private System.Windows.Forms.GroupBox GBox_RemotePortNo;
        private System.Windows.Forms.GroupBox GBox_RemoteAddress;
        private System.Windows.Forms.TextBox TBox_RemoteAddress;
        private System.Windows.Forms.NumericUpDown Num_LocalPortNo;
        private System.Windows.Forms.NumericUpDown Num_RemotePortNo;
    }
}
