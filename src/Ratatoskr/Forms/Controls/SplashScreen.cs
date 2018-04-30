using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Controls
{
    internal partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private delegate void SetProgressDelegate(string text, byte value);
        public void SetProgress(string text, byte value)
        {
            if (InvokeRequired) {
                Invoke((SetProgressDelegate)SetProgress, text, value);
                return;
            }

            Label_SequenceText.Text = text;

            /* アニメーション防止のため、上げてから戻す */
            PBar_SequenceValue.Maximum = PBar_SequenceValue.Maximum + 1;
            PBar_SequenceValue.Value = PBar_SequenceValue.Maximum;
            PBar_SequenceValue.Value = value;
            PBar_SequenceValue.Maximum = PBar_SequenceValue.Maximum - 1;
        }
    }
}
