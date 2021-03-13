using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.User;
using Ratatoskr.FileFormat;
using Ratatoskr.Forms;
using Ratatoskr.Forms.Dialog;
using Ratatoskr.Gate;
using Ratatoskr.Plugin;

namespace Ratatoskr.Forms
{
    /* 優先度  高:上  低:下 */
    internal enum StatusTextID
    {
        SaveLoadEventFile,
        ReloadScreen,
        SequentialCommandStatus,
        Unknown,
    }

    internal static class FormUiManager
    {
        private const int STARTUP_PROGRESS_VISIBLE_DELAY = 500;

        private static Stopwatch startup_busy_timer_;
        private static bool      startup_complete_ = false;

        private static string          last_save_path_ = null;
        private static FileFormatClass last_save_format_ = null;

        private static FormStatus status_busy_;
        private static FormStatus status_new_;
        private static readonly object status_sync_ = new object();

        private static string[]   status_text_;

        private static Stopwatch     popup_timer_ = new Stopwatch();
        private static Queue<string> popup_text_;

        private static Stopwatch     progress_bar_timer_ = new Stopwatch();

        private static WatchEventNotifyDialog watch_event_dialog_ = new WatchEventNotifyDialog();


        public static void Startup()
        {
			MainFrame = new MainWindow.MainWindow_Form();

            status_busy_ = new FormStatus();
            status_new_ = new FormStatus();
            status_text_ = new string[Enum.GetValues(typeof(StatusTextID)).Length];
            popup_text_ = new Queue<string>();

            /* 起動処理開始 */
            startup_complete_ = false;
            startup_busy_timer_ = new Stopwatch();
            startup_busy_timer_.Restart();
        }

        public static void Shutdown()
        {
            if (MainFrame != null) {
                MainFrame.Dispose();
                MainFrame = null;
            }

            if (ScriptWindow != null) {
                ScriptWindow.Dispose();
                ScriptWindow = null;
            }
        }

        public static void Poll()
        {
            StartupTaskPoll();
            StatusTextPoll();
            ProgressBarPoll();
            StatusPoll();
        }

        public static void LoadConfig()
        {
            MainFrame?.LoadConfig();
        }

        public static void BackupConfig()
        {
            MainFrame?.BackupConfig();
            ScriptWindow?.BackupConfig();
        }

        private static SplashScreen          SplashScreen { get; set; }
        private static MainWindow.MainWindow_Form     MainFrame    { get; set; }
        private static ScriptWindow.ScriptWindow_Form ScriptWindow { get; set; }

        public static bool InvokeRequired
        {
            get
            {
                if (MainFrame != null) {
                    return (MainFrame.InvokeRequired);
                } else {
                    return (false);
                }
            }
        }

        public static object Invoke(Delegate method, params object[] args)
        {
            return (MainFrame.Invoke(method, args));
        }

        public static void MainFormVisible(bool show)
        {
            if (!MainFrame.Visible) {
                /* 非表示→表示 */
                MainFrame.Activate();
                MainFrame.Show();
                MainFrame.Update();

            } else {
                /* 表示→非表示 */
                MainFrame.Hide();
            }
        }

        public static bool MainFormVisible()
        {
            return ((MainFrame != null) ? (MainFrame.Visible) : (false));
        }

        public static void ScriptWindowVisible(bool show)
        {
            /* フォーム作成 */
            if (ScriptWindow == null) {
                ScriptWindow = new ScriptWindow.ScriptWindow_Form();
                ScriptWindow.LoadConfig();
            }

            if (!ScriptWindow.Visible) {
                /* 非表示→表示 */
                ScriptWindow.Activate();
                ScriptWindow.Show();
                ScriptWindow.Update();

            } else {
                /* 表示→非表示 */
                ScriptWindow.Hide();
            }
        }

        public static bool ScriptWindowVisible()
        {
            return ((ScriptWindow != null) ? (ScriptWindow.Visible) : (false));
        }

        public static void SplashScreen_SetValue(string text, byte value)
        {
            if (SplashScreen == null) {
                SplashScreen = new SplashScreen()
                {
                    Visible = true
                };
            }

            SplashScreen.SetProgress(text, value);
        }

        private static void SplashScreen_Close()
        {
            if (SplashScreen != null) {
                SplashScreen.Visible = false;
                SplashScreen.Dispose();
                SplashScreen = null;
            }
        }

