﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.PacketConverters.Convert.CodeExtentionEncode
{
    internal sealed class AlgorithmObjectImpl : AlgorithmObject
    {
        private byte ext_target_min_ = 0x00;
        private byte ext_target_max_ = 0x00;
        private byte ext_code_ = 0x00;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private NumericUpDown Num_ExtTarget_Min;
        private Label label2;
        private NumericUpDown Num_ExtTarget_Max;
        private Label label5;
        private Label label3;
        private NumericUpDown Num_ExtCode;
        private Label label4;
        private Label label6;
        private NumericUpDown Num_ExtMask;
        private byte ext_mask_ = 0x00;

        public AlgorithmObjectImpl()
        {
            InitializeComponent();
        }

        public AlgorithmObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            Num_ExtTarget_Min.Value = prop.CodeExtentionEncodeProperty.ExtTargetCode_Min.Value;
            Num_ExtTarget_Max.Value = prop.CodeExtentionEncodeProperty.ExtTargetCode_Max.Value;
            Num_ExtCode.Value = prop.CodeExtentionEncodeProperty.ExtCode.Value;
            Num_ExtMask.Value = prop.CodeExtentionEncodeProperty.ExtMask.Value;

            Apply();
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_ExtTarget_Min = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Num_ExtTarget_Max = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_ExtCode = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Num_ExtMask = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtTarget_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtTarget_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtMask)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.Num_ExtTarget_Min);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.Num_ExtTarget_Max);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.Num_ExtCode);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.Num_ExtMask);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(507, 24);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "拡張対象コード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_ExtTarget_Min
            // 
            this.Num_ExtTarget_Min.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_ExtTarget_Min.Hexadecimal = true;
            this.Num_ExtTarget_Min.Location = new System.Drawing.Point(86, 3);
            this.Num_ExtTarget_Min.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.Num_ExtTarget_Min.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_ExtTarget_Min.Name = "Num_ExtTarget_Min";
            this.Num_ExtTarget_Min.Size = new System.Drawing.Size(50, 19);
            this.Num_ExtTarget_Min.TabIndex = 10;
            this.Num_ExtTarget_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(140, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num_ExtTarget_Max
            // 
            this.Num_ExtTarget_Max.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_ExtTarget_Max.Hexadecimal = true;
            this.Num_ExtTarget_Max.Location = new System.Drawing.Point(154, 3);
            this.Num_ExtTarget_Max.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.Num_ExtTarget_Max.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_ExtTarget_Max.Name = "Num_ExtTarget_Max";
            this.Num_ExtTarget_Max.Size = new System.Drawing.Size(50, 19);
            this.Num_ExtTarget_Max.TabIndex = 12;
            this.Num_ExtTarget_Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(209, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(2, 24);
            this.label5.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "拡張先行コード";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_ExtCode
            // 
            this.Num_ExtCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_ExtCode.Hexadecimal = true;
            this.Num_ExtCode.Location = new System.Drawing.Point(300, 3);
            this.Num_ExtCode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.Num_ExtCode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_ExtCode.Name = "Num_ExtCode";
            this.Num_ExtCode.Size = new System.Drawing.Size(50, 19);
            this.Num_ExtCode.TabIndex = 16;
            this.Num_ExtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(354, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(2, 24);
            this.label4.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(360, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "対象コードマスク";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_ExtMask
            // 
            this.Num_ExtMask.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_ExtMask.Hexadecimal = true;
            this.Num_ExtMask.Location = new System.Drawing.Point(446, 3);
            this.Num_ExtMask.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.Num_ExtMask.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_ExtMask.Name = "Num_ExtMask";
            this.Num_ExtMask.Size = new System.Drawing.Size(50, 19);
            this.Num_ExtMask.TabIndex = 19;
            this.Num_ExtMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AlgorithmObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlgorithmObjectImpl";
            this.Size = new System.Drawing.Size(507, 24);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtTarget_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtTarget_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtMask)).EndInit();
            this.ResumeLayout(false);

        }

        private void Apply()
        {
            ext_target_min_ = (byte)Property.CodeExtentionEncodeProperty.ExtTargetCode_Min.Value;
            ext_target_max_ = (byte)Property.CodeExtentionEncodeProperty.ExtTargetCode_Max.Value;
            ext_code_ = (byte)Property.CodeExtentionEncodeProperty.ExtCode.Value;
            ext_mask_ = (byte)Property.CodeExtentionEncodeProperty.ExtMask.Value;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            Num_ExtTarget_Min.ForeColor = (Num_ExtTarget_Min.Value == Property.CodeExtentionEncodeProperty.ExtTargetCode_Min.Value)
                                        ? (Color.Black)
                                        : (Color.Gray);

            Num_ExtTarget_Max.ForeColor = (Num_ExtTarget_Max.Value == Property.CodeExtentionEncodeProperty.ExtTargetCode_Max.Value)
                                        ? (Color.Black)
                                        : (Color.Gray);

            Num_ExtCode.ForeColor = (Num_ExtCode.Value == Property.CodeExtentionEncodeProperty.ExtCode.Value)
                                  ? (Color.Black)
                                  : (Color.Gray);

            Num_ExtMask.ForeColor = (Num_ExtMask.Value == Property.CodeExtentionEncodeProperty.ExtMask.Value)
                                  ? (Color.Black)
                                  : (Color.Gray);
        }

        public override void OnBackupProperty()
        {
            Property.CodeExtentionEncodeProperty.ExtTargetCode_Min.Value = Num_ExtTarget_Min.Value;
            Property.CodeExtentionEncodeProperty.ExtTargetCode_Max.Value = Num_ExtTarget_Min.Value;
            Property.CodeExtentionEncodeProperty.ExtCode.Value = Num_ExtCode.Value;
            Property.CodeExtentionEncodeProperty.ExtMask.Value = Num_ExtMask.Value;
        }

        public override void OnInputPacket(DataPacketObject input, ref List<PacketObject> output)
        {
            var packet_new = new DynamicDataPacketObject(input);

            foreach (var data in input.GetData()) {
                if ((data >= ext_target_min_) && (data <= ext_target_max_)) {
                    packet_new.AddData(ext_code_);
                    packet_new.AddData((byte)(data & ext_mask_));
                } else {
                    packet_new.AddData(data);
                }
            }

            output.Add(packet_new.Compile());
        }

        private void Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (   (Num_ExtTarget_Min.Value != Property.CodeExtentionEncodeProperty.ExtTargetCode_Min.Value)
                    || (Num_ExtTarget_Max.Value != Property.CodeExtentionEncodeProperty.ExtTargetCode_Max.Value)
                    || (Num_ExtCode.Value != Property.CodeExtentionEncodeProperty.ExtCode.Value)
                    || (Num_ExtMask.Value != Property.CodeExtentionEncodeProperty.ExtMask.Value)
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

        private void Num_ExtTarget_Min_ValueChanged(object sender, EventArgs e)
        {
            Num_ExtTarget_Max.Value = Math.Max(Num_ExtTarget_Min.Value, Num_ExtTarget_Max.Value);

            UpdateView();
        }

        private void Num_ExtTarget_Max_ValueChanged(object sender, EventArgs e)
        {
            Num_ExtTarget_Min.Value = Math.Min(Num_ExtTarget_Min.Value, Num_ExtTarget_Max.Value);

            UpdateView();
        }
    }
}
