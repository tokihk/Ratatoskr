using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters
{
    [Serializable]
    internal abstract class PacketConverterProperty : ConfigHolder
    {
        public BoolConfig   ConverterEnable    { get; } = new BoolConfig(true);
        public BoolConfig   TargetFilterEnable { get; } = new BoolConfig(true);
        public StringConfig TargetFilterValue  { get; } = new StringConfig("");


        public abstract PacketConverterProperty Clone();
    }
}
