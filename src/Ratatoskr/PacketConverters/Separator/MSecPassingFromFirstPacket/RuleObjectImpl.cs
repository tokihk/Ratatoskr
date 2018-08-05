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

namespace Ratatoskr.PacketConverters.Separator.MSecPassingFromFirstPacket
{
    internal sealed class RuleObjectImpl : RuleObject
    {
        private DynamicPacketObject packet_busy_ = null;
        private PacketObject            packet_last_ = null;

        private DateTime dt_base_ = DateTime.MaxValue;
        private int match_interval_ = -1;
        private Label label1;
        private NumericUpDown Num_Value;


        public RuleObjectImpl()
        {
            InitializeComponent();
        }

        public RuleObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            SetInterval((int)prop.MSecPassingFromFirstPacketProperty.Interval.Value);

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
            this.Num_Value.Location = new System.Drawing.Point(0, 0);
            this.Num_Value.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.Num_Value.Name = "Num_Value";
            this.Num_Value.Size = new System.Drawing.Size(90, 19);
            this.Num_Value.TabIndex = 0;
            this.Num_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_Value.Value = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.Num_Value.ValueChanged += new System.EventHandler(this.Num_Value_ValueChanged);
            this.Num_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Num_Value_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "msec";
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

        private void SetInterval(int value)
        {
            Num_Value.Value = value;
        }

        private void UpdateView()
        {
            var value = Num_Value.Value;

            Num_Value.ForeColor = (value == Property.MSecPassingFromFirstPacketProperty.Interval.Value)
                                ? (Color.Black)
                                : (Color.Gray);
        }

        private void Apply()
        {
            match_interval_ = (int)Property.MSecPassingFromFirstPacketProperty.Interval.Value;

            UpdateConvertStatus();
            UpdateView();
        }

        public override void OnBackupProperty()
        {
            Property.MSecPassingFromFirstPacketProperty.Interval.Value = Num_Value.Value;
        }

        public override void OnInputStatusClear()
        {
            packet_busy_ = null;
            dt_base_ = DateTime.MaxValue;
        }

        public override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* パターンが正しくない場合はスルー */
            if (match_interval_ < 0) {
                output.Add(input);
            }

            /* 収集開始 */
            if (packet_busy_ == null) {
                packet_busy_ = new DynamicPacketObject(input);

                /* 受信開始時刻を記憶 */
                dt_base_ = input.MakeTime;
            }

            /* 最終受信パケットを記憶 */
            packet_last_ = input;

            /* データ収集 */
            packet_busy_.AddData(input.Data);

            /* 入力パケット時刻が基準時刻を超えていない場合は無視 */
            if (input.MakeTime < dt_base_) {
                return;
            }

            /* 時間が経過したかどうかを確認 */
            if ((input.MakeTime - dt_base_).TotalMilliseconds < match_interval_) {
                return;
            }

            /* 最新パケットで生成 */
            var packet_new = packet_busy_.Compile(packet_last_);

            if (packet_new != null) {
                output.Add(packet_new);
            }

            packet_busy_ = null;
        }

        public override void OnInputBreak(ref List<PacketObject> output)
        {
            if (packet_busy_ == null)return;

            if (packet_busy_.DataLength > 0) {
                var packet_new = packet_busy_.Compile(packet_last_);

                if (packet_new != null) {
                    output.Add(packet_new);
                }
            }

            packet_busy_ = null;
        }

        public override void OnInputPoll(ref List<PacketObject> output)
        {
            if (packet_busy_ == null)return;

            var dt_now = DateTime.UtcNow;

            if (dt_now < dt_base_)return;
            if ((dt_now - dt_base_).TotalMilliseconds < match_interval_)return;

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
                && (Num_Value.Value != Property.MSecPassingFromFirstPacketProperty.Interval.Value)
            ) {
                OnBackupProperty();
                Apply();
            }
        }
    }
}
