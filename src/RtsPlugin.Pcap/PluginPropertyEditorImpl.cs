using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.Plugin;

namespace RtsPlugin.Pcap
{
    public partial class PluginPropertyEditorImpl : PluginPropertyEditor
    {
        public PluginPropertyEditorImpl(PluginPropertyImpl plgp) : base(plgp)
        {
            InitializeComponent();
        }
    }
}
