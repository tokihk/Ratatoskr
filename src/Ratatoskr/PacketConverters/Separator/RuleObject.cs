using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.PacketConverter;
using RtsCore.Packet;

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

        public void InputStatusClear()
        {
            OnInputStatusClear();
        }

        public void InputPacket(uint input_ch, PacketObject input, ref List<PacketObject> output)
        {
            OnInputPacket(input_ch, input, ref output);
        }

        public void InputBreak(ref List<PacketObject> output)
        {
            OnInputBreak(ref output);
        }

        public void InputPoll(ref List<PacketObject> output)
        {
            OnInputPoll(ref output);
        }

        protected virtual void OnInputStatusClear()
        {
        }

        protected virtual void OnInputPacket(uint input_ch, PacketObject input, ref List<PacketObject> output)
        {
        }

        protected virtual void OnInputBreak(ref List<PacketObject> output)
        {
        }

        protected virtual void OnInputPoll(ref List<PacketObject> output)
        {
        }
    }
}
