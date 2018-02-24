using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.FileFormats;
using Ratatoskr.Forms.Dialog;
using Ratatoskr.Gate;
using Ratatoskr.Generic;

namespace Ratatoskr.Forms
{
    internal enum StatusTextId
    {
        SaveLoadEventFile,
        ReloadScreen,
        SequentialCommandStatus,
        Unknown,
    }

    internal static class FormUiManager
    {
        private static string          last_save_path_ = null;
        private static FileFormatClass last_save_format_ = null;

        private static FormStatus status_busy_;
        private static FormStatus status_new_;
        private static object     status_sync_ = new object();

        private static string[]   status_text_;

        private static Stopwatch     popup_timer_ = new Stopwatch();
        private static Queue<string> popup_text_;

        private static Stopwatch     progress_bar_timer_ = new Stopwatch();

        private static WatchEventNotifyDialog watch_event_dialog_ = new WatchEventNotifyDialog();


        public static void Startup()
        {
            status_busy_ = new FormStatus();
            status_new_ = new FormStatus();
            status_text_ = new string[Enum.GetValues(typeof(StatusTextId)).Length];
            popup_text_ = new Queue<string>();
        }

        public static void Shutdown()
        {
            MainFrameVisible(false);
        }

        public static void Poll()
        {
            StatusTextPoll();
            ProgressBarPoll();
            StatusPoll();
        }

        public static void LoadConfig()
        {
            MainFrame.LoadConfig();
        }

        public static void BackupConfig()
        {
            MainFrame.BackupConfig();
        }

        public static MainFrame.MainFrame MainFrame
        {
            get; private set;
        }

        public static void MainFrameCreate()
        {
            /* メインフォーム作成 */
            MainFrame = new Forms.MainFrame.MainFrame();
        }

        public static void MainFrameVisible(bool visible)
        {
            if (visible) {
                MainFrame.Activate();
                MainFrame.Show();
                MainFrame.Update();
            } else {
                MainFrame.Hide();
            }
        }

        public static void MainFrameMenuBarUpdate()
        {
            MainFrame.UpdateMenuBar();
        }

        public static void MainFrameStatusBarUpdate()
        {
            MainFrame.UpdateStatusBar();
        }

        public static void SetStatusText(StatusTextId id, string text)
        {
            lock (popup_text_) {
                status_text_[(int)id] = text;
            }
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
            /* ステータス値に変化がない場合は無視 */
            if (ClassUtil.Compare(status_busy_, status_new_))return;

            /* ステータス値をバックアップ */
            lock (status_sync_) {
                status_busy_ = ClassUtil.Clone(status_new_);
            }

            /* ステータス値を適用 */
            MainFrame.SetFormStatus(status_busy_);
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

        private static void ProgressBarPoll()
        {
            if (   (progress_bar_timer_.IsRunning)
                && (progress_bar_timer_.ElapsedMilliseconds > ConfigManager.Fixed.PopupStatusTextTime.Value)
            ) {
                progress_bar_timer_.Stop();
                ClearProgressBar();
            }
        }

        public static void SetPacketCounter(ulong count_base, ulong count_draw, ulong count_wait)
        {
            lock (status_sync_) {
                status_new_.PacketCount_All = count_base;
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

        public static void AddPacket(string protocol, byte[] bitdata, uint bitsize)
        {
            MainFrame.AddPacket(protocol, bitdata, bitsize);
        }

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

        public static void ShowOptionDialog()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ShowOptionDialog);
                return;
            }

            var config = ClassUtil.Clone(ConfigManager.User.Option);

            if (config == null)return;

            var dialog = new Forms.OptionForm.OptionForm(config);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ConfigManager.User.Option = config;
        }

