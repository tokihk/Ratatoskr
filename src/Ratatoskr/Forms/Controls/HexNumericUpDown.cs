using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Native;

namespace Ratatoskr.Forms.Controls
{
    internal class HexNumericUpDown : NumericUpDown
    {
        private bool zero_padding_ = false;


        public HexNumericUpDown()
        {
            Hexadecimal = true;
        }

        public bool ZeroPadding
        {
            get { return (zero_padding_); }
            set
            {
                zero_padding_ = value;
                Refresh();
            }
        }

        public override string Text
        {
            get { return (base.Text); }
            set
            {
                var value_max_int = (ulong)Math.Truncate(Maximum);
                var value_max_int_str = value_max_int.ToString((Hexadecimal) ? ("X") : (""));

                var value_int = (ulong)Math.Truncate(Value);
                var value_int_str = value_int.ToString((Hexadecimal) ? ("X") : (""));

                var value_str = new StringBuilder();

                if (value_max_int_str.Length > value_int_str.Length) {
                    value_str.Append('0', value_max_int_str.Length - value_int_str.Length);
                }

                value_str.Append(value_int.ToString((Hexadecimal) ? ("X") : ("")));

                base.Text = value_str.ToString();
            }
        }

        private int GetValueText(decimal value)
        {
            var value_int = Math.Truncate(value);

            return ((Hexadecimal) ? (value_int.ToString("X").Length) : (value_int.ToString().Length));
        }

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
        }
    }
}