        public static void MainFrameMenuBarUpdate()
        {
            MainFrame.UpdateTitle();
            MainFrame.UpdateMenuBarStatus();
        }

        public static void MainFrameStatusBarUpdate()
        {
            MainFrame.UpdateStatusBarStatus();
        }

        private static void StartupTaskPoll()
        {
            if (startup_complete_)return;

            var task_id = 0;
            var task_id_max = Enum.GetValues(typeof(Program.StartupTaskID)).Length;

            /* 優先度の高いタスクから検索し、進捗度が100%になっていないものを取得 */
            while (   (task_id < task_id_max)
                   && (Program.GetStartupProgress((Program.StartupTaskID)task_id) == 100)
            ) {
                task_id++;
            }

            /* === 進捗率表示 === */
            if (startup_busy_timer_.ElapsedMilliseconds > STARTUP_PROGRESS_VISIBLE_DELAY) {
                SplashScreen_SetValue(
                    GetStartupText((Program.StartupTaskID)task_id),
                    Math.Min((byte)100, Program.GetStartupProgressAverage()));
            }

            if (task_id >= task_id_max) {
                /* === 全タスクが完了 === */
                startup_complete_ = true;
                startup_busy_timer_ = null;

                Program.SystemStart();

                SplashScreen_Close();
            }
        }

        private static string GetStartupText(Program.StartupTaskID id)
        {
            if (Enum.IsDefined(typeof(Program.StartupTaskID), id)) {
                switch (id) {
                    case Program.StartupTaskID.LoadPlugin: return ("Load plugin...");
                    default:                               return (((Program.StartupTaskID)id).ToString());
                }
            } else {
                return ("Complete");
            }
        }

        private static void StatusTextPoll()
        {
            /* ポップアップタイマーが経過していなければ無視 */
            if (   (popup_timer_.IsRunning)
                && (popup_timer_.ElapsedMilliseconds < ConfigManager.Fixed.PopupStatusTextTime.Value)
            ) {
                return;
            }

            /* 次のステータステキストを取得 */
            var text_new = GetNextStatusText();

            lock (status_sync_) {
                status_new_.MainStatusBar_Text = text_new;
            }
        }

        private static void StatusPoll()
        {
            lock (status_sync_) {
                /* ステータス値に変化がない場合は無視 */
                if (status_busy_.Equals(status_new_))return;

                /* ステータス値をバックアップ */
                status_new_.CopyTo(status_busy_);
            }

            /* ステータス値を適用 */
            MainFrame.SetFormStatus(status_busy_);
        }

        public static void SetPopupText(string text)
        {
            lock (popup_text_) {
                popup_text_.Enqueue(text);
            }
        }

        private static string GetNextStatusText()
        {
            lock (popup_text_) {
                if (popup_text_.Count > 0) {
                    return (popup_text_.Dequeue());
                } else {
                    foreach (var text in status_text_) {
                        if (text == null)continue;
                        if (text.Length == 0)continue;

                        return (text);
                    }

                    return ("");
                }
            }
        }

        public static void SetStatusText(StatusTextID id, string text)
        {
            lock (popup_text_) {
                status_text_[(int)id] = text;
            }
        }

        private static void ProgressBarPoll()
        {
            if (   (progress_bar_timer_.IsRunning)
                && (progress_bar_timer_.ElapsedMilliseconds > ConfigManager.Fixed.PopupStatusTextTime.Value)
            ) {
                progress_bar_timer_.Stop();
                ClearProgressBar();
            }
        }

        public static void ClearProgressBar()
        {
            lock (status_sync_) {
                status_new_.MainProgressBar_Visible = false;
            }
        }

        public static void SetProgressBar(byte value, bool auto_clear)
        {
            lock (status_sync_) {
                status_new_.MainProgressBar_Visible = true;
                status_new_.MainProgressBar_Value = value;
            }

            if (auto_clear) {
                progress_bar_timer_.Restart();
            }
        }

        public static void SetPacketCounter(ulong count_cache, ulong count_raw, ulong count_draw, ulong count_wait)
        {
            lock (status_sync_) {
                status_new_.PacketCount_Cache = count_cache;
                status_new_.PacketCount_Raw = count_raw;
                status_new_.PacketCount_DrawAll = count_draw;
                status_new_.PacketCount_DrawBusy = count_wait;
            }
        }

        public static void SetCommRate(ulong rate)
        {
            lock (status_sync_) {
                status_new_.PacketBytePSec_All = rate;
            }
        }

