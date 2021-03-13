using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace Ratatoskr.PacketConverters.Convert.DataRemove
{
    [Serializable]
    internal sealed class AlgorithmProperty : ConfigHolder
    {
        public IntegerConfig DataOffset { get; } = new IntegerConfig(0);
        public IntegerConfig DataLength { get; } = new IntegerConfig(0);
    }
}
