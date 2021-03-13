using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public abstract class ModuleClass<TManager, TClass, TInstance>
        where TManager : ModuleManager<TManager, TClass, TInstance>
        where TClass : ModuleClass<TManager, TClass, TInstance>
        where TInstance : ModuleInstance<TManager, TClass, TInstance>, IDisposable
    {
        public ModuleClass(Guid id)
        {
            ID = id;
        }

        public Guid             ID      { get; }

        public abstract string  Name    { get; }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Guid) {
                return (((Guid)obj) == ID);
            }

            return (base.Equals(obj));
        }

        public TInstance CreateInstance(TManager module_manager)
        {
            return (OnCreateInstance(module_manager));
        }

        protected virtual TInstance OnCreateInstance(TManager module_manager)
        {
            return (null);
        }
    }
}
