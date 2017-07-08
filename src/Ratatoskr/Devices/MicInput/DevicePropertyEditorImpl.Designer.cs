namespace Ratatoskr.Devices.MicInput
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
            this.GBox_SamplingRate = new System.Windows.Forms.GroupBox();
            this.CBox_SamplingRate = new System.Windows.Forms.ComboBox();
            this.GBox_BitsPerSample = new System.Windows.Forms.GroupBox();
            this.CBox_BitsPerSample = new System.Windows.Forms.ComboBox();
            this.GBox_ChannelNum = new System.Windows.Forms.GroupBox();
            this.CBox_ChannelNum = new System.Windows.Forms.ComboBox();
            this.GBox_InputDeviceList = new System.Windows.Forms.GroupBox();
            this.CBox_InputDeviceList = new System.Windows.Forms.ComboBox();
            this.GBox_SamplingRate.SuspendLayout();
            this.GBox_BitsPerSample.SuspendLayout();
            this.GBox_ChannelNum.SuspendLayout();
            this.GBox_InputDeviceList.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_SamplingRate
            // 
            this.GBox_SamplingRate.Controls.Add(this.CBox_SamplingRate);
            this.GBox_SamplingRate.Location = new System.Drawing.Point(4, 52);
            this.GBox_SamplingRate.Name = "GBox_SamplingRate";
            this.GBox_SamplingRate.Size = new System.Drawing.Size(200, 42);
            this.GBox_SamplingRate.TabIndex = 0;
            this.GBox_SamplingRate.TabStop = false;
            this.GBox_SamplingRate.Text = "サンプリングレート(Hz)";
            // 
            // CBox_SamplingRate
            // 
            this.CBox_SamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_SamplingRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_SamplingRate.FormattingEnabled = true;
            this.CBox_SamplingRate.Location = new System.Drawing.Point(3, 15);
            this.CBox_SamplingRate.Name = "CBox_SamplingRate";
            this.CBox_SamplingRate.Size = new System.Drawing.Size(194, 20);
            this.CBox_SamplingRate.TabIndex = 0;
            // 
            // GBox_BitsPerSample
            // 
            this.GBox_BitsPerSample.Controls.Add(this.CBox_BitsPerSample);
            this.GBox_BitsPerSample.Location = new System.Drawing.Point(4, 100);
            this.GBox_BitsPerSample.Name = "GBox_BitsPerSample";
            this.GBox_BitsPerSample.Size = new System.Drawing.Size(200, 42);
            this.GBox_BitsPerSample.TabIndex = 1;
            this.GBox_BitsPerSample.TabStop = false;
            this.GBox_BitsPerSample.Text = "量子化ビット数";
            // 
            // CBox_BitsPerSample
            // 
            this.CBox_BitsPerSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_BitsPerSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_BitsPerSample.FormattingEnabled = true;
            this.CBox_BitsPerSample.Location = new System.Drawing.Point(3, 15);
            this.CBox_BitsPerSample.Name = "CBox_BitsPerSample";
            this.CBox_BitsPerSample.Size = new System.Drawing.Size(194, 20);
            this.CBox_BitsPerSample.TabIndex = 0;
            // 
            // GBox_ChannelNum
            // 
            this.GBox_ChannelNum.Controls.Add(this.CBox_ChannelNum);
            this.GBox_ChannelNum.Location = new System.Drawing.Point(4, 148);
            this.GBox_ChannelNum.Name = "GBox_ChannelNum";
            this.GBox_ChannelNum.Size = new System.Drawing.Size(200, 42);
            this.GBox_ChannelNum.TabIndex = 2;
            this.GBox_ChannelNum.TabStop = false;
            this.GBox_ChannelNum.Text = "チャンネル数";
            // 
            // CBox_ChannelNum
            // 
            this.CBox_ChannelNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ChannelNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ChannelNum.FormattingEnabled = true;
            this.CBox_ChannelNum.Location = new System.Drawing.Point(3, 15);
            this.CBox_ChannelNum.Name = "CBox_ChannelNum";
            this.CBox_ChannelNum.Size = new System.Drawing.Size(194, 20);
            this.CBox_ChannelNum.TabIndex = 0;
            // 
            // GBox_InputDeviceList
            // 
            this.GBox_InputDeviceList.Controls.Add(this.CBox_InputDeviceList);
            this.GBox_InputDeviceList.Location = new System.Drawing.Point(4, 4);
            this.GBox_InputDeviceList.Name = "GBox_InputDeviceList";
            this.GBox_InputDeviceList.Size = new System.Drawing.Size(407, 42);
            this.GBox_InputDeviceList.TabIndex = 5;
            this.GBox_InputDeviceList.TabStop = false;
            this.GBox_InputDeviceList.Text = "音声入力デバイス";
            // 
            // CBox_InputDeviceList
            // 
            this.CBox_InputDeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_InputDeviceList.FormattingEnabled = true;
            this.CBox_InputDeviceList.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputDeviceList.Name = "CBox_InputDeviceList";
            this.CBox_InputDeviceList.Size = new System.Drawing.Size(401, 20);
            this.CBox_InputDeviceList.TabIndex = 0;
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_InputDeviceList);
            this.Controls.Add(this.GBox_ChannelNum);
            this.Controls.Add(this.GBox_BitsPerSample);
            this.Controls.Add(this.GBox_SamplingRate);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(418, 248);
            this.GBox_SamplingRate.ResumeLayout(false);
            this.GBox_BitsPerSample.ResumeLayout(false);
            this.GBox_ChannelNum.ResumeLayout(false);
            this.GBox_InputDeviceList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_SamplingRate;
        private System.Windows.Forms.ComboBox CBox_SamplingRate;
        private System.Windows.Forms.GroupBox GBox_BitsPerSample;
        private System.Windows.Forms.ComboBox CBox_BitsPerSample;
        private System.Windows.Forms.GroupBox GBox_ChannelNum;
        private System.Windows.Forms.ComboBox CBox_ChannelNum;
        private System.Windows.Forms.GroupBox GBox_InputDeviceList;
        private System.Windows.Forms.ComboBox CBox_InputDeviceList;
    }
}
