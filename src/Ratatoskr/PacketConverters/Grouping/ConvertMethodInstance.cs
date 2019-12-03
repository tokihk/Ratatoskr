using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.PacketConverters.Grouping
{
    internal class ConvertMethodInstance
    {
        public void InputPacket(PacketObject input, ref List<PacketObject> output)
        {
            OnInputPacket(input, ref output);
        }

        public void InputBreak(ref List<PacketObject> output)
        {
            OnInputBreak(ref output);
        }

        public void InputPoll(ref List<PacketObject> output)
        {
            OnInputPoll(ref output);
        }

        protected virtual void OnInputPacket(PacketObject input, ref List<PacketObject> output)
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
