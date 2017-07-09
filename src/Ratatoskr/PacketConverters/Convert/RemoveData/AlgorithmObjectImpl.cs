using System;
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

namespace Ratatoskr.PacketConverters.Convert.RemoveData
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
        private Label label5;
        private int data_length_ = 0;

        public AlgorithmObjectImpl()
        {
            InitializeComponent();
        }

        public AlgorithmObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            Num_DataOffset.Value = prop.RemoveDataProperty.DataOffset.Value;
            Num_DataLength.Value = prop.RemoveDataProperty.DataLength.Value;

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
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataLength)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.Num_DataOffset);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.Num_DataLength);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(555, 25);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Remove offset";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_DataOffset
            // 
            this.Num_DataOffset.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_DataOffset.Location = new System.Drawing.Point(89, 3);
            this.Num_DataOffset.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_DataOffset.Minimum = new decimal(new int[] {
            999999,
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
            this.Num_DataOffset.ValueChanged += new System.EventHandler(this.Num_ValueChanged);
            this.Num_DataOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Num_KeyDown);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(330, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label2.Size = new System.Drawing.Size(2, 24);
            this.label2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(70, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "Remove size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_DataLength
            // 
            this.Num_DataLength.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_DataLength.Location = new System.Drawing.Point(414, 3);
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
            this.Num_DataLength.ValueChanged += new System.EventHandler(this.Num_ValueChanged);
            this.Num_DataLength.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Num_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(500, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(33, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "bytes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label5.Size = new System.Drawing.Size(149, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "(X < 0 : Count from the end)";
            // 
            // AlgorithmObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlgorithmObjectImpl";
            this.Size = new System.Drawing.Size(555, 25);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataLength)).EndInit();
            this.ResumeLayout(false);

        }

        private void Apply()
        {
            data_offset_ = (byte)Property.RemoveDataProperty.DataOffset.Value;
            data_length_ = (byte)Property.RemoveDataProperty.DataLength.Value;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            Num_DataOffset.ForeColor = (Num_DataOffset.Value == Property.RemoveDataProperty.DataOffset.Value)
                                     ? (Color.Black)
                                     : (Color.Gray);

            Num_DataLength.ForeColor = (Num_DataLength.Value == Property.RemoveDataProperty.DataLength.Value)
                                     ? (Color.Black)
                                     : (Color.Gray);
        }

        public override void OnBackupProperty()
        {
            Property.RemoveDataProperty.DataOffset.Value = Num_DataOffset.Value;
            Property.RemoveDataProperty.DataLength.Value = Num_DataLength.Value;
        }

        public override void OnInputPacket(DataPacketObject input, ref List<PacketObject> output)
        {
            var data = input.GetData();
            var offset = 0;
            var length = 0;

            /* 削除開始位置 */
            if (data_offset_ < 0) {
                /* データの最後尾から計算 */
                offset = Math.Max(0, data.Length + data_offset_);

            } else {
                /* データの先頭から計算 */
                offset = Math.Min(data_offset_, Math.Max(0, data.Length - 1));
            }

            /* 削除サイズ */
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
                Array.Copy(data, 0, data_new, 0, offset);
            }
            if ((offset + length) < data.Length) {
                Array.Copy(data, offset + length, data_new, offset, data.Length - offset - length);
            }

            output.Add(new StaticDataPacketObject(input, data_new));
        }

        private void Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (   (Num_DataOffset.Value != Property.RemoveDataProperty.DataOffset.Value)
                    || (Num_DataLength.Value != Property.RemoveDataProperty.DataLength.Value)
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
