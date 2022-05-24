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
			this.CBox_ConnectAction = new System.Windows.Forms.ComboBox();
			this.Num_RepeatCount = new System.Windows.Forms.NumericUpDown();
			this.GBox_InputDeviceList = new System.Windows.Forms.GroupBox();
			this.TBox_InputFileInfo = new System.Windows.Forms.TextBox();
			this.Btn_InputFileSelect = new System.Windows.Forms.Button();
			this.TBox_InputFilePath = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.CBox_InputChannelNum = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.CBox_InputBitsPerSample = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CBox_InputSamplingRate = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).BeginInit();
			this.GBox_InputDeviceList.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// CBox_ConnectAction
			// 
			this.CBox_ConnectAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_ConnectAction.FormattingEnabled = true;
			this.CBox_ConnectAction.Location = new System.Drawing.Point(163, 52);
			this.CBox_ConnectAction.Name = "CBox_ConnectAction";
			this.CBox_ConnectAction.Size = new System.Drawing.Size(104, 20);
			this.CBox_ConnectAction.TabIndex = 0;
			// 
			// Num_RepeatCount
			// 
			this.Num_RepeatCount.Location = new System.Drawing.Point(163, 26);
			this.Num_RepeatCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.Num_RepeatCount.Name = "Num_RepeatCount";
			this.Num_RepeatCount.Size = new System.Drawing.Size(60, 19);
			this.Num_RepeatCount.TabIndex = 0;
			this.Num_RepeatCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.CBox_ConnectAction);
			this.groupBox3.Controls.Add(this.Num_RepeatCount);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(4, 238);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(282, 84);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Play setting";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 51);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(150, 21);
			this.label5.TabIndex = 14;
			this.label5.Text = "Connect action";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 21);
			this.label4.TabIndex = 13;
			this.label4.Text = "Repeat count (0=Infinite)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.CBox_InputChannelNum);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.CBox_InputBitsPerSample);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.CBox_InputSamplingRate);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Location = new System.Drawing.Point(4, 130);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(282, 102);
			this.groupBox4.TabIndex = 11;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Input PCM format";
			// 
			// CBox_InputChannelNum
			// 
			this.CBox_InputChannelNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_InputChannelNum.FormattingEnabled = true;
			this.CBox_InputChannelNum.Location = new System.Drawing.Point(133, 70);
			this.CBox_InputChannelNum.Name = "CBox_InputChannelNum";
			this.CBox_InputChannelNum.Size = new System.Drawing.Size(134, 20);
			this.CBox_InputChannelNum.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 21);
			this.label3.TabIndex = 15;
			this.label3.Text = "Channel Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CBox_InputBitsPerSample
			// 
			this.CBox_InputBitsPerSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_InputBitsPerSample.FormattingEnabled = true;
			this.CBox_InputBitsPerSample.Location = new System.Drawing.Point(133, 44);
			this.CBox_InputBitsPerSample.Name = "CBox_InputBitsPerSample";
			this.CBox_InputBitsPerSample.Size = new System.Drawing.Size(134, 20);
			this.CBox_InputBitsPerSample.TabIndex = 14;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 21);
			this.label2.TabIndex = 13;
			this.label2.Text = "Quantization bit rate";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CBox_InputSamplingRate
			// 
			this.CBox_InputSamplingRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_InputSamplingRate.FormattingEnabled = true;
			this.CBox_InputSamplingRate.Location = new System.Drawing.Point(133, 18);
			this.CBox_InputSamplingRate.Name = "CBox_InputSamplingRate";
			this.CBox_InputSamplingRate.Size = new System.Drawing.Size(134, 20);
			this.CBox_InputSamplingRate.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 21);
			this.label1.TabIndex = 12;
			this.label1.Text = "Sampling rate";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DevicePropertyEditorImpl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.GBox_InputDeviceList);
			this.Name = "DevicePropertyEditorImpl";
			this.Size = new System.Drawing.Size(396, 331);
			((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).EndInit();
			this.GBox_InputDeviceList.ResumeLayout(false);
			this.GBox_InputDeviceList.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CBox_ConnectAction;
        private System.Windows.Forms.GroupBox GBox_InputDeviceList;
        private System.Windows.Forms.Button Btn_InputFileSelect;
        private System.Windows.Forms.TextBox TBox_InputFilePath;
        private System.Windows.Forms.NumericUpDown Num_RepeatCount;
        private System.Windows.Forms.TextBox TBox_InputFileInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox CBox_InputSamplingRate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox CBox_InputChannelNum;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox CBox_InputBitsPerSample;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
