using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;

namespace Ratatoskr.Scripts.API
{
    public class API_SendFile
    {
        private sealed class GateTaskObject
        {
            private FileStream file_;
            private GateObject gate_;

            private byte[] send_buffer_;
            private long   send_pos_ = 0;

            public GateTaskObject(FileStream file, GateObject gate, uint send_block_size)
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


        private bool         cancel_req_ = false;
        private IAsyncResult ar_task_ = null;
        private EventHandler ev_completed_ = null;

        public bool Success       { get; private set; } = false;
        public uint ProgressMax   { get; private set; } = 0;
        public uint ProgressNow   { get; private set; } = 0;


        private void Dispose()
        {
            if (ev_completed_ != null) {
                ev_completed_(this, EventArgs.Empty);
                ev_completed_ = null;
            }

            cancel_req_ = false;
            Success = false;
            ProgressMax = 0;
            ProgressNow = 0;
        }

        public bool IsBusy
        {
            get { return ((ar_task_ != null) && (!ar_task_.IsCompleted));}
        }

        public void CancelRequest()
        {
            cancel_req_ = true;
        }

        internal void ExecAsync(string gate, string file_path, uint block_size, EventHandler complete)
        {
            Dispose();

            ev_completed_ = complete;

            if (   (gate == null)
                || (file_path == null)
                || (!File.Exists(file_path))
                || (block_size == 0)
            ) {
                Dispose();
                return;
            }

            var file = (FileStream)null;
            var gate_tasks = (List<GateTaskObject>)null;

            /* 送信ファイル取得 */

            try {
                file = File.OpenRead(file_path);
            } catch {
                Dispose();
                return;
            }

            /* 送信先ゲート取得 */
            var gate_objs = GateManager.FindGateObjectFromWildcardAlias(gate);

            if (gate_objs == null) {
                Dispose();
                return;
            }

            /* 送信オブジェクト生成 */
            gate_tasks = gate_objs.Select(gate_obj => new GateTaskObject(file, gate_obj, block_size)).ToList();

            ProgressMax = (uint)file.Length;
            ProgressNow = 0;

            /* 送信タスク開始 */
            ar_task_ = (new ExecTaskHandler(ExecTask)).BeginInvoke(gate_tasks, ExecTaskComplete, null);
        }

        private delegate void ExecTaskHandler(List<GateTaskObject> gate_tasks);
        private void ExecTask(List<GateTaskObject> gate_tasks)
        {
            while ((!cancel_req_) && (gate_tasks.Count > 0)) {
                var progress = ProgressMax;

                /* 送信実行 */
                gate_tasks.ForEach(obj => obj.Poll(ref progress));

                /* 完了オブジェクトを解放 */
                gate_tasks.RemoveAll(obj => obj.IsComplete);

                /* 進捗率更新 */
                ProgressNow = Math.Min(progress, ProgressMax);

                System.Threading.Thread.Sleep(10);
            }

            Success = (gate_tasks.Count == 0);
        }

        private void ExecTaskComplete(IAsyncResult ar)
        {
            Dispose();
        }
    }
}
