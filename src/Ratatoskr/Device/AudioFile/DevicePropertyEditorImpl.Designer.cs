namespace Ratatoskr.Device.AudioFile
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
            this.GBox_ConnectAction = new System.Windows.Forms.GroupBox();
            this.CBox_ConnectAction = new System.Windows.Forms.ComboBox();
            this.GBox_BitsPerSample = new System.Windows.Forms.GroupBox();
            this.Num_RepeatCount = new System.Windows.Forms.NumericUpDown();
            this.GBox_InputDeviceList = new System.Windows.Forms.GroupBox();
            this.TBox_InputFileInfo = new System.Windows.Forms.TextBox();
            this.Btn_InputFileSelect = new System.Windows.Forms.Button();
            this.TBox_InputFilePath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GBox_InputSamplingRate = new System.Windows.Forms.GroupBox();
            this.CBox_InputSamplingRate = new System.Windows.Forms.ComboBox();
            this.GBox_InputBitsPerSample = new System.Windows.Forms.GroupBox();
            this.CBox_InputBitsPerSample = new System.Windows.Forms.ComboBox();
            this.GBox_InputChannelNum = new System.Windows.Forms.GroupBox();
            this.CBox_InputChannelNum = new System.Windows.Forms.ComboBox();
            this.GBox_ConnectAction.SuspendLayout();
            this.GBox_BitsPerSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).BeginInit();
            this.GBox_InputDeviceList.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GBox_InputSamplingRate.SuspendLayout();
            this.GBox_InputBitsPerSample.SuspendLayout();
            this.GBox_InputChannelNum.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_ConnectAction
            // 
            this.GBox_ConnectAction.Controls.Add(this.CBox_ConnectAction);
            this.GBox_ConnectAction.Location = new System.Drawing.Point(162, 18);
            this.GBox_ConnectAction.Name = "GBox_ConnectAction";
            this.GBox_ConnectAction.Size = new System.Drawing.Size(120, 42);
            this.GBox_ConnectAction.TabIndex = 0;
            this.GBox_ConnectAction.TabStop = false;
            this.GBox_ConnectAction.Text = "Connect action";
            // 
            // CBox_ConnectAction
            // 
            this.CBox_ConnectAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ConnectAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ConnectAction.FormattingEnabled = true;
            this.CBox_ConnectAction.Location = new System.Drawing.Point(3, 15);
            this.CBox_ConnectAction.Name = "CBox_ConnectAction";
            this.CBox_ConnectAction.Size = new System.Drawing.Size(114, 20);
            this.CBox_ConnectAction.TabIndex = 0;
            // 
            // GBox_BitsPerSample
            // 
            this.GBox_BitsPerSample.Controls.Add(this.Num_RepeatCount);
            this.GBox_BitsPerSample.Location = new System.Drawing.Point(6, 18);
            this.GBox_BitsPerSample.Name = "GBox_BitsPerSample";
            this.GBox_BitsPerSample.Size = new System.Drawing.Size(150, 42);
            this.GBox_BitsPerSample.TabIndex = 1;
            this.GBox_BitsPerSample.TabStop = false;
            this.GBox_BitsPerSample.Text = "Repeat count (0=Infinite)";
            // 
            // Num_RepeatCount
            // 
            this.Num_RepeatCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_RepeatCount.Location = new System.Drawing.Point(3, 15);
            this.Num_RepeatCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_RepeatCount.Name = "Num_RepeatCount";
            this.Num_RepeatCount.Size = new System.Drawing.Size(144, 19);
            this.Num_RepeatCount.TabIndex = 0;
            this.Num_RepeatCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RepeatCount.ThousandsSeparator = true;
            // 
            // GBox_InputDeviceList
            // 
            this.GBox_InputDeviceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_InputDeviceList.Controls.Add(this.TBox_InputFileInfo);
            this.GBox_InputDeviceList.Controls.Add(this.Btn_InputFileSelect);
            this.GBox_InputDeviceList.Controls.Add(this.TBox_InputFilePath);
            this.GBox_InputDeviceList.Location = new System.Drawing.Point(4, 4);
            this.GBox_InputDeviceList.Name = "GBox_InputDeviceList";
            this.GBox_InputDeviceList.Size = new System.Drawing.Size(385, 120);
            this.GBox_InputDeviceList.TabIndex = 5;
            this.GBox_InputDeviceList.TabStop = false;
            this.GBox_InputDeviceList.Text = "Audio data file";
            // 
            // TBox_InputFileInfo
            // 
            this.TBox_InputFileInfo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_InputFileInfo.Location = new System.Drawing.Point(3, 45);
            this.TBox_InputFileInfo.Multiline = true;
            this.TBox_InputFileInfo.Name = "TBox_InputFileInfo";
            this.TBox_InputFileInfo.ReadOnly = true;
            this.TBox_InputFileInfo.Size = new System.Drawing.Size(320, 69);
            this.TBox_InputFileInfo.TabIndex = 2;
            // 
            // Btn_InputFileSelect
            // 
            this.Btn_InputFileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_InputFileSelect.Location = new System.Drawing.Point(319, 18);
            this.Btn_InputFileSelect.Name = "Btn_InputFileSelect";
            this.Btn_InputFileSelect.Size = new System.Drawing.Size(60, 20);
            this.Btn_InputFileSelect.TabIndex = 1;
            this.Btn_InputFileSelect.Text = "Browse...";
            this.Btn_InputFileSelect.UseVisualStyleBackColor = true;
            this.Btn_InputFileSelect.Click += new System.EventHandler(this.Btn_InputFileSelect_Click);
            // 
            // TBox_InputFilePath
            // 
            this.TBox_InputFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_InputFilePath.Location = new System.Drawing.Point(3, 19);
            this.TBox_InputFilePath.Name = "TBox_InputFilePath";
            this.TBox_InputFilePath.Size = new System.Drawing.Size(310, 19);
            this.TBox_InputFilePath.TabIndex = 0;
            this.TBox_InputFilePath.TextChanged += new System.EventHandler(this.TBox_InputFilePath_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.GBox_BitsPerSample);
            this.groupBox3.Controls.Add(this.GBox_ConnectAction);
            this.groupBox3.Location = new System.Drawing.Point(4, 203);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 68);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Play setting";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GBox_InputSamplingRate);
            this.groupBox4.Controls.Add(this.GBox_InputBitsPerSample);
            this.groupBox4.Controls.Add(this.GBox_InputChannelNum);
            this.groupBox4.Location = new System.Drawing.Point(4, 130);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(384, 67);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Input PCM format";
            // 
            // GBox_InputSamplingRate
            // 
            this.GBox_InputSamplingRate.Controls.Add(this.CBox_InputSamplingRate);
            this.GBox_InputSamplingRate.Location = new System.Drawing.Point(6, 18);
            this.GBox_InputSamplingRate.Name = "GBox_InputSamplingRate";
            this.GBox_InputSamplingRate.Size = new System.Drawing.Size(140, 42);
            this.GBox_InputSamplingRate.TabIndex = 0;
            this.GBox_InputSamplingRate.TabStop = false;
            this.GBox_InputSamplingRate.Text = "Sampling bit rate (Hz)";
            // 
            // CBox_InputSamplingRate
            // 
            this.CBox_InputSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputSamplingRate.FormattingEnabled = true;
            this.CBox_InputSamplingRate.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputSamplingRate.Name = "CBox_InputSamplingRate";
            this.CBox_InputSamplingRate.Size = new System.Drawing.Size(134, 20);
            this.CBox_InputSamplingRate.TabIndex = 0;
            // 
            // GBox_InputBitsPerSample
            // 
            this.GBox_InputBitsPerSample.Controls.Add(this.CBox_InputBitsPerSample);
            this.GBox_InputBitsPerSample.Location = new System.Drawing.Point(152, 18);
            this.GBox_InputBitsPerSample.Name = "GBox_InputBitsPerSample";
            this.GBox_InputBitsPerSample.Size = new System.Drawing.Size(140, 42);
            this.GBox_InputBitsPerSample.TabIndex = 1;
            this.GBox_InputBitsPerSample.TabStop = false;
            this.GBox_InputBitsPerSample.Text = "Quantization bit rate";
            // 
            // CBox_InputBitsPerSample
            // 
            this.CBox_InputBitsPerSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputBitsPerSample.FormattingEnabled = true;
            this.CBox_InputBitsPerSample.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputBitsPerSample.Name = "CBox_InputBitsPerSample";
            this.CBox_InputBitsPerSample.Size = new System.Drawing.Size(134, 20);
            this.CBox_InputBitsPerSample.TabIndex = 0;
            // 
            // GBox_InputChannelNum
            // 
            this.GBox_InputChannelNum.Controls.Add(this.CBox_InputChannelNum);
            this.GBox_InputChannelNum.Location = new System.Drawing.Point(298, 18);
            this.GBox_InputChannelNum.Name = "GBox_InputChannelNum";
            this.GBox_InputChannelNum.Size = new System.Drawing.Size(80, 42);
            this.GBox_InputChannelNum.TabIndex = 2;
            this.GBox_InputChannelNum.TabStop = false;
            this.GBox_InputChannelNum.Text = "Channel";
            // 
            // CBox_InputChannelNum
            // 
            this.CBox_InputChannelNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputChannelNum.FormattingEnabled = true;
            this.CBox_InputChannelNum.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputChannelNum.Name = "CBox_InputChannelNum";
            this.CBox_InputChannelNum.Size = new System.Drawing.Size(74, 20);
            this.CBox_InputChannelNum.TabIndex = 0;
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.GBox_InputDeviceList);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(396, 277);
            this.GBox_ConnectAction.ResumeLayout(false);
            this.GBox_BitsPerSample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).EndInit();
            this.GBox_InputDeviceList.ResumeLayout(false);
            this.GBox_InputDeviceList.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.GBox_InputSamplingRate.ResumeLayout(false);
            this.GBox_InputBitsPerSample.ResumeLayout(false);
            this.GBox_InputChannelNum.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_ConnectAction;
        private System.Windows.Forms.ComboBox CBox_ConnectAction;
        private System.Windows.Forms.GroupBox GBox_BitsPerSample;
        private System.Windows.Forms.GroupBox GBox_InputDeviceList;
        private System.Windows.Forms.Button Btn_InputFileSelect;
        private System.Windows.Forms.TextBox TBox_InputFilePath;
        private System.Windows.Forms.NumericUpDown Num_RepeatCount;
        private System.Windows.Forms.TextBox TBox_InputFileInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox GBox_InputSamplingRate;
        private System.Windows.Forms.ComboBox CBox_InputSamplingRate;
        private System.Windows.Forms.GroupBox GBox_InputBitsPerSample;
        private System.Windows.Forms.ComboBox CBox_InputBitsPerSample;
        private System.Windows.Forms.GroupBox GBox_InputChannelNum;
        private System.Windows.Forms.ComboBox CBox_InputChannelNum;
    }
}
