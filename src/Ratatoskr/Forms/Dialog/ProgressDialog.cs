using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Dialog
{
    public partial class ProgressDialog : Form
    {
        public ProgressDialog()
        {
            InitializeComponent();
        }

        public string ProgressText
        {
            get { return (Label_Text.Text); }
            set { Label_Text.Text = value; }
        }

        public int ProgressValueMax
        {
            get { return (PBar_Value.Maximum); }
        }

        public int ProgressValueMin
        {
            get { return (PBar_Value.Minimum); }
        }

        public int ProgressValue
        {
            get { return (PBar_Value.Value); }
        }
    }
}
