using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions
{
    internal abstract class ActionObject
    {
        protected enum ExecState
        {
            Idle,
            Busy,
        }

        public enum ActionResultType
        {
            Success,
            Error_Default,
            Error_Running,
            Error_Cancel,
            Error_Argument,
            Error_FileOpen,
            Error_Unknown,
        }


        private bool init_state_ = false;
        private bool exit_req_ = false;
        private bool exit_state_ = false;

        private List<ActionParam> params_ = new List<ActionParam>();
        private ActionResultType  result_status_ = ActionResultType.Error_Running;
        private ActionParam[]     result_values_ = null;

        private object running_sync_ = new object();

        public delegate void ActionCompletedDelegate(object sender, ActionResultType result, ActionParam[] result_values);
        public event ActionCompletedDelegate ActionCompleted = delegate(object sender, ActionResultType result, ActionParam[] result_values) { };

        public ExpressionCallStack CallStack { get; set; } = null;


        public ActionObject()
        {
        }

        protected void RegisterArgument(string name, Type value_type, object value = null)
        {
            lock (running_sync_) {
                /* 同じ名前の引数を削除 */
                params_.RemoveAll(param => param.Name == name);

                /* パラメータを追加 */
                params_.Add(new ActionParam(name, value_type, value));
            }
        }

        private void SetArgumentValue(ActionParam container, object value)
        {
            if (container == null)return;

            /* 引数のタイプが異なる場合は無視 */
            if (   (value != null)
                && (container.ValueType != null)
                && (value.GetType() != container.ValueType)
            ) {
                return;
            }

            /* 引数変更 */
            container.Value = value;
        }

        public void SetArgumentValue(string name, object value)
        {
            lock (running_sync_) {
                SetArgumentValue(params_.Find(param => param.Name == name), value);
            }
        }

        public void SetArgumentValue(int index, object value)
        {
            lock (running_sync_) {
                if (index >= params_.Count)return;

                SetArgumentValue(params_[index], value);
            }
        }

        protected object GetArgumentValue(string name)
        {
            lock (running_sync_) {
                var container = params_.Find(param => param.Name == name);

                if (container == null)return (null);

                return (container.Value);
            }
        }

        protected object GetArgumentValue(int index)
        {
            lock (running_sync_) {
                if (index >= params_.Count)return (null);

                var container = params_[index];

                return (container.Value);
            }
        }

        public void SetResult(ActionResultType result, ActionParam[] result_values)
        {
            lock (running_sync_) {
                result_status_ = result;
                result_values_ = result_values;
                exit_req_ = true;
            }
        }

        public ActionResultType GetResultStatus()
        {
            return (result_status_);
        }

        public ActionParam[] GetResultValues()
        {
            return (result_values_);
        }

        public bool IsComplete
        {
            get { return (exit_state_); }
        }

        public uint ProgressMax { get; protected set; } = 0;
        public uint ProgressNow { get; protected set; } = 0;

        public bool ArgumentCheck()
        {
            foreach (var param in params_) {
                if (param.Value == null) {
                    return (false);
                }
            }

            return (OnArgumentCheck());
        }

        public void Cancel()
        {
            SetResult(ActionResultType.Error_Cancel, null);
        }

        public void Poll()
        {
            if (exit_state_)return;

            /* 初期化処理 */
            if ((!exit_req_) && (!init_state_)) {
                /* 引数チェック */
                if (ArgumentCheck()) {
                    OnExecStart();
                    init_state_ = true;

                } else {
                    /* 引数エラー */
                    SetResult(ActionResultType.Error_Argument, null);
                }
            }

            /* 実行処理 */
            if (!exit_req_) {
                OnExecPoll();
            }

            /* 終了処理 */
            if ((exit_req_) && (!exit_state_)) {
                /* 初期化処理が実行されたときのみ終了処理を呼ぶ */
                if (init_state_) {
                    OnExecComplete();
                }

                ActionCompleted(this, result_status_, result_values_);
                exit_state_ = true;
            }
        }

        public void Exec()
        {
            while (!IsComplete) {
                Poll();
            }
        }

        protected virtual void OnExecStart()
        {
        }

        protected virtual void OnExecPoll()
        {
        }

        protected virtual void OnExecComplete()
        {
        }

        protected virtual bool OnArgumentCheck()
        {
            return (true);
        }
    }
}
