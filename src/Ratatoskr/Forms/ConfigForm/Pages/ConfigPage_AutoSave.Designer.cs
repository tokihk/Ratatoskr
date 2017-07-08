namespace Ratatoskr.Forms.ConfigForm.Pages
{
    partial class ConfigPage_AutoSave
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
            this.GBox_SavePrefix = new System.Windows.Forms.GroupBox();
            this.TBox_SavePrefix = new System.Windows.Forms.TextBox();
            this.GBox_Basic = new System.Windows.Forms.GroupBox();
            this.GBox_SaveFormat = new System.Windows.Forms.GroupBox();
            this.CBox_SaveFormat = new System.Windows.Forms.ComboBox();
            this.GBox_SaveDir = new System.Windows.Forms.GroupBox();
            this.Btn_SaveDir_Ref = new System.Windows.Forms.Button();
            this.TBox_SaveDir = new System.Windows.Forms.TextBox();
            this.GBox_Timing = new System.Windows.Forms.GroupBox();
            this.Num_PacketCount = new System.Windows.Forms.NumericUpDown();
            this.Num_FileSize = new System.Windows.Forms.NumericUpDown();
            this.Num_Interval = new System.Windows.Forms.NumericUpDown();
            this.RBtn_Timing_PacketCount = new System.Windows.Forms.RadioButton();
            this.RBtn_Timing_FileSize = new System.Windows.Forms.RadioButton();
            this.RBtn_Timing_Interval = new System.Windows.Forms.RadioButton();
            this.GBox_SavePrefix.SuspendLayout();
            this.GBox_Basic.SuspendLayout();
            this.GBox_SaveFormat.SuspendLayout();
            this.GBox_SaveDir.SuspendLayout();
            this.GBox_Timing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_FileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Interval)).BeginInit();
            this.SuspendLayout();
            // 
            // GBox_SavePrefix
            // 
            this.GBox_SavePrefix.Controls.Add(this.TBox_SavePrefix);
            this.GBox_SavePrefix.Location = new System.Drawing.Point(212, 18);
            this.GBox_SavePrefix.Name = "GBox_SavePrefix";
            this.GBox_SavePrefix.Size = new System.Drawing.Size(200, 41);
            this.GBox_SavePrefix.TabIndex = 3;
            this.GBox_SavePrefix.TabStop = false;
            this.GBox_SavePrefix.Text = "保存ファイルの接頭語";
            // 
            // TBox_SavePrefix
            // 
            this.TBox_SavePrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_SavePrefix.Location = new System.Drawing.Point(3, 15);
            this.TBox_SavePrefix.Name = "TBox_SavePrefix";
            this.TBox_SavePrefix.Size = new System.Drawing.Size(194, 19);
            this.TBox_SavePrefix.TabIndex = 0;
            // 
            // GBox_Basic
            // 
            this.GBox_Basic.Controls.Add(this.GBox_SaveFormat);
            this.GBox_Basic.Controls.Add(this.GBox_SaveDir);
            this.GBox_Basic.Controls.Add(this.GBox_SavePrefix);
            this.GBox_Basic.Location = new System.Drawing.Point(3, 3);
            this.GBox_Basic.Name = "GBox_Basic";
            this.GBox_Basic.Size = new System.Drawing.Size(514, 120);
            this.GBox_Basic.TabIndex = 4;
            this.GBox_Basic.TabStop = false;
            this.GBox_Basic.Text = "基本設定";
            // 
            // GBox_SaveFormat
            // 
            this.GBox_SaveFormat.Controls.Add(this.CBox_SaveFormat);
            this.GBox_SaveFormat.Location = new System.Drawing.Point(6, 18);
            this.GBox_SaveFormat.Name = "GBox_SaveFormat";
            this.GBox_SaveFormat.Size = new System.Drawing.Size(200, 41);
            this.GBox_SaveFormat.TabIndex = 4;
            this.GBox_SaveFormat.TabStop = false;
            this.GBox_SaveFormat.Text = "保存フォーマット";
            // 
            // CBox_SaveFormat
            // 
            this.CBox_SaveFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_SaveFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_SaveFormat.FormattingEnabled = true;
            this.CBox_SaveFormat.Location = new System.Drawing.Point(3, 15);
            this.CBox_SaveFormat.Name = "CBox_SaveFormat";
            this.CBox_SaveFormat.Size = new System.Drawing.Size(194, 20);
            this.CBox_SaveFormat.TabIndex = 0;
            // 
            // GBox_SaveDir
            // 
            this.GBox_SaveDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_SaveDir.Controls.Add(this.Btn_SaveDir_Ref);
            this.GBox_SaveDir.Controls.Add(this.TBox_SaveDir);
            this.GBox_SaveDir.Location = new System.Drawing.Point(6, 65);
            this.GBox_SaveDir.Name = "GBox_SaveDir";
            this.GBox_SaveDir.Size = new System.Drawing.Size(502, 49);
            this.GBox_SaveDir.TabIndex = 3;
            this.GBox_SaveDir.TabStop = false;
            this.GBox_SaveDir.Text = "保存ディレクトリ";
            // 
            // Btn_SaveDir_Ref
            // 
            this.Btn_SaveDir_Ref.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SaveDir_Ref.Location = new System.Drawing.Point(440, 18);
            this.Btn_SaveDir_Ref.Name = "Btn_SaveDir_Ref";
            this.Btn_SaveDir_Ref.Size = new System.Drawing.Size(56, 20);
            this.Btn_SaveDir_Ref.TabIndex = 1;
            this.Btn_SaveDir_Ref.Text = "参照";
            this.Btn_SaveDir_Ref.UseVisualStyleBackColor = true;
            this.Btn_SaveDir_Ref.Click += new System.EventHandler(this.Btn_SaveDir_Ref_Click);
            // 
            // TBox_SaveDir
            // 
            this.TBox_SaveDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_SaveDir.Location = new System.Drawing.Point(3, 19);
            this.TBox_SaveDir.Name = "TBox_SaveDir";
            this.TBox_SaveDir.Size = new System.Drawing.Size(431, 19);
            this.TBox_SaveDir.TabIndex = 0;
            // 
            // GBox_Timing
            // 
            this.GBox_Timing.Controls.Add(this.Num_PacketCount);
            this.GBox_Timing.Controls.Add(this.Num_FileSize);
            this.GBox_Timing.Controls.Add(this.Num_Interval);
            this.GBox_Timing.Controls.Add(this.RBtn_Timing_PacketCount);
            this.GBox_Timing.Controls.Add(this.RBtn_Timing_FileSize);
            this.GBox_Timing.Controls.Add(this.RBtn_Timing_Interval);
            this.GBox_Timing.Location = new System.Drawing.Point(3, 130);
            this.GBox_Timing.Name = "GBox_Timing";
            this.GBox_Timing.Size = new System.Drawing.Size(325, 91);
            this.GBox_Timing.TabIndex = 5;
            this.GBox_Timing.TabStop = false;
            this.GBox_Timing.Text = "保存タイミング";
            // 
            // Num_PacketCount
            // 
            this.Num_PacketCount.Location = new System.Drawing.Point(215, 65);
            this.Num_PacketCount.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.Num_PacketCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_PacketCount.Name = "Num_PacketCount";
            this.Num_PacketCount.Size = new System.Drawing.Size(100, 19);
            this.Num_PacketCount.TabIndex = 10;
            this.Num_PacketCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_PacketCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Num_FileSize
            // 
            this.Num_FileSize.Location = new System.Drawing.Point(215, 40);
            this.Num_FileSize.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.Num_FileSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_FileSize.Name = "Num_FileSize";
            this.Num_FileSize.Size = new System.Drawing.Size(100, 19);
            this.Num_FileSize.TabIndex = 9;
            this.Num_FileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_FileSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Num_Interval
            // 
            this.Num_Interval.Location = new System.Drawing.Point(215, 15);
            this.Num_Interval.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.Num_Interval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_Interval.Name = "Num_Interval";
            this.Num_Interval.Size = new System.Drawing.Size(100, 19);
            this.Num_Interval.TabIndex = 8;
            this.Num_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_Interval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RBtn_Timing_PacketCount
            // 
            this.RBtn_Timing_PacketCount.Location = new System.Drawing.Point(9, 65);
            this.RBtn_Timing_PacketCount.Name = "RBtn_Timing_PacketCount";
            this.RBtn_Timing_PacketCount.Size = new System.Drawing.Size(200, 16);
            this.RBtn_Timing_PacketCount.TabIndex = 7;
            this.RBtn_Timing_PacketCount.TabStop = true;
            this.RBtn_Timing_PacketCount.Text = "パケット数";
            this.RBtn_Timing_PacketCount.UseVisualStyleBackColor = true;
            // 
            // RBtn_Timing_FileSize
            // 
            this.RBtn_Timing_FileSize.Location = new System.Drawing.Point(9, 40);
            this.RBtn_Timing_FileSize.Name = "RBtn_Timing_FileSize";
            this.RBtn_Timing_FileSize.Size = new System.Drawing.Size(200, 16);
            this.RBtn_Timing_FileSize.TabIndex = 6;
            this.RBtn_Timing_FileSize.TabStop = true;
            this.RBtn_Timing_FileSize.Text = "ファイルサイズ(kByte)";
            this.RBtn_Timing_FileSize.UseVisualStyleBackColor = true;
            // 
            // RBtn_Timing_Interval
            // 
            this.RBtn_Timing_Interval.Location = new System.Drawing.Point(9, 15);
            this.RBtn_Timing_Interval.Name = "RBtn_Timing_Interval";
            this.RBtn_Timing_Interval.Size = new System.Drawing.Size(200, 16);
            this.RBtn_Timing_Interval.TabIndex = 5;
            this.RBtn_Timing_Interval.TabStop = true;
            this.RBtn_Timing_Interval.Text = "時間間隔(分)";
            this.RBtn_Timing_Interval.UseVisualStyleBackColor = true;
            // 
            // ConfigPage_AutoSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_Timing);
            this.Controls.Add(this.GBox_Basic);
            this.Name = "ConfigPage_AutoSave";
            this.Size = new System.Drawing.Size(525, 227);
            this.GBox_SavePrefix.ResumeLayout(false);
            this.GBox_SavePrefix.PerformLayout();
            this.GBox_Basic.ResumeLayout(false);
            this.GBox_SaveFormat.ResumeLayout(false);
            this.GBox_SaveDir.ResumeLayout(false);
            this.GBox_SaveDir.PerformLayout();
            this.GBox_Timing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_FileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Interval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_SavePrefix;
        private System.Windows.Forms.TextBox TBox_SavePrefix;
        private System.Windows.Forms.GroupBox GBox_Basic;
        private System.Windows.Forms.GroupBox GBox_SaveFormat;
        private System.Windows.Forms.ComboBox CBox_SaveFormat;
        private System.Windows.Forms.GroupBox GBox_SaveDir;
        private System.Windows.Forms.Button Btn_SaveDir_Ref;
        private System.Windows.Forms.TextBox TBox_SaveDir;
        private System.Windows.Forms.GroupBox GBox_Timing;
        private System.Windows.Forms.NumericUpDown Num_PacketCount;
        private System.Windows.Forms.NumericUpDown Num_FileSize;
        private System.Windows.Forms.NumericUpDown Num_Interval;
        private System.Windows.Forms.RadioButton RBtn_Timing_PacketCount;
        private System.Windows.Forms.RadioButton RBtn_Timing_FileSize;
        private System.Windows.Forms.RadioButton RBtn_Timing_Interval;
    }
}
