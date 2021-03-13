using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace Ratatoskr.PacketConverters.Grouping.DataContentsMatch
{
    [Serializable]
    internal sealed class ConvertMethodProperty : ConfigHolder
    {
        public StringConfig Pattern { get; } = new StringConfig("");
    }
}