        public static void SetWatchEventText(string text)
        {
            watch_event_dialog_.AddText(text);

            if (!watch_event_dialog_.Visible) {
                watch_event_dialog_.Show();
            }
        }

        public static string SendTarget
        {
            get { return (MainFrame.SendTarget); }
        }

        private delegate bool ConfirmMessageBoxDelegate(string message, string caption);
        public static bool ConfirmMessageBox(string message, string caption = null)
        {
            if (InvokeRequired) {
                return ((bool)MainFrame.Invoke(new ConfirmMessageBoxDelegate(ConfirmMessageBox), message, caption));
            }

            if (caption == null) {
                caption = ConfigManager.Fixed.ApplicationName.Value;
            }

            if (MessageBox.Show(message, caption, MessageBoxButtons.OKCancel) != DialogResult.OK)return (false);

            return (true);
        }

        private delegate bool ShowProfileEditDialogHandler(string title, UserConfig config, params string[] ignore_names);
        public static bool ShowProfileEditDialog(string title, UserConfig config, params string[] ignore_names)
        {
            if (InvokeRequired) {
                return ((bool)Invoke((ShowProfileEditDialogHandler)ShowProfileEditDialog, title, config, ignore_names));
            }

            var dialog = new ProfileEditDialog()
            {
                Text = title,
                ProfileName = config.ProfileName.Value,
                ProfileComment = config.ProfileComment.Value,
                ProfileReadOnly = config.ReadOnly.Value,
                ProfileReadOnlyLock = config.ReadOnlyLock.Value,
            };

            dialog.ProfileEnableNames.AddRange(ignore_names);

            if (dialog.ShowDialog() != DialogResult.OK)return (false);

            config.ProfileName.Value = dialog.ProfileName;
            config.ProfileComment.Value = dialog.ProfileComment;
            config.ReadOnly.Value = dialog.ProfileReadOnly;
            config.ReadOnlyLock.Value = dialog.ProfileReadOnlyLock;

            return (true);
        }

