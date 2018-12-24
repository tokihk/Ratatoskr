namespace Ratatoskr.Forms.Dialog
{
    partial class ProfileEditDialog
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
            this.GBox_ProfileName = new System.Windows.Forms.GroupBox();
            this.TBox_ProfileName = new System.Windows.Forms.TextBox();
            this.ChkBox_ReadOnly = new System.Windows.Forms.CheckBox();
            this.GBox_ProfileComment = new System.Windows.Forms.GroupBox();
            this.TBox_ProfileComment = new System.Windows.Forms.TextBox();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.GBox_ProfileName.SuspendLayout();
            this.GBox_ProfileComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_ProfileName
            // 
            this.GBox_ProfileName.Controls.Add(this.TBox_ProfileName);
            this.GBox_ProfileName.Location = new System.Drawing.Point(12, 12);
            this.GBox_ProfileName.Name = "GBox_ProfileName";
            this.GBox_ProfileName.Size = new System.Drawing.Size(180, 43);
            this.GBox_ProfileName.TabIndex = 0;
            this.GBox_ProfileName.TabStop = false;
            this.GBox_ProfileName.Text = "Profile name";
            // 
            // TBox_ProfileName
            // 
            this.TBox_ProfileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_ProfileName.Location = new System.Drawing.Point(3, 15);
            this.TBox_ProfileName.Name = "TBox_ProfileName";
            this.TBox_ProfileName.Size = new System.Drawing.Size(174, 19);
            this.TBox_ProfileName.TabIndex = 0;
            this.TBox_ProfileName.TextChanged += new System.EventHandler(this.TBox_ProfileName_TextChanged);
            // 
            // ChkBox_ReadOnly
            // 
            this.ChkBox_ReadOnly.AutoSize = true;
            this.ChkBox_ReadOnly.Location = new System.Drawing.Point(199, 29);
            this.ChkBox_ReadOnly.Name = "ChkBox_ReadOnly";
            this.ChkBox_ReadOnly.Size = new System.Drawing.Size(75, 16);
            this.ChkBox_ReadOnly.TabIndex = 1;
            this.ChkBox_ReadOnly.Text = "Read only";
            this.ChkBox_ReadOnly.UseVisualStyleBackColor = true;
            // 
            // GBox_ProfileComment
            // 
            this.GBox_ProfileComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_ProfileComment.Controls.Add(this.TBox_ProfileComment);
            this.GBox_ProfileComment.Location = new System.Drawing.Point(12, 61);
            this.GBox_ProfileComment.Name = "GBox_ProfileComment";
            this.GBox_ProfileComment.Size = new System.Drawing.Size(379, 213);
            this.GBox_ProfileComment.TabIndex = 3;
            this.GBox_ProfileComment.TabStop = false;
            this.GBox_ProfileComment.Text = "Profile comment";
            // 
            // TBox_ProfileComment
            // 
            this.TBox_ProfileComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_ProfileComment.Location = new System.Drawing.Point(3, 15);
            this.TBox_ProfileComment.Multiline = true;
            this.TBox_ProfileComment.Name = "TBox_ProfileComment";
            this.TBox_ProfileComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBox_ProfileComment.Size = new System.Drawing.Size(373, 195);
            this.TBox_ProfileComment.TabIndex = 0;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(316, 280);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 30);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Ok.Location = new System.Drawing.Point(231, 280);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(75, 30);
            this.Btn_Ok.TabIndex = 5;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // ProfileEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 322);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.GBox_ProfileComment);
            this.Controls.Add(this.ChkBox_ReadOnly);
            this.Controls.Add(this.GBox_ProfileName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileEditDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profile config";
            this.Load += new System.EventHandler(this.ProfileEditDialog_Load);
            this.GBox_ProfileName.ResumeLayout(false);
            this.GBox_ProfileName.PerformLayout();
            this.GBox_ProfileComment.ResumeLayout(false);
            this.GBox_ProfileComment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_ProfileName;
        private System.Windows.Forms.TextBox TBox_ProfileName;
        private System.Windows.Forms.CheckBox ChkBox_ReadOnly;
        private System.Windows.Forms.GroupBox GBox_ProfileComment;
        private System.Windows.Forms.TextBox TBox_ProfileComment;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Ok;
    }
}