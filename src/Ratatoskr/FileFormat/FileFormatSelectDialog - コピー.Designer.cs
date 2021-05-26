namespace Ratatoskr.FileFormat
{
    partial class FileFormatSelectDialog
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
            this.LBox_Main = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LBox_Main
            // 
            this.LBox_Main.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LBox_Main.FormattingEnabled = true;
            this.LBox_Main.Location = new System.Drawing.Point(0, 0);
            this.LBox_Main.Name = "LBox_Main";
            this.LBox_Main.Size = new System.Drawing.Size(344, 199);
            this.LBox_Main.TabIndex = 0;
            this.LBox_Main.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LBox_Main_DrawItem);
            this.LBox_Main.DoubleClick += new System.EventHandler(this.LBox_Main_DoubleClick);
            // 
            // FileFormatSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 509);
            this.Controls.Add(this.LBox_Main);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileFormatSelectDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select file format";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LBox_Main;
    }
}