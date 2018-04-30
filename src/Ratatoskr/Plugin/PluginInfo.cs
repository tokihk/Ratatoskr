using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Plugin
{
    internal class PluginInfo
    {
        public PluginInfo(string asm_path, Assembly asm_object, Type asm_type)
        {
            AssemblyPath = asm_path;
            AssemblyObject = asm_object;
            AssemblyType = asm_type;
        }

        public string    AssemblyPath   { get; }
        public Assembly  AssemblyObject { get; }
        public Type      AssemblyType   { get; }

        public object LoadModule()
        {
            return (Activator.CreateInstance(AssemblyType));
        }
    }
}
