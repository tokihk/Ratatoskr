using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions
{
    internal abstract class ActionObject
    {
        protected enum ExecState
        {
            Busy,
            Complete,
        }

        public enum ActionResultStatus
        {
            Success,
            Error,
        }


        public delegate void ActionCompletedDelegate(ActionObject sender, ActionResultStatus result);
        public event ActionCompletedDelegate ActionCompleted = delegate(ActionObject sender, ActionResultStatus result) { };


        private bool init_state_ = false;
        private bool exit_state_ = false;
        private bool cancel_req_ = false;

        private List<ActionParam> params_ = new List<ActionParam>();
        private List<ActionParam> results_ = new List<ActionParam>();

        public ActionResultStatus ResultStatus { get; protected set; } = ActionResultStatus.Success;

        public ExpressionCallStack CallStack { get; set; } = null;


        public ActionObject()
        {
        }

        public bool IsComplete
        {
            get { return (exit_state_); }
        }

        protected void InitParameter<T>(string name)
            where T : Term
        {
            /* 重複パラメータを削除 */
            params_.RemoveAll(param => param.Name == name);

            /* 終端にパラメータを追加 */
            params_.Add(new ActionParam(name, null, typeof(T)));
        }

        public void SetParameter(uint index, Term value)
        {
            if (index >= params_.Count)return;
            if (value == null)return;

            var param = params_[(int)index];

            /* 設定パラメータがVoidのときは初期値を設定 */
            if (value.GetType() == typeof(Term_Void)) {
                value = param.ValueType.InvokeMember(
                                            null,
                                            System.Reflection.BindingFlags.CreateInstance,
                                            null,
                                            null,
                                            new object[] { }) as Term_Void;
            }

            /* 引数の型と設定パラメータの型が一致しない */
            if (   (param.ValueType != typeof(Term_Void)
                && (param.ValueType != value.GetType()))
            ) {
                return;
            }

            /* パラメータを設定 */
            param.Value = value;
        }

        public void SetParameter(string name, Term value)
        {
            SetParameter((uint)params_.FindIndex(param => param.Name == name), value);
        }

        protected Term GetParameter(uint index)
        {
            if (index >= params_.Count) {
                return (new Term_Void());
            }

            /* 一致するIDのパラメータを取得 */
            var param = params_[(int)index];

            if (param.Name == null) {
                return (new Term_Void());
            }

            return (param.Value);
        }

        protected T GetParameter<T>(uint index)
            where T : Term
        {
            return (GetParameter(index) as T);
        }

        protected Term GetParameter(string name)
        {
            return (GetParameter((uint)params_.FindIndex(param => param.Name == name)));
        }

        protected T GetParameter<T>(string name)
            where T : Term
        {
            return (GetParameter(name) as T);
        }

        protected void InitResult<T>(string name)
            where T : Term, new()
        {
            /* 重複パラメータを削除 */
            results_.RemoveAll(result => result.Name == name);

            /* 終端にパラメータを追加 */
            results_.Add(new ActionParam(name, new T(), typeof(T)));
        }

        public void SetResult(uint index, Term value)
        {
            if (index >= results_.Count)return;

            var result = results_[(int)index];

            /* 設定パラメータがVoidのときは初期値を設定 */
            if (value.GetType() == typeof(Term_Void)) {
                value = result.ValueType.InvokeMember(
                                            null,
                                            System.Reflection.BindingFlags.CreateInstance,
                                            null,
                                            null,
                                            new object[] { }) as Term_Void;
            }

            /* 引数の型と設定パラメータの型が一致しない */
            if (   (result.ValueType != typeof(Term_Void)
                && (result.ValueType != value.GetType()))
            ) {
                return;
            }

            /* パラメータを設定 */
            result.Value = value;
        }

        public void SetResult(string name, Term value)
        {
            SetResult((uint)results_.FindIndex(result => result.Name == name), value);
        }

        public Term GetResult(uint index)
        {
            if (index >= (uint)results_.Count) {
                return (new Term_Void());
            }

            /* 一致するIDのパラメータを取得 */
            var result = results_[(int)index];

            if (result.Name == null) {
                return (new Term_Void());
            }

            return (result.Value);
        }

        public T GetResult<T>(uint index)
            where T : Term
        {
            return (GetResult(index) as T);
        }

        public Term GetResult(string name)
        {
            return (GetResult((uint)results_.FindIndex(result => result.Name == name)));
        }

        public T GetResult<T>(string name)
            where T : Term
        {
            return (GetResult(name) as T);
        }

        public KeyValuePair<string, Term>[] GetAllResult()
        {
            return (from result in results_
                    select new KeyValuePair<string, Term>(result.Name, result.Value)
                   ).ToArray();
        }

        public void Cancel()
        {
            cancel_req_ = true;
        }

        public bool ParameterCheck()
        {
            foreach (var param in params_) {
                if (param.Value == null)return (false);
            }

            return (OnParameterCheck());
        }

        public void Poll()
        {
            if (exit_state_)return;

            var cancel_req = cancel_req_;

            /* 初期化処理 */
            if ((!cancel_req) && (!init_state_)) {
                cancel_req = (OnExecStart() == ExecState.Complete);
                init_state_ = true;
            }

            /* 実行処理 */
            if (!cancel_req) {
                cancel_req = (OnExecPoll() == ExecState.Complete);
            }

            /* 終了処理 */
            if ((cancel_req) && (!exit_state_)) {
                /* 初期化処理が実行されたときのみ終了処理を呼ぶ */
                if (init_state_) {
                    OnExecComplete();
                }

                ActionCompleted(this, ResultStatus);
                exit_state_ = true;
            }
        }

        public void Exec()
        {
            while (!IsComplete) {
                Poll();
            }
        }

        public virtual bool OnParameterCheck() { return (true); }

        protected virtual ExecState OnExecStart()    { return (ExecState.Busy); }
        protected virtual ExecState OnExecPoll()     { return (ExecState.Complete); }
        protected virtual void      OnExecComplete() { }
    }
}
