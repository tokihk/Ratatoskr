﻿namespace Ratatoskr.Device.AudioDevice
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
            this.GBox_InputSamplingRate = new System.Windows.Forms.GroupBox();
            this.CBox_InputSamplingRate = new System.Windows.Forms.ComboBox();
            this.GBox_InputBitsPerSample = new System.Windows.Forms.GroupBox();
            this.CBox_InputBitsPerSample = new System.Windows.Forms.ComboBox();
            this.GBox_InputChannelNum = new System.Windows.Forms.GroupBox();
            this.CBox_InputChannelNum = new System.Windows.Forms.ComboBox();
            this.GBox_InputDeviceList = new System.Windows.Forms.GroupBox();
            this.CBox_InputDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChkBox_InputEnable = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.CBox_OutputVolume = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CBox_OutputSamplingRate = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CBox_OutputBitsPerSample = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.CBox_OutputChannelNum = new System.Windows.Forms.ComboBox();
            this.GBox_OutputDeviceList = new System.Windows.Forms.GroupBox();
            this.CBox_OutputDeviceList = new System.Windows.Forms.ComboBox();
            this.ChkBox_OutputEnable = new System.Windows.Forms.CheckBox();
            this.GBox_InputSamplingRate.SuspendLayout();
            this.GBox_InputBitsPerSample.SuspendLayout();
            this.GBox_InputChannelNum.SuspendLayout();
            this.GBox_InputDeviceList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.GBox_OutputDeviceList.SuspendLayout();
            this.SuspendLayout();
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
            this.CBox_InputSamplingRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.CBox_InputBitsPerSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.CBox_InputChannelNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_InputChannelNum.FormattingEnabled = true;
            this.CBox_InputChannelNum.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputChannelNum.Name = "CBox_InputChannelNum";
            this.CBox_InputChannelNum.Size = new System.Drawing.Size(74, 20);
            this.CBox_InputChannelNum.TabIndex = 0;
            // 
            // GBox_InputDeviceList
            // 
            this.GBox_InputDeviceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_InputDeviceList.Controls.Add(this.CBox_InputDeviceList);
            this.GBox_InputDeviceList.Location = new System.Drawing.Point(6, 18);
            this.GBox_InputDeviceList.Name = "GBox_InputDeviceList";
            this.GBox_InputDeviceList.Size = new System.Drawing.Size(434, 42);
            this.GBox_InputDeviceList.TabIndex = 5;
            this.GBox_InputDeviceList.TabStop = false;
            this.GBox_InputDeviceList.Text = "Input device";
            // 
            // CBox_InputDeviceList
            // 
            this.CBox_InputDeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_InputDeviceList.FormattingEnabled = true;
            this.CBox_InputDeviceList.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputDeviceList.Name = "CBox_InputDeviceList";
            this.CBox_InputDeviceList.Size = new System.Drawing.Size(428, 20);
            this.CBox_InputDeviceList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.GBox_InputDeviceList);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 140);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.GBox_InputSamplingRate);
            this.groupBox3.Controls.Add(this.GBox_InputBitsPerSample);
            this.groupBox3.Controls.Add(this.GBox_InputChannelNum);
            this.groupBox3.Location = new System.Drawing.Point(6, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(384, 67);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Input PCM format";
            // 
            // ChkBox_InputEnable
            // 
            this.ChkBox_InputEnable.AutoSize = true;
            this.ChkBox_InputEnable.Location = new System.Drawing.Point(12, 0);
            this.ChkBox_InputEnable.Name = "ChkBox_InputEnable";
            this.ChkBox_InputEnable.Size = new System.Drawing.Size(88, 16);
            this.ChkBox_InputEnable.TabIndex = 6;
            this.ChkBox_InputEnable.Text = "Input setting";
            this.ChkBox_InputEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.GBox_OutputDeviceList);
            this.groupBox2.Location = new System.Drawing.Point(3, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 182);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.CBox_OutputVolume);
            this.groupBox8.Location = new System.Drawing.Point(6, 132);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(120, 42);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Output volume (%)";
            // 
            // comboBox4
            // 
            this.CBox_OutputVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_OutputVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_OutputVolume.FormattingEnabled = true;
            this.CBox_OutputVolume.Location = new System.Drawing.Point(3, 15);
            this.CBox_OutputVolume.Name = "comboBox4";
            this.CBox_OutputVolume.Size = new System.Drawing.Size(114, 20);
            this.CBox_OutputVolume.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Location = new System.Drawing.Point(6, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(384, 66);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output PCM format";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.CBox_OutputSamplingRate);
            this.groupBox5.Location = new System.Drawing.Point(6, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(140, 42);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Sampling bit rate (Hz)";
            // 
            // comboBox1
            // 
            this.CBox_OutputSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_OutputSamplingRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_OutputSamplingRate.FormattingEnabled = true;
            this.CBox_OutputSamplingRate.Location = new System.Drawing.Point(3, 15);
            this.CBox_OutputSamplingRate.Name = "comboBox1";
            this.CBox_OutputSamplingRate.Size = new System.Drawing.Size(134, 20);
            this.CBox_OutputSamplingRate.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CBox_OutputBitsPerSample);
            this.groupBox6.Location = new System.Drawing.Point(152, 18);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(140, 42);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Quantization bit rate";
            // 
            // comboBox2
            // 
            this.CBox_OutputBitsPerSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_OutputBitsPerSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_OutputBitsPerSample.FormattingEnabled = true;
            this.CBox_OutputBitsPerSample.Location = new System.Drawing.Point(3, 15);
            this.CBox_OutputBitsPerSample.Name = "comboBox2";
            this.CBox_OutputBitsPerSample.Size = new System.Drawing.Size(134, 20);
            this.CBox_OutputBitsPerSample.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.CBox_OutputChannelNum);
            this.groupBox7.Location = new System.Drawing.Point(298, 18);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(80, 42);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Channel";
            // 
            // comboBox3
            // 
            this.CBox_OutputChannelNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_OutputChannelNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_OutputChannelNum.FormattingEnabled = true;
            this.CBox_OutputChannelNum.Location = new System.Drawing.Point(3, 15);
            this.CBox_OutputChannelNum.Name = "comboBox3";
            this.CBox_OutputChannelNum.Size = new System.Drawing.Size(74, 20);
            this.CBox_OutputChannelNum.TabIndex = 0;
            // 
            // GBox_OutputDeviceList
            // 
            this.GBox_OutputDeviceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_OutputDeviceList.Controls.Add(this.CBox_OutputDeviceList);
            this.GBox_OutputDeviceList.Location = new System.Drawing.Point(6, 18);
            this.GBox_OutputDeviceList.Name = "GBox_OutputDeviceList";
            this.GBox_OutputDeviceList.Size = new System.Drawing.Size(434, 42);
            this.GBox_OutputDeviceList.TabIndex = 5;
            this.GBox_OutputDeviceList.TabStop = false;
            this.GBox_OutputDeviceList.Text = "Output device";
            // 
            // CBox_OutputDeviceList
            // 
            this.CBox_OutputDeviceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_OutputDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_OutputDeviceList.FormattingEnabled = true;
            this.CBox_OutputDeviceList.Location = new System.Drawing.Point(3, 15);
            this.CBox_OutputDeviceList.Name = "CBox_OutputDeviceList";
            this.CBox_OutputDeviceList.Size = new System.Drawing.Size(428, 20);
            this.CBox_OutputDeviceList.TabIndex = 0;
            // 
            // ChkBox_OutputEnable
            // 
            this.ChkBox_OutputEnable.AutoSize = true;
            this.ChkBox_OutputEnable.Location = new System.Drawing.Point(12, 147);
            this.ChkBox_OutputEnable.Name = "ChkBox_OutputEnable";
            this.ChkBox_OutputEnable.Size = new System.Drawing.Size(97, 16);
            this.ChkBox_OutputEnable.TabIndex = 8;
            this.ChkBox_OutputEnable.Text = "Output setting";
            this.ChkBox_OutputEnable.UseVisualStyleBackColor = true;
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChkBox_InputEnable);
            this.Controls.Add(this.ChkBox_OutputEnable);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(452, 336);
            this.GBox_InputSamplingRate.ResumeLayout(false);
            this.GBox_InputBitsPerSample.ResumeLayout(false);
            this.GBox_InputChannelNum.ResumeLayout(false);
            this.GBox_InputDeviceList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.GBox_OutputDeviceList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_InputSamplingRate;
        private System.Windows.Forms.ComboBox CBox_InputSamplingRate;
        private System.Windows.Forms.GroupBox GBox_InputBitsPerSample;
        private System.Windows.Forms.ComboBox CBox_InputBitsPerSample;
        private System.Windows.Forms.GroupBox GBox_InputChannelNum;
        private System.Windows.Forms.ComboBox CBox_InputChannelNum;
        private System.Windows.Forms.GroupBox GBox_InputDeviceList;
        private System.Windows.Forms.ComboBox CBox_InputDeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox GBox_OutputDeviceList;
        private System.Windows.Forms.ComboBox CBox_OutputDeviceList;
        private System.Windows.Forms.CheckBox ChkBox_InputEnable;
        private System.Windows.Forms.CheckBox ChkBox_OutputEnable;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox CBox_OutputSamplingRate;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox CBox_OutputBitsPerSample;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox CBox_OutputChannelNum;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox CBox_OutputVolume;
    }
}
