namespace Ratatoskr.Forms.ConfigForm.Pages
{
    partial class ConfigPage_AutoTimeStamp
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
            this.ChkBox_Trigger_RecvPeriod = new System.Windows.Forms.CheckBox();
            this.Num_Trigger_RecvPeriod = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Trigger_RecvPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // ChkBox_Trigger_RecvPeriod
            // 
            this.ChkBox_Trigger_RecvPeriod.Location = new System.Drawing.Point(3, 9);
            this.ChkBox_Trigger_RecvPeriod.Name = "ChkBox_Trigger_RecvPeriod";
            this.ChkBox_Trigger_RecvPeriod.Size = new System.Drawing.Size(260, 24);
            this.ChkBox_Trigger_RecvPeriod.TabIndex = 0;
            this.ChkBox_Trigger_RecvPeriod.Text = "最後に受信してから指定時間(ミリ秒)が経過";
            this.ChkBox_Trigger_RecvPeriod.UseVisualStyleBackColor = true;
            // 
            // Num_Trigger_RecvPeriod
            // 
            this.Num_Trigger_RecvPeriod.Location = new System.Drawing.Point(269, 9);
            this.Num_Trigger_RecvPeriod.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.Num_Trigger_RecvPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_Trigger_RecvPeriod.Name = "Num_Trigger_RecvPeriod";
            this.Num_Trigger_RecvPeriod.Size = new System.Drawing.Size(120, 19);
            this.Num_Trigger_RecvPeriod.TabIndex = 1;
            this.Num_Trigger_RecvPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_Trigger_RecvPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ConfigPage_AutoTimeStamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Num_Trigger_RecvPeriod);
            this.Controls.Add(this.ChkBox_Trigger_RecvPeriod);
            this.Name = "ConfigPage_AutoTimeStamp";
            this.Size = new System.Drawing.Size(441, 227);
            ((System.ComponentModel.ISupportInitialize)(this.Num_Trigger_RecvPeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkBox_Trigger_RecvPeriod;
        private System.Windows.Forms.NumericUpDown Num_Trigger_RecvPeriod;
    }
}
