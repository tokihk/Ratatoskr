using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtsCore.Framework.Plugin
{
    public partial class PluginPropertyEditor : UserControl
    {
        private PluginPropertyEditor()
        {
            InitializeComponent();
        }

        public PluginPropertyEditor(PluginProperty plgp) : this()
        {

        }

        public virtual void Flush() { }
    }
}
