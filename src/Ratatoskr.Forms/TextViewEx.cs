using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms
{
    public partial class TextViewEx : UserControl
    {
        private sealed class TextParagraph
        {
            public Color  ForeColor { get; set; } = Color.Black;
            public string Text      { get; set; } = "";

            public TextParagraph(Color fore_color, string text)
            {
                ForeColor = fore_color;
                Text = text;
            }
        }


        public TextViewEx()
        {
            InitializeComponent();
        }
    }
}
