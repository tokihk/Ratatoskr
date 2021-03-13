using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.PacketConverter;
using RtsCore.Packet;
using RtsCore.Utility;

namespace Ratatoskr.PacketConverters.Grouping.DataContentsMatch
{
    internal sealed class ConvertMethodClassImpl : ConvertMethodClass
    {
        private Forms.Controls.RoundTextBox TBox_Value;


        public ConvertMethodClassImpl()
        {
            InitializeComponent();
        }

        public ConvertMethodClassImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            TBox_Value.Text = prop.DataContentsMatchProperty.Pattern.Value;

            Apply();
        }

        private void InitializeComponent()
        {
            this.TBox_Value = new Ratatoskr.Forms.Controls.RoundTextBox();
            this.SuspendLayout();
            // 
            // TBox_Value
            // 
            this.TBox_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Value.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_Value.Location = new System.Drawing.Point(0, 0);
            this.TBox_Value.Margin = new System.Windows.Forms.Padding(0);
            this.TBox_Value.Name = "TBox_Value";
            this.TBox_Value.Size = new System.Drawing.Size(433, 19);
            this.TBox_Value.TabIndex = 1;
            this.TBox_Value.TextChanged += new System.EventHandler(this.TBox_Value_TextChanged);
            this.TBox_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_Value_KeyDown);
            // 
            // RuleObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.TBox_Value);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RuleObjectImpl";
            this.Size = new System.Drawing.Size(433, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void OnBackupProperty()
        {
            Property.DataContentsMatchProperty.Pattern.Value = TBox_Value.Text;
        }

        protected override ConvertMethodInstance OnCreateMethodInstance()
        {
            return (new ConvertMethodInstanceImpl(Property.DataContentsMatchProperty));
        }

        private void Apply()
        {
            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            var text = TBox_Value.Text;

            TBox_Value.ForeColor = (text == Property.DataContentsMatchProperty.Pattern.Value)
                                 ? (Color.Black)
                                 : (Color.Gray);

            if (text.Length > 0) {
                TBox_Value.BackColor = (HexTextEncoder.ToByteArray(TBox_Value.Text) != null)
                                     ? (RtsCore.Parameter.COLOR_OK)
                                     : (RtsCore.Parameter.COLOR_NG);
            } else {
                TBox_Value.BackColor = Color.White;
            }
        }

        private void TBox_Value_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void TBox_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (   (e.KeyCode == Keys.Enter)
                && (TBox_Value.Text != Property.DataContentsMatchProperty.Pattern.Value)
            ) {
                OnBackupProperty();
                Apply();
            }
        }
    }
}
