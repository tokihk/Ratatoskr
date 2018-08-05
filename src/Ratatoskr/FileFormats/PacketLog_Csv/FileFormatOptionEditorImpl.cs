using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats.PacketLog_Csv
{
    internal sealed class FileFormatOptionEditorImpl : FileFormatOptionEditor
    {
        private System.Windows.Forms.ListBox listBox1;

        public FileFormatOptionEditorImpl(FileFormatOptionImpl option)
        {
        }

        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(3, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(140, 242);
            this.listBox1.TabIndex = 0;
            // 
            // FileFormatOptionEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.listBox1);
            this.Name = "FileFormatOptionEditorImpl";
            this.Size = new System.Drawing.Size(480, 310);
            this.ResumeLayout(false);

        }
    }
}
