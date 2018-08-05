using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Gate.AutoLogger;
using Ratatoskr.Generic;
using Ratatoskr.Native;
using Ratatoskr.Plugin;
using Ratatoskr.Scripts;
using Ratatoskr.Update;

namespace Ratatoskr
{
    internal static class Program
    {
        private const int APP_TIMER_DEFAULT   = 10;
        private const int GC_CONTROL_INTERVAL = 30000;
        private const int MUTEX_TIMEOUT       = 15000;
        private const int PROCESS_TIMEOUT     = 15000;


        [STAThread]
        static void Main(string[] args)
        {
            /* タイマー分解能変更 */
            NativeMethods.timeBeginPeriod(1);

            /* システムUI設定 */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            /* デバッグウィンドウ起動 */
            Debugger.DebugManager.Startup();

#if !DEBUG
            try {
#endif
                CommandLineParse(args);

                Exec();

#if !DEBUG
            } catch (Exception exp) {
                ExceptionInfoOutput(exp);
                throw exp;
            }
#endif

            /* for Debug */
            System.Diagnostics.Debug.WriteLine("ExitThread2");

            /* タイマー分解能差し戻し */
            NativeMethods.timeBeginPeriod(1);
        }

        private static void ExceptionInfoOutput(Exception exp)
        {
            try {
                var output_path = Application.StartupPath + "\\abort.log";

                /* ログファイルが膨れ上がった場合は削除する */
                if (File.Exists(output_path)) {
                    if ((new FileInfo(output_path)).Length > 2048000) {
                        File.Delete(output_path);
                    }
                }

                using (var writer = new StreamWriter(output_path, true)) {
                    writer.WriteLine("--------------------------------------------------------------------------");

                    /* 日時情報 */
                    writer.WriteLine(string.Format("Datetime(Local):     {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                    /* アプリケーション情報 */
                    writer.WriteLine(string.Format("Application Name:    {0}", Application.ExecutablePath));
                    writer.WriteLine(string.Format("Application Version: {0}", AppInfo.Version));

                    /* 例外情報 */
                    writer.WriteLine();
                    writer.WriteLine(exp.ToString());
                    writer.WriteLine();
                    writer.WriteLine();
                }
            } catch {
            }
        }


        internal enum StartupTaskID
        {
            LoadPlugin,
        }


        private static ApplicationContext         app_context_ = new ApplicationContext();
        private static System.Windows.Forms.Timer app_timer_ = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer gc_control_timer_ = new System.Windows.Forms.Timer();

        private static byte[] startup_task_progress_;

        private static bool startup_state_ = false;
        private static bool shutdown_req_ = false;
        private static bool shutdown_state_ = false;
        private static bool restart_req_ = false;

        private static Guid profile_id_load_ = Guid.Empty;

        private static List<(ScriptRunMode mode, string path)> startup_script_list_ = new List<(ScriptRunMode, string)>();


        public static AppVersion Version    { get; private set; }
        public static bool       IsSafeMode { get; private set; } = false;

        public static Api.ApiSandbox API { get; } = new Api.ApiSandbox();

        private static void CommandLineParse(string[] cmdlines)
        {
            Debugger.DebugManager.MessageOut("Program.CommandLineParse - Start");

            foreach (var cmdline in cmdlines) {
                var (name, value) = CommandLineParse(cmdline);

                if (name == null)continue;

                CommandLineSetup(name, value);
            }

            Debugger.DebugManager.MessageOut("Program.CommandLineParse - End");
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

                /* セーフモード */
                case "-safe-mode":
                {
                    IsSafeMode = true;
                }
                    break;

                /* スクリプト(OneShot) */
                case "-script":
                {
                    startup_script_list_.Add((ScriptRunMode.OneShot, value));
                }
                    break;

                /* スクリプト(Repeat) */
                case "-rscript":
                {
                    startup_script_list_.Add((ScriptRunMode.Repeat, value));
                }
                    break;
            }
        }

        private static void LoadAppInfo()
        {
            /* バージョン情報取得 */
            Version = new AppVersion(AppInfo.Version);
        }

        public static byte GetStartupProgressAverage()
        {
            return ((byte)startup_task_progress_.Average(value => (double)value));
        }

        public static byte GetStartupProgress(StartupTaskID id)
        {
            return (startup_task_progress_[(int)id]);
        }

        public static void SetStartupProgress(StartupTaskID id, byte value)
        {
            if (   (startup_task_progress_ != null)
                && ((int)id < startup_task_progress_.Length)
            ) {
                startup_task_progress_[(int)id] = value;
            }
        }

        public static void SystemStart()
        {
            /* メインフレーム作成 */
            FormUiManager.MainFormVisible(true);

            /* イベント処理開始 */
            AutoLogManager.Poll();
            GateTransferManager.Startup();
        }

        public static void Exec()
        {
            startup_task_progress_ = new byte[Enum.GetValues(typeof(StartupTaskID)).Length];

            LoadAppInfo();

            /* メインタイマー設定 */
            app_timer_.Tick += OnAppTimer;
            app_timer_.Interval = APP_TIMER_DEFAULT;
            app_timer_.Start();

            /* ガベージコレクション制御タイマー */
            gc_control_timer_.Tick += OnGcControlTimer;
            gc_control_timer_.Interval = GC_CONTROL_INTERVAL;
            gc_control_timer_.Start();

            /* プラグイン読み込み開始 */
            PluginManager.Startup();

            /* スクリプトマネージャ初期化 */
            ScriptManager.Startup();

            /* スタートアップスクリプト登録 */
            foreach (var script_info in startup_script_list_) {
                var script = ScriptManager.Register(script_info.path, script_info.mode);

                if (script != null) {
                    script.RunAsync();
                }
            }

            /* アプリケーションループ実行 */
            Debugger.DebugManager.MessageOut("Application.Run - Start");
            Application.Run(app_context_);
            Debugger.DebugManager.MessageOut("Application.Run - End");

            /* 実行中スクリプトを停止 */
            ScriptManager.Shutdown();
        }

        private static bool Startup()
        {
            Debugger.DebugManager.MessageOut("Program.Startup - Start");

            /* 下層マネージャー初期化 */
            ConfigManager.Startup();
            GatePacketManager.Startup();

            /* マネージャー初期化 */
            GateManager.Startup();
            FormUiManager.Startup();
            FormTaskManager.Startup();
//            UpdateManager.Startup();

            /* 設定ファイル読み込み */
            ConfigManager.LoadConfig(profile_id_load_);

            /* アプリケーションタイマー再起動 */
            app_timer_.Stop();
            app_timer_.Interval = (int)ConfigManager.System.ApplicationCore.AppTimerInterval.Value;
            app_timer_.Start();

            /* アップデート開始 */
//            if (UpdateManager.UpdateExec()) {
//                return (false);
//            }

            startup_state_ = true;
            restart_req_ = false;

            Debugger.DebugManager.MessageOut("Program.Startup - End");

            return (true);
        }

        private static void Shutdown()
        {
            Debugger.DebugManager.MessageOut("Program.Shutdown - Start");

            /* 設定ファイル保存 */
            ConfigManager.SaveConfig();

            /* イベント処理停止 */
            GatePacketManager.Shutdown();
            GateTransferManager.Shutdown();

            /* マネージャー停止 */
//            UpdateManager.Shutdown();
            FormUiManager.Shutdown();
            FormTaskManager.Shutdown();
            GateManager.Shutdown();
            ConfigManager.Shutdown();

            startup_state_ = false;

            Debugger.DebugManager.MessageOut("Program.Shutdown - End");
        }

        public static void ShutdownRequest()
        {
            shutdown_req_ = true;
        }

        public static void RestartRequest()
        {
            restart_req_ = true;
        }

        public static void ChangeProfile(Guid profile_id)
        {
            profile_id_load_ = profile_id;

            RestartRequest();
        }

        private static void OnAppTimer(object sender, EventArgs e)
        {
            /* 初期化処理 */
            if (   (!startup_state_)
                && (!shutdown_req_)
            ) {
                Startup();
            }

            /* シャットダウン処理 */
            if (   (startup_state_)
                && ((restart_req_ || shutdown_req_))
            ) {
                Shutdown();
            }

            /* タスク処理 */
            if (startup_state_) {
                GateManager.Poll();
                GatePacketManager.Poll();
                FormUiManager.Poll();
                FormTaskManager.Poll();
                UpdateManager.Poll();
                ScriptManager.Poll();
            }

            /* アプリケーション終了処理 */
            if (   (shutdown_req_)
                && (!startup_state_)
                && (!shutdown_state_)
            ) {
                /* for Debug */
                System.Diagnostics.Debug.WriteLine("ExitThread");

                shutdown_state_ = true;

                app_context_.ExitThread();
            }
        }

        private static void OnGcControlTimer(object sender, EventArgs e)
        {
            GC.Collect();
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
