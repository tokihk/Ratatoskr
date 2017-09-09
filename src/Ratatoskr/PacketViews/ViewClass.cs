using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews
{
    internal abstract class ViewClass
    {
        public Guid ID { get; }

        public abstract string Name { get; }
        public abstract string Details { get; }


        public ViewClass(Guid id)
        {
            ID = id;
        }


        public abstract Type GetPropertyType();
        public abstract ViewProperty CreateProperty();


        internal ViewInstance CreateInstance(ViewManager viewm, Guid obj_id, ViewProperty viewp)
        {
            /* プロパティがnullのときはデフォルト値で新規作成 */
            if (viewp == null) {
                viewp = CreateProperty();
            }

            /* プロパティタイプが異なるときは失敗 */
            if (viewp.GetType() != GetPropertyType())return (null);

            return (OnCreateInstance(viewm, obj_id, viewp));
        }

        protected virtual ViewInstance OnCreateInstance(ViewManager devm, Guid obj_id, ViewProperty devp)
        {
            return (null);
        }
    }
}
