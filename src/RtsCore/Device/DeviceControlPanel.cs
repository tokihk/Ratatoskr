using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtsCore.Device
{
    public partial class DeviceControlPanel : UserControl
    {
        private DeviceInstance devi_ = null;


        public DeviceControlPanel(DeviceInstance devi)
        {
            InitializeComponent();

            devi_ = devi;
        }

        protected DeviceInstance Instance
        {
            get { return (devi_); }
        }
    }
}
