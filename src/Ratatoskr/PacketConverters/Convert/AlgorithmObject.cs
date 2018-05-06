using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Packet;

namespace Ratatoskr.PacketConverters.Convert
{
    internal partial class AlgorithmObject : UserControl
    {
        public AlgorithmObject()
        {
            InitializeComponent();
        }

        public AlgorithmObject(PacketConverterInstance instance, PacketConverterPropertyImpl prop)
        {
            Instance = instance;
            Property = prop;
        }

        public PacketConverterInstance     Instance { get; }
        public PacketConverterPropertyImpl Property { get; }    


        public void UpdateConvertStatus()
        {
            Instance.UpdateConvertStatus();
        }

        public virtual void OnBackupProperty()
        {
        }

        public virtual void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
        }
    }
}
