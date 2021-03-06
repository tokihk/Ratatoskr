﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketConverter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter.Transfer.File
{
    internal sealed class AlgorithmObjectImpl : AlgorithmObject
    {
        private int data_offset_ = 0;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private NumericUpDown Num_DataLength;
        private Label label2;
        private Label label3;
        private NumericUpDown Num_DataOffset;
        private Label label4;
        private int data_length_ = 0;

        public AlgorithmObjectImpl()
        {
            InitializeComponent();
        }

        public AlgorithmObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            Num_DataOffset.Value = prop.FileProperty.DataOffset.Value;
            Num_DataLength.Value = prop.FileProperty.DataLength.Value;

            Apply();
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_DataOffset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_DataLength = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataLength)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.Num_DataOffset);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.Num_DataLength);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(365, 25);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "削除開始位置";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_DataOffset
            // 
            this.Num_DataOffset.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_DataOffset.Location = new System.Drawing.Point(86, 3);
            this.Num_DataOffset.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_DataOffset.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Num_DataOffset.Name = "Num_DataOffset";
            this.Num_DataOffset.Size = new System.Drawing.Size(80, 19);
            this.Num_DataOffset.TabIndex = 11;
            this.Num_DataOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataOffset.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(172, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label2.Size = new System.Drawing.Size(2, 24);
            this.label2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "削除サイズ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_DataLength
            // 
            this.Num_DataLength.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_DataLength.Location = new System.Drawing.Point(244, 3);
            this.Num_DataLength.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_DataLength.Name = "Num_DataLength";
            this.Num_DataLength.Size = new System.Drawing.Size(80, 19);
            this.Num_DataLength.TabIndex = 13;
            this.Num_DataLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataLength.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(27, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "byte";
            // 
            // AlgorithmObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlgorithmObjectImpl";
            this.Size = new System.Drawing.Size(365, 25);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataLength)).EndInit();
            this.ResumeLayout(false);

        }

        private void Apply()
        {
            data_offset_ = (byte)Property.FileProperty.DataOffset.Value;
            data_length_ = (byte)Property.FileProperty.DataLength.Value;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            Num_DataOffset.ForeColor = (Num_DataOffset.Value == Property.FileProperty.DataOffset.Value)
                                     ? (Color.Black)
                                     : (Color.Gray);

            Num_DataLength.ForeColor = (Num_DataLength.Value == Property.FileProperty.DataLength.Value)
                                     ? (Color.Black)
                                     : (Color.Gray);
        }

        public override void OnBackupProperty()
        {
            Property.FileProperty.DataOffset.Value = Num_DataOffset.Value;
            Property.FileProperty.DataLength.Value = Num_DataLength.Value;
        }

        public override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            var data = input.Data;
            var offset = 0;
            var length = 0;

            /* データの最後尾から計算 */
            if (data_offset_ < 0) {
                offset = Math.Max(data.Length + data_offset_ - data_length_, 0);

            /* データの先頭から計算 */
            } else {
                offset = Math.Min(data_offset_, Math.Max(0, data.Length - 1));
            }
            length = Math.Min(data.Length - offset, data_length_);

            /* 削除データが存在しないため変換無し */
            if (length == 0) {
                output.Add(input);
                return;
            }

            /* 削除後のデータサイズが0場合はパケット消去 */
            if (length == data.Length) {
                return;
            }

            /* 削除後のデータを生成 */
            var data_new = new byte[data.Length - length];

            if (offset > 0) {
                Buffer.BlockCopy(data, 0, data_new, 0, offset);
            }
            if ((offset + length) < data.Length) {
                Buffer.BlockCopy(data, offset + length, data_new, offset, data.Length - offset - length);
            }

            output.Add(new PacketObject(input, data_new));
        }

        private void Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (   (Num_DataOffset.Value != Property.FileProperty.DataOffset.Value)
                    || (Num_DataLength.Value != Property.FileProperty.DataLength.Value)
                ) {
                    OnBackupProperty();
                    Apply();
                }
            }
        }

        private void Num_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
