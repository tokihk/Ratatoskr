using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.ScriptEngines;

namespace Ratatoskr.Forms.ScriptWindow
{
    internal partial class ScriptWindow_Console : UserControl
    {
        public ScriptWindow_Console()
        {
            InitializeComponent();
        }

        public void ClearMessage()
        {
            RTBox_Output.ResetText();
        }

        public void AddMessage(ScriptMessageData msg)
        {
            var color_text = Color.Black;

            switch (msg.Type) {
                case ScriptMessageType.Error:               color_text = Color.Red;     break;
                case ScriptMessageType.Informational:       color_text = Color.Blue;    break;
            }

            var selection_start_backup = RTBox_Output.SelectionStart;
            var selection_length_backup = RTBox_Output.SelectionLength;

            RTBox_Output.SelectionStart = RTBox_Output.TextLength;
            RTBox_Output.SelectionLength = 0;
            RTBox_Output.SelectionColor = color_text;

            RTBox_Output.AppendText(string.Format("[{0}] {1}", msg.CreateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), msg.Message));
            RTBox_Output.AppendText(System.Environment.NewLine);

            RTBox_Output.SelectionStart = selection_start_backup;
            RTBox_Output.SelectionLength = selection_length_backup;
        }
    }
}
