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
//        private static FileFormatManager ffm_open_ = new FileFormatManager();
//        private static FileFormatManager ffm_save_packet_ = new FileFormatManager();

        private static string          last_save_path_ = null;
        private static FileFormatClass last_save_format_ = null;

        private static FormStatus status_busy_ = new FormStatus();
        private static FormStatus status_new_ = new FormStatus();
        private static object     status_sync_ = new object();

        private static string[]      status_text_ = new string[Enum.GetValues(typeof(StatusTextId)).Length];

        private static Stopwatch     popup_timer_ = new Stopwatch();
        private static Queue<string> popup_text_ = new Queue<string>();

        private static Stopwatch     progress_bar_timer_ = new Stopwatch();

        private static WatchEventNotifyDialog watch_event_dialog_ = new WatchEventNotifyDialog();


        public static void Startup()
        {
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

        public static bool InvokeRequired()
        {
            if (MainFrame != null) {
                return (MainFrame.InvokeRequired);
            } else {
                return (false);
            }
        }

        public static object Invoke(Delegate method, params object[] args)
        {
            return (MainFrame.Invoke(method, args));
        }

        public static object BeginInvoke(Delegate method, params object[] args)
        {
            return (MainFrame.BeginInvoke(method, args));
        }

        private delegate bool ConfirmMessageBoxDelegate(string message, string caption);
        public static bool ConfirmMessageBox(string message, string caption = null)
        {
            if (MainFrame.InvokeRequired) {
                return ((bool)MainFrame.Invoke(new ConfirmMessageBoxDelegate(ConfirmMessageBox), message, caption));
            }

            if (caption == null) {
                caption = ConfigManager.Fixed.ApplicationName.Value;
            }

            if (MessageBox.Show(message, caption, MessageBoxButtons.OKCancel) != DialogResult.OK)return (false);

            return (true);
        }

        public static void FileOpen()
        {
            /* ダイアログ表示 */
            var paths_input = (string[])null;
            var format = FileManager.AllFormat.SelectReaderFormatFromDialog(ConfigManager.GetCurrentDirectory(), true, true, ref paths_input);

            if ((paths_input == null) || (paths_input.Length == 0))return;
            if (format == null)return;

            FileOpen(paths_input, format);

            /* カレントディレクトリ更新 */
            ConfigManager.SetCurrentDirectory(Path.GetDirectoryName(paths_input.First()));
        }

        public static void FileOpen(IEnumerable<string> paths, FileFormatClass format)
        {
            if (paths.Count() == 0)return;

            if (format == null) {
                format = FileManager.AllFormat.SelectReaderFormatFromPath(paths.First());
            }
            if (format == null)return;

            var reader = format.GetReader();

            /* パケットログ */
            if (reader.reader is IPacketLogReader) {
                GatePacketManager.LoadPacketFile(paths, reader.reader as PacketLogReader, reader.option);
            }
        }

        public static string AnyFileOpen(string init_dir = null)
        {
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

        public static void PacketSave(bool overwrite, bool rule)
        {
            /* 保存先とフォーマットを取得 */
            if (   (!overwrite)
                || (last_save_path_ == null)
                || (last_save_format_ == null)
            ) {
                /* ダイアログ表示 */
                last_save_format_ = FileManager.AllFormat.SelectWriterFormatFromDialog(ConfigManager.GetCurrentDirectory(), ref last_save_path_, typeof(IPacketLogWriter));
            }
            
            if (last_save_path_ == null)return;
            if (last_save_format_ == null)return;

            /* ライター取得 */
            var writer = last_save_format_.GetWriter();

            if (rule) {
                GatePacketManager.SavePacketFile(last_save_path_, writer.writer as PacketLogWriter, writer.option, FormTaskManager.GetPacketConverterClone());
            } else {
                GatePacketManager.SavePacketFile(last_save_path_, writer.writer as PacketLogWriter, writer.option, null);
            }

            /* カレントディレクトリ更新 */
            ConfigManager.SetCurrentDirectory(Path.GetDirectoryName(last_save_path_));
        }
    }
}
