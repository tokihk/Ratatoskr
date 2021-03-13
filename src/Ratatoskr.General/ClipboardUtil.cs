using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.General
{
    public static class ClipboardUtil
    {
        public static void SetText(string text, TextDataFormat format)
        {
            if (string.IsNullOrEmpty(text)) {
                Clipboard.Clear();
            } else {
                Clipboard.SetText(text, format);
            }
        }
    }
}
