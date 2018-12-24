using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using RtsCore.Framework.PacketConverter;
using RtsCore.Packet;

namespace Ratatoskr.PacketConverters.Separator.DataLengthMatch
{
    internal sealed class RuleObjectImpl : RuleObject
    {
        private PacketBuilder packet_busy_ = null;
        private PacketObject        packet_last_ = null;

        private int match_length_ = -1;
        private Label label1;
        private NumericUpDown Num_Value;


        public RuleObjectImpl()
        {
            InitializeComponent();
        }

        public RuleObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            SetLength((int)prop.DataLengthMatchProperty.Length.Value);

            Apply();
        }

        private void InitializeComponent()
        {
            this.Num_Value = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // Num_Value
            // 
            this.Num_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num_Value.Location = new System.Drawing.Point(0, 0);
            this.Num_Value.Margin = new System.Windows.Forms.Padding(0);
            this.Num_Value.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Num_Value.Name = "Num_Value";
            this.Num_Value.Size = new System.Drawing.Size(80, 19);
            this.Num_Value.TabIndex = 0;
            this.Num_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_Value.Value = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Num_Value.ValueChanged += new System.EventHandler(this.Num_Value_ValueChanged);
            this.Num_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Num_Value_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "byte";
            // 
            // RuleObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Num_Value);
            this.Name = "RuleObjectImpl";
            this.Size = new System.Drawing.Size(433, 25);
            ((System.ComponentModel.ISupportInitialize)(this.Num_Value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetLength(int value)
        {
            Num_Value.Value = value;
        }

        private void Apply()
        {
            match_length_ = (int)Property.DataLengthMatchProperty.Length.Value;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            var value = Num_Value.Value;

            Num_Value.ForeColor = (value == Property.DataLengthMatchProperty.Length.Value)
                                ? (Color.Black)
                                : (Color.Gray);
        }

        public override void OnBackupProperty()
        {
            Property.DataLengthMatchProperty.Length.Value = Num_Value.Value;
        }

        protected override void OnInputStatusClear()
        {
            packet_busy_ = null;
        }

        protected override void OnInputPacket(uint input_ch, PacketObject input, ref List<PacketObject> output)
        {
            /* パターンが正しくない場合は無視 */
            if (match_length_ < 0) {
                output.Add(input);
                return;
            }

            /* 収集開始 */
            if (packet_busy_ == null) {
                packet_busy_ = new PacketBuilder(input);
            }

            /* 最終受信パケットを記憶 */
            packet_last_ = input;

            var packets = new Queue<PacketObject>();
            var packet_new = (PacketObject)null;

            /* データ収集 */
            foreach (var data in input.Data) {
                /* 仮想パケットにデータを追加 */
                packet_busy_.AddData(data);

                /* データ長が指定長以上になるまで無視 */
                if (packet_busy_.DataLength < match_length_)continue;

                /* パケット生成 */
                packet_new = packet_busy_.Compile(input);

                if (packet_new != null) {
                    output.Add(packet_new);
                }

                /* 新しいパケットの収集を開始 */
                packet_busy_ = new PacketBuilder(input);
            }
        }

        protected override void OnInputBreak(ref List<PacketObject> output)
        {
            if (packet_busy_ == null) {
                return;
            }

            if (packet_busy_.DataLength > 0) {
                var packet_new = packet_busy_.Compile(packet_last_);

                if (packet_new != null) {
                    output.Add(packet_new);
                }
            }

            packet_busy_ = null;
        }

        private void Num_Value_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void Num_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (   (e.KeyCode == Keys.Enter)
                && (Num_Value.Value != Property.DataLengthMatchProperty.Length.Value)
            ) {
                OnBackupProperty();
                Apply();
            }
        }
    }
}
