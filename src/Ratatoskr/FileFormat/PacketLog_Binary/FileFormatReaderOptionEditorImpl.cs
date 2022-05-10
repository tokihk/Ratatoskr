using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.BinaryText;

namespace Ratatoskr.FileFormat.PacketLog_Binary
{
    internal sealed class FileFormatReaderOptionEditorImpl : FileFormatOptionEditor
    {
        private System.Windows.Forms.NumericUpDown Num_MaxDataSize;
        private System.Windows.Forms.NumericUpDown Num_PacketInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GBox_CustomAlias;
        private System.Windows.Forms.TextBox TBox_PacketAlias;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown Num_FirstPacketDateTime_Usec;
		private System.Windows.Forms.DateTimePicker DTP_FirstPacketDateTime;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox TBox_TerminateDataPattern;
		private System.Windows.Forms.CheckBox ChkBox_PacketTime_Local;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.ComboBox CBox_AliasMode;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox3;


        public FileFormatReaderOptionEditorImpl()
        {
            InitializeComponent();
			InitializeAliasMode();
        }

        private void InitializeComponent()
        {
            this.Num_MaxDataSize = new System.Windows.Forms.NumericUpDown();
            this.Num_PacketInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.GBox_CustomAlias = new System.Windows.Forms.GroupBox();
            this.TBox_PacketAlias = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TBox_TerminateDataPattern = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ChkBox_PacketTime_Local = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Num_FirstPacketDateTime_Usec = new System.Windows.Forms.NumericUpDown();
            this.DTP_FirstPacketDateTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CBox_AliasMode = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Num_MaxDataSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketInterval)).BeginInit();
            this.GBox_CustomAlias.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_FirstPacketDateTime_Usec)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // Num_MaxDataSize
            // 
            this.Num_MaxDataSize.Location = new System.Drawing.Point(6, 18);
            this.Num_MaxDataSize.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_MaxDataSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_MaxDataSize.Name = "Num_MaxDataSize";
            this.Num_MaxDataSize.Size = new System.Drawing.Size(100, 19);
            this.Num_MaxDataSize.TabIndex = 1;
            this.Num_MaxDataSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_MaxDataSize.ThousandsSeparator = true;
            this.Num_MaxDataSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // Num_PacketInterval
            // 
            this.Num_PacketInterval.Location = new System.Drawing.Point(6, 18);
            this.Num_PacketInterval.Maximum = new decimal(new int[] {
            60000000,
            0,
            0,
            0});
            this.Num_PacketInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_PacketInterval.Name = "Num_PacketInterval";
            this.Num_PacketInterval.Size = new System.Drawing.Size(120, 19);
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
            this.label1.Location = new System.Drawing.Point(132, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "usec";
            // 
            // GBox_CustomAlias
            // 
            this.GBox_CustomAlias.Controls.Add(this.TBox_PacketAlias);
            this.GBox_CustomAlias.Location = new System.Drawing.Point(154, 18);
            this.GBox_CustomAlias.Name = "GBox_CustomAlias";
            this.GBox_CustomAlias.Size = new System.Drawing.Size(200, 48);
            this.GBox_CustomAlias.TabIndex = 0;
            this.GBox_CustomAlias.TabStop = false;
            this.GBox_CustomAlias.Text = "Custom Alias";
            // 
            // TBox_PacketAlias
            // 
            this.TBox_PacketAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_PacketAlias.Location = new System.Drawing.Point(3, 15);
            this.TBox_PacketAlias.MaxLength = 32;
            this.TBox_PacketAlias.Name = "TBox_PacketAlias";
            this.TBox_PacketAlias.Size = new System.Drawing.Size(194, 19);
            this.TBox_PacketAlias.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "byte";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Location = new System.Drawing.Point(3, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(451, 76);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Packet Data";
            // 
            // TBox_TerminateDataPattern
            // 
            this.TBox_TerminateDataPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_TerminateDataPattern.Location = new System.Drawing.Point(6, 18);
            this.TBox_TerminateDataPattern.Name = "TBox_TerminateDataPattern";
            this.TBox_TerminateDataPattern.Size = new System.Drawing.Size(268, 19);
            this.TBox_TerminateDataPattern.TabIndex = 14;
            this.TBox_TerminateDataPattern.TextChanged += new System.EventHandler(this.TBox_TerminateDataPattern_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ChkBox_PacketTime_Local);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Location = new System.Drawing.Point(3, 83);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(507, 91);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Packet Datetime";
            // 
            // ChkBox_PacketTime_Local
            // 
            this.ChkBox_PacketTime_Local.AutoSize = true;
            this.ChkBox_PacketTime_Local.Location = new System.Drawing.Point(16, 69);
            this.ChkBox_PacketTime_Local.Name = "ChkBox_PacketTime_Local";
            this.ChkBox_PacketTime_Local.Size = new System.Drawing.Size(77, 16);
            this.ChkBox_PacketTime_Local.TabIndex = 14;
            this.ChkBox_PacketTime_Local.Text = "Local time";
            this.ChkBox_PacketTime_Local.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(161, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "+";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "usec";
            // 
            // Num_FirstPacketDateTime_Usec
            // 
            this.Num_FirstPacketDateTime_Usec.Location = new System.Drawing.Point(179, 18);
            this.Num_FirstPacketDateTime_Usec.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_FirstPacketDateTime_Usec.Name = "Num_FirstPacketDateTime_Usec";
            this.Num_FirstPacketDateTime_Usec.Size = new System.Drawing.Size(77, 19);
            this.Num_FirstPacketDateTime_Usec.TabIndex = 9;
            this.Num_FirstPacketDateTime_Usec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_FirstPacketDateTime_Usec.ThousandsSeparator = true;
            // 
            // DTP_FirstPacketDateTime
            // 
            this.DTP_FirstPacketDateTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.DTP_FirstPacketDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_FirstPacketDateTime.Location = new System.Drawing.Point(6, 18);
            this.DTP_FirstPacketDateTime.Name = "DTP_FirstPacketDateTime";
            this.DTP_FirstPacketDateTime.Size = new System.Drawing.Size(149, 19);
            this.DTP_FirstPacketDateTime.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.GBox_CustomAlias);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 74);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packet Alias";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CBox_AliasMode);
            this.groupBox6.Location = new System.Drawing.Point(8, 18);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(140, 48);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Select alias";
            // 
            // CBox_AliasMode
            // 
            this.CBox_AliasMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_AliasMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_AliasMode.FormattingEnabled = true;
            this.CBox_AliasMode.Location = new System.Drawing.Point(3, 15);
            this.CBox_AliasMode.Name = "CBox_AliasMode";
            this.CBox_AliasMode.Size = new System.Drawing.Size(134, 20);
            this.CBox_AliasMode.TabIndex = 0;
            this.CBox_AliasMode.SelectedIndexChanged += new System.EventHandler(this.CBox_AliasMode_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DTP_FirstPacketDateTime);
            this.groupBox1.Controls.Add(this.Num_FirstPacketDateTime_Usec);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 46);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First packet datetime";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Num_PacketInterval);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(315, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(177, 46);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Packet interval";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.Num_MaxDataSize);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Location = new System.Drawing.Point(8, 18);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(151, 46);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Maximum data size";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.TBox_TerminateDataPattern);
            this.groupBox8.Location = new System.Drawing.Point(165, 18);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(280, 46);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Terminate data pattern";
            // 
            // FileFormatReaderOptionEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "FileFormatReaderOptionEditorImpl";
            this.Size = new System.Drawing.Size(520, 264);
            ((System.ComponentModel.ISupportInitialize)(this.Num_MaxDataSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PacketInterval)).EndInit();
            this.GBox_CustomAlias.ResumeLayout(false);
            this.GBox_CustomAlias.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_FirstPacketDateTime_Usec)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

		private void InitializeAliasMode()
		{
			CBox_AliasMode.BeginUpdate();
			{
				CBox_AliasMode.Items.Clear();
				foreach (PacketAliasModeType mode in Enum.GetValues(typeof(PacketAliasModeType))) {
					CBox_AliasMode.Items.Add(mode);
				}
			}
			CBox_AliasMode.EndUpdate();
		}

		private void UpdateAliasMode()
		{
			if (CBox_AliasMode.SelectedItem is PacketAliasModeType mode) {
				GBox_CustomAlias.Enabled = (mode == PacketAliasModeType.Custom);
			}
		}

        private void UpdateTerminateDataPattern()
        {
            var pattern_exp = TBox_TerminateDataPattern.Text;

            if (pattern_exp.Length > 0) {
                var pattern_data = BinaryTextCompiler.Build(pattern_exp);

                TBox_TerminateDataPattern.BackColor = (pattern_data != null)
                                       ? (Ratatoskr.Resource.AppColors.Ok)
                                       : (Ratatoskr.Resource.AppColors.Ng);
            } else {
                TBox_TerminateDataPattern.BackColor = Color.White;
            }
        }

		protected override void OnLoadOption(FileFormatOption option)
		{
			if (option is FileFormatReaderOptionImpl option_i) {
				CBox_AliasMode.SelectedItem = option_i.PacketAliasMode;
				TBox_PacketAlias.Text = option_i.PacketCustomAlias;

				DTP_FirstPacketDateTime.Value = option_i.PacketBaseTime;
				Num_FirstPacketDateTime_Usec.Value = (option_i.PacketBaseTime.Ticks / 10) % 1000000;
				ChkBox_PacketTime_Local.Checked = (option_i.PacketBaseTime.Kind == DateTimeKind.Local);
				Num_PacketInterval.Value = option_i.PacketIntervalUsec;

				Num_MaxDataSize.Value = option_i.DataMaxSize;
				TBox_TerminateDataPattern.Text = option_i.DataTerminatePattern;

				UpdateAliasMode();
				UpdateTerminateDataPattern();
			}
		}

		protected override void OnBackupOption(FileFormatOption option)
		{
			if (option is FileFormatReaderOptionImpl option_i) {
				UpdateTerminateDataPattern();

				option_i.PacketAliasMode = (PacketAliasModeType)CBox_AliasMode.SelectedItem;
				option_i.PacketCustomAlias = TBox_PacketAlias.Text;

				option_i.PacketBaseTime = (new DateTime(
					DTP_FirstPacketDateTime.Value.Year,
					DTP_FirstPacketDateTime.Value.Month,
					DTP_FirstPacketDateTime.Value.Day,
					DTP_FirstPacketDateTime.Value.Hour,
					DTP_FirstPacketDateTime.Value.Minute,
					DTP_FirstPacketDateTime.Value.Second,
					(ChkBox_PacketTime_Local.Checked) ? (DateTimeKind.Local) : (DateTimeKind.Utc)
				)).AddTicks((long)Num_FirstPacketDateTime_Usec.Value * 10);

				option_i.PacketIntervalUsec = (uint)Num_PacketInterval.Value;

				option_i.DataMaxSize = (uint)Num_MaxDataSize.Value;
				option_i.DataTerminatePattern = TBox_TerminateDataPattern.Text;
			}
        }

		private void CBox_AliasMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateAliasMode();
		}

		private void TBox_TerminateDataPattern_TextChanged(object sender, EventArgs e)
		{
			UpdateTerminateDataPattern();
		}
	}
}
