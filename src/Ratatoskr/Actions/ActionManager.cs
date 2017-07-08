using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Ratatoskr.Forms;

namespace Ratatoskr.Actions
{
    internal static class ActionManager
    {
        private const int THREAD_IVAL = 10;


        private static System.Threading.Timer thread_;

        private static Queue<ActionObject>  act_list_interrupt_ = new Queue<ActionObject>();
        private static Queue<ActionObject>  act_list_normal_    = new Queue<ActionObject>();

        private static ActionObject act_busy_interrupt_ = null;
        private static ActionObject act_busy_normal_ = null;

        private static bool exit_state_ = false;

        private static object action_sync_ = new object();


        public static void Startup()
        {
            ThreadStart();
        }

        public static void Shutdown()
        {
            exit_state_ = true;

            lock (act_list_interrupt_) {
                foreach (var act in act_list_interrupt_) {
                    act.Cancel();
                }
            }

            lock (act_list_normal_) {
                foreach (var act in act_list_normal_) {
                    act.Cancel();
                }
            }

            while (   (act_list_interrupt_.Count > 0)
                   || (act_list_normal_.Count > 0)
            ) {
                Thread.Sleep(1);
            }
        }

        private static void ThreadStart()
        {
            thread_ = new System.Threading.Timer(OnThread, null, 0, THREAD_IVAL);
        }

        private static void ThreadStop()
        {
            thread_.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private static void OnThread(object state)
        {
            thread_.Change(Timeout.Infinite, Timeout.Infinite);   // スレッドタイマー停止

            /* タスク実行 */
            Poll();

            /* === スレッド継続判定 === */
            if (!exit_state_) {
                thread_.Change(THREAD_IVAL, 0);            // スレッドタイマー再開
            }
        }

        public static void AddInterruptAction(ActionObject action)
        {
            if (action == null)return;
            if (exit_state_)return;

            lock (act_list_interrupt_) {
                act_list_interrupt_.Enqueue(action);
            }
        }

        public static void AddNormalAction(ActionObject action)
        {
            if (action == null)return;
            if (exit_state_)return;

            lock (act_list_normal_) {
                act_list_normal_.Enqueue(action);
            }
        }

        private static ActionObject LoadInterruptAction()
        {
            if (act_list_interrupt_.Count == 0)return (null);

            lock (act_list_interrupt_) {
                return (act_list_interrupt_.Dequeue());
            }
        }

        private static ActionObject LoadNormalAction()
        {
            if (act_list_normal_.Count == 0)return (null);

            lock (act_list_normal_) {
                return (act_list_normal_.Dequeue());
            }
        }

        private static bool Exec_InterruptAction()
        {
            /* 新しいアクションを読み込み */
            if (act_busy_interrupt_ == null) {
                act_busy_interrupt_ = LoadInterruptAction();
            }

            /* アクションが無い場合は終了 */
            if (act_busy_interrupt_ == null)return (false);

            /* アクション実行 */
            act_busy_interrupt_.Poll();

            /* 終了確認 */
            if (act_busy_interrupt_.IsComplete) {
                act_busy_interrupt_ = null;
            }

            return (true);
        }

        private static bool Exec_NormalAction()
        {
            /* 新しいアクションを読み込み */
            if (act_busy_normal_ == null) {
                act_busy_normal_ = LoadNormalAction();
            }

            /* アクションが無い場合は終了 */
            if (act_busy_normal_ == null)return (false);

            /* アクション実行 */
            act_busy_normal_.Poll();

            /* 終了確認 */
            if (act_busy_normal_.IsComplete) {
                act_busy_normal_ = null;
            }

            return (true);
        }

        private static void Poll()
        {
            if (!Exec_InterruptAction()) {
                Exec_NormalAction();
            }
        }
    }
}
