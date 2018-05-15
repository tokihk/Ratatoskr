using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Scripts.ScriptEngines
{
    internal class ScriptFileRunner : IScriptRunner
    {
        private ScriptCodeRunner runner_;
        private string script_path_;


        public ScriptFileRunner(string script_path)
        {
            script_path_ = script_path;
        }

        public event EventHandler                 StatusChanged;
        public event ScriptMessageAppendedHandler MessageAppended;
        public event ScriptCommentUpdatedHandler  CommentUpdated;

        public bool IsRunning
        {
            get { return ((runner_ != null) ? (runner_.IsRunning) : (false)); }
        }

        public bool IsPause
        {
            get { return ((runner_ != null) ? (runner_.IsPause) : (false)); }
        }

        public string ScriptPath
        {
            get { return (script_path_); }
        }

        public void RunAsync()
        {
            runner_?.Stop();
            runner_ = null;

            /* ファイル存在確認 */
            if (   (script_path_ == null)
                || (!File.Exists(script_path_))
            ) {
                return;
            }

            /* スクリプト読み込み */
            var script_code = (string)null;

            try {
                script_code = File.ReadAllText(script_path_);
            } catch {
                return;
            }

            if (script_code.Length == 0)return;

            /* 実行 */
            runner_ = new ScriptCodeRunner(script_code);
            runner_.StatusChanged += Runner_StatusChanged;
            runner_.MessageAppended += Runner_MessageAppended;
            runner_.CommentUpdated += Runner_CommentUpdated;
            runner_.RunAsync();
        }

        public void PauseAsync(bool pause)
        {
            runner_?.PauseAsync(pause);
        }

        public void Pause(bool pause)
        {
            runner_?.Pause(pause);
        }

        public void StopAsync()
        {
            runner_?.StopAsync();
        }

        public void Stop()
        {
            runner_?.Stop();
        }

        public void ClearMessage()
        {
            runner_?.ClearMessage();
        }

        public ScriptMessageData[] GetMessageList()
        {
            return ((runner_ != null) ? (runner_.GetMessageList()) : (new ScriptMessageData[] { }));
        }

        public void ClearComment()
        {
            runner_?.ClearComment();
        }

        public ScriptMessageData[] GetCommentList()
        {
            return ((runner_ != null) ? (runner_.GetCommentList()) : (new ScriptMessageData[] { }));
        }

        private void Runner_StatusChanged(object sender, EventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        private void Runner_MessageAppended(object sender, ScriptMessageData msg)
        {
            MessageAppended?.Invoke(this, msg);
        }

        private void Runner_CommentUpdated(object sender, int line_no, ScriptMessageData msg)
        {
            CommentUpdated?.Invoke(this, line_no, msg);
        }
    }
}
