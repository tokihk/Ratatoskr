using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters.Separator.DataContentsMatch
{
    [Serializable]
    internal sealed class RuleProperty : ConfigHolder
    {
        public StringConfig Pattern { get; } = new StringConfig("");
    }
}
