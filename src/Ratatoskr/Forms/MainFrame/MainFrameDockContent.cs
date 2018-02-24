using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameDockContent : DockContent
    {
        private Control control_ = null;


        public MainFrameDockContent()
        {
            InitializeComponent();
        }

        public MainFrameDockContent(string name, MainFrameDockPanel panel) : this()
        {
            ContentName = name;

            FormClosed += panel.OnChildFormClosed;
        }

        public string  ContentName    { get; } = "";
        public Control ContentControl { get { return (control_); } }

        public void SetupControl(Control control)
        {
            Controls.Clear();
            control_ = null;

            if (control == null)return;

            control.Dock = DockStyle.Fill;
            
            Controls.Add(control);
            control_ = control;
        }

        protected override string GetPersistString()
        {
            return (ContentName);
        }
    }
}
