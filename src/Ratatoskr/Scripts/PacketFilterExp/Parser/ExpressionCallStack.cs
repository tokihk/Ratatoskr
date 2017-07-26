using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Actions;
using Ratatoskr.Scripts.PacketFilterExp.Terms;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Scripts.PacketFilterExp.Parser
{
    [Flags]
    internal enum ExecuteFlags
    {
        ActionExecute = 1 << 0,
    }

    internal enum ExecuteErrorID
    {
        DividerOverFlow,
    }

    internal sealed class ExpressionCallStack
    {
        private ActionObject action_busy_ = null;


        public ExecuteFlags   ExecuteFlag  { get; set; } = 0;
        public ExecuteErrorID ExecuteError { get; set; } = 0;
        public bool           ExecuteCancelRequest { get; private set; } = false;

        public ActionObject BusyAction
        {
            get { lock (this) { return (action_busy_); } }
            set { lock (this) { action_busy_ = value; } }
        }

        public ActionObject LastAction { get; set; } = null;

        public PacketObject PrevPacket { get; set; } = null;
        public PacketObject CurrentPacket { get; set; } = null;

        public Dictionary<string,Term> Variables { get; } = new Dictionary<string, Term>();



        public void ExecuteCancel()
        {
            lock (this) {
                ExecuteCancelRequest = true;

                /* 実行中のアクションをキャンセル */
                if (BusyAction != null) {
                    BusyAction.Cancel();
                }
            }
        }
    }
}
