using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.Generic;
using Ratatoskr.Packet;

namespace Ratatoskr.PacketConverters.Convert.CodeExtentionDecode
{
    internal sealed class AlgorithmObjectImpl : AlgorithmObject
    {
        private byte ext_code_ = 0x00;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label3;
        private NumericUpDown Num_ExtCode;
        private Label label5;
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

            Num_ExtCode.Value = prop.CodeExtentionDecodeProperty.ExtCode.Value;
            Num_ExtMask.Value = prop.CodeExtentionDecodeProperty.ExtMask.Value;

            Apply();
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_ExtCode = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Num_ExtMask = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtMask)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.Num_ExtCode);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.Num_ExtMask);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 24);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Pre code";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_ExtCode
            // 
            this.Num_ExtCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_ExtCode.Hexadecimal = true;
            this.Num_ExtCode.Location = new System.Drawing.Point(56, 3);
            this.Num_ExtCode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.Num_ExtCode.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_ExtCode.Name = "Num_ExtCode";
            this.Num_ExtCode.Size = new System.Drawing.Size(50, 19);
            this.Num_ExtCode.TabIndex = 12;
            this.Num_ExtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(111, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(2, 22);
            this.label5.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Output mask";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_ExtMask
            // 
            this.Num_ExtMask.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_ExtMask.Hexadecimal = true;
            this.Num_ExtMask.Location = new System.Drawing.Point(194, 3);
            this.Num_ExtMask.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.Num_ExtMask.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_ExtMask.Name = "Num_ExtMask";
            this.Num_ExtMask.Size = new System.Drawing.Size(50, 19);
            this.Num_ExtMask.TabIndex = 15;
            this.Num_ExtMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AlgorithmObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlgorithmObjectImpl";
            this.Size = new System.Drawing.Size(300, 24);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ExtMask)).EndInit();
            this.ResumeLayout(false);

        }

        private void Apply()
        {
            ext_code_ = (byte)Property.CodeExtentionDecodeProperty.ExtCode.Value;
            ext_mask_ = (byte)Property.CodeExtentionDecodeProperty.ExtMask.Value;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            Num_ExtCode.ForeColor = (Num_ExtCode.Value == Property.CodeExtentionDecodeProperty.ExtCode.Value)
                                  ? (Color.Black)
                                  : (Color.Gray);

            Num_ExtMask.ForeColor = (Num_ExtMask.Value == Property.CodeExtentionDecodeProperty.ExtMask.Value)
                                  ? (Color.Black)
                                  : (Color.Gray);
        }

        public override void OnBackupProperty()
        {
            Property.CodeExtentionDecodeProperty.ExtCode.Value = Num_ExtCode.Value;
            Property.CodeExtentionDecodeProperty.ExtMask.Value = Num_ExtMask.Value;
        }

        public override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            var packet_new = new DynamicPacketObject(input);
            var fxext = false;

            foreach (var data in input.Data) {
                if (fxext) {
                    packet_new.AddData((byte)(data | ext_mask_));
                    fxext = false;
                } else if (data == ext_code_) {
                    fxext = true;
                } else {
                    packet_new.AddData(data);
                }
            }

            output.Add(packet_new.Compile());
        }

        private void Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (   (Num_ExtCode.Value != Property.CodeExtentionDecodeProperty.ExtCode.Value)
                    || (Num_ExtMask.Value != Property.CodeExtentionDecodeProperty.ExtMask.Value)
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
