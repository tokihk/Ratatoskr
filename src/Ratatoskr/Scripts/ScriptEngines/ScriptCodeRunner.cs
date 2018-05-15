using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Ratatoskr.Scripts.ScriptEngines
{
    internal class ScriptCodeRunner : IScriptRunner
    {
        private const int TASK_EXIT_WAIT_TIME  = 2000;
        private const int TASK_ABORT_WAIT_TIME = 1000;

        private Thread       task_thread_ = null;
        private IAsyncResult task_watcher_ = null;

        private object        task_sync_obj_ = new object();
        private ScriptSandbox task_sandbox_ = null;

        private string script_code_ = null;
        private int    script_code_line_count_ = 0;
        private bool   script_busy_ = false;

        private object                  message_list_sync_ = new object();
        private List<ScriptMessageData> message_list_;

        private object              comment_list_sync_ = new object();
        private ScriptMessageData[] comment_list_;


        public event EventHandler                 StatusChanged;
        public event ScriptMessageAppendedHandler MessageAppended;
        public event ScriptCommentUpdatedHandler  CommentUpdated;


        public ScriptCodeRunner(string script_code)
        {
            script_code_ = script_code;
            if (script_code_ != null) {
                script_code_line_count_ = script_code_.Count(code => code == '\n');
            }
        }

        public bool IsRunning
        {
            get
            {
                return (script_busy_);
            }
        }

        public bool IsPause
        {
            get
            {
                return ((script_busy_) && (task_sandbox_.IsPause));
            }
        }

        public void RunAsync()
        {
            Stop();

            /* ステータス初期化 */
            ClearMessage();
            ClearComment();

            AddMessage(ScriptMessageType.Informational, "<< Script Build and Start >>");

            if (   (script_code_ == null)
                || (script_code_.Length == 0)
            ) {
                return;
            }

            lock (task_sync_obj_) {
                script_busy_ = true;

                /* スクリプトタスク開始 */
                if (task_thread_ == null) {
                    task_thread_ = new Thread(RunTask);
                    task_sandbox_ = new ScriptSandbox(this, task_thread_);

                    task_thread_.Start();
                    task_watcher_ = (new RunTaskWatcherHandler(RunTaskWatcher)).BeginInvoke(null, null);
                }
            }

            /* 開始イベント */
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void PauseAsync(bool pause)
        {
            lock (task_sync_obj_) {
                task_sandbox_?.PauseRequest(pause);
            }
        }

        public void Pause(bool pause)
        {
            PauseAsync(pause);

            while ((IsRunning) && (!IsPause)) {
                System.Threading.Thread.Sleep(1);
            }
        }

        public void StopAsync()
        {
            lock (task_sync_obj_) {
                if (task_thread_ == null)return;

                /* サンドボックスに対して終了要求 */
                task_sandbox_.CancelRequest();
            }
        }

        public void Stop()
        {
            StopAsync();

            /* 実行中タスクを取得 */
            var task_thread = (Thread)null;

            /* 多重解放を防ぐため実行中タスクを未登録にする */
            lock (task_sync_obj_) {
                task_thread = task_thread_;
                task_thread_ = null;
                task_sandbox_ = null;
            }

            /* 実行中タスクを停止 */
            if (task_thread != null) {
                if (!task_thread.Join(TASK_EXIT_WAIT_TIME)) {
                    /* 正常終了しない場合は強制終了 */
                    task_thread.Abort();
                }
            }

            /* 監視タスクが停止するまで待つ */
            while ((task_watcher_ != null) && (!task_watcher_.IsCompleted)) { }
        }

        public void ClearMessage()
        {
            message_list_ = new List<ScriptMessageData>();
        }

        public ScriptMessageData[] GetMessageList()
        {
            lock (message_list_sync_) {
                return (message_list_.ToArray());
            }
        }

        public void ClearComment()
        {
            comment_list_ = new ScriptMessageData[script_code_line_count_];
        }

        public ScriptMessageData[] GetCommentList()
        {
            lock (comment_list_sync_) {
                return (comment_list_.Clone() as ScriptMessageData[]);
            }
        }

        private void AddMessage(ScriptMessageType type, string message)
        {
            var msg_info = new ScriptMessageData(DateTime.UtcNow, type, message);

            lock (message_list_sync_) {
                message_list_.Add(msg_info);
            }

            MessageAppended?.Invoke(this, msg_info);
        }

        private void AddCompileErrorMessage(CompilationErrorException exp)
        {
            AddMessage(ScriptMessageType.Error, exp.Message);
        }

        public void AddScriptMessage(string message)
        {
            if (message == null)return;

            AddMessage(ScriptMessageType.Notice, message);
        }

        public void SetScriptComment(int line_no, ScriptMessageType type, string message)
        {
            var change = false;
            var msg_data = (ScriptMessageData)null;

            lock (comment_list_sync_) {
                if (line_no < comment_list_.Length) {
                    if (message != null) {
                        /* データ有りの場合はデータベースに上書き登録 */
                        comment_list_[line_no] = new ScriptMessageData(DateTime.UtcNow, type, message);
                        change = true;

                    } else {
                        /* データ無しの場合はデータベースから削除 */
                        comment_list_[line_no] = null;
                        change = true;
                    }
                }
            }

            if (change) {
                CommentUpdated?.Invoke(this, line_no, msg_data);
            }
        }

        private void RunTask()
        {
            try {
                /* スクリプト開始 */
                var task_states = CSharpScript.RunAsync(script_code_, null, task_sandbox_, task_sandbox_.GetType());

                /* スクリプト終了まで待機 */
                task_states.Wait();

            } catch (CompilationErrorException exp) {
                AddCompileErrorMessage(exp);

            } catch (TaskCanceledException) {
            } catch (OperationCanceledException) {
            } catch (Exception) {
            }
        }

        private delegate void RunTaskWatcherHandler();
        private void RunTaskWatcher()
        {
            task_thread_.Join();

            if (script_busy_) {
                script_busy_ = false;

                AddMessage(ScriptMessageType.Informational, "<< Script Stop >>");

                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
