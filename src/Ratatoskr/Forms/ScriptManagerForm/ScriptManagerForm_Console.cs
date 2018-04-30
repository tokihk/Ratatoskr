using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.ScriptManagerForm
{
    public partial class ScriptManagerForm_Console : UserControl
    {
        public ScriptManagerForm_Console()
        {
            InitializeComponent();
        }

        public void ClearMessage()
        {
            RTBox_Output.ResetText();
        }

        public void OutputMessageLine(string message)
        {
            RTBox_Output.AppendText(message);
            RTBox_Output.AppendText(System.Environment.NewLine);
        }
    }
}
