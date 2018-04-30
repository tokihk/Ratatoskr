using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.ScriptEngines;

namespace Ratatoskr.Scripts
{
    internal class ScriptFileController : ScriptController
    {
        private ScriptFileRunner runner_;


        public ScriptFileController(string script_path) : base(new ScriptFileRunner(script_path))
        {
        }

        public string ScriptPath
        {
            get { return ((Runner as ScriptFileRunner).ScriptPath); }
        }

        protected override bool GetDetachStatus()
        {
            return (!File.Exists(ScriptPath));
        }

        public override string ToString()
        {
            return (ScriptPath);
        }
    }
}
