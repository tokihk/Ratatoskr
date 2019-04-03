using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Framework.PacketView
{
    public abstract class PacketViewClass
    {
        public Guid ID { get; }

        public abstract string Name    { get; }
        public abstract string Details { get; }


        public PacketViewClass(Guid id)
        {
            ID = id;
        }


        public abstract Type GetPropertyType();
        public abstract PacketViewProperty CreateProperty();


        internal PacketViewInstance CreateInstance(PacketViewManager viewm, Guid obj_id, PacketViewProperty viewp)
        {
            /* プロパティがnullのときはデフォルト値で新規作成 */
            if (viewp == null) {
                viewp = CreateProperty();
            }

            /* プロパティタイプが異なるときは失敗 */
            if (viewp.GetType() != GetPropertyType())return (null);

            return (OnCreateInstance(viewm, obj_id, viewp));
        }

        protected virtual PacketViewInstance OnCreateInstance(PacketViewManager devm, Guid obj_id, PacketViewProperty devp)
        {
            return (null);
        }
    }
}
