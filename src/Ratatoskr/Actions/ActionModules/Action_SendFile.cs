using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_SendFile : ActionObject
    {
        public enum Argument
        {
            Gate,
            FilePath,
            BlockSize,
        }

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

            public void Poll(ref uint progress_min)
            {
                if (IsComplete)return;

                if (gate_.ConnectStatus == Devices.ConnectState.Disconnected) {
                    /* 切断時はすぐに終端に移動 */
                    send_pos_ = file_.Length;
                    return;
                }

                /* 送信データ位置補正 */
                file_.Seek(send_pos_, SeekOrigin.Begin);
                
                /* 送信データ読み込み */
                var read_size = file_.Read(send_buffer_, 0, send_buffer_.Length);

                if (read_size == 0)return;

                /* 送信実行 */
                if (gate_.SendRequest(send_buffer_.Take(read_size).ToArray()).discard_req) {
                    send_pos_ += read_size;
                }

                /* 最小の進捗率を設定 */
                if (progress_min > (uint)send_pos_) {
                    progress_min = (uint)send_pos_;
                }
            }

            public bool IsComplete
            {
                get { return (send_pos_ >= file_.Length); }
            }

            public uint Progress { get; private set; } = 0;
        }


        private FileStream           file_ = null;
        private List<SendPartObject> send_objs_ = new List<SendPartObject>();


        public Action_SendFile()
        {
            RegisterArgument(Argument.Gate.ToString(), typeof(string), null);
            RegisterArgument(Argument.FilePath.ToString(), typeof(string), null);
            RegisterArgument(Argument.BlockSize.ToString(), typeof(int), null);
        }

        public Action_SendFile(string gate, string file_path, int block_size) : this()
        {
            SetArgumentValue(Argument.Gate.ToString(), gate);
            SetArgumentValue(Argument.FilePath.ToString(), file_path);
            SetArgumentValue(Argument.BlockSize.ToString(), block_size);
        }

        protected override bool OnArgumentCheck()
        {
            /* file_path */
            var file_path = GetArgumentValue(Argument.FilePath.ToString()) as string;

            if (!File.Exists(file_path)) {
                return (false);
            }

            /* block-size */
            var block_size = (int)GetArgumentValue(Argument.BlockSize.ToString());

            if (block_size == 0) {
                return (false);
            }

            return (true);
        }

        protected override void OnExecStart()
        {
            var param_gate = GetArgumentValue(Argument.Gate.ToString()) as string;
            var param_file_path = GetArgumentValue(Argument.FilePath.ToString()) as string;
            var param_block_size = (int)GetArgumentValue(Argument.BlockSize.ToString());

            /* 送信ファイル取得 */
            file_ = File.OpenRead(param_file_path);

            if (file_ == null) {
                SetResult(ActionResultType.Error_FileOpen, null);
                return;
            }

            /* 送信先ゲート取得 */
            var gates = GateManager.FindGateObjectFromWildcardAlias(param_gate);

            /* 送信オブジェクト生成 */
            foreach (var gate in gates) {
                send_objs_.Add(new SendPartObject(file_, gate, param_block_size));
            }

            ProgressMax = (uint)file_.Length;
        }

        protected override void OnExecComplete()
        {
            if (file_ != null) {
                file_.Close();
            }
        }

        protected override void OnExecPoll()
        {
            var progress = (uint)file_.Length;

            /* 送信実行 */
            send_objs_.ForEach(obj => obj.Poll(ref progress));

            /* 完了オブジェクトを解放 */
            send_objs_.RemoveAll(obj => obj.IsComplete);

            /* 進捗率更新 */
            ProgressNow = progress;

            if (send_objs_.Count == 0) {
                SetResult(ActionResultType.Success, null);
            }
        }
    }
}
