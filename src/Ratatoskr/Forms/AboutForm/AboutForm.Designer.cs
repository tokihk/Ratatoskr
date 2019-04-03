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
            this.TabPage_About = new System.Windows.Forms.TabPage();
            this.LLabel_HomePage = new System.Windows.Forms.LinkLabel();
            this.Label_Copyright = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.PictBox_Logo = new System.Windows.Forms.PictureBox();
            this.PictBox_Icon = new System.Windows.Forms.PictureBox();
            this.TabPage_License = new System.Windows.Forms.TabPage();
            this.RTBox_LicenseList = new System.Windows.Forms.RichTextBox();
            this.TabPage_Plugin = new System.Windows.Forms.TabPage();
            this.TView_PluginList = new System.Windows.Forms.TreeView();
            this.tabControl1.SuspendLayout();
            this.TabPage_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Icon)).BeginInit();
            this.TabPage_License.SuspendLayout();
            this.TabPage_Plugin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPage_About);
            this.tabControl1.Controls.Add(this.TabPage_License);
            this.tabControl1.Controls.Add(this.TabPage_Plugin);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(160, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(504, 172);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // TabPage_About
            // 
            this.TabPage_About.Controls.Add(this.LLabel_HomePage);
            this.TabPage_About.Controls.Add(this.Label_Copyright);
            this.TabPage_About.Controls.Add(this.Label_Version);
            this.TabPage_About.Controls.Add(this.PictBox_Logo);
            this.TabPage_About.Controls.Add(this.PictBox_Icon);
            this.TabPage_About.Location = new System.Drawing.Point(4, 22);
            this.TabPage_About.Name = "TabPage_About";
            this.TabPage_About.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_About.Size = new System.Drawing.Size(496, 146);
            this.TabPage_About.TabIndex = 0;
            this.TabPage_About.Text = "About application";
            this.TabPage_About.UseVisualStyleBackColor = true;
            // 
            // LLabel_HomePage
            // 
            this.LLabel_HomePage.AutoEllipsis = true;
            this.LLabel_HomePage.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LLabel_HomePage.Location = new System.Drawing.Point(148, 106);
            this.LLabel_HomePage.Name = "LLabel_HomePage";
            this.LLabel_HomePage.Size = new System.Drawing.Size(340, 29);
            this.LLabel_HomePage.TabIndex = 4;
            this.LLabel_HomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LLabel_HomePage_LinkClicked);
            // 
            // Label_Copyright
            // 
            this.Label_Copyright.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_Copyright.Location = new System.Drawing.Point(159, 83);
            this.Label_Copyright.Name = "Label_Copyright";
            this.Label_Copyright.Size = new System.Drawing.Size(331, 23);
            this.Label_Copyright.TabIndex = 3;
            this.Label_Copyright.Text = "Copyright";
            this.Label_Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Version
            // 
            this.Label_Version.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_Version.Location = new System.Drawing.Point(159, 60);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(331, 23);
            this.Label_Version.TabIndex = 2;
            this.Label_Version.Text = "0.0.0.0";
            this.Label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PictBox_Logo
            // 
            this.PictBox_Logo.Location = new System.Drawing.Point(139, 7);
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
            // TabPage_License
            // 
            this.TabPage_License.Controls.Add(this.RTBox_LicenseList);
            this.TabPage_License.Location = new System.Drawing.Point(4, 22);
            this.TabPage_License.Name = "TabPage_License";
            this.TabPage_License.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_License.Size = new System.Drawing.Size(496, 146);
            this.TabPage_License.TabIndex = 1;
            this.TabPage_License.Text = "Third party license";
            this.TabPage_License.UseVisualStyleBackColor = true;
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
            // TabPage_Plugin
            // 
            this.TabPage_Plugin.Controls.Add(this.TView_PluginList);
            this.TabPage_Plugin.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Plugin.Name = "TabPage_Plugin";
            this.TabPage_Plugin.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Plugin.Size = new System.Drawing.Size(496, 146);
            this.TabPage_Plugin.TabIndex = 2;
            this.TabPage_Plugin.Text = "Plugin";
            this.TabPage_Plugin.UseVisualStyleBackColor = true;
            // 
            // TView_PluginList
            // 
            this.TView_PluginList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TView_PluginList.Location = new System.Drawing.Point(3, 3);
            this.TView_PluginList.Name = "TView_PluginList";
            this.TView_PluginList.Size = new System.Drawing.Size(490, 140);
            this.TView_PluginList.TabIndex = 0;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 172);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.tabControl1.ResumeLayout(false);
            this.TabPage_About.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictBox_Icon)).EndInit();
            this.TabPage_License.ResumeLayout(false);
            this.TabPage_Plugin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPage_About;
        private System.Windows.Forms.TabPage TabPage_License;
        private System.Windows.Forms.PictureBox PictBox_Icon;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.PictureBox PictBox_Logo;
        private System.Windows.Forms.Label Label_Copyright;
        private System.Windows.Forms.RichTextBox RTBox_LicenseList;
        private System.Windows.Forms.LinkLabel LLabel_HomePage;
        private System.Windows.Forms.TabPage TabPage_Plugin;
        private System.Windows.Forms.TreeView TView_PluginList;
    }
}