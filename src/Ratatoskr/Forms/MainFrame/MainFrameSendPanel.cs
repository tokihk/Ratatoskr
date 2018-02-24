using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms.MainFrame
{
    internal class MainFrameSendPanel : UserControl
    {
        public MainFrameSendPanel()
        {
        }

        public MainFrameSendPanel(MainFrameSendPanelContainer panel) : this()
        {
            ControlPanel = panel;
        }

        protected MainFrameSendPanelContainer ControlPanel { get; }

        public virtual void LoadConfig() { }
        public virtual void BackupConfig() { }

        public void SendExecRequest()
        {
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
                Invoke(new SendExecCompleteHandler(SendExecComplete), success);
                return;
            }

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
