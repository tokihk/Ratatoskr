using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public abstract class ModuleInstance<TManager, TClass, TInstance>
        where TManager : ModuleManager<TManager, TClass, TInstance>
        where TClass : ModuleClass<TManager, TClass, TInstance>
        where TInstance : ModuleInstance<TManager, TClass, TInstance>
        , IDisposable
    {
        private bool        dispose_state_ = false;


        public ModuleInstance(TManager module_manager)
        {
            Manager = module_manager;
        }

        public TManager Manager { get; }

        public void Dispose()
        {
            if (dispose_state_) {
                return;
            }

            dispose_state_ = true;

            OnDispose();

            Manager.RemoveInstance(this as TInstance);
        }

        protected virtual void OnDispose()
        {
        }
    }
}
