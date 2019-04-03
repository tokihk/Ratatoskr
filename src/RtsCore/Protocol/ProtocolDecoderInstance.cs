using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public abstract class ProtocolDecoderInstance
    {
        private const int CHANNEL_MAX = 63;


        private ProtocolDecoderClass prdc_;
        private ProtocolDecodeStack  decode_stack_ = null;

        private List<ProtocolDecodeChannel> channel_list_ = new List<ProtocolDecodeChannel>();

        private int next_channel_no_ = 0;

        private DateTime last_input_dt_ = DateTime.MinValue;


        public ProtocolDecoderInstance(ProtocolDecoderClass prdc)
        {
            prdc_ = prdc;
        }

        public ProtocolDecoderClass Class
        {
            get { return (prdc_); }
        }

        public ProtocolDecodeChannel[] GetChannelList()
        {
            lock (channel_list_) {
                return (channel_list_.ToArray());
            }
        }

        protected ProtocolDecodeChannel CreateChannel(string name)
        {
            if (next_channel_no_ >= CHANNEL_MAX)return (null);

            var channel = new ProtocolDecodeChannel(this, name, next_channel_no_++);

            lock (channel_list_) {
                channel_list_.Add(channel);
            }

            return (channel);
        }

        public void InputData(byte[] input_data, List<ProtocolDecodeEvent> output)
        {
            var input_dt = DateTime.UtcNow;

            if (last_input_dt_ == DateTime.MinValue) {
                last_input_dt_ = input_dt;
            }

            InputData(input_dt, input_data, output);
        }

        public void InputData(TimeSpan input_offset, byte[] input_data, List<ProtocolDecodeEvent> output)
        {
            if (last_input_dt_ == DateTime.MinValue) {
                last_input_dt_ = DateTime.UtcNow;
            }

            InputData(last_input_dt_ + input_offset, input_data, output);
        }

        public void InputData(DateTime input_dt, byte[] input_data, List<ProtocolDecodeEvent> output)
        {
            if (last_input_dt_ == DateTime.MinValue) {
                last_input_dt_ = DateTime.UtcNow;
            }

            OnInputData(input_dt, input_data, output);

            last_input_dt_ = input_dt;
        }

        public void InputBreakOff(List<ProtocolDecodeEvent> output)
        {
        }

        public void InputPoll(List<ProtocolDecodeEvent> output)
        {
        }

        protected virtual void OnInputData(DateTime input_dt, byte[] input_data, List<ProtocolDecodeEvent> output)
        {
        }

        protected virtual void OnInputBreakOff(List<ProtocolDecodeEvent> output)
        {
        }

        protected virtual void OnInputPoll(List<ProtocolDecodeEvent> output)
        {
        }

        public ProtocolDecodeData2[] Decode(DateTime input_datetime, byte[] input_data)
        {
            var decode_data_list = new List<ProtocolDecodeData2>();

            /* スタックバッファ作成 */
            if (decode_stack_ == null) {
                decode_stack_ = OnCreateDecodeStack();
            }

            /* デコード処理 */
            if (input_data != null) {
                OnDecode(decode_stack_, new ProtocolDecoderParam()
                {
                    DecodeDataList = decode_data_list,
                    InputDateTime = input_datetime,
                    InputData = input_data,
                });
            }

            return (decode_data_list.ToArray());
        }

        protected virtual ProtocolDecodeStack OnCreateDecodeStack()
        {
            return (new ProtocolDecodeStack());
        }

        protected virtual void OnDecode(ProtocolDecodeStack ds, ProtocolDecoderParam param)
        {
        }
    }
}
