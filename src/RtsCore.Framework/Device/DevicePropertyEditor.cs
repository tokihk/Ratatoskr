using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtsCore.Framework.Device
{
    public partial class DevicePropertyEditor : UserControl
    {
        public DevicePropertyEditor()
        {
            InitializeComponent();
        }

        public virtual void Flush() { }
    }
}
