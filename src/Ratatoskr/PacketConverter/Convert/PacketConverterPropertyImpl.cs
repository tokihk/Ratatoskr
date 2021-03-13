using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.PacketConverter;
using Ratatoskr.General;

namespace Ratatoskr.PacketConverter.Convert
{
    internal enum AlgorithmType
    {
        None,
        ChangeAlias,
        CodeExtentionEncode,
        CodeExtentionDecode,
        DataRemove,
        DataChange,
//        Custom,
    }

    internal sealed class PacketConverterPropertyImpl : PacketConverterProperty
    {
        public EnumConfig<AlgorithmType> Algorithm { get; } = new EnumConfig<AlgorithmType>(AlgorithmType.None);

        public ChangeAlias.AlgorithmProperty         ChangeAliasProperty         { get; } = new ChangeAlias.AlgorithmProperty();
        public CodeExtentionEncode.AlgorithmProperty CodeExtentionEncodeProperty { get; } = new CodeExtentionEncode.AlgorithmProperty();
        public CodeExtentionDecode.AlgorithmProperty CodeExtentionDecodeProperty { get; } = new CodeExtentionDecode.AlgorithmProperty();
        public DataRemove.AlgorithmProperty          DataRemoveProperty          { get; } = new DataRemove.AlgorithmProperty();
        public DataChange.AlgorithmProperty          DataChangeProperty          { get; } = new DataChange.AlgorithmProperty();

        public override PacketConverterProperty Clone()
        {
            return (ClassUtil.Clone(this));
        }
    }
}
