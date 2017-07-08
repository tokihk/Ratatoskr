using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketViews
{
    public abstract class ViewProperty : ConfigHolder
    {
        public abstract ViewProperty Clone();

        public BoolConfig   TargetFilterEnable { get; } = new BoolConfig(true);
        public StringConfig TargetFilterValue  { get; } = new StringConfig("");
    }
}
