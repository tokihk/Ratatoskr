using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace RtsCore.Framework.PacketView
{
    [Serializable]
    public abstract class PacketViewProperty : ConfigHolder
    {
        public abstract PacketViewProperty Clone();

        public BoolConfig   TargetFilterEnable { get; } = new BoolConfig(true);
        public StringConfig TargetFilterValue  { get; } = new StringConfig("");
    }
}