        public static void ShowOptionDialog()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ShowOptionDialog);
                return;
            }

            var clone_data = new OptionEditForm.OptionDataMap(ConfigManager.System, ConfigManager.User, ConfigManager.Language);

            if (clone_data == null)return;

            var dialog = new OptionEditForm.OptionEditForm(clone_data);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            clone_data.Backup(ConfigManager.System, ConfigManager.User, ConfigManager.Language);
        }

        public static void ShowAppDocument()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ShowAppDocument);
                return;
            }

            try {
                var uri_base = Application.StartupPath;

                Process.Start(uri_base + "\\docs\\index.html", "#test");
            } catch {
            }
        }

        public static void ShowAppInfo()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ShowAppInfo);
                return;
            }

            (new Forms.AboutForm.AboutForm()).ShowDialog();
        }

        public static void FileOpen()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)FileOpen);
                return;
            }

            /* 開くファイルをユーザーに選択させる */
            var infos = FileManager.FileOpen.SelectReadTargetFromDialog(ConfigManager.GetCurrentDirectory(), true, true);

            if (infos == null)return;

            /* 選択したファイルを開く */
            FileOpen(infos);

            /* カレントディレクトリ更新 */
            ConfigManager.SetCurrentDirectory(Path.GetDirectoryName(infos.First().FilePath));
        }

        public static void FileOpen(IEnumerable<string> paths)
        {
            var file_paths = new List<string>();

            /* ディレクトリはファイルに分解する  */
            foreach (var path in paths) {
                try {
                    if (Directory.Exists(path)) {
                        file_paths.AddRange(Directory.GetFiles(path, "*", SearchOption.AllDirectories));
                    } else if (File.Exists(path)) {
                        file_paths.Add(path);
                    }
                } catch { }
            }

            /* ファイル名でソートする */
            file_paths.Sort();

            /* ファイル名もしくはファイル内容から初期フォーマットを特定する */
            FileOpen(FileManager.FileOpen.GetReadTargetFromPaths(file_paths));
        }

        private delegate void FileOpenDelegate(IEnumerable<FileReadTargetInfo> infos);
        public static void FileOpen(IEnumerable<FileReadTargetInfo> infos)
        {
            if (infos == null)return;
            if (infos.Count() == 0)return;

            if (InvokeRequired) {
                Invoke((FileOpenDelegate)FileOpen, infos);
                return;
            }

            /* ファイル種別毎に分類分け */
            var infos_config = infos.Where(info => info.Reader is UserConfigReader);
            var infos_log = infos.Where(info => info.Reader is PacketLogReader);

            FileOpen_UserConfig(infos.Where(info => info.Reader is UserConfigReader));
            FileOpen_PacketLog(infos.Where(info => info.Reader is PacketLogReader));
        }

        private static void FileOpen_UserConfig(IEnumerable<FileReadTargetInfo> infos)
        {
            if (infos == null)return;

            foreach (var info in infos) {
                FileOpen_UserConfig(info);
            }
        }

        private static void FileOpen_UserConfig(FileReadTargetInfo info)
        {
            var reader = info.Reader as UserConfigReader;

            if (reader == null)return;

            if (!reader.Open(null, info.FilePath))return;

            var config = reader.Load();

            if (config != null) {
                /* 新しいプロファイルとして読み込む */
                ConfigManager.ImportProfile(config.Config, config.ExtDataList);
            }

            reader.Close();
        }

        private static void FileOpen_PacketLog(IEnumerable<FileReadTargetInfo> infos)
        {
            if (infos == null)return;

            /* ファイルの読み込み順を昇順にソート */
            infos = infos.OrderBy(info => info.FilePath);

            GatePacketManager.LoadPacketFile(infos);
        }

        private delegate string AnyFileOpenDelegate(string init_dir);
        public static string AnyFileOpen(string init_dir = null)
        {
            if (InvokeRequired) {
                return (Invoke((AnyFileOpenDelegate)AnyFileOpen, init_dir) as string);
            }

            var dialog = new OpenFileDialog();

            if (   (init_dir != null)
                && (Directory.Exists(init_dir))
            ) {
                dialog.InitialDirectory = init_dir;
            } else {
                dialog.InitialDirectory = ConfigManager.GetCurrentDirectory();
            }

            dialog.Multiselect = false;

            if (dialog.ShowDialog() != DialogResult.OK)return (null);

            return (dialog.FileName);
        }

        private delegate string AnyFileSaveDelegate(string init_dir);
        public static string AnyFileSave(string init_dir = null)
        {
            if (InvokeRequired) {
                return (Invoke((AnyFileSaveDelegate)AnyFileSave, init_dir) as string);
            }

            var dialog = new SaveFileDialog();

            if (   (init_dir != null)
                && (Directory.Exists(init_dir))
            ) {
                dialog.InitialDirectory = init_dir;
            } else {
                dialog.InitialDirectory = ConfigManager.GetCurrentDirectory();
            }

            dialog.CheckPathExists = true;

            if (dialog.ShowDialog() != DialogResult.OK)return (null);

            return (dialog.FileName);
        }

        private delegate void PacketSaveDelegate(bool overwrite, bool rule);
        public static void PacketSave(bool overwrite, bool rule)
        {
            if (InvokeRequired) {
                Invoke((PacketSaveDelegate)PacketSave, overwrite, rule);
                return;
            }

            var info = (FileWriteTargetInfo)null;

            /* 保存先とフォーマットを取得 */
            if (   (overwrite)
                && (last_save_path_ != null)
                && (last_save_format_ != null)
            ) {
                info = new FileWriteTargetInfo()
                {
                    FilePath = last_save_path_,
                    Writer = last_save_format_.CreateWriter(),
                    Option = last_save_format_.CreateWriterOption()
                };
            } else {
                info = FileManager.PacketLogSave.SelectWriterTargetFromDialog(ConfigManager.GetCurrentDirectory());
            }

            if (info == null)return;

            if (rule) {
                GatePacketManager.SavePacketFile(info, FormTaskManager.GetPacketConverterClone());
            } else {
                GatePacketManager.SavePacketFile(info, null);
            }

            last_save_format_ = info.Writer.Class;
            last_save_path_ = info.FilePath;

            /* カレントディレクトリ更新 */
            ConfigManager.SetCurrentDirectory(Path.GetDirectoryName(last_save_path_));
        }

        private delegate FileWriteTargetInfo CreateUserConfigWriterDelegate();
        public static FileWriteTargetInfo CreateUserConfigWriter()
        {
            if (MainFrame.InvokeRequired) {
                return ((FileWriteTargetInfo)MainFrame.Invoke(new CreateUserConfigWriterDelegate(CreateUserConfigWriter)));
            }

            return (FileManager.UserConfigSave.SelectWriterTargetFromDialog(ConfigManager.GetCurrentDirectory()));
        }
    }
}
