using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        private const int GC_CONTROL_INTERVAL = 30000;
        private const int MUTEX_TIMEOUT       = 15000;
        private const int PROCESS_TIMEOUT     = 15000;


        [STAThread]
        static void Main(string[] args)
        {
            CommandLineParse(args);

            /* タイマー分解能変更 */
            NativeMethods.timeBeginPeriod(1);

            /* システムUI設定 */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            /* デバッグウィンドウ表示 */
#if DEBUG
            DebugWindowEnable(true);
#endif
            do {
                LoadAppInfo();

                if (Startup()) {
                    Exec();
                }
                Shutdown();
            } while (restart_req_);

            /* タイマー分解能差し戻し */
            NativeMethods.timeBeginPeriod(1);
        }


        private static DebugForm debug_form_ = null;

        private static ApplicationContext         app_context_ = new ApplicationContext();
        private static System.Windows.Forms.Timer app_timer_ = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer gc_control_timer_ = new System.Windows.Forms.Timer();

        private static bool shutdown_req_ = false;
        private static bool restart_req_ = false;

        private static Guid profile_id_load_ = Guid.Empty;


        public static AppVersion Version   { get; private set; }


        private static void CommandLineParse(string[] cmdlines)
        {
            foreach (var cmdline in cmdlines) {
                var param_info = CommandLineParse(cmdline);

                if (param_info.name == null)continue;

                CommandLineSetup(param_info.name, param_info.value);
            }
        }

        private static (string name, string value) CommandLineParse(string cmdline)
        {
            var sep = cmdline.IndexOf('=');

            if (sep < 0) {
                sep = cmdline.Length - 1;
            }

            return (cmdline.Substring(0, sep), (sep < cmdline.Length) ? (cmdline.Substring(sep + 1, cmdline.Length - sep - 1)) : (null));
        }

        private static void CommandLineSetup(string name, string value)
        {
            switch (name) {
                /* プロファイル指定 */
                case "-profile":
                {
                    try {
                        profile_id_load_ = Guid.Parse(value);
                    } catch {
                    }
                }
                    break;

                /* ミューテックス終了待ち */
                case "-wait-mutex-lock":
                {
                    var mutex = new System.Threading.Mutex(false, value);

                    try {
                        mutex.WaitOne(MUTEX_TIMEOUT);
                    } finally {
                        mutex.ReleaseMutex();
                        mutex.Close();
                    }
                }
                    break;

                /* プロセス終了待ち */
                case "-wait-pid-exit":
                {
                    var process = (System.Diagnostics.Process)null;

                    try {
                        process = System.Diagnostics.Process.GetProcessById(int.Parse(value));
                        process.WaitForExit(PROCESS_TIMEOUT);
                    } finally {
                        process.Close();
                        process.Dispose();
                    }
                }
                    break;
            }
        }

        private static void LoadAppInfo()
        {
            /* バージョン情報取得 */
            Version = new AppVersion(AppInfo.Version);
        }

        private static bool Startup()
        {
            shutdown_req_ = false;
            restart_req_ = false;

            /* 下層マネージャー初期化 */
            ConfigManager.Startup();
            GatePacketManager.Startup();

            /* マネージャー初期化 */
            GateManager.Startup();
            FormUiManager.Startup();
            FormTaskManager.Startup();
            ActionManager.Startup();
            UpdateManager.Startup();

            /* 設定ファイル読み込み */
            ConfigManager.LoadConfig(profile_id_load_);

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

            /* ガベージコレクション制御タイマー */
            gc_control_timer_.Tick += OnGcControlTimer;
            gc_control_timer_.Interval = GC_CONTROL_INTERVAL;

            /* イベント処理開始 */
            PacketAutoSaveManager.Setup();
            GateTransferManager.Startup();

            return (true);
        }

        private static void Shutdown()
        {
            /* イベント処理停止 */
            GatePacketManager.Shutdown();
            GateTransferManager.Shutdown();

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

            /* ガベージコレクション制御タイマー開始 */
            gc_control_timer_.Start();

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

        public static void RestartRequest()
        {
            restart_req_ = true;

            ShutdownRequest();
        }

        public static void ChangeProfile(Guid profile_id)
        {
            profile_id_load_ = profile_id;

            RestartRequest();
        }

        private static void OnAppTimer(object sender, EventArgs e)
        {
            if (shutdown_req_) {
                /* === シャットダウン処理 === */
                /* 設定ファイル保存 */
                ConfigManager.SaveConfig();

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

        private static void OnGcControlTimer(object sender, EventArgs e)
        {
            GC.Collect();
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

        public static void DebugMessage(object obj)
        {
            if (debug_form_ == null)return;

            debug_form_.AddMessage(obj.ToString());
        }

        public static string GetWorkspaceDirectory(string subname = null)
        {
            var path_root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ConfigManager.Fixed.ApplicationName.Value;

            if (subname != null) {
                path_root = path_root + "\\" + subname;
            }

            return (path_root);
        }

        public static bool IsAdministratorMode
        {
            get
            {
                /* 管理者権限かどうかを確認 */
                return ((new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator));
            }
        }
    }
}
