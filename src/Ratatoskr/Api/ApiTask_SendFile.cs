using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;

namespace Ratatoskr.Api
{
    public class ApiTask_SendFile : IApiTask
    {
        private sealed class GateTaskObject
        {
            private FileStream file_;
            private GateObject gate_;

            private long   send_pos_ = 0;

            private byte[] send_block_buffer_ = null;
            private uint   send_block_size_ = 0;
            private uint   send_block_delay_ = 0;

            private bool   send_ready_ = false;
            private bool   send_busy_ = false;

            private Stopwatch send_delay_timer_ = new Stopwatch();


            public GateTaskObject(FileStream file, GateObject gate, uint send_block_size, uint send_delay)
            {
                file_ = file;
                gate_ = gate;
                send_block_size_ = send_block_size;
                send_block_delay_ = send_delay;
            }

            private void ReloadSendData()
            {
                if (send_pos_ >= file_.Length)return;

                /* 送信データ位置補正 */
                file_.Seek(send_pos_, SeekOrigin.Begin);
                
                /* 送信バッファ作成 */
                var send_block_buffer = new byte[send_block_size_];

                var read_size = 0;

                try {
                    /* 送信データ読み込み */
                    read_size = file_.Read(send_block_buffer, 0, send_block_buffer.Length);
                    if (read_size == 0)return;

                    /* 送信バッファサイズ補正 */
                    if (read_size < send_block_buffer.Length) {
                        send_block_buffer = send_block_buffer.Take(read_size).ToArray();
                    }

                    send_block_buffer_ = send_block_buffer;
                    send_pos_ += read_size;

                } catch { }
            }

            public void Poll(ref uint progress_min)
            {
                if (IsComplete)return;

                if (gate_.ConnectStatus == Devices.ConnectState.Disconnected) {
                    /* 切断時はすぐに終端に移動 */
                    send_pos_ = file_.Length;
                    send_block_buffer_ = null;
                    return;
                }

                /* 送信バッファ読み込み */
                if (send_block_buffer_ == null) {
                    ReloadSendData();

                    if (send_block_buffer_ == null)return;
                }

                /* 送信準備判定 */
                if (!send_ready_) {
                    if (gate_.SendRequest(null).status == GateObject.SendRequestStatus.Accept) {
                        send_ready_ = true;
                        send_delay_timer_.Restart();
                    }
                }

                /* 送信遅延 */
                if ((send_ready_) && (!send_busy_)) {
                    if (send_delay_timer_.ElapsedMilliseconds >= send_block_delay_) {
                        send_busy_ = true;
                        send_delay_timer_.Stop();
                    }
                }

                /* 送信実行 */
                if (send_busy_) {
                    if (gate_.SendRequest(send_block_buffer_).discard_req) {
                        send_ready_ = false;
                        send_busy_ = false;
                        send_block_buffer_ = null;
                    }
                }

                /* 最小の進捗率を設定 */
                if (progress_min > (uint)send_pos_) {
                    progress_min = (uint)send_pos_;
                }
            }

            public bool IsComplete
            {
                get { return ((send_pos_ >= file_.Length) && (send_block_buffer_ == null)); }
            }

            public uint Progress { get; private set; } = 0;
        }


        private ApiSandbox sandbox_ = null;

        private bool         cancel_req_ = false;
        private IAsyncResult ar_task_ = null;

        public string GateAlias     { get; }
        public string FilePath      { get; }
        public uint   SendBlockSize { get; }
        public uint   SendDelay     { get; }

        public bool Success       { get; private set; } = false;
        public uint ProgressMax   { get; private set; } = 1;
        public uint ProgressNow   { get; private set; } = 1;


        internal ApiTask_SendFile(ApiSandbox sandbox, string gate_alias, string file_path, uint send_block_size = 1024, uint send_delay = 0)
        {
            sandbox_ = sandbox;
            GateAlias = gate_alias ?? "";
            FilePath = file_path ?? "";
            SendBlockSize = send_block_size;
            SendDelay = send_delay;
        }

        public void Dispose()
        {
            Stop();
        }

        public bool IsRunning
        {
            get { return ((ar_task_ != null) && (!ar_task_.IsCompleted));}
        }

        internal void StartAsync()
        {
            Stop();

            /* 進捗初期化 */
            cancel_req_ = false;
            Success = false;
            ProgressMax = 1;
            ProgressNow = 1;

            var task_objs = (List<GateTaskObject>)null;

            /* 送信ファイル取得 */
            if (!File.Exists(FilePath))return;

            var file = (FileStream)null;

            try {
                file = File.OpenRead(FilePath);
            } catch {
                return;
            }

            /* 送信先ゲート取得 */
            var gate_objs = GateManager.FindGateObjectFromWildcardAlias(GateAlias);

            if (gate_objs == null)return;

            /* 送信オブジェクト生成 */
            task_objs = gate_objs.Select(gate_obj => new GateTaskObject(file, gate_obj, SendBlockSize, SendDelay)).ToList();

            ProgressMax = (uint)file.Length;
            ProgressNow = 0;

            /* 送信タスク開始 */
            ar_task_ = (new ExecTaskHandler(ExecTask)).BeginInvoke(task_objs, null, null);
        }

        public void Stop()
        {
            cancel_req_ = true;

            while (IsRunning) {
                sandbox_.API_Sleep(10);
            }
        }

        public bool Join(int timeout_ms = -1)
        {
            var sw_timeout = new System.Diagnostics.Stopwatch();

            /* timeoutが0以上のときのみタイムアウト監視 */
            if (timeout_ms >= 0) {
                sw_timeout.Restart();
            }

            /* 終了監視 */
            while (IsRunning) {
                /* タイムアウト監視 */
                if ((sw_timeout.IsRunning) && (sw_timeout.ElapsedMilliseconds >= timeout_ms)) {
                    break;
                }

                sandbox_.API_Sleep(10);
            }

            return (Success);
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
    }
}
