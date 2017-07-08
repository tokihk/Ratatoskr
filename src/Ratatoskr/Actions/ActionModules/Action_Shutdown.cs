using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_Shutdown : ActionObject
    {
        public Action_Shutdown()
        {
        }

        protected override ExecState OnExecStart()
        {
            /* メインループ停止 */
            Program.ShutdownRequest();

            return (ExecState.Complete);
        }
    }
}
