namespace Ratatoskr.Forms.Dialog
{
    partial class ProgressDialog
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
            this.PBar_Value = new System.Windows.Forms.ProgressBar();
            this.Label_Text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PBar_Value
            // 
            this.PBar_Value.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PBar_Value.Location = new System.Drawing.Point(0, 23);
            this.PBar_Value.Name = "PBar_Value";
            this.PBar_Value.Size = new System.Drawing.Size(232, 20);
            this.PBar_Value.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.PBar_Value.TabIndex = 0;
            this.PBar_Value.Value = 50;
            // 
            // Label_Text
            // 
            this.Label_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_Text.Location = new System.Drawing.Point(0, 0);
            this.Label_Text.Name = "Label_Text";
            this.Label_Text.Size = new System.Drawing.Size(232, 23);
            this.Label_Text.TabIndex = 1;
            this.Label_Text.Text = "label1";
            this.Label_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 43);
            this.Controls.Add(this.Label_Text);
            this.Controls.Add(this.PBar_Value);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ProgressDialog";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar PBar_Value;
        private System.Windows.Forms.Label Label_Text;
    }
}