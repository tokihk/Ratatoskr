using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.PacketConverter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter.Grouping.DataLengthMatch
{
    internal sealed class ConvertMethodClassImpl : ConvertMethodClass
    {
        private Label label1;
        private NumericUpDown Num_Value;


        public ConvertMethodClassImpl()
        {
            InitializeComponent();
        }

        public ConvertMethodClassImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            Num_Value.Value = prop.DataLengthMatchProperty.Length.Value;

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

        protected override void OnBackupProperty()
        {
            Property.DataLengthMatchProperty.Length.Value = Num_Value.Value;
        }

        protected override ConvertMethodInstance OnCreateMethodInstance()
        {
            return (new ConvertMethodInstanceImpl(Property.DataLengthMatchProperty));
        }

        private void Apply()
        {
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
