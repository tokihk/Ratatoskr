namespace Ratatoskr.Forms.Controls
{
    partial class SplashScreen
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
            this.Label_SequenceText = new System.Windows.Forms.Label();
            this.PBar_SequenceValue = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Label_SequenceText
            // 
            this.Label_SequenceText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_SequenceText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_SequenceText.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_SequenceText.Location = new System.Drawing.Point(0, 0);
            this.Label_SequenceText.Name = "Label_SequenceText";
            this.Label_SequenceText.Size = new System.Drawing.Size(160, 37);
            this.Label_SequenceText.TabIndex = 1;
            this.Label_SequenceText.Text = "起動中";
            this.Label_SequenceText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PBar_SequenceValue
            // 
            this.PBar_SequenceValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PBar_SequenceValue.Location = new System.Drawing.Point(0, 37);
            this.PBar_SequenceValue.Name = "PBar_SequenceValue";
            this.PBar_SequenceValue.Size = new System.Drawing.Size(160, 23);
            this.PBar_SequenceValue.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PBar_SequenceValue.TabIndex = 0;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 60);
            this.ControlBox = false;
            this.Controls.Add(this.Label_SequenceText);
            this.Controls.Add(this.PBar_SequenceValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Label_SequenceText;
        private System.Windows.Forms.ProgressBar PBar_SequenceValue;
    }
}