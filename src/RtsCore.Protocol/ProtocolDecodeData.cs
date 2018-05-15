using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public enum DecodeDataStatus
    {
        Normal,
        PartError,
        FullError,
    }

    public class ProtocolDecodeData
    {
        public ProtocolDecodeData(DateTime decode_time, uint channel, DateTime block_time, uint block_index, string message, ProtocolFrameElement data, DecodeDataStatus status)
        {
            DecodeTime = decode_time;

            DataChannel = channel;
            DataBlockTime = block_time;
            DataBlockIndex = block_index;

            Message = message;
            Data = data;
        }

        public DateTime DecodeTime { get; }

        public uint     DataChannel    { get; }
        public DateTime DataBlockTime  { get; }
        public uint     DataBlockIndex { get; }

        public string               Message    { get; }
        public ProtocolFrameElement Data       { get; }
        public DecodeDataStatus     DataStatus { get; }
    }
}
