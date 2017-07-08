using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketConverters
{
    internal abstract class PacketConverterClass
    {
        public Guid ID { get; }

        public abstract string Name    { get; }
        public abstract string Details { get; }


        public PacketConverterClass(Guid id)
        {
            ID = id;
        }


        public abstract Type GetPropertyType();
        public abstract PacketConverterProperty CreateProperty();


        internal PacketConverterInstance CreateInstance(PacketConverterManager pcvtm, Guid obj_id, PacketConverterProperty pcvtp)
        {
            /* プロパティがnullのときはデフォルト値で新規作成 */
            if (pcvtp == null) {
                pcvtp = CreateProperty();
            }

            /* プロパティタイプが異なるときは失敗 */
            if (pcvtp.GetType() != GetPropertyType())return (null);

            return (OnCreateInstance(pcvtm, obj_id, pcvtp));
        }

        protected virtual PacketConverterInstance OnCreateInstance(PacketConverterManager pcvtm, Guid obj_id, PacketConverterProperty pcvtp)
        {
            return (null);
        }
    }
}
