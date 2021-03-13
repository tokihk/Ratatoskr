using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.PacketConverter;
using Ratatoskr.General;

namespace Ratatoskr.PacketConverter.Transfer
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
