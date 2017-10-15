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
    public partial class WatchEventNotifyDialog : Form
    {
        public WatchEventNotifyDialog()
        {
            InitializeComponent();
        }

        public void AddText(string text)
        {
            TBox_Event.AppendText(text + Environment.NewLine);
        }

        private void WatchEventNotifyDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            Hide();

            TBox_Event.Clear();
        }
    }
}
