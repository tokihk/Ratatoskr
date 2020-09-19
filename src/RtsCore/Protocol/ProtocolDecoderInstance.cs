using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public abstract class ProtocolDecoderInstance
    {
        private ProtocolDecoderClass prdc_;

        private List<ProtocolDecodeChannel> prdch_list_ = new List<ProtocolDecodeChannel>();
        private List<ProtocolDecodeEvent>   event_list_new_ = new List<ProtocolDecodeEvent>();


        public ProtocolDecoderInstance(ProtocolDecoderClass prdc)
        {
            prdc_ = prdc;
        }

        public ProtocolDecoderClass Class
        {
            get { return (prdc_); }
        }

        public DateTime LastInputDateTime { get; private set; } = DateTime.MinValue;

        public ProtocolDecodeChannel[] GetProtocolDecodeChannels()
        {
            return (prdch_list_.ToArray());
        }

        protected ProtocolDecodeChannel CreateChannel(string name)
        {
            var group = new ProtocolDecodeChannel(this, name);

            prdch_list_.Add(group);

            return (group);
        }

        internal void RegisterNewEvent(ProtocolDecodeEvent prde)
        {
            event_list_new_.Add(prde);
        }

        private void ExtractNewEvent(List<ProtocolDecodeEvent> new_event)
        {
            new_event.AddRange(event_list_new_);

            event_list_new_ = new List<ProtocolDecodeEvent>();
        }

        public void InputData(string input_alias, byte[] input_data, List<ProtocolDecodeEvent> output)
        {
            var input_dt = DateTime.UtcNow;

            if (LastInputDateTime == DateTime.MinValue) {
                LastInputDateTime = input_dt;
            }

            InputData(input_dt, input_data, output);
        }

        public void InputData(TimeSpan input_offset, byte[] input_data, List<ProtocolDecodeEvent> output)
        {
            if (LastInputDateTime == DateTime.MinValue) {
                LastInputDateTime = DateTime.UtcNow;
            }

            InputData(LastInputDateTime + input_offset, input_data, output);
        }

        public void InputData(DateTime input_dt, byte[] input_data, List<ProtocolDecodeEvent> output)
        {
            if (LastInputDateTime == DateTime.MinValue) {
                LastInputDateTime = DateTime.UtcNow;
            }

            try {
                OnInputData(input_dt, input_data);
            } catch {
            }

            LastInputDateTime = input_dt;

            ExtractNewEvent(output);
        }

        public void InputBreak(List<ProtocolDecodeEvent> output)
        {
            OnInputBreak();

            ExtractNewEvent(output);
        }

        public void Poll(List<ProtocolDecodeEvent> output)
        {
            OnPoll();

            ExtractNewEvent(output);
        }

        protected virtual void OnInputData(DateTime input_dt, byte[] input_data)
        {
        }

        protected virtual void OnInputBreak()
        {
        }

        protected virtual void OnPoll()
        {
        }
    }
}
