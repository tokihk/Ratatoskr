namespace Ratatoskr.Forms.Dialog
{
    partial class GateEditDialog
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Btn_SelectColor = new System.Windows.Forms.Button();
            this.ChkBox_DataSendEnable = new System.Windows.Forms.CheckBox();
            this.GBox_DeviceEvent = new System.Windows.Forms.GroupBox();
            this.ChkBox_Notify_DataRecvCompleted = new System.Windows.Forms.CheckBox();
            this.ChkBox_Notify_DataSendCompleted = new System.Windows.Forms.CheckBox();
            this.ChkBox_Notify_DeviceConnect = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Num_DataSendQueueLimit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_DataRedirectQueueLimit = new System.Windows.Forms.NumericUpDown();
            this.ChkBox_DataRedirectEnable = new System.Windows.Forms.CheckBox();
            this.GBox_RedirectList = new System.Windows.Forms.GroupBox();
            this.TBox_RecvDataRedirectTargetAlias = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TBox_ConnectCommand = new System.Windows.Forms.TextBox();
            this.GBox_Alias = new System.Windows.Forms.GroupBox();
            this.TBox_Alias = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TBox_SendDataRedirectTargetAlias = new System.Windows.Forms.TextBox();
            this.TabCtrl_Main.SuspendLayout();
            this.TabPage_Device.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.GBox_DeviceType.SuspendLayout();
            this.TabPage_General.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GBox_DeviceEvent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataSendQueueLimit)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataRedirectQueueLimit)).BeginInit();
            this.GBox_RedirectList.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GBox_Alias.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(896, 687);
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
            this.Btn_Ok.Location = new System.Drawing.Point(790, 687);
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
            this.TabCtrl_Main.Size = new System.Drawing.Size(984, 669);
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
            this.TabPage_Device.Size = new System.Drawing.Size(976, 643);
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
            this.GBox_DeviceProperty.Size = new System.Drawing.Size(964, 583);
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
            this.TabPage_General.Controls.Add(this.groupBox4);
            this.TabPage_General.Controls.Add(this.ChkBox_DataSendEnable);
            this.TabPage_General.Controls.Add(this.GBox_DeviceEvent);
            this.TabPage_General.Controls.Add(this.groupBox1);
            this.TabPage_General.Controls.Add(this.groupBox5);
            this.TabPage_General.Controls.Add(this.groupBox3);
            this.TabPage_General.Controls.Add(this.GBox_Alias);
            this.TabPage_General.Location = new System.Drawing.Point(4, 22);
            this.TabPage_General.Name = "TabPage_General";
            this.TabPage_General.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_General.Size = new System.Drawing.Size(976, 643);
            this.TabPage_General.TabIndex = 1;
            this.TabPage_General.Text = "General";
            this.TabPage_General.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Btn_SelectColor);
            this.groupBox4.Location = new System.Drawing.Point(212, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(57, 42);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Color";
            // 
            // Btn_SelectColor
            // 
            this.Btn_SelectColor.Location = new System.Drawing.Point(6, 13);
            this.Btn_SelectColor.Name = "Btn_SelectColor";
            this.Btn_SelectColor.Size = new System.Drawing.Size(45, 23);
            this.Btn_SelectColor.TabIndex = 0;
            this.Btn_SelectColor.UseVisualStyleBackColor = true;
            this.Btn_SelectColor.Click += new System.EventHandler(this.Btn_SelectColor_Click);
            // 
            // ChkBox_DataSendEnable
            // 
            this.ChkBox_DataSendEnable.AutoCheck = false;
            this.ChkBox_DataSendEnable.AutoSize = true;
            this.ChkBox_DataSendEnable.Location = new System.Drawing.Point(15, 52);
            this.ChkBox_DataSendEnable.Name = "ChkBox_DataSendEnable";
            this.ChkBox_DataSendEnable.Size = new System.Drawing.Size(127, 16);
            this.ChkBox_DataSendEnable.TabIndex = 0;
            this.ChkBox_DataSendEnable.Text = "Data send operation";
            this.ChkBox_DataSendEnable.ThreeState = true;
            this.ChkBox_DataSendEnable.UseVisualStyleBackColor = true;
            this.ChkBox_DataSendEnable.Click += new System.EventHandler(this.ChkBox_DataSendEnable_Click);
            // 
            // GBox_DeviceEvent
            // 
            this.GBox_DeviceEvent.Controls.Add(this.ChkBox_Notify_DataRecvCompleted);
            this.GBox_DeviceEvent.Controls.Add(this.ChkBox_Notify_DataSendCompleted);
            this.GBox_DeviceEvent.Controls.Add(this.ChkBox_Notify_DeviceConnect);
            this.GBox_DeviceEvent.Location = new System.Drawing.Point(6, 304);
            this.GBox_DeviceEvent.Name = "GBox_DeviceEvent";
            this.GBox_DeviceEvent.Size = new System.Drawing.Size(200, 91);
            this.GBox_DeviceEvent.TabIndex = 5;
            this.GBox_DeviceEvent.TabStop = false;
            this.GBox_DeviceEvent.Text = "Notify event setting";
            // 
            // ChkBox_Notify_DataRecvCompleted
            // 
            this.ChkBox_Notify_DataRecvCompleted.AutoSize = true;
            this.ChkBox_Notify_DataRecvCompleted.Location = new System.Drawing.Point(6, 40);
            this.ChkBox_Notify_DataRecvCompleted.Name = "ChkBox_Notify_DataRecvCompleted";
            this.ChkBox_Notify_DataRecvCompleted.Size = new System.Drawing.Size(145, 16);
            this.ChkBox_Notify_DataRecvCompleted.TabIndex = 12;
            this.ChkBox_Notify_DataRecvCompleted.Text = "Data receive completed";
            this.ChkBox_Notify_DataRecvCompleted.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Notify_DataSendCompleted
            // 
            this.ChkBox_Notify_DataSendCompleted.AutoSize = true;
            this.ChkBox_Notify_DataSendCompleted.Location = new System.Drawing.Point(6, 18);
            this.ChkBox_Notify_DataSendCompleted.Name = "ChkBox_Notify_DataSendCompleted";
            this.ChkBox_Notify_DataSendCompleted.Size = new System.Drawing.Size(132, 16);
            this.ChkBox_Notify_DataSendCompleted.TabIndex = 12;
            this.ChkBox_Notify_DataSendCompleted.Text = "Data send completed";
            this.ChkBox_Notify_DataSendCompleted.UseVisualStyleBackColor = true;
            // 
            // ChkBox_Notify_DeviceConnect
            // 
            this.ChkBox_Notify_DeviceConnect.AutoSize = true;
            this.ChkBox_Notify_DeviceConnect.Location = new System.Drawing.Point(6, 62);
            this.ChkBox_Notify_DeviceConnect.Name = "ChkBox_Notify_DeviceConnect";
            this.ChkBox_Notify_DeviceConnect.Size = new System.Drawing.Size(91, 16);
            this.ChkBox_Notify_DeviceConnect.TabIndex = 1;
            this.ChkBox_Notify_DeviceConnect.Text = "Device event";
            this.ChkBox_Notify_DeviceConnect.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Num_DataSendQueueLimit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 47);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // Num_DataSendQueueLimit
            // 
            this.Num_DataSendQueueLimit.Location = new System.Drawing.Point(172, 19);
            this.Num_DataSendQueueLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_DataSendQueueLimit.Name = "Num_DataSendQueueLimit";
            this.Num_DataSendQueueLimit.Size = new System.Drawing.Size(94, 19);
            this.Num_DataSendQueueLimit.TabIndex = 1;
            this.Num_DataSendQueueLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataSendQueueLimit.Value = new decimal(new int[] {
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
            this.label1.Text = "Data send queue limit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.Num_DataRedirectQueueLimit);
            this.groupBox5.Controls.Add(this.ChkBox_DataRedirectEnable);
            this.groupBox5.Controls.Add(this.GBox_RedirectList);
            this.groupBox5.Location = new System.Drawing.Point(6, 107);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(541, 191);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data redirect queue limit";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Num_DataRedirectQueueLimit
            // 
            this.Num_DataRedirectQueueLimit.Location = new System.Drawing.Point(172, 22);
            this.Num_DataRedirectQueueLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_DataRedirectQueueLimit.Name = "Num_DataRedirectQueueLimit";
            this.Num_DataRedirectQueueLimit.Size = new System.Drawing.Size(94, 19);
            this.Num_DataRedirectQueueLimit.TabIndex = 5;
            this.Num_DataRedirectQueueLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataRedirectQueueLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ChkBox_DataRedirectEnable
            // 
            this.ChkBox_DataRedirectEnable.AutoCheck = false;
            this.ChkBox_DataRedirectEnable.AutoSize = true;
            this.ChkBox_DataRedirectEnable.BackColor = System.Drawing.SystemColors.Window;
            this.ChkBox_DataRedirectEnable.Location = new System.Drawing.Point(9, 0);
            this.ChkBox_DataRedirectEnable.Name = "ChkBox_DataRedirectEnable";
            this.ChkBox_DataRedirectEnable.Size = new System.Drawing.Size(91, 16);
            this.ChkBox_DataRedirectEnable.TabIndex = 2;
            this.ChkBox_DataRedirectEnable.Text = "Data redirect";
            this.ChkBox_DataRedirectEnable.ThreeState = true;
            this.ChkBox_DataRedirectEnable.UseVisualStyleBackColor = false;
            this.ChkBox_DataRedirectEnable.Click += new System.EventHandler(this.ChkBox_DataRedirectEnable_Click);
            // 
            // GBox_RedirectList
            // 
            this.GBox_RedirectList.Controls.Add(this.TBox_RecvDataRedirectTargetAlias);
            this.GBox_RedirectList.Location = new System.Drawing.Point(272, 49);
            this.GBox_RedirectList.Name = "GBox_RedirectList";
            this.GBox_RedirectList.Size = new System.Drawing.Size(260, 136);
            this.GBox_RedirectList.TabIndex = 6;
            this.GBox_RedirectList.TabStop = false;
            this.GBox_RedirectList.Text = "Receive data redirect destination alias";
            // 
            // TBox_RecvDataRedirectTargetAlias
            // 
            this.TBox_RecvDataRedirectTargetAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_RecvDataRedirectTargetAlias.Location = new System.Drawing.Point(3, 15);
            this.TBox_RecvDataRedirectTargetAlias.Multiline = true;
            this.TBox_RecvDataRedirectTargetAlias.Name = "TBox_RecvDataRedirectTargetAlias";
            this.TBox_RecvDataRedirectTargetAlias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBox_RecvDataRedirectTargetAlias.Size = new System.Drawing.Size(254, 118);
            this.TBox_RecvDataRedirectTargetAlias.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TBox_ConnectCommand);
            this.groupBox3.Location = new System.Drawing.Point(6, 401);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(541, 40);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send command on connect";
            // 
            // TBox_ConnectCommand
            // 
            this.TBox_ConnectCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_ConnectCommand.Location = new System.Drawing.Point(3, 15);
            this.TBox_ConnectCommand.Name = "TBox_ConnectCommand";
            this.TBox_ConnectCommand.Size = new System.Drawing.Size(535, 19);
            this.TBox_ConnectCommand.TabIndex = 0;
            this.TBox_ConnectCommand.TextChanged += new System.EventHandler(this.TBox_ConnectCommand_TextChanged);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TBox_SendDataRedirectTargetAlias);
            this.groupBox2.Location = new System.Drawing.Point(6, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 136);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send data redirect destination alias";
            // 
            // TBox_SendDataRedirectTargetAlias
            // 
            this.TBox_SendDataRedirectTargetAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_SendDataRedirectTargetAlias.Location = new System.Drawing.Point(3, 15);
            this.TBox_SendDataRedirectTargetAlias.Multiline = true;
            this.TBox_SendDataRedirectTargetAlias.Name = "TBox_SendDataRedirectTargetAlias";
            this.TBox_SendDataRedirectTargetAlias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBox_SendDataRedirectTargetAlias.Size = new System.Drawing.Size(254, 118);
            this.TBox_SendDataRedirectTargetAlias.TabIndex = 0;
            // 
            // GateEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.TabCtrl_Main);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.Btn_Cancel);
            this.Name = "GateEditDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gate setting";
            this.TabCtrl_Main.ResumeLayout(false);
            this.TabPage_Device.ResumeLayout(false);
            this.TabPage_Device.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.GBox_DeviceType.ResumeLayout(false);
            this.TabPage_General.ResumeLayout(false);
            this.TabPage_General.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.GBox_DeviceEvent.ResumeLayout(false);
            this.GBox_DeviceEvent.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataSendQueueLimit)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataRedirectQueueLimit)).EndInit();
            this.GBox_RedirectList.ResumeLayout(false);
            this.GBox_RedirectList.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GBox_Alias.ResumeLayout(false);
            this.GBox_Alias.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TextBox TBox_RecvDataRedirectTargetAlias;
        private System.Windows.Forms.GroupBox GBox_DeviceEvent;
        private System.Windows.Forms.CheckBox ChkBox_Notify_DeviceConnect;
        private System.Windows.Forms.CheckBox ChkBox_DataSendEnable;
        private System.Windows.Forms.CheckBox ChkBox_DataRedirectEnable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown Num_DataRedirectQueueLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Num_DataSendQueueLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label_DeviceNotice;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox TBox_ConnectCommand;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox ChkBox_Notify_DataRecvCompleted;
		private System.Windows.Forms.CheckBox ChkBox_Notify_DataSendCompleted;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button Btn_SelectColor;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox TBox_SendDataRedirectTargetAlias;
	}
}