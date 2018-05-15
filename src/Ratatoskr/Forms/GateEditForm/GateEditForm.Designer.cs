namespace Ratatoskr.Forms.GateEditForm
{
    partial class GateEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.TabCtrl_Main = new System.Windows.Forms.TabControl();
            this.TabPage_Device = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Label_DeviceNotice = new System.Windows.Forms.Label();
            this.GBox_DeviceProperty = new System.Windows.Forms.GroupBox();
            this.GBox_DeviceType = new System.Windows.Forms.GroupBox();
            this.CBox_DeviceType = new System.Windows.Forms.ComboBox();
            this.TabPage_General = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_RedirectQueueLimit = new System.Windows.Forms.NumericUpDown();
            this.ChkBox_RedirectEnable = new System.Windows.Forms.CheckBox();
            this.GBox_RedirectList = new System.Windows.Forms.GroupBox();
            this.TBox_RedirectTargetAlias = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GBox_Operation = new System.Windows.Forms.GroupBox();
            this.ChkBox_RecvEnable = new System.Windows.Forms.CheckBox();
            this.ChkBox_SendEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Num_SendQueueLimit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TBox_ConnectCommand = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Num_DataRate_GraphLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ChkBox_DaraRateTarget_Recv = new System.Windows.Forms.CheckBox();
            this.ChkBox_DaraRateTarget_Send = new System.Windows.Forms.CheckBox();
            this.GBox_Alias = new System.Windows.Forms.GroupBox();
            this.TBox_Alias = new System.Windows.Forms.TextBox();
            this.TabCtrl_Main.SuspendLayout();
            this.TabPage_Device.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.GBox_DeviceType.SuspendLayout();
            this.TabPage_General.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RedirectQueueLimit)).BeginInit();
            this.GBox_RedirectList.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GBox_Operation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendQueueLimit)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataRate_GraphLimit)).BeginInit();
            this.GBox_Alias.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(821, 640);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 30);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.OnClick_Cancel);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Ok.Location = new System.Drawing.Point(715, 640);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(100, 30);
            this.Btn_Ok.TabIndex = 3;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.OnClick_Ok);
            // 
            // TabCtrl_Main
            // 
            this.TabCtrl_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabCtrl_Main.Controls.Add(this.TabPage_Device);
            this.TabCtrl_Main.Controls.Add(this.TabPage_General);
            this.TabCtrl_Main.Location = new System.Drawing.Point(12, 12);
            this.TabCtrl_Main.Name = "TabCtrl_Main";
            this.TabCtrl_Main.SelectedIndex = 0;
            this.TabCtrl_Main.Size = new System.Drawing.Size(909, 622);
            this.TabCtrl_Main.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabCtrl_Main.TabIndex = 5;
            // 
            // TabPage_Device
            // 
            this.TabPage_Device.Controls.Add(this.groupBox6);
            this.TabPage_Device.Controls.Add(this.Label_DeviceNotice);
            this.TabPage_Device.Controls.Add(this.GBox_DeviceProperty);
            this.TabPage_Device.Controls.Add(this.GBox_DeviceType);
            this.TabPage_Device.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Device.Name = "TabPage_Device";
            this.TabPage_Device.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Device.Size = new System.Drawing.Size(901, 596);
            this.TabPage_Device.TabIndex = 0;
            this.TabPage_Device.Text = "Device";
            this.TabPage_Device.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBox1);
            this.groupBox6.Location = new System.Drawing.Point(258, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(240, 42);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Transfer protocol type";
            this.groupBox6.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(234, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // Label_DeviceNotice
            // 
            this.Label_DeviceNotice.AutoSize = true;
            this.Label_DeviceNotice.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_DeviceNotice.ForeColor = System.Drawing.Color.Red;
            this.Label_DeviceNotice.Location = new System.Drawing.Point(252, 24);
            this.Label_DeviceNotice.Name = "Label_DeviceNotice";
            this.Label_DeviceNotice.Size = new System.Drawing.Size(0, 12);
            this.Label_DeviceNotice.TabIndex = 6;
            // 
            // GBox_DeviceProperty
            // 
            this.GBox_DeviceProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_DeviceProperty.Location = new System.Drawing.Point(6, 54);
            this.GBox_DeviceProperty.Name = "GBox_DeviceProperty";
            this.GBox_DeviceProperty.Size = new System.Drawing.Size(660, 536);
            this.GBox_DeviceProperty.TabIndex = 5;
            this.GBox_DeviceProperty.TabStop = false;
            this.GBox_DeviceProperty.Text = "Device parameter";
            // 
            // GBox_DeviceType
            // 
            this.GBox_DeviceType.Controls.Add(this.CBox_DeviceType);
            this.GBox_DeviceType.Location = new System.Drawing.Point(6, 6);
            this.GBox_DeviceType.Name = "GBox_DeviceType";
            this.GBox_DeviceType.Size = new System.Drawing.Size(240, 42);
            this.GBox_DeviceType.TabIndex = 4;
            this.GBox_DeviceType.TabStop = false;
            this.GBox_DeviceType.Text = "Device type";
            // 
            // CBox_DeviceType
            // 
            this.CBox_DeviceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DeviceType.FormattingEnabled = true;
            this.CBox_DeviceType.Location = new System.Drawing.Point(3, 15);
            this.CBox_DeviceType.Name = "CBox_DeviceType";
            this.CBox_DeviceType.Size = new System.Drawing.Size(234, 20);
            this.CBox_DeviceType.TabIndex = 0;
            this.CBox_DeviceType.SelectedIndexChanged += new System.EventHandler(this.CBox_DeviceType_SelectedIndexChanged);
            // 
            // TabPage_General
            // 
            this.TabPage_General.Controls.Add(this.groupBox5);
            this.TabPage_General.Controls.Add(this.groupBox4);
            this.TabPage_General.Controls.Add(this.groupBox2);
            this.TabPage_General.Controls.Add(this.GBox_Alias);
            this.TabPage_General.Location = new System.Drawing.Point(4, 22);
            this.TabPage_General.Name = "TabPage_General";
            this.TabPage_General.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_General.Size = new System.Drawing.Size(901, 596);
            this.TabPage_General.TabIndex = 1;
            this.TabPage_General.Text = "General";
            this.TabPage_General.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.Num_RedirectQueueLimit);
            this.groupBox5.Controls.Add(this.ChkBox_RedirectEnable);
            this.groupBox5.Controls.Add(this.GBox_RedirectList);
            this.groupBox5.Location = new System.Drawing.Point(6, 294);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(330, 191);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Redirect data queue limit";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Num_RedirectQueueLimit
            // 
            this.Num_RedirectQueueLimit.Location = new System.Drawing.Point(172, 22);
            this.Num_RedirectQueueLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_RedirectQueueLimit.Name = "Num_RedirectQueueLimit";
            this.Num_RedirectQueueLimit.Size = new System.Drawing.Size(94, 19);
            this.Num_RedirectQueueLimit.TabIndex = 5;
            this.Num_RedirectQueueLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RedirectQueueLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ChkBox_RedirectEnable
            // 
            this.ChkBox_RedirectEnable.AutoCheck = false;
            this.ChkBox_RedirectEnable.AutoSize = true;
            this.ChkBox_RedirectEnable.BackColor = System.Drawing.SystemColors.Control;
            this.ChkBox_RedirectEnable.Location = new System.Drawing.Point(6, 0);
            this.ChkBox_RedirectEnable.Name = "ChkBox_RedirectEnable";
            this.ChkBox_RedirectEnable.Size = new System.Drawing.Size(134, 16);
            this.ChkBox_RedirectEnable.TabIndex = 2;
            this.ChkBox_RedirectEnable.Text = "Receive data redirect";
            this.ChkBox_RedirectEnable.ThreeState = true;
            this.ChkBox_RedirectEnable.UseVisualStyleBackColor = false;
            this.ChkBox_RedirectEnable.Click += new System.EventHandler(this.ChkBox_RedirectEnable_Click);
            // 
            // GBox_RedirectList
            // 
            this.GBox_RedirectList.Controls.Add(this.TBox_RedirectTargetAlias);
            this.GBox_RedirectList.Location = new System.Drawing.Point(6, 47);
            this.GBox_RedirectList.Name = "GBox_RedirectList";
            this.GBox_RedirectList.Size = new System.Drawing.Size(260, 136);
            this.GBox_RedirectList.TabIndex = 6;
            this.GBox_RedirectList.TabStop = false;
            this.GBox_RedirectList.Text = "Receive data redirect alias";
            // 
            // TBox_RedirectTargetAlias
            // 
            this.TBox_RedirectTargetAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_RedirectTargetAlias.Location = new System.Drawing.Point(3, 15);
            this.TBox_RedirectTargetAlias.Multiline = true;
            this.TBox_RedirectTargetAlias.Name = "TBox_RedirectTargetAlias";
            this.TBox_RedirectTargetAlias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBox_RedirectTargetAlias.Size = new System.Drawing.Size(254, 118);
            this.TBox_RedirectTargetAlias.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GBox_Operation);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Location = new System.Drawing.Point(6, 54);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(420, 140);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Operation setting";
            // 
            // GBox_Operation
            // 
            this.GBox_Operation.Controls.Add(this.ChkBox_RecvEnable);
            this.GBox_Operation.Controls.Add(this.ChkBox_SendEnable);
            this.GBox_Operation.Location = new System.Drawing.Point(6, 18);
            this.GBox_Operation.Name = "GBox_Operation";
            this.GBox_Operation.Size = new System.Drawing.Size(120, 66);
            this.GBox_Operation.TabIndex = 5;
            this.GBox_Operation.TabStop = false;
            this.GBox_Operation.Text = "Function";
            // 
            // ChkBox_RecvEnable
            // 
            this.ChkBox_RecvEnable.AutoCheck = false;
            this.ChkBox_RecvEnable.AutoSize = true;
            this.ChkBox_RecvEnable.Location = new System.Drawing.Point(7, 41);
            this.ChkBox_RecvEnable.Name = "ChkBox_RecvEnable";
            this.ChkBox_RecvEnable.Size = new System.Drawing.Size(89, 16);
            this.ChkBox_RecvEnable.TabIndex = 1;
            this.ChkBox_RecvEnable.Text = "Data receive";
            this.ChkBox_RecvEnable.ThreeState = true;
            this.ChkBox_RecvEnable.UseVisualStyleBackColor = true;
            this.ChkBox_RecvEnable.Click += new System.EventHandler(this.ChkBox_RecvEnable_Click);
            // 
            // ChkBox_SendEnable
            // 
            this.ChkBox_SendEnable.AutoCheck = false;
            this.ChkBox_SendEnable.AutoSize = true;
            this.ChkBox_SendEnable.Location = new System.Drawing.Point(7, 19);
            this.ChkBox_SendEnable.Name = "ChkBox_SendEnable";
            this.ChkBox_SendEnable.Size = new System.Drawing.Size(76, 16);
            this.ChkBox_SendEnable.TabIndex = 0;
            this.ChkBox_SendEnable.Text = "Data send";
            this.ChkBox_SendEnable.ThreeState = true;
            this.ChkBox_SendEnable.UseVisualStyleBackColor = true;
            this.ChkBox_SendEnable.Click += new System.EventHandler(this.ChkBox_SendEnable_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Num_SendQueueLimit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(132, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 66);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Performance";
            // 
            // Num_SendQueueLimit
            // 
            this.Num_SendQueueLimit.Location = new System.Drawing.Point(172, 19);
            this.Num_SendQueueLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_SendQueueLimit.Name = "Num_SendQueueLimit";
            this.Num_SendQueueLimit.Size = new System.Drawing.Size(94, 19);
            this.Num_SendQueueLimit.TabIndex = 1;
            this.Num_SendQueueLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SendQueueLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Send data queue limit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TBox_ConnectCommand);
            this.groupBox3.Location = new System.Drawing.Point(6, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(404, 40);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send command on connect";
            // 
            // TBox_ConnectCommand
            // 
            this.TBox_ConnectCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_ConnectCommand.Location = new System.Drawing.Point(3, 15);
            this.TBox_ConnectCommand.Name = "TBox_ConnectCommand";
            this.TBox_ConnectCommand.Size = new System.Drawing.Size(398, 19);
            this.TBox_ConnectCommand.TabIndex = 0;
            this.TBox_ConnectCommand.TextChanged += new System.EventHandler(this.TBox_ConnectCommand_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Num_DataRate_GraphLimit);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ChkBox_DaraRateTarget_Recv);
            this.groupBox2.Controls.Add(this.ChkBox_DaraRateTarget_Send);
            this.groupBox2.Location = new System.Drawing.Point(6, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 88);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data rate view setting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "B/s";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Num_DataRate_GraphLimit
            // 
            this.Num_DataRate_GraphLimit.Location = new System.Drawing.Point(172, 62);
            this.Num_DataRate_GraphLimit.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.Num_DataRate_GraphLimit.Name = "Num_DataRate_GraphLimit";
            this.Num_DataRate_GraphLimit.Size = new System.Drawing.Size(120, 19);
            this.Num_DataRate_GraphLimit.TabIndex = 6;
            this.Num_DataRate_GraphLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataRate_GraphLimit.ThousandsSeparator = true;
            this.Num_DataRate_GraphLimit.Value = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Graph maximum rate";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChkBox_DaraRateTarget_Recv
            // 
            this.ChkBox_DaraRateTarget_Recv.AutoSize = true;
            this.ChkBox_DaraRateTarget_Recv.Location = new System.Drawing.Point(7, 40);
            this.ChkBox_DaraRateTarget_Recv.Name = "ChkBox_DaraRateTarget_Recv";
            this.ChkBox_DaraRateTarget_Recv.Size = new System.Drawing.Size(133, 16);
            this.ChkBox_DaraRateTarget_Recv.TabIndex = 1;
            this.ChkBox_DaraRateTarget_Recv.Text = "Receive data monitor";
            this.ChkBox_DaraRateTarget_Recv.UseVisualStyleBackColor = true;
            // 
            // ChkBox_DaraRateTarget_Send
            // 
            this.ChkBox_DaraRateTarget_Send.AutoSize = true;
            this.ChkBox_DaraRateTarget_Send.Location = new System.Drawing.Point(7, 18);
            this.ChkBox_DaraRateTarget_Send.Name = "ChkBox_DaraRateTarget_Send";
            this.ChkBox_DaraRateTarget_Send.Size = new System.Drawing.Size(117, 16);
            this.ChkBox_DaraRateTarget_Send.TabIndex = 0;
            this.ChkBox_DaraRateTarget_Send.Text = "Send data monitor";
            this.ChkBox_DaraRateTarget_Send.UseVisualStyleBackColor = true;
            // 
            // GBox_Alias
            // 
            this.GBox_Alias.Controls.Add(this.TBox_Alias);
            this.GBox_Alias.Location = new System.Drawing.Point(6, 6);
            this.GBox_Alias.Name = "GBox_Alias";
            this.GBox_Alias.Size = new System.Drawing.Size(200, 42);
            this.GBox_Alias.TabIndex = 4;
            this.GBox_Alias.TabStop = false;
            this.GBox_Alias.Text = "Alias";
            // 
            // TBox_Alias
            // 
            this.TBox_Alias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Alias.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.TBox_Alias.Location = new System.Drawing.Point(3, 15);
            this.TBox_Alias.MaxLength = 16;
            this.TBox_Alias.Name = "TBox_Alias";
            this.TBox_Alias.Size = new System.Drawing.Size(194, 19);
            this.TBox_Alias.TabIndex = 0;
            // 
            // GateEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 682);
            this.Controls.Add(this.TabCtrl_Main);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.Btn_Cancel);
            this.Name = "GateEditForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gate setting";
            this.TabCtrl_Main.ResumeLayout(false);
            this.TabPage_Device.ResumeLayout(false);
            this.TabPage_Device.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.GBox_DeviceType.ResumeLayout(false);
            this.TabPage_General.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RedirectQueueLimit)).EndInit();
            this.GBox_RedirectList.ResumeLayout(false);
            this.GBox_RedirectList.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.GBox_Operation.ResumeLayout(false);
            this.GBox_Operation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendQueueLimit)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataRate_GraphLimit)).EndInit();
            this.GBox_Alias.ResumeLayout(false);
            this.GBox_Alias.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.TabControl TabCtrl_Main;
        private System.Windows.Forms.TabPage TabPage_Device;
        private System.Windows.Forms.GroupBox GBox_DeviceProperty;
        private System.Windows.Forms.GroupBox GBox_DeviceType;
        private System.Windows.Forms.ComboBox CBox_DeviceType;
        private System.Windows.Forms.TabPage TabPage_General;
        private System.Windows.Forms.GroupBox GBox_Alias;
        private System.Windows.Forms.TextBox TBox_Alias;
        private System.Windows.Forms.GroupBox GBox_RedirectList;
        private System.Windows.Forms.TextBox TBox_RedirectTargetAlias;
        private System.Windows.Forms.GroupBox GBox_Operation;
        private System.Windows.Forms.CheckBox ChkBox_RecvEnable;
        private System.Windows.Forms.CheckBox ChkBox_SendEnable;
        private System.Windows.Forms.CheckBox ChkBox_RedirectEnable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown Num_RedirectQueueLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Num_SendQueueLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown Num_DataRate_GraphLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ChkBox_DaraRateTarget_Recv;
        private System.Windows.Forms.CheckBox ChkBox_DaraRateTarget_Send;
        private System.Windows.Forms.Label Label_DeviceNotice;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox TBox_ConnectCommand;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}