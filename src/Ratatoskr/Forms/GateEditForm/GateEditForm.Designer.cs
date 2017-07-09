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
            this.GBox_DeviceProperty = new System.Windows.Forms.GroupBox();
            this.GBox_DeviceType = new System.Windows.Forms.GroupBox();
            this.CBox_DeviceType = new System.Windows.Forms.ComboBox();
            this.GBox_Alias = new System.Windows.Forms.GroupBox();
            this.TBox_Alias = new System.Windows.Forms.TextBox();
            this.TabPage_Option = new System.Windows.Forms.TabPage();
            this.TabCtrl_Main.SuspendLayout();
            this.TabPage_Device.SuspendLayout();
            this.GBox_DeviceType.SuspendLayout();
            this.GBox_Alias.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(552, 460);
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
            this.Btn_Ok.Location = new System.Drawing.Point(446, 460);
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
            this.TabCtrl_Main.Controls.Add(this.TabPage_Option);
            this.TabCtrl_Main.Location = new System.Drawing.Point(12, 12);
            this.TabCtrl_Main.Name = "TabCtrl_Main";
            this.TabCtrl_Main.SelectedIndex = 0;
            this.TabCtrl_Main.Size = new System.Drawing.Size(640, 442);
            this.TabCtrl_Main.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabCtrl_Main.TabIndex = 5;
            // 
            // TabPage_Device
            // 
            this.TabPage_Device.Controls.Add(this.GBox_DeviceProperty);
            this.TabPage_Device.Controls.Add(this.GBox_DeviceType);
            this.TabPage_Device.Controls.Add(this.GBox_Alias);
            this.TabPage_Device.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Device.Name = "TabPage_Device";
            this.TabPage_Device.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Device.Size = new System.Drawing.Size(632, 416);
            this.TabPage_Device.TabIndex = 0;
            this.TabPage_Device.Text = "Device";
            this.TabPage_Device.UseVisualStyleBackColor = true;
            // 
            // GBox_DeviceProperty
            // 
            this.GBox_DeviceProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_DeviceProperty.Location = new System.Drawing.Point(6, 102);
            this.GBox_DeviceProperty.Name = "GBox_DeviceProperty";
            this.GBox_DeviceProperty.Size = new System.Drawing.Size(620, 308);
            this.GBox_DeviceProperty.TabIndex = 5;
            this.GBox_DeviceProperty.TabStop = false;
            this.GBox_DeviceProperty.Text = "Device parameter";
            // 
            // GBox_DeviceType
            // 
            this.GBox_DeviceType.Controls.Add(this.CBox_DeviceType);
            this.GBox_DeviceType.Location = new System.Drawing.Point(6, 54);
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
            this.CBox_DeviceType.SelectedIndexChanged += new System.EventHandler(this.OnDeviceChanged);
            // 
            // GBox_Alias
            // 
            this.GBox_Alias.Controls.Add(this.TBox_Alias);
            this.GBox_Alias.Location = new System.Drawing.Point(6, 6);
            this.GBox_Alias.Name = "GBox_Alias";
            this.GBox_Alias.Size = new System.Drawing.Size(240, 42);
            this.GBox_Alias.TabIndex = 3;
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
            this.TBox_Alias.Size = new System.Drawing.Size(234, 19);
            this.TBox_Alias.TabIndex = 0;
            // 
            // TabPage_Option
            // 
            this.TabPage_Option.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Option.Name = "TabPage_Option";
            this.TabPage_Option.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Option.Size = new System.Drawing.Size(632, 416);
            this.TabPage_Option.TabIndex = 1;
            this.TabPage_Option.Text = "Option";
            this.TabPage_Option.UseVisualStyleBackColor = true;
            // 
            // GateEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 502);
            this.Controls.Add(this.TabCtrl_Main);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.Btn_Cancel);
            this.Name = "GateEditForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gate setting";
            this.TabCtrl_Main.ResumeLayout(false);
            this.TabPage_Device.ResumeLayout(false);
            this.GBox_DeviceType.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox GBox_Alias;
        private System.Windows.Forms.TextBox TBox_Alias;
        private System.Windows.Forms.TabPage TabPage_Option;
    }
}