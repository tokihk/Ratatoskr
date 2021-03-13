using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace RtsCore.Framework.PacketConverter
{
    [Serializable]
    public abstract class PacketConverterProperty : ConfigHolder
    {
        public BoolConfig   ConverterEnable    { get; } = new BoolConfig(true);
        public BoolConfig   TargetFilterEnable { get; } = new BoolConfig(true);
        public StringConfig TargetFilterValue  { get; } = new StringConfig("");


        public abstract PacketConverterProperty Clone();
    }
}
