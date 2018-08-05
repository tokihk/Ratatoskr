namespace Ratatoskr.Forms.Dialog
{
    partial class PacketLogMultiOpenDialog
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
            this.DGView_Setting = new System.Windows.Forms.DataGridView();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RBtn_TargetPattern_FilePath = new System.Windows.Forms.RadioButton();
            this.RBtn_TargetPattern_FileFormat = new System.Windows.Forms.RadioButton();
            this.TBox_TargetPattern_FilePath = new System.Windows.Forms.TextBox();
            this.CBox_TargetPattern_FileFormat = new System.Windows.Forms.ComboBox();
            this.GBox_ChangedFormat = new System.Windows.Forms.GroupBox();
            this.CBox_ChangedFormat = new System.Windows.Forms.ComboBox();
            this.Btn_ChangedFormat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGView_Setting)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GBox_ChangedFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGView_Setting
            // 
            this.DGView_Setting.AllowUserToAddRows = false;
            this.DGView_Setting.AllowUserToDeleteRows = false;
            this.DGView_Setting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGView_Setting.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DGView_Setting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGView_Setting.Location = new System.Drawing.Point(12, 118);
            this.DGView_Setting.Name = "DGView_Setting";
            this.DGView_Setting.RowTemplate.Height = 21;
            this.DGView_Setting.Size = new System.Drawing.Size(695, 352);
            this.DGView_Setting.TabIndex = 0;
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Ok.Location = new System.Drawing.Point(551, 476);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.Btn_Ok.TabIndex = 1;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(632, 476);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 2;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_ChangedFormat);
            this.groupBox1.Controls.Add(this.GBox_ChangedFormat);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collective setting";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CBox_TargetPattern_FileFormat);
            this.groupBox2.Controls.Add(this.TBox_TargetPattern_FilePath);
            this.groupBox2.Controls.Add(this.RBtn_TargetPattern_FileFormat);
            this.groupBox2.Controls.Add(this.RBtn_TargetPattern_FilePath);
            this.groupBox2.Location = new System.Drawing.Point(7, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 72);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target file pattern";
            // 
            // RBtn_TargetPattern_FilePath
            // 
            this.RBtn_TargetPattern_FilePath.AutoSize = true;
            this.RBtn_TargetPattern_FilePath.Location = new System.Drawing.Point(7, 19);
            this.RBtn_TargetPattern_FilePath.Name = "RBtn_TargetPattern_FilePath";
            this.RBtn_TargetPattern_FilePath.Size = new System.Drawing.Size(68, 16);
            this.RBtn_TargetPattern_FilePath.TabIndex = 0;
            this.RBtn_TargetPattern_FilePath.TabStop = true;
            this.RBtn_TargetPattern_FilePath.Text = "File path";
            this.RBtn_TargetPattern_FilePath.UseVisualStyleBackColor = true;
            // 
            // RBtn_TargetPattern_FileFormat
            // 
            this.RBtn_TargetPattern_FileFormat.AutoSize = true;
            this.RBtn_TargetPattern_FileFormat.Location = new System.Drawing.Point(7, 45);
            this.RBtn_TargetPattern_FileFormat.Name = "RBtn_TargetPattern_FileFormat";
            this.RBtn_TargetPattern_FileFormat.Size = new System.Drawing.Size(79, 16);
            this.RBtn_TargetPattern_FileFormat.TabIndex = 1;
            this.RBtn_TargetPattern_FileFormat.TabStop = true;
            this.RBtn_TargetPattern_FileFormat.Text = "File format";
            this.RBtn_TargetPattern_FileFormat.UseVisualStyleBackColor = true;
            // 
            // TBox_TargetPattern_FilePath
            // 
            this.TBox_TargetPattern_FilePath.Location = new System.Drawing.Point(126, 19);
            this.TBox_TargetPattern_FilePath.Name = "TBox_TargetPattern_FilePath";
            this.TBox_TargetPattern_FilePath.Size = new System.Drawing.Size(188, 19);
            this.TBox_TargetPattern_FilePath.TabIndex = 2;
            // 
            // CBox_TargetPattern_FileFormat
            // 
            this.CBox_TargetPattern_FileFormat.FormattingEnabled = true;
            this.CBox_TargetPattern_FileFormat.Location = new System.Drawing.Point(126, 44);
            this.CBox_TargetPattern_FileFormat.Name = "CBox_TargetPattern_FileFormat";
            this.CBox_TargetPattern_FileFormat.Size = new System.Drawing.Size(188, 20);
            this.CBox_TargetPattern_FileFormat.TabIndex = 3;
            // 
            // GBox_ChangedFormat
            // 
            this.GBox_ChangedFormat.Controls.Add(this.CBox_ChangedFormat);
            this.GBox_ChangedFormat.Location = new System.Drawing.Point(409, 35);
            this.GBox_ChangedFormat.Name = "GBox_ChangedFormat";
            this.GBox_ChangedFormat.Size = new System.Drawing.Size(200, 45);
            this.GBox_ChangedFormat.TabIndex = 1;
            this.GBox_ChangedFormat.TabStop = false;
            this.GBox_ChangedFormat.Text = "Format after change";
            // 
            // CBox_ChangedFormat
            // 
            this.CBox_ChangedFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ChangedFormat.FormattingEnabled = true;
            this.CBox_ChangedFormat.Location = new System.Drawing.Point(3, 15);
            this.CBox_ChangedFormat.Name = "CBox_ChangedFormat";
            this.CBox_ChangedFormat.Size = new System.Drawing.Size(194, 20);
            this.CBox_ChangedFormat.TabIndex = 0;
            // 
            // Btn_ChangedFormat
            // 
            this.Btn_ChangedFormat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_ChangedFormat.Location = new System.Drawing.Point(333, 19);
            this.Btn_ChangedFormat.Name = "Btn_ChangedFormat";
            this.Btn_ChangedFormat.Size = new System.Drawing.Size(70, 72);
            this.Btn_ChangedFormat.TabIndex = 2;
            this.Btn_ChangedFormat.Text = "Convert";
            this.Btn_ChangedFormat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_ChangedFormat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn_ChangedFormat.UseVisualStyleBackColor = true;
            // 
            // FilesFormatSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.DGView_Setting);
            this.Name = "FilesFormatSelectDialog";
            this.ShowIcon = false;
            this.Text = "File open setting";
            ((System.ComponentModel.ISupportInitialize)(this.DGView_Setting)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GBox_ChangedFormat.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGView_Setting;
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CBox_TargetPattern_FileFormat;
        private System.Windows.Forms.TextBox TBox_TargetPattern_FilePath;
        private System.Windows.Forms.RadioButton RBtn_TargetPattern_FileFormat;
        private System.Windows.Forms.RadioButton RBtn_TargetPattern_FilePath;
        private System.Windows.Forms.GroupBox GBox_ChangedFormat;
        private System.Windows.Forms.ComboBox CBox_ChangedFormat;
        private System.Windows.Forms.Button Btn_ChangedFormat;
    }
}