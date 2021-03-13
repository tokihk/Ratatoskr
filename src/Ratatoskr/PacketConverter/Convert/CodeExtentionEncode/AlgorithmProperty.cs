using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.PacketConverter.Convert.CodeExtentionEncode
{
    [Serializable]
    internal sealed class AlgorithmProperty : ConfigHolder
    {
        public IntegerConfig ExtTargetCode_Min { get; } = new IntegerConfig(0xF0);
        public IntegerConfig ExtTargetCode_Max { get; } = new IntegerConfig(0xFF);
        public IntegerConfig ExtCode           { get; } = new IntegerConfig(0xFF);
        public IntegerConfig ExtMask           { get; } = new IntegerConfig(0x0F);
    }
}
