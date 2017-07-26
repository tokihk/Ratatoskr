using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Scripts.PacketFilterExp;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts
{
    internal class CommandRunner
    {
        private object              exec_sync_ = new object();
        private ExpressionCommand   exec_runner_ = null;
        private ExpressionCallStack exec_cs_ = null;
        private EventHandler        exec_end_ = null;


        public bool IsRunning
        {
            get {
                return (exec_runner_ != null);
            }
        }

        private static ExpressionCommand Build(string target, string command)
        {
            if ((command == null) || (command.Length == 0))return (null);

            var exp_param = new ExpressionParameter();

            /* コマンドパラメータ設定 */
            exp_param.Params["target"] = target;
            exp_param.Params["command"] = command;

            if (command.First() != '#') {
                /* コマンドエディタの先頭文字が'#'ではない場合は設定ファイルのデフォルトフォーマットに従う */
                command = ConfigManager.User.SingleCommandFormat.Value;
            } else {
                command = command.Substring(1);
            }

            /* コマンド解析 */
            return (ExpressionCommand.Build(command, exp_param));
        }

        public static bool FormatCheck(string target, string command)
        {
            return (Build(target, command) != null);
        }

        public void Reset()
        {
            Cancel();

            exec_cs_ = null;
        }

        public void Cancel()
        {
            lock (exec_sync_) {
                /* 実行中のコマンドをキャンセル */
                exec_cs_?.ExecuteCancel();
            }

            /* すぐにリソースを解放 */
            ExecComplete();
        }

        public bool Exec(string target, string command, EventHandler callback)
        {
            /* 実行中の場合は失敗 */
            if (IsRunning)return (false);

            lock (exec_sync_) {
                /* コマンドコンパイル */
                exec_runner_ = Build(target, command);

                if (exec_runner_ == null)return (false);

                if (exec_cs_ == null) {
                    /* コールスタック生成 */
                    exec_cs_ = new ExpressionCallStack();

                    /* 実行モード */
                    exec_cs_.ExecuteFlag = ExecuteFlags.ActionExecute;
                }

                /* 終了イベントをバックアップ */
                exec_end_ = callback;

                /* コマンドを別タスクで実行 */
                (new ExecTaskDelegate(ExecTask)).BeginInvoke(exec_runner_, exec_cs_, null, null);
            }

            return (true);
        }

        private delegate void ExecTaskDelegate(ExpressionCommand runner, ExpressionCallStack cs);
        private void ExecTask(ExpressionCommand runner, ExpressionCallStack cs)
        {
            /* コマンド実行 */
            runner.Exec(cs);

            /* 終了イベント実施 */
            ExecComplete();
        }

        private void ExecComplete()
        {
            lock (exec_sync_) {
                if (exec_runner_ != null) {
                    exec_runner_ = null;
                    exec_end_?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
