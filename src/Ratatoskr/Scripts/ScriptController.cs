using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.ScriptEngines;

namespace Ratatoskr.Scripts
{
    public enum ScriptRunMode
    {
        OneShot,
        Repeat,
    }

    public enum ScriptRunStatus
    {
        Stop,
        Running,
        Pause,
    }

    internal class ScriptController : IScriptRunner
    {
        private IScriptRunner runner_;

        private ScriptRunMode    mode_ = ScriptRunMode.OneShot;
        private ScriptRunStatus  status_ = ScriptRunStatus.Stop;

        private bool detach_req_ = false;
        private bool detach_state_ = false;


        public ScriptController(IScriptRunner runner, ScriptRunMode mode = ScriptRunMode.OneShot)
        {
            runner_ = runner;
            runner_.StatusChanged += Runner_StatusChanged;
            runner_.MessageAppended += Runner_MessageAppended;
            runner_.CommentUpdated += Runner_CommentUpdated;
            Mode = mode;
        }

        public event EventHandler                 StatusChanged;
        public event ScriptMessageAppendedHandler MessageAppended;
        public event ScriptCommentUpdatedHandler  CommentUpdated;

        protected IScriptRunner Runner
        {
            get { return (runner_); }
        }

        public bool IsDetach
        {
            get { return (detach_state_); }
        }

        public ScriptRunMode Mode
        {
            get { return (mode_); }
            set { mode_ = value;  }
        }

        public ScriptRunStatus Status
        {
            get
            {
                return ((!IsDetach) ? (status_) : (ScriptRunStatus.Stop));
            }

            set
            {
                switch (value) {
                    case ScriptRunStatus.Running:
                        if (runner_.IsRunning) {
                            runner_.PauseAsync(false);
                        } else {
                            runner_.RunAsync();
                        }
                        break;
                    case ScriptRunStatus.Pause:
                        runner_.PauseAsync(true);
                        break;
                    case ScriptRunStatus.Stop:
                        runner_.StopAsync();
                        break;
                }
            }
        }

        protected virtual bool GetDetachStatus()
        {
            return (false);
        }

        protected ScriptRunStatus GetRunnerStatus()
        {
            var status = ScriptRunStatus.Stop;

            if ((!detach_req_) && (!GetDetachStatus())) {
                if (runner_.IsPause) {
                    status = ScriptRunStatus.Pause;
                } else if (runner_.IsRunning) {
                    status = ScriptRunStatus.Running;
                }
            }

            return (status);
        }

        public bool Poll()
        {
            if (detach_state_)return (false);

            if ((!detach_req_) && (GetDetachStatus())) {
                detach_req_ = true;
                runner_.StopAsync();
            }

            if ((!detach_req_) && (!runner_.IsRunning)) {
                if (mode_ == ScriptRunMode.Repeat) {
                    runner_.RunAsync();
                }
            }

            if ((detach_req_) && (!detach_state_)) {
                detach_state_ = !runner_.IsRunning;
            }

            /* 状態更新 */
            var update_state = false;
            var status_new = GetRunnerStatus();

            if (status_ != status_new) {
                status_ = status_new;
                update_state = true;
            }

            return (update_state);
        }

        public bool IsRunning
        {
            get { return (runner_.IsRunning); }
        }

        public bool IsPause
        {
            get { return (runner_.IsPause); }
        }

        public void RunAsync()
        {
            runner_.RunAsync();
        }

        public void PauseAsync(bool pause)
        {
            runner_.PauseAsync(pause);
        }

        public void Pause(bool pause)
        {
            runner_.Pause(pause);
        }

        public void StopAsync()
        {
            runner_.StopAsync();
        }

        public void Stop()
        {
            runner_.Stop();
        }

        public void ClearMessage()
        {
            runner_.ClearMessage();
        }

        public ScriptMessageData[] GetMessageList()
        {
            return (runner_.GetMessageList());
        }

        public void ClearComment()
        {
            runner_.ClearComment();
        }

        public ScriptMessageData[] GetCommentList()
        {
            return (runner_.GetCommentList());
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
