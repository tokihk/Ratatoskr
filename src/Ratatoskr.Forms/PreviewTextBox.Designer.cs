namespace Ratatoskr.Forms
{
    partial class PreviewTextBox
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
            this.TBox_Data = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TBox_Data
            // 
            this.TBox_Data.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBox_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Data.Location = new System.Drawing.Point(0, 0);
            this.TBox_Data.Multiline = true;
            this.TBox_Data.Name = "TBox_Data";
            this.TBox_Data.Size = new System.Drawing.Size(284, 262);
            this.TBox_Data.TabIndex = 0;
            // 
            // PreviewLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.TBox_Data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewLabel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NonActiveForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBox_Data;
    }
}