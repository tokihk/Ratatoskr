using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters.Transfer
{
    internal enum AlgorithmType
    {
        None,
        File,
    }

    internal sealed class PacketConverterPropertyImpl : PacketConverterProperty
    {
        public EnumConfig<AlgorithmType> Algorithm { get; } = new EnumConfig<AlgorithmType>(AlgorithmType.None);

        public File.AlgorithmProperty FileProperty { get; } = new File.AlgorithmProperty();


        public override PacketConverterProperty Clone()
        {
            return (ClassUtil.Clone(this));
        }
    }
}
