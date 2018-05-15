using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms.MainWindow
{
    internal class MainWindow_SendPanel : UserControl
    {
        public MainWindow_SendPanel()
        {
        }

        public MainWindow_SendPanel(MainWindow_SendPanelContainer panel) : this()
        {
            ControlPanel = panel;
        }

        protected MainWindow_SendPanelContainer ControlPanel { get; }

        protected bool IsSendBusy { get; private set; }

        public virtual void LoadConfig() { }
        public virtual void BackupConfig() { }

        public void SendExec()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)SendExec);
                return;
            }

            SendExecComplete(false);

            IsSendBusy = true;

            /*パネルの送信処理開始 */
            var target = ControlPanel.SendExecBegin();

            /* 送信処理が失敗した場合は即座に終了 */
            if (target.target_gates == null)return;

            /* コンテンツの送信処理開始 */
            OnSendExecBegin(target.target_alias);
        }

        private delegate void SendExecCompleteHandler(bool success);
        protected void SendExecComplete(bool success)
        {
            if (InvokeRequired) {
                Invoke((SendExecCompleteHandler)SendExecComplete, success);
                return;
            }

            if (!IsSendBusy)return;

            IsSendBusy = false;

            /* パネルの送信処理を終了 */
            ControlPanel.SendExecEnd(success);

            /* コンテンツの送信処理を終了 */
            OnSendExecEnd(success);
        }

        protected virtual void OnSendExecBegin(string target)
        {
            /* 何も定義していないときは即座に終了 */
            SendExecComplete(true);
        }

        protected virtual void OnSendExecEnd(bool success)
        {
        }

        public virtual void OnMainFormDeactivated()
        {
        }
    }
}
