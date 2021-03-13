using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace Ratatoskr.PacketConverters.Convert.DataChange
{
    [Serializable]
    internal sealed class AlgorithmProperty : ConfigHolder
    {
        public StringConfig TargetPattern  { get; } = new StringConfig("");
        public StringConfig ReplacePattern { get; } = new StringConfig("");
    }
}
