namespace Ratatoskr.Forms.AboutForm
{
    partial class AboutForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Label_Copyright = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.PictBox_Logo = new System.Windows.Forms.PictureBox();
            this.PictBox_Icon = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.RTBox_LicenseList = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Icon)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(160, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(504, 172);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Label_Copyright);
            this.tabPage1.Controls.Add(this.Label_Version);
            this.tabPage1.Controls.Add(this.PictBox_Logo);
            this.tabPage1.Controls.Add(this.PictBox_Icon);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(496, 146);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "アプリケーションについて";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Label_Copyright
            // 
            this.Label_Copyright.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_Copyright.Location = new System.Drawing.Point(139, 99);
            this.Label_Copyright.Name = "Label_Copyright";
            this.Label_Copyright.Size = new System.Drawing.Size(351, 23);
            this.Label_Copyright.TabIndex = 3;
            this.Label_Copyright.Text = "Copyright";
            this.Label_Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Version
            // 
            this.Label_Version.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_Version.Location = new System.Drawing.Point(159, 76);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(329, 23);
            this.Label_Version.TabIndex = 2;
            this.Label_Version.Text = "0.0.0.0";
            this.Label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictBox_Logo
            // 
            this.PictBox_Logo.Location = new System.Drawing.Point(139, 23);
            this.PictBox_Logo.Name = "PictBox_Logo";
            this.PictBox_Logo.Size = new System.Drawing.Size(351, 50);
            this.PictBox_Logo.TabIndex = 1;
            this.PictBox_Logo.TabStop = false;
            // 
            // PictBox_Icon
            // 
            this.PictBox_Icon.Location = new System.Drawing.Point(4, 7);
            this.PictBox_Icon.Name = "PictBox_Icon";
            this.PictBox_Icon.Size = new System.Drawing.Size(128, 128);
            this.PictBox_Icon.TabIndex = 0;
            this.PictBox_Icon.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RTBox_LicenseList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(496, 146);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ライセンス情報";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RTBox_LicenseList
            // 
            this.RTBox_LicenseList.BackColor = System.Drawing.SystemColors.Window;
            this.RTBox_LicenseList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTBox_LicenseList.DetectUrls = false;
            this.RTBox_LicenseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBox_LicenseList.Location = new System.Drawing.Point(3, 3);
            this.RTBox_LicenseList.Name = "RTBox_LicenseList";
            this.RTBox_LicenseList.ReadOnly = true;
            this.RTBox_LicenseList.Size = new System.Drawing.Size(490, 140);
            this.RTBox_LicenseList.TabIndex = 0;
            this.RTBox_LicenseList.Text = "";
            // 
            // InformationFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 172);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InformationFrame";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InformationFrame";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Icon)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox PictBox_Icon;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.PictureBox PictBox_Logo;
        private System.Windows.Forms.Label Label_Copyright;
        private System.Windows.Forms.RichTextBox RTBox_LicenseList;
    }
}