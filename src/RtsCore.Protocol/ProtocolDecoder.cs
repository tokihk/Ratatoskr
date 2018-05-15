using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public abstract class ProtocolDecoder
    {
        public abstract Guid   ID      { get; }
        public abstract string Name    { get; }
        public abstract string Details { get; }

        private ProtocolDecodeStack decode_stack_ = null;


        public ProtocolDecodeChannel GetChannelData(uint channel)
        {
            return (OnLoadChannelData(channel));
        }

        public ProtocolDecodeData[] Decode(DateTime input_datetime, byte[] input_data)
        {
            var decode_data_list = new List<ProtocolDecodeData>();

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

        protected virtual ProtocolDecodeChannel OnLoadChannelData(uint channel)
        {
            return (new ProtocolDecodeChannel(channel.ToString()));
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
