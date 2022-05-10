using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.General.BinaryText;

namespace Ratatoskr.Forms
{
    public class BinaryTextBox : TextBox
    {
        public static byte[] UpdateExpression(string exp, Control control)
        {
            var exp_bin = (byte[])null;

            if ((exp != null) && (exp.Length > 0)) {
                exp_bin = BinaryTextCompiler.Build(exp);

                control.BackColor = (exp_bin != null)
                                  ? (Ratatoskr.Resource.AppColors.Ok)
                                  : (Ratatoskr.Resource.AppColors.Ng);
            } else {
                exp_bin = null;

                control.BackColor = Color.White;
            }

            return (exp_bin);
        }


        private string  data_exp_ = null;
        private byte[]  data_bin_ = null;

        public byte[] Value
        {
            get { return (data_bin_); }
        }

        private void UpdateExpView()
        {
            data_exp_ = Text;

            data_bin_ = UpdateExpression(data_exp_, this);

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            UpdateExpView();
        }
    }
}
