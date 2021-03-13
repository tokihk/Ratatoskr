using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.General;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter.Convert.ChangeAlias
{
    internal sealed class AlgorithmObjectImpl : AlgorithmObject
    {
        private string     value_ = "";


        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Forms.RoundTextBox TBox_Value;

        public AlgorithmObjectImpl()
        {
            InitializeComponent();
        }

        public AlgorithmObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            TBox_Value.Text = prop.ChangeAliasProperty.Value.Value;

            Apply();
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TBox_Value = new Ratatoskr.Forms.RoundTextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.TBox_Value);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(490, 25);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "Changed value";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBox_Value
            // 
            this.TBox_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_Value.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_Value.Location = new System.Drawing.Point(89, 3);
            this.TBox_Value.Name = "TBox_Value";
            this.TBox_Value.Size = new System.Drawing.Size(200, 19);
            this.TBox_Value.TabIndex = 18;
            this.TBox_Value.TextChanged += new System.EventHandler(this.TBox_Value_TextChanged);
            this.TBox_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_Value_KeyDown);
            // 
            // AlgorithmObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlgorithmObjectImpl";
            this.Size = new System.Drawing.Size(490, 25);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Apply()
        {
            value_ = Property.ChangeAliasProperty.Value.Value;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            TBox_Value.ForeColor = (TBox_Value.Text == Property.ChangeAliasProperty.Value.Value)
                                 ? (Color.Black)
                                 : (Color.Gray);
        }

        public override void OnBackupProperty()
        {
            Property.ChangeAliasProperty.Value.Value = TBox_Value.Text;
        }

        public override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* パケットを複製 */
            var packet = ClassUtil.Clone(input);

            /* パラメータを変更 */
            packet.Alias = value_;

            output.Add(packet);
        }

        private void Num_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void TBox_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (TBox_Value.Text != Property.ChangeAliasProperty.Value.Value) {
                    OnBackupProperty();
                    Apply();
                }
            }
        }

        private void TBox_Value_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void CBox_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnBackupProperty();
            Apply();
        }
    }
}
