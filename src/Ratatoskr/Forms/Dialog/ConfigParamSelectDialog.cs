using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Dialog
{
    internal enum ConfigType
    {
        GateConfig,
        PacketViewConfig,
        PacketConverterConfig,
    }

    internal partial class ConfigParamSelectDialog : Form
    {
        public ConfigParamSelectDialog()
        {
            InitializeComponent();
        }


    }
}
