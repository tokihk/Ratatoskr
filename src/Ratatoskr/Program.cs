using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ratatoskr.Actions;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Forms.DebugForm;
using Ratatoskr.Gate;
using Ratatoskr.Gate.PacketAutoSave;
using Ratatoskr.Generic;
using Ratatoskr.Native;
using Ratatoskr.Update;

namespace Ratatoskr
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            /* タイマー分解能変更 */
            NativeMethods.timeBeginPeriod(1);

            /* システムUI設定 */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            /* デバッグウィンドウ表示 */
#if DEBUG
            DebugWindowEnable(true);
#endif

            if (Startup()) {
                Exec();
            }
            Shutdown();

            /* タイマー分解能差し戻し */
            NativeMethods.timeBeginPeriod(1);
        }


        private static DebugForm debug_form_ = null;

        private static ApplicationContext         app_context_ = new ApplicationContext();
        private static System.Windows.Forms.Timer app_timer_ = new System.Windows.Forms.Timer();

        private static bool shutdown_req_ = false;


        public static AppVersion Version { get; private set; }


        private static bool Startup()
        {
            /* バージョン情報取得 */
            Version = new AppVersion(
                ((System.Reflection.AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(
                    System.Reflection.Assembly.GetExecutingAssembly(), 
                    typeof(System.Reflection.AssemblyFileVersionAttribute))
                ).Version);

            /* マネージャー初期化 */
            ConfigManager.Startup();
            GateManager.Startup();
            FormUiManager.Startup();
            FormTaskManager.Startup();
            ActionManager.Startup();
            UpdateManager.Startup();

            /* 設定ファイル読み込み */
            ConfigManager.LoadAllConfig();

            /* アップデート開始 */
            if (UpdateManager.UpdateExec()) {
                return (false);
            }

            /* メインフレーム作成 */
            FormUiManager.MainFrameCreate();

            /* 設定適用 */
            FormUiManager.LoadConfig();
            FormUiManager.MainFrameVisible(true);

            /* メインタイマー設定 */
            app_timer_.Tick += OnAppTimer;
            app_timer_.Interval = (int)ConfigManager.System.ApplicationCore.AppTimerInterval.Value;

            /* イベント処理開始 */
            PacketAutoSaveManager.Update();
            GateRedirectManager.Startup();
            GateTransferManager.Startup();
            GatePacketManager.Startup();

            return (true);
        }

        private static void Shutdown()
        {
            /* イベント処理停止 */
            GatePacketManager.Shutdown();
            GateTransferManager.Shutdown();
            GateRedirectManager.Shutdown();

            /* メインタイマー破棄 */
            app_timer_.Tick -= OnAppTimer;

            /* マネージャー停止 */
            UpdateManager.Shutdown();
            ActionManager.Shutdown();
            FormUiManager.Shutdown();
            FormTaskManager.Shutdown();
            GateManager.Shutdown();
            ConfigManager.Shutdown();
        }

        public static void Exec()
        {
            /* メインタイマー開始 */
            app_timer_.Start();

            /* メインフォーム表示 */
            FormUiManager.MainFrameVisible(true);

            /* アプリケーションループ実行 */
            Application.Run(app_context_);
        }

        public static void ShutdownRequest()
        {
            if (!shutdown_req_) {
                shutdown_req_ = true;
            }
        }

        private static void OnAppTimer(object sender, EventArgs e)
        {
            if (shutdown_req_) {
                /* === シャットダウン処理 === */
                /* 設定バックアップ */
                FormUiManager.BackupConfig();

                /* 設定ファイル保存 */
                ConfigManager.SaveAllConfig();

                /* 終了処理 */
                app_context_.ExitThread();

            } else {
                GateManager.Poll();
                GatePacketManager.Poll();
                FormUiManager.Poll();
                FormTaskManager.Poll();
                UpdateManager.Poll();
            }
        }

        public static void DebugWindowEnable(bool enable)
        {
            if (enable) {
                if (debug_form_ == null) {
                    debug_form_ = new DebugForm();
                }
            } else {
                if (debug_form_ != null) {
                    debug_form_.Hide();
                    debug_form_.Dispose();
                    debug_form_ = null;
                }
            }
        }

        public static void DebugMessage(string text)
        {
            if (debug_form_ == null)return;

            debug_form_.AddMessage(text);
        }

        public static string GetWorkspaceDirectory(string subdir = null)
        {
            var path_root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ratatoskr";

            if (subdir != null) {
                path_root = path_root + "\\" + subdir;
            }

            return (path_root);
        }
    }
}
