using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat.PacketLog_Binary
{
    internal sealed class FileFormatReaderOptionEditorImpl : FileFormatOptionEditor
    {
        private System.Windows.Forms.NumericUpDown Num_PacketDataSize;
        private System.Windows.Forms.NumericUpDown Num_PacketInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TBox_PacketAlias;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;


        public FileFormatReaderOptionEditorImpl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Num_PacketDataSize = new System.Windows.Forms.NumericUpDown();
            this.Num_PacketInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TBox_PacketAlias = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketDataSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Num_PacketDataSize
            // 
            this.Num_PacketDataSize.Location = new System.Drawing.Point(6, 17);
            this.Num_PacketDataSize.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_PacketDataSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_PacketDataSize.Name = "Num_PacketDataSize";
            this.Num_PacketDataSize.Size = new System.Drawing.Size(100, 19);
            this.Num_PacketDataSize.TabIndex = 1;
            this.Num_PacketDataSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_PacketDataSize.ThousandsSeparator = true;
            this.Num_PacketDataSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // Num_PacketInterval
            // 
            this.Num_PacketInterval.Location = new System.Drawing.Point(6, 18);
            this.Num_PacketInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_PacketInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_PacketInterval.Name = "Num_PacketInterval";
            this.Num_PacketInterval.Size = new System.Drawing.Size(100, 19);
            this.Num_PacketInterval.TabIndex = 2;
            this.Num_PacketInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_PacketInterval.ThousandsSeparator = true;
            this.Num_PacketInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "msec";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TBox_PacketAlias);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 42);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet Alias";
            // 
            // TBox_PacketAlias
            // 
            this.TBox_PacketAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_PacketAlias.Location = new System.Drawing.Point(3, 15);
            this.TBox_PacketAlias.Name = "TBox_PacketAlias";
            this.TBox_PacketAlias.Size = new System.Drawing.Size(143, 19);
            this.TBox_PacketAlias.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Num_PacketDataSize);
            this.groupBox2.Location = new System.Drawing.Point(3, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 42);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packet Data Size";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Num_PacketInterval);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(124, 51);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(149, 42);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Packet Interval";
            // 
            // FileFormatReaderOptionEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FileFormatReaderOptionEditorImpl";
            this.Size = new System.Drawing.Size(278, 98);
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketDataSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

		protected override void OnLoadOption(FileFormatOption option)
		{
			if (option is FileFormatReaderOptionImpl option_i) {
				TBox_PacketAlias.Text = option_i.PacketAlias;
				Num_PacketDataSize.Value = option_i.PacketDataSize;
				Num_PacketInterval.Value = option_i.PacketInterval;
			}
		}

		protected override void OnBackupOption(FileFormatOption option)
		{
			if (option is FileFormatReaderOptionImpl option_i) {
				option_i.PacketAlias = TBox_PacketAlias.Text;
				option_i.PacketDataSize = (uint)Num_PacketDataSize.Value;
				option_i.PacketInterval = (uint)Num_PacketInterval.Value;
			}
        }
    }
}
