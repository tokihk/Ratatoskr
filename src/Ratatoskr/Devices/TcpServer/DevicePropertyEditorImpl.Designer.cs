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
            this.GBox_ConnectMax = new System.Windows.Forms.GroupBox();
            this.Num_ConnextMax = new System.Windows.Forms.NumericUpDown();
            this.GBox_LocalPortNo = new System.Windows.Forms.GroupBox();
            this.Num_LocalPortNo = new System.Windows.Forms.NumericUpDown();
            this.GBox_ConnectMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ConnextMax)).BeginInit();
            this.GBox_LocalPortNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).BeginInit();
            this.SuspendLayout();
            // 
            // GBox_ConnectMax
            // 
            this.GBox_ConnectMax.Controls.Add(this.Num_ConnextMax);
            this.GBox_ConnectMax.Location = new System.Drawing.Point(4, 3);
            this.GBox_ConnectMax.Name = "GBox_ConnectMax";
            this.GBox_ConnectMax.Size = new System.Drawing.Size(100, 41);
            this.GBox_ConnectMax.TabIndex = 7;
            this.GBox_ConnectMax.TabStop = false;
            this.GBox_ConnectMax.Text = "接続最大数";
            // 
            // Num_ConnextMax
            // 
            this.Num_ConnextMax.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.GBox_LocalPortNo.Location = new System.Drawing.Point(4, 50);
            this.GBox_LocalPortNo.Name = "GBox_LocalPortNo";
            this.GBox_LocalPortNo.Size = new System.Drawing.Size(100, 41);
            this.GBox_LocalPortNo.TabIndex = 8;
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
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_LocalPortNo);
            this.Controls.Add(this.GBox_ConnectMax);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(111, 98);
            this.GBox_ConnectMax.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_ConnextMax)).EndInit();
            this.GBox_LocalPortNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_ConnectMax;
        private System.Windows.Forms.NumericUpDown Num_ConnextMax;
        private System.Windows.Forms.GroupBox GBox_LocalPortNo;
        private System.Windows.Forms.NumericUpDown Num_LocalPortNo;
    }
}
