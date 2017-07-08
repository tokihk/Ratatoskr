using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_PacketSave : ActionObject
    {
        public Action_PacketSave()
        {
            InitParameter<Term_Bool>("overwrite");
            InitParameter<Term_Bool>("rule");
        }

        public Action_PacketSave(bool over, bool rule) : this()
        {
            SetParameter("overwrite", new Term_Bool(over));
            SetParameter("rule",      new Term_Bool(rule));
        }

        protected override ExecState OnExecStart()
        {
            var over = GetParameter<Term_Bool>("overwrite");

            if (over == null) {
                return (ExecState.Complete);
            }

            var rule = GetParameter<Term_Bool>("rule");

            if (rule == null) {
                return (ExecState.Complete);
            }

            PacketSave(over.Value, rule.Value);

            return (ExecState.Complete);
        }

        private delegate void PacketSaveDelegate(bool over, bool rule);
        private void PacketSave(bool over, bool rule)
        {
            if (FormUiManager.InvokeRequired()) {
                FormUiManager.Invoke(new PacketSaveDelegate(PacketSave), over, rule);
                return;
            }

            FormUiManager.PacketSave(over, rule);
        }
    }
}
