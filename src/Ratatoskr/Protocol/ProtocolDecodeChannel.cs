using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.General;

namespace RtsCore.Protocol
{
    public class ProtocolDecodeChannel
    {
        private List<ProtocolDecodeEvent> event_list_ = new List<ProtocolDecodeEvent>();


        internal ProtocolDecodeChannel(ProtocolDecoderInstance prdi, string name)
        {
            Instance = prdi;
            Name = name;
        }

        public ProtocolDecoderInstance Instance { get; }

        public string Name { get; }

        public IEnumerable<ProtocolDecodeEvent> Events
        {
            get { return (event_list_); }
        }

        public DateTime FirstEventTime
        {
            get { return ((event_list_.Count > 0) ? (event_list_.First().EventDateTime) : (DateTime.MaxValue)); }
        }

        public DateTime LastEventTime
        {
            get { return ((event_list_.Count > 0) ? (event_list_.Last().EventDateTime) : (DateTime.MinValue)); }
        }

        private void AddEvent(ProtocolDecodeEvent prde)
        {
            event_list_.Add(prde);

            Instance.RegisterNewEvent(prde);
        }

        public void CreateMessageEvent(DateTime dt_block, DateTime dt_event, string message)
        {
            AddEvent(new ProtocolDecodeEvent_Message(this, dt_block, dt_event, message));
        }

        public void CreateBitDataEvent(DateTime dt_block, DateTime dt_event, BitData bitdata)
        {
            AddEvent(new ProtocolDecodeEvent_BitData(this, dt_block, dt_event, bitdata));
        }

        public void CreateValueEvent(DateTime dt_block, DateTime dt_event, double value)
        {
            AddEvent(new ProtocolDecodeEvent_Value(this, dt_block, dt_event, value));
        }

        public void CreateFrameEvent(DateTime dt_block, DateTime dt_event, ProtocolFrameElement frame)
        {
            AddEvent(new ProtocolDecodeEvent_Frame(this, dt_block, dt_event, frame));
        }
    }
}
