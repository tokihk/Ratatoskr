using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Config;

namespace RtsCore.Config
{
    public partial class ConfigEditor : UserControl
    {
        private ConfigHolder    confh_;


        private ConfigEditor()
        {
            InitializeComponent();
        }

        public ConfigEditor(ConfigHolder confh) : this()
        {
            confh_ = confh;
        }
    }
}
