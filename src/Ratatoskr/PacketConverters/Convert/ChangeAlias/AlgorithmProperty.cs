using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters.Convert.ChangeAlias
{
    [Serializable]
    internal sealed class AlgorithmProperty : ConfigHolder
    {
        public StringConfig Value { get; } = new StringConfig("");
    }
}
