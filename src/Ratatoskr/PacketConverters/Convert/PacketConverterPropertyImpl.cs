using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketConverters.Convert
{
    internal enum AlgorithmType
    {
        None,
        ChangeAlias,
        CodeExtentionEncode,
        CodeExtentionDecode,
        RemoveData,
//        Custom,
    }

    internal sealed class PacketConverterPropertyImpl : PacketConverterProperty
    {
        public EnumConfig<AlgorithmType> Algorithm { get; } = new EnumConfig<AlgorithmType>(AlgorithmType.None);

        public ChangeAlias.AlgorithmProperty         ChangeAliasProperty         { get; } = new ChangeAlias.AlgorithmProperty();
        public CodeExtentionEncode.AlgorithmProperty CodeExtentionEncodeProperty { get; } = new CodeExtentionEncode.AlgorithmProperty();
        public CodeExtentionDecode.AlgorithmProperty CodeExtentionDecodeProperty { get; } = new CodeExtentionDecode.AlgorithmProperty();
        public RemoveData.AlgorithmProperty          RemoveDataProperty          { get; } = new RemoveData.AlgorithmProperty();

//        public Custom.AlgorithmProperty              CustomProperty              { get; } = new Custom.AlgorithmProperty();


        public override PacketConverterProperty Clone()
        {
            return (ClassUtil.Clone(this));
        }
    }
}
