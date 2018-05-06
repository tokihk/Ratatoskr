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

namespace Ratatoskr.PacketConverters.Separator
{
    internal partial class RuleObject : UserControl
    {
        public RuleObject()
        {
            InitializeComponent();
        }

        public RuleObject(PacketConverterInstance instance, PacketConverterPropertyImpl prop)
        {
            Instance = instance;
            Property = prop;
        }

        public PacketConverterInstance     Instance { get; }
        public PacketConverterPropertyImpl Property { get; }    


        public virtual void OnBackupProperty()
        {
        }

        public void UpdateConvertStatus()
        {
            Instance.UpdateConvertStatus();
        }

        public virtual void OnInputStatusClear()
        {
        }

        public virtual void OnInputPacket(PacketObject intput, ref List<PacketObject> output)
        {
        }

        public virtual void OnInputBreak(ref List<PacketObject> output)
        {
        }

        public virtual void OnInputPoll(ref List<PacketObject> output)
        {
        }
    }
}
