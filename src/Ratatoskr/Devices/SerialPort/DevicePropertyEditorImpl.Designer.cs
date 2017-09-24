namespace Ratatoskr.Devices.SerialPort
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
            this.GBox_BaudRate = new System.Windows.Forms.GroupBox();
            this.CBox_BaudRate = new System.Windows.Forms.ComboBox();
            this.GBox_Parity = new System.Windows.Forms.GroupBox();
            this.CBox_Parity = new System.Windows.Forms.ComboBox();
            this.GBox_DataBits = new System.Windows.Forms.GroupBox();
            this.CBox_DataBits = new System.Windows.Forms.ComboBox();
            this.GBox_StopBits = new System.Windows.Forms.GroupBox();
            this.CBox_StopBits = new System.Windows.Forms.ComboBox();
            this.GBox_FlowControl = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.GBox_XoffLim = new System.Windows.Forms.GroupBox();
            this.Num_XoffLim = new System.Windows.Forms.NumericUpDown();
            this.GBox_XonLim = new System.Windows.Forms.GroupBox();
            this.Num_XonLim = new System.Windows.Forms.NumericUpDown();
            this.GBox_XoffChar = new System.Windows.Forms.GroupBox();
            this.ChkBox_fTXContinueOnXoff = new System.Windows.Forms.CheckBox();
            this.GBox_XonChar = new System.Windows.Forms.GroupBox();
            this.ChkBox_fInX = new System.Windows.Forms.CheckBox();
            this.ChkBox_fOutX = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChkBox_fDsrSensitivity = new System.Windows.Forms.CheckBox();
            this.GBox_fDtrControl = new System.Windows.Forms.GroupBox();
            this.CBox_fDtrControl = new System.Windows.Forms.ComboBox();
            this.ChkBox_fOutxDsrFlow = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GBox_fRtsControl = new System.Windows.Forms.GroupBox();
            this.CBox_fRtsControl = new System.Windows.Forms.ComboBox();
            this.ChkBox_fOutxCtsFlow = new System.Windows.Forms.CheckBox();
            this.GBox_PortList = new System.Windows.Forms.GroupBox();
            this.CBox_PortList = new System.Windows.Forms.ComboBox();
            this.Num_XonChar = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Num_XoffChar = new System.Windows.Forms.NumericUpDown();
            this.GBox_BaudRate.SuspendLayout();
            this.GBox_Parity.SuspendLayout();
            this.GBox_DataBits.SuspendLayout();
            this.GBox_StopBits.SuspendLayout();
            this.GBox_FlowControl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GBox_XoffLim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_XoffLim)).BeginInit();
            this.GBox_XonLim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_XonLim)).BeginInit();
            this.GBox_XoffChar.SuspendLayout();
            this.GBox_XonChar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GBox_fDtrControl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GBox_fRtsControl.SuspendLayout();
            this.GBox_PortList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_XonChar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_XoffChar)).BeginInit();
            this.SuspendLayout();
            // 
            // GBox_BaudRate
            // 
            this.GBox_BaudRate.Controls.Add(this.CBox_BaudRate);
            this.GBox_BaudRate.Location = new System.Drawing.Point(4, 52);
            this.GBox_BaudRate.Name = "GBox_BaudRate";
            this.GBox_BaudRate.Size = new System.Drawing.Size(200, 42);
            this.GBox_BaudRate.TabIndex = 0;
            this.GBox_BaudRate.TabStop = false;
            this.GBox_BaudRate.Text = "Baudrate (bps)";
            // 
            // CBox_BaudRate
            // 
            this.CBox_BaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_BaudRate.FormattingEnabled = true;
            this.CBox_BaudRate.Location = new System.Drawing.Point(3, 15);
            this.CBox_BaudRate.Name = "CBox_BaudRate";
            this.CBox_BaudRate.Size = new System.Drawing.Size(194, 20);
            this.CBox_BaudRate.TabIndex = 0;
            // 
            // GBox_Parity
            // 
            this.GBox_Parity.Controls.Add(this.CBox_Parity);
            this.GBox_Parity.Location = new System.Drawing.Point(4, 100);
            this.GBox_Parity.Name = "GBox_Parity";
            this.GBox_Parity.Size = new System.Drawing.Size(200, 42);
            this.GBox_Parity.TabIndex = 1;
            this.GBox_Parity.TabStop = false;
            this.GBox_Parity.Text = "Parity";
            // 
            // CBox_Parity
            // 
            this.CBox_Parity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_Parity.FormattingEnabled = true;
            this.CBox_Parity.Location = new System.Drawing.Point(3, 15);
            this.CBox_Parity.Name = "CBox_Parity";
            this.CBox_Parity.Size = new System.Drawing.Size(194, 20);
            this.CBox_Parity.TabIndex = 0;
            // 
            // GBox_DataBits
            // 
            this.GBox_DataBits.Controls.Add(this.CBox_DataBits);
            this.GBox_DataBits.Location = new System.Drawing.Point(4, 148);
            this.GBox_DataBits.Name = "GBox_DataBits";
            this.GBox_DataBits.Size = new System.Drawing.Size(200, 42);
            this.GBox_DataBits.TabIndex = 2;
            this.GBox_DataBits.TabStop = false;
            this.GBox_DataBits.Text = "Data bit";
            // 
            // CBox_DataBits
            // 
            this.CBox_DataBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DataBits.FormattingEnabled = true;
            this.CBox_DataBits.Location = new System.Drawing.Point(3, 15);
            this.CBox_DataBits.Name = "CBox_DataBits";
            this.CBox_DataBits.Size = new System.Drawing.Size(194, 20);
            this.CBox_DataBits.TabIndex = 0;
            // 
            // GBox_StopBits
            // 
            this.GBox_StopBits.Controls.Add(this.CBox_StopBits);
            this.GBox_StopBits.Location = new System.Drawing.Point(4, 196);
            this.GBox_StopBits.Name = "GBox_StopBits";
            this.GBox_StopBits.Size = new System.Drawing.Size(200, 42);
            this.GBox_StopBits.TabIndex = 3;
            this.GBox_StopBits.TabStop = false;
            this.GBox_StopBits.Text = "Stop bit";
            // 
            // CBox_StopBits
            // 
            this.CBox_StopBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_StopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_StopBits.FormattingEnabled = true;
            this.CBox_StopBits.Location = new System.Drawing.Point(3, 15);
            this.CBox_StopBits.Name = "CBox_StopBits";
            this.CBox_StopBits.Size = new System.Drawing.Size(194, 20);
            this.CBox_StopBits.TabIndex = 0;
            // 
            // GBox_FlowControl
            // 
            this.GBox_FlowControl.Controls.Add(this.groupBox3);
            this.GBox_FlowControl.Controls.Add(this.groupBox2);
            this.GBox_FlowControl.Controls.Add(this.groupBox1);
            this.GBox_FlowControl.Location = new System.Drawing.Point(210, 52);
            this.GBox_FlowControl.Name = "GBox_FlowControl";
            this.GBox_FlowControl.Size = new System.Drawing.Size(413, 245);
            this.GBox_FlowControl.TabIndex = 4;
            this.GBox_FlowControl.TabStop = false;
            this.GBox_FlowControl.Text = "Option";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.GBox_XoffLim);
            this.groupBox3.Controls.Add(this.GBox_XonLim);
            this.groupBox3.Controls.Add(this.GBox_XoffChar);
            this.groupBox3.Controls.Add(this.ChkBox_fTXContinueOnXoff);
            this.groupBox3.Controls.Add(this.GBox_XonChar);
            this.groupBox3.Controls.Add(this.ChkBox_fInX);
            this.groupBox3.Controls.Add(this.ChkBox_fOutX);
            this.groupBox3.Location = new System.Drawing.Point(222, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(181, 181);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "XON/XOFF";
            // 
            // GBox_XoffLim
            // 
            this.GBox_XoffLim.Controls.Add(this.Num_XoffLim);
            this.GBox_XoffLim.Location = new System.Drawing.Point(92, 133);
            this.GBox_XoffLim.Name = "GBox_XoffLim";
            this.GBox_XoffLim.Size = new System.Drawing.Size(80, 42);
            this.GBox_XoffLim.TabIndex = 13;
            this.GBox_XoffLim.TabStop = false;
            this.GBox_XoffLim.Text = "XoffLim";
            // 
            // Num_XoffLim
            // 
            this.Num_XoffLim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_XoffLim.Location = new System.Drawing.Point(3, 15);
            this.Num_XoffLim.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_XoffLim.Name = "Num_XoffLim";
            this.Num_XoffLim.Size = new System.Drawing.Size(74, 19);
            this.Num_XoffLim.TabIndex = 0;
            this.Num_XoffLim.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_XoffLim.ThousandsSeparator = true;
            this.Num_XoffLim.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            // 
            // GBox_XonLim
            // 
            this.GBox_XonLim.Controls.Add(this.Num_XonLim);
            this.GBox_XonLim.Location = new System.Drawing.Point(92, 85);
            this.GBox_XonLim.Name = "GBox_XonLim";
            this.GBox_XonLim.Size = new System.Drawing.Size(80, 42);
            this.GBox_XonLim.TabIndex = 12;
            this.GBox_XonLim.TabStop = false;
            this.GBox_XonLim.Text = "XonLim";
            // 
            // Num_XonLim
            // 
            this.Num_XonLim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_XonLim.Location = new System.Drawing.Point(3, 15);
            this.Num_XonLim.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_XonLim.Name = "Num_XonLim";
            this.Num_XonLim.Size = new System.Drawing.Size(74, 19);
            this.Num_XonLim.TabIndex = 0;
            this.Num_XonLim.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_XonLim.ThousandsSeparator = true;
            // 
            // GBox_XoffChar
            // 
            this.GBox_XoffChar.Controls.Add(this.label2);
            this.GBox_XoffChar.Controls.Add(this.Num_XoffChar);
            this.GBox_XoffChar.Location = new System.Drawing.Point(6, 133);
            this.GBox_XoffChar.Name = "GBox_XoffChar";
            this.GBox_XoffChar.Size = new System.Drawing.Size(80, 42);
            this.GBox_XoffChar.TabIndex = 11;
            this.GBox_XoffChar.TabStop = false;
            this.GBox_XoffChar.Text = "XoffChar";
            // 
            // ChkBox_fTXContinueOnXoff
            // 
            this.ChkBox_fTXContinueOnXoff.AutoSize = true;
            this.ChkBox_fTXContinueOnXoff.Location = new System.Drawing.Point(6, 62);
            this.ChkBox_fTXContinueOnXoff.Name = "ChkBox_fTXContinueOnXoff";
            this.ChkBox_fTXContinueOnXoff.Size = new System.Drawing.Size(122, 16);
            this.ChkBox_fTXContinueOnXoff.TabIndex = 10;
            this.ChkBox_fTXContinueOnXoff.Text = "fTXContinueOnXoff";
            this.ChkBox_fTXContinueOnXoff.UseVisualStyleBackColor = true;
            // 
            // GBox_XonChar
            // 
            this.GBox_XonChar.Controls.Add(this.label1);
            this.GBox_XonChar.Controls.Add(this.Num_XonChar);
            this.GBox_XonChar.Location = new System.Drawing.Point(6, 85);
            this.GBox_XonChar.Name = "GBox_XonChar";
            this.GBox_XonChar.Size = new System.Drawing.Size(80, 42);
            this.GBox_XonChar.TabIndex = 9;
            this.GBox_XonChar.TabStop = false;
            this.GBox_XonChar.Text = "XonChar";
            // 
            // ChkBox_fInX
            // 
            this.ChkBox_fInX.AutoSize = true;
            this.ChkBox_fInX.Location = new System.Drawing.Point(6, 40);
            this.ChkBox_fInX.Name = "ChkBox_fInX";
            this.ChkBox_fInX.Size = new System.Drawing.Size(44, 16);
            this.ChkBox_fInX.TabIndex = 7;
            this.ChkBox_fInX.Text = "fInX";
            this.ChkBox_fInX.UseVisualStyleBackColor = true;
            // 
            // ChkBox_fOutX
            // 
            this.ChkBox_fOutX.AutoSize = true;
            this.ChkBox_fOutX.Location = new System.Drawing.Point(6, 18);
            this.ChkBox_fOutX.Name = "ChkBox_fOutX";
            this.ChkBox_fOutX.Size = new System.Drawing.Size(53, 16);
            this.ChkBox_fOutX.TabIndex = 6;
            this.ChkBox_fOutX.Text = "fOutX";
            this.ChkBox_fOutX.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChkBox_fDsrSensitivity);
            this.groupBox2.Controls.Add(this.GBox_fDtrControl);
            this.groupBox2.Controls.Add(this.ChkBox_fOutxDsrFlow);
            this.groupBox2.Location = new System.Drawing.Point(13, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 114);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DTR/DSR";
            // 
            // ChkBox_fDsrSensitivity
            // 
            this.ChkBox_fDsrSensitivity.AutoSize = true;
            this.ChkBox_fDsrSensitivity.Location = new System.Drawing.Point(6, 91);
            this.ChkBox_fDsrSensitivity.Name = "ChkBox_fDsrSensitivity";
            this.ChkBox_fDsrSensitivity.Size = new System.Drawing.Size(100, 16);
            this.ChkBox_fDsrSensitivity.TabIndex = 4;
            this.ChkBox_fDsrSensitivity.Text = "fDsrSensitivity";
            this.ChkBox_fDsrSensitivity.UseVisualStyleBackColor = true;
            // 
            // GBox_fDtrControl
            // 
            this.GBox_fDtrControl.Controls.Add(this.CBox_fDtrControl);
            this.GBox_fDtrControl.Location = new System.Drawing.Point(6, 40);
            this.GBox_fDtrControl.Name = "GBox_fDtrControl";
            this.GBox_fDtrControl.Size = new System.Drawing.Size(180, 42);
            this.GBox_fDtrControl.TabIndex = 3;
            this.GBox_fDtrControl.TabStop = false;
            this.GBox_fDtrControl.Text = "fDtrControl";
            // 
            // CBox_fDtrControl
            // 
            this.CBox_fDtrControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_fDtrControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_fDtrControl.FormattingEnabled = true;
            this.CBox_fDtrControl.Location = new System.Drawing.Point(3, 15);
            this.CBox_fDtrControl.Name = "CBox_fDtrControl";
            this.CBox_fDtrControl.Size = new System.Drawing.Size(174, 20);
            this.CBox_fDtrControl.TabIndex = 0;
            // 
            // ChkBox_fOutxDsrFlow
            // 
            this.ChkBox_fOutxDsrFlow.AutoSize = true;
            this.ChkBox_fOutxDsrFlow.Location = new System.Drawing.Point(6, 18);
            this.ChkBox_fOutxDsrFlow.Name = "ChkBox_fOutxDsrFlow";
            this.ChkBox_fOutxDsrFlow.Size = new System.Drawing.Size(94, 16);
            this.ChkBox_fOutxDsrFlow.TabIndex = 2;
            this.ChkBox_fOutxDsrFlow.Text = "fOutxDsrFlow";
            this.ChkBox_fOutxDsrFlow.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GBox_fRtsControl);
            this.groupBox1.Controls.Add(this.ChkBox_fOutxCtsFlow);
            this.groupBox1.Location = new System.Drawing.Point(13, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RTS/CTS";
            // 
            // GBox_fRtsControl
            // 
            this.GBox_fRtsControl.Controls.Add(this.CBox_fRtsControl);
            this.GBox_fRtsControl.Location = new System.Drawing.Point(6, 41);
            this.GBox_fRtsControl.Name = "GBox_fRtsControl";
            this.GBox_fRtsControl.Size = new System.Drawing.Size(180, 42);
            this.GBox_fRtsControl.TabIndex = 8;
            this.GBox_fRtsControl.TabStop = false;
            this.GBox_fRtsControl.Text = "fRtsControl";
            // 
            // CBox_fRtsControl
            // 
            this.CBox_fRtsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_fRtsControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_fRtsControl.FormattingEnabled = true;
            this.CBox_fRtsControl.Location = new System.Drawing.Point(3, 15);
            this.CBox_fRtsControl.Name = "CBox_fRtsControl";
            this.CBox_fRtsControl.Size = new System.Drawing.Size(174, 20);
            this.CBox_fRtsControl.TabIndex = 0;
            // 
            // ChkBox_fOutxCtsFlow
            // 
            this.ChkBox_fOutxCtsFlow.AutoSize = true;
            this.ChkBox_fOutxCtsFlow.Location = new System.Drawing.Point(6, 19);
            this.ChkBox_fOutxCtsFlow.Name = "ChkBox_fOutxCtsFlow";
            this.ChkBox_fOutxCtsFlow.Size = new System.Drawing.Size(94, 16);
            this.ChkBox_fOutxCtsFlow.TabIndex = 1;
            this.ChkBox_fOutxCtsFlow.Text = "fOutxCtsFlow";
            this.ChkBox_fOutxCtsFlow.UseVisualStyleBackColor = true;
            // 
            // GBox_PortList
            // 
            this.GBox_PortList.Controls.Add(this.CBox_PortList);
            this.GBox_PortList.Location = new System.Drawing.Point(4, 4);
            this.GBox_PortList.Name = "GBox_PortList";
            this.GBox_PortList.Size = new System.Drawing.Size(407, 42);
            this.GBox_PortList.TabIndex = 5;
            this.GBox_PortList.TabStop = false;
            this.GBox_PortList.Text = "Port";
            // 
            // CBox_PortList
            // 
            this.CBox_PortList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_PortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_PortList.FormattingEnabled = true;
            this.CBox_PortList.Location = new System.Drawing.Point(3, 15);
            this.CBox_PortList.Name = "CBox_PortList";
            this.CBox_PortList.Size = new System.Drawing.Size(401, 20);
            this.CBox_PortList.TabIndex = 0;
            // 
            // Num_XonChar
            // 
            this.Num_XonChar.Hexadecimal = true;
            this.Num_XonChar.Location = new System.Drawing.Point(25, 16);
            this.Num_XonChar.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Num_XonChar.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_XonChar.Name = "Num_XonChar";
            this.Num_XonChar.Size = new System.Drawing.Size(49, 19);
            this.Num_XonChar.TabIndex = 0;
            this.Num_XonChar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_XonChar.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "0x";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "0x";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_XoffChar
            // 
            this.Num_XoffChar.Hexadecimal = true;
            this.Num_XoffChar.Location = new System.Drawing.Point(25, 15);
            this.Num_XoffChar.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Num_XoffChar.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_XoffChar.Name = "Num_XoffChar";
            this.Num_XoffChar.Size = new System.Drawing.Size(49, 19);
            this.Num_XoffChar.TabIndex = 2;
            this.Num_XoffChar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_XoffChar.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_PortList);
            this.Controls.Add(this.GBox_FlowControl);
            this.Controls.Add(this.GBox_StopBits);
            this.Controls.Add(this.GBox_DataBits);
            this.Controls.Add(this.GBox_Parity);
            this.Controls.Add(this.GBox_BaudRate);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(632, 306);
            this.GBox_BaudRate.ResumeLayout(false);
            this.GBox_Parity.ResumeLayout(false);
            this.GBox_DataBits.ResumeLayout(false);
            this.GBox_StopBits.ResumeLayout(false);
            this.GBox_FlowControl.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GBox_XoffLim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_XoffLim)).EndInit();
            this.GBox_XonLim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_XonLim)).EndInit();
            this.GBox_XoffChar.ResumeLayout(false);
            this.GBox_XonChar.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GBox_fDtrControl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GBox_fRtsControl.ResumeLayout(false);
            this.GBox_PortList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_XonChar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_XoffChar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_BaudRate;
        private System.Windows.Forms.ComboBox CBox_BaudRate;
        private System.Windows.Forms.GroupBox GBox_Parity;
        private System.Windows.Forms.ComboBox CBox_Parity;
        private System.Windows.Forms.GroupBox GBox_DataBits;
        private System.Windows.Forms.ComboBox CBox_DataBits;
        private System.Windows.Forms.GroupBox GBox_StopBits;
        private System.Windows.Forms.ComboBox CBox_StopBits;
        private System.Windows.Forms.GroupBox GBox_FlowControl;
        private System.Windows.Forms.GroupBox GBox_PortList;
        private System.Windows.Forms.ComboBox CBox_PortList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox GBox_XoffLim;
        private System.Windows.Forms.NumericUpDown Num_XoffLim;
        private System.Windows.Forms.GroupBox GBox_XonLim;
        private System.Windows.Forms.NumericUpDown Num_XonLim;
        private System.Windows.Forms.GroupBox GBox_XoffChar;
        private System.Windows.Forms.CheckBox ChkBox_fTXContinueOnXoff;
        private System.Windows.Forms.GroupBox GBox_XonChar;
        private System.Windows.Forms.CheckBox ChkBox_fInX;
        private System.Windows.Forms.CheckBox ChkBox_fOutX;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ChkBox_fDsrSensitivity;
        private System.Windows.Forms.GroupBox GBox_fDtrControl;
        private System.Windows.Forms.ComboBox CBox_fDtrControl;
        private System.Windows.Forms.CheckBox ChkBox_fOutxDsrFlow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox GBox_fRtsControl;
        private System.Windows.Forms.ComboBox CBox_fRtsControl;
        private System.Windows.Forms.CheckBox ChkBox_fOutxCtsFlow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Num_XonChar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Num_XoffChar;
    }
}
