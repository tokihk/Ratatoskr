using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public abstract class ModuleManager<TManager, TClass, TInstance>
		where TManager : ModuleManager<TManager, TClass, TInstance>
        where TClass : ModuleClass<TManager, TClass, TInstance>
        where TInstance : ModuleInstance<TManager, TClass, TInstance>, IDisposable
    {
		public static TManager Instance { get; private set; }

		public static void Initialize(TManager instance = null)
		{
			if (instance == null) {
				instance = Activator.CreateInstance(typeof(TManager)) as TManager;
			}
			Instance = instance;
		}


        private List<TClass>	class_list_		= new List<TClass>();
        private List<TInstance>	instance_list_	= new List<TInstance>();


        public IEnumerable<TClass> GetClassList()
        {
            lock (class_list_) {
                return (class_list_.ToArray());
            }
        }

        public TClass FindClass(Guid class_id)
        {
            lock (class_list_) {
                return (class_list_.Find(cls => cls.ID == class_id));
            }
        }

        public bool AddClass(TClass module_class)
        {
            if (module_class == null)return (false);

            /* 重複IDをチェック */
            if (FindClass(module_class.ID) != null)return (false);

            /* 新しいデバイスを追加 */
            lock (class_list_) {
                class_list_.Add(module_class);

                /* 名前順にソート */
                class_list_.Sort((a, b) => a.Name.CompareTo(b.Name));
            }

            return (true);
        }

        public void RemoveAllInstance()
        {
            foreach (var instance in GetInstanceList()) {
                instance.Dispose();
            }
        }

        public TInstance[] GetInstanceList()
        {
            lock (instance_list_) {
                return (instance_list_.ToArray());
            }
        }

        public TInstance CreateInstance(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var module_class = FindClass(class_id);

            if (module_class == null)return (null);

            /* インスタンス作成 */
            var module_instance = module_class.CreateInstance(this as TManager);

            if (module_instance == null)return (null);

            /* インスタンス登録 */
            lock (instance_list_) {
                instance_list_.Add(module_instance);
            }

            return (module_instance);
        }

        internal void RemoveInstance(TInstance module_instance)
        {
            lock (instance_list_) {
                instance_list_.Remove(module_instance);
            }
        }
    }
}