        public static void ShowAppDocument()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ShowAppDocument);
                return;
            }

            try {
                var uri_base = Application.StartupPath;

                Process.Start(uri_base + "\\docs\\index.html");
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

            /* ダイアログ表示 */
            var format_info = FileManager.FileOpen.SelectReaderFormatFromDialog(ConfigManager.GetCurrentDirectory(), true, true);

            if (format_info.format == null)return;

            FileOpen(format_info.paths, format_info.format);

            /* カレントディレクトリ更新 */
            ConfigManager.SetCurrentDirectory(Path.GetDirectoryName(format_info.paths.First()));
        }

        private delegate void FileOpenDelegate(IEnumerable<string> paths, FileFormatClass format);
        public static void FileOpen(IEnumerable<string> paths, FileFormatClass format)
        {
            if (InvokeRequired) {
                Invoke((FileOpenDelegate)FileOpen, paths, format);
                return;
            }

            if (paths.Count() == 0)return;

            if (format == null) {
                format = FileManager.FileOpen.SelectReaderFormatFromPath(paths.First());
            }
            if (format == null)return;

            var reader = format.GetReader();

            /* パケットログ */
            if (reader.reader is PacketLogReader) {
                FileOpen_PacketLog(paths, reader.reader as PacketLogReader, reader.option);
            } else if (reader.reader is UserConfigReader) {
                FileOpen_UserConfig(paths, reader.reader as UserConfigReader);
            }
        }

        private static void FileOpen_PacketLog(IEnumerable<string> paths, PacketLogReader reader, FileFormatOption option)
        {
            if (reader == null)return;

            GatePacketManager.LoadPacketFile(paths, reader, option);
        }

        private static void FileOpen_UserConfig(IEnumerable<string> paths, UserConfigReader reader)
        {
            if (reader == null)return;

            if (reader.Open(null, paths.First())) {
                var config = reader.Load();

                if (config.config != null) {
                    /* 現在のUserConfigに上書きする */
                    ConfigManager.OverrideConfig(config.config);
                }

                reader.Close();
            }
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

        private delegate void PacketSaveDelegate(bool overwrite, bool rule);
        public static void PacketSave(bool overwrite, bool rule)
        {
            if (InvokeRequired) {
                Invoke((PacketSaveDelegate)PacketSave, overwrite, rule);
                return;
            }

            var format = last_save_format_;
            var path = last_save_path_;

            /* 保存先とフォーマットを取得 */
            if (   (!overwrite)
                || (format == null)
                || (path == null)
            ) {
                /* ダイアログ表示 */
                (format, path) = FileManager.PacketLogSave.SelectWriterFormatFromDialog(ConfigManager.GetCurrentDirectory());
            }
            
            if (format == null)return;
            if (path == null)return;

            /* ライター取得 */
            var writer_info = format.GetWriter();
            var writer = writer_info.writer as PacketLogWriter;

            if (writer == null)return;

            if (rule) {
                GatePacketManager.SavePacketFile(path, writer, writer_info.option, FormTaskManager.GetPacketConverterClone());
            } else {
                GatePacketManager.SavePacketFile(path, writer, writer_info.option, null);
            }

            /* カレントディレクトリ更新 */
            ConfigManager.SetCurrentDirectory(Path.GetDirectoryName(last_save_path_));

            last_save_format_ = format;
            last_save_path_ = path;
        }

        private delegate (UserConfigWriter writer, UserConfigWriterOption option, string path) CreateUserConfigWriterDelegate();
        public static (UserConfigWriter writer, UserConfigWriterOption option, string path) CreateUserConfigWriter()
        {
            if (MainFrame.InvokeRequired) {
                return (((UserConfigWriter writer, UserConfigWriterOption option, string path))MainFrame.Invoke(new CreateUserConfigWriterDelegate(CreateUserConfigWriter)));
            }

            var format = (FileFormatClass)null;
            var path = (string)null;

            /* ダイアログでフォーマットを判別 */
            (format, path) = FileManager.UserConfigSave.SelectWriterFormatFromDialog(ConfigManager.GetCurrentDirectory());

            if (format == null)return (null, null, null);

            var writer_info = format.GetWriter();
            var writer = writer_info.writer as UserConfigWriter;
            var option = writer_info.option as UserConfigWriterOption;

            if (   (writer == null)
                || (option == null)
            ) {
                return (null, null, null);
            }

            return (writer, option, path);
        }

        private delegate (PacketLogReader reader, FileFormatOption option, string[] paths) CreatePacketLogReaderDelegate(string file_path);
        public static (PacketLogReader reader, FileFormatOption option, string[] paths) CreatePacketLogReader(string file_path = null)
        {
            if (MainFrame.InvokeRequired) {
                return (((PacketLogReader, FileFormatOption, string[]))MainFrame.Invoke(new CreatePacketLogReaderDelegate(CreatePacketLogReader), file_path));
            }

            /* パス名からフォーマットを判別 */
            var format = FileManager.PacketLogOpen.SelectReaderFormatFromPath(file_path);
            var paths = new string[] { file_path };

            /* パス名で見つからない場合はダイアログで選択 */
            if (format == null) {
                (format, paths) = FileManager.PacketLogOpen.SelectReaderFormatFromDialog(ConfigManager.GetCurrentDirectory(), true, true);
            }

            if (format == null)return (null, null, null);

            var reader_info = format.GetReader();
            var reader = reader_info.reader as PacketLogReader;

            if (reader == null)return (null, null, null);

            return (reader, reader_info.option, paths);
        }

        private delegate (PacketLogWriter writer, FileFormatOption option, string path) CreatePacketLogWriterDelegate();
        public static (PacketLogWriter writer, FileFormatOption option, string path) CreatePacketLogWriter()
        {
            if (MainFrame.InvokeRequired) {
                return (((PacketLogWriter, FileFormatOption, string))MainFrame.Invoke(new CreatePacketLogWriterDelegate(CreatePacketLogWriter)));
            }

            var format = (FileFormatClass)null;
            var path = (string)null;

            /* ダイアログでフォーマットを判別 */
            (format, path) = FileManager.PacketLogSave.SelectWriterFormatFromDialog(ConfigManager.GetCurrentDirectory());

            if (format == null)return (null, null, null);

            var writer_info = format.GetWriter();
            var writer = writer_info.writer as PacketLogWriter;

            if (writer == null)return (null, null, null);

            return (writer, writer_info.option, path);
        }
    }
}
