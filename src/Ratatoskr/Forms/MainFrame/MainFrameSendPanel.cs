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

        public MainFrameSendPanel(MainFrameSendPanelContainer panel)
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
            if (target == null)return;

            /* コンテンツの送信処理開始 */
            OnSendExecBegin(target);
        }

        private delegate void SendExecCompleteHandler(bool success);
        protected void SendExecComplete(bool success)
        {
            if (InvokeRequired) {
                Invoke(new SendExecCompleteHandler(SendExecComplete), success);
                return;
            }

            /* コンテンツの送信処理を終了 */
            OnSendExecEnd(success);

            /* パネルの送信処理を終了 */
            ControlPanel.SendExecEnd(success);
        }

        protected virtual void OnSendExecBegin(Tuple<string, GateObject[]> target)
        {
            /* 何も定義していないときは即座に終了 */
            SendExecComplete(true);
        }

        protected virtual void OnSendExecEnd(bool success)
        {
        }
    }
}
