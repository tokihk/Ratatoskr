using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters.Convert.CodeExtentionDecode
{
    [Serializable]
    internal sealed class AlgorithmProperty : ConfigHolder
    {
        public IntegerConfig ExtCode           { get; } = new IntegerConfig(0xFF);
        public IntegerConfig ExtMask           { get; } = new IntegerConfig(0xF0);
    }
}
