namespace Ratatoskr.Forms.OptionEditForm
{
    partial class OptionEditPage_Memory
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Label_RawPacketCountLimit = new System.Windows.Forms.Label();
            this.Num_Packet_ViewPacketCountLimit = new System.Windows.Forms.NumericUpDown();
            this.Label_Packet_ViewPacketCountLimit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RawPacketCountLimit)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Packet_ViewPacketCountLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // Num_RawPacketCountLimit
            // 
            this.Num_RawPacketCountLimit.Location = new System.Drawing.Point(234, 19);
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
            this.groupBox3.Controls.Add(this.Num_RawPacketCountLimit);
            this.groupBox3.Controls.Add(this.Label_RawPacketCountLimit);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 52);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Basic setting";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Num_Packet_ViewPacketCountLimit);
            this.groupBox4.Controls.Add(this.Label_Packet_ViewPacketCountLimit);
            this.groupBox4.Location = new System.Drawing.Point(3, 61);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(340, 49);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Packet view setting - Packet";
            // 
            // Label_RawPacketCountLimit
            // 
            this.Label_RawPacketCountLimit.Location = new System.Drawing.Point(9, 19);
            this.Label_RawPacketCountLimit.Name = "Label_RawPacketCountLimit";
            this.Label_RawPacketCountLimit.Size = new System.Drawing.Size(220, 23);
            this.Label_RawPacketCountLimit.TabIndex = 0;
            this.Label_RawPacketCountLimit.Text = "Raw packet count limit";
            this.Label_RawPacketCountLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // OptionEditPage_Memory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "OptionEditPage_Memory";
            this.Size = new System.Drawing.Size(352, 119);
            ((System.ComponentModel.ISupportInitialize)(this.Num_RawPacketCountLimit)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_Packet_ViewPacketCountLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown Num_RawPacketCountLimit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Label_RawPacketCountLimit;
        private System.Windows.Forms.NumericUpDown Num_Packet_ViewPacketCountLimit;
        private System.Windows.Forms.Label Label_Packet_ViewPacketCountLimit;
    }
}
