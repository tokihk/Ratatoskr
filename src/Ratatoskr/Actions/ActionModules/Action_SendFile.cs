using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_SendFile : ActionObject
    {
        private sealed class SendPartObject
        {
            private FileStream file_;
            private GateObject gate_;

            private byte[] send_buffer_;
            private long   send_pos_ = 0;

            public SendPartObject(FileStream file, GateObject gate, int send_block_size)
            {
                file_ = file;
                gate_ = gate;
                send_buffer_ = new byte[send_block_size];
            }

            public void Poll()
            {
                if (IsComplete)return;
                if (!gate_.SendDataEmpty)return;

                /* 送信データ位置補正 */
                file_.Seek(send_pos_, SeekOrigin.Begin);
                
                /* 送信データ読み込み */
                var read_size = file_.Read(send_buffer_, 0, send_buffer_.Length);

                if (read_size == 0)return;

                /* 送信実行 */
                gate_.SendDataPush(send_buffer_.Take(read_size).ToArray());

                send_pos_ += read_size;
            }

            public bool IsComplete
            {
                get { return (send_pos_ >= file_.Length); }
            }
        }


        private FileStream           file_ = null;
        private List<SendPartObject> send_objs_ = new List<SendPartObject>();


        public Action_SendFile()
        {
            InitParameter<Term_Text>("gate");
            InitParameter<Term_Text>("path");
            InitParameter<Term_Double>("block-size");
        }

        public override bool OnParameterCheck()
        {
            /* gate */
            if (GetParameter<Term_Text>("gate") == null) {
                return (false);
            }

            /* path */
            var path = GetParameter<Term_Text>("path");
                
            if (   (path == null)
                || (!File.Exists(path.Value))
            ) {
                return (false);
            }

            /* block-size */
            var block_size = GetParameter<Term_Double>("block-size");

            if (   (block_size == null)
                || (block_size.Value <= 0)
            ) {
                return (false);
            }

            return (true);
        }

        protected override ExecState OnExecStart()
        {
            if (!ParameterCheck()) {
                return (ExecState.Complete);
            }

            var param_gate = GetParameter<Term_Text>("gate");
            var param_path = GetParameter<Term_Text>("path");
            var param_block_size = GetParameter<Term_Double>("block-size");

            /* 送信ファイル取得 */
            file_ = File.OpenRead(param_path.Value);

            if (file_ == null) {
                return (ExecState.Complete);
            }

            /* 送信先ゲート取得 */
            var gates = GateManager.FindGateObjectFromWildcardAlias(param_gate.Value);

            /* 送信オブジェクト生成 */
            foreach (var gate in gates) {
                send_objs_.Add(new SendPartObject(file_, gate, (int)param_block_size.Value));
            }

            return (ExecState.Busy);
        }

        protected override void OnExecComplete()
        {
            if (file_ != null) {
                file_.Close();
            }
        }

        protected override ExecState OnExecPoll()
        {
            /* 送信実行 */
            send_objs_.ForEach(obj => obj.Poll());

            /* 完了オブジェクトを解放 */
            send_objs_.RemoveAll(obj => obj.IsComplete);

            return ((send_objs_.Count > 0) ? (ExecState.Busy) : (ExecState.Complete));
        }
    }
}
