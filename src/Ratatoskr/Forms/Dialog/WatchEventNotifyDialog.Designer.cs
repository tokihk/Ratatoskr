namespace Ratatoskr.Forms.Dialog
{
    partial class WatchEventNotifyDialog
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
            this.TBox_Event = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TBox_Event
            // 
            this.TBox_Event.BackColor = System.Drawing.SystemColors.Control;
            this.TBox_Event.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBox_Event.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Event.Location = new System.Drawing.Point(0, 0);
            this.TBox_Event.Multiline = true;
            this.TBox_Event.Name = "TBox_Event";
            this.TBox_Event.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBox_Event.Size = new System.Drawing.Size(464, 321);
            this.TBox_Event.TabIndex = 0;
            // 
            // WatchEventNotifyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.TBox_Event);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WatchEventNotifyDialog";
            this.ShowIcon = false;
            this.Text = "Watch event catch";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WatchEventNotifyDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBox_Event;
    }
}