using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.PacketConverter.Convert.DataChange
{
    [Serializable]
    internal sealed class AlgorithmProperty : ConfigHolder
    {
        public StringConfig TargetPattern  { get; } = new StringConfig("");
        public StringConfig ReplacePattern { get; } = new StringConfig("");
    }
}
