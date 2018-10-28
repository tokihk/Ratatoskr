using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Native;

namespace Ratatoskr.Forms.Controls
{
    internal partial class PreviewTextBox : Form
    {
        private class LabelEx : Label
        {
            public LabelEx()
            {
                SetStyle(ControlStyles.Selectable, false);

                DoubleBuffered = true;
            }

            protected override void WndProc(ref Message m)
            {
                switch ((uint)m.Msg) {
                    case WinAPI.WM_MOUSEACTIVATE:
                        m.Result = new IntPtr(WinAPI.MA_NOACTIVATE);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
        }


        private LabelEx label_main_ = new LabelEx();


        public PreviewTextBox()
        {
            InitializeComponent();

            label_main_.BorderStyle = BorderStyle.FixedSingle;
            label_main_.Dock = DockStyle.Fill;

            Controls.Add(label_main_);
        }

        protected override void WndProc(ref Message m)
        {
            switch ((uint)m.Msg) {
                case WinAPI.WM_MOUSEACTIVATE:
                    m.Result = new IntPtr(WinAPI.MA_NOACTIVATE);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return (true);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var p = base.CreateParams;

                p.ExStyle |= WinAPI.WS_EX_TOPMOST | WinAPI.WS_EX_NOACTIVATE;

                return (p);
            }
        }

        public Font Label_Font
        {
            get { return (label_main_.Font); }
            set { label_main_.Font = value;  }
        }

        public Color Label_BackColor
        {
            get { return (label_main_.BackColor); }
            set { label_main_.BackColor = value;  }
        }

        public string Label_Text
        {
            get { return (label_main_.Text); }
            set { label_main_.Text = value;  }
        }
    }
}
