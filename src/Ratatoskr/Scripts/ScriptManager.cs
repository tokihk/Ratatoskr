using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Scripts.ScriptEngines;

namespace Ratatoskr.Scripts
{
    internal delegate void ScriptControllerListUpdatedHandler();
    internal delegate void ScriptControllerStatusUpdatedHandler(ScriptController controller);

    internal static class ScriptManager
    {
        private const int SCRIPT_PAUSE_TIMEOUT = 2000;
        private const int SCRIPT_STOP_TIMEOUT  = 2000;


        private static List<ScriptFileController> controller_list_ = new List<ScriptFileController>();
        private static object                     controller_list_sync_ = new object();


        public static event ScriptControllerListUpdatedHandler   ControllerListUpdated;
        public static event ScriptControllerStatusUpdatedHandler ControllerStatusUpdated;


        public static void Startup()
        {
        }

        public static void Shutdown()
        {
            Clear();
        }

        public static void Poll()
        {
            var update_list = new List<ScriptController>();
            var remove_state = false;

            lock (controller_list_sync_) {
                /* 実行 */
                foreach (var controller in controller_list_) {
                    if ((controller.Poll()) && (!controller.IsDetach)) {
                        update_list.Add(controller);
                    }
                }

                /* デタッチ済みスクリプトを解放 */
                remove_state = (controller_list_.RemoveAll(script => script.IsDetach) > 0);

                /* アップデートリストから削除済みコントローラを削除する */
                update_list.RemoveAll(controller => !controller_list_.Contains(controller));
            }

            if (update_list.Count > 0) {
                update_list.ForEach(controller => ControllerStatusUpdated?.Invoke(controller));
            }

            if (remove_state) {
                ControllerListUpdated?.Invoke();
            }
        }

        public static IEnumerable<ScriptController> ScriptList
        {
            get
            {
                lock (controller_list_sync_) {
                    return (controller_list_.ToArray());
                }   
            }
        }

        private static void Clear()
        {
            var script_list = (List<ScriptFileController>)null;

            /* 新しいリストと入れ替え */
            lock (controller_list_sync_) {
                script_list = controller_list_;
                controller_list_ = new List<ScriptFileController>();
            }

            if (script_list.Count > 0) {
                var sw_timeout = new System.Diagnostics.Stopwatch();

                /* 全スクリプトに停止要求 */
                script_list.ForEach(script => script.StopAsync());

                /* 全スクリプトが停止するまで待つ */
                sw_timeout.Restart();
                while (   (sw_timeout.ElapsedMilliseconds < SCRIPT_STOP_TIMEOUT)
                       && (!script_list.TrueForAll(script => script.Status == ScriptRunStatus.Stop))
                ) {
                    System.Threading.Thread.Sleep(100);
                }

                /* 残っているスクリプトを完全に停止させる */
                controller_list_.ForEach(script => script.Stop());

                ControllerListUpdated?.Invoke();
            }
        }

        private static void Pause(bool pause)
        {
            var sw_timeout = new System.Diagnostics.Stopwatch();

            lock (controller_list_sync_) {
                if (controller_list_.Count > 0) {
                    /* 全てのスクリプトに一時停止要求 */
                    controller_list_.ForEach(script => script.PauseAsync(true));

                    /* 全てのスクリプトが停止するか一定時間経過を待つ */
                    sw_timeout.Restart();
                    while (   (sw_timeout.ElapsedMilliseconds < SCRIPT_PAUSE_TIMEOUT)
                           && (!controller_list_.TrueForAll(script => script.Status != ScriptRunStatus.Running))
                    ) {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }

            /* 停止しなかったスクリプトを強制終了する */
            var remove_list = (List<ScriptFileController>)null;

            lock (controller_list_sync_) {
                /* 登録済みスクリプトのうち、実行中のスクリプトのみ抽出 */
                remove_list = controller_list_.FindAll(script => script.Status == ScriptRunStatus.Running);

                /* 実行中のスクリプトをデータベースから削除 */
                remove_list.ForEach(script => controller_list_.Remove(script));
            }

            /* 削除対象スクリプトを完全に停止させる */
            remove_list.ForEach(script => script.Stop());
        }

        public static ScriptController Register(string script_path, ScriptRunMode mode)
        {
            if (script_path == null)return (null);

            /* ファイルが存在しなければ無視 */
            if (!File.Exists(script_path))return (null);

            var controller = (ScriptFileController)null;
            var add_state = false;
            var update_state = false;

            lock (controller_list_sync_) {
                /* 登録リストから検索 */
                controller = controller_list_.Find(script => script.ScriptPath == script_path);

                /* 未登録の場合は新規登録 */
                if (controller == null) {
                    controller = new ScriptFileController(script_path);
                    controller_list_.Add(controller);
                    add_state = true;
                }
            }

            /* モード変更 */
            if ((controller != null) && (controller.Mode != mode)) {
                controller.Mode = mode;
                update_state = true;
            }

            /* イベント */
            if (add_state) {
                ControllerListUpdated?.Invoke();
            } else if (update_state) {
                ControllerStatusUpdated?.Invoke(controller);
            }

            return (controller);
        }
    }
}
