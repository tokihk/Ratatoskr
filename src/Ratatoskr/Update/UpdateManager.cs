using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Native.Windows;
using Ratatoskr.General;

namespace Ratatoskr.Update
{
    internal static class UpdateManager
    {
        private static readonly string PATH_WORKSPACE     = "update";
        private static readonly string PATH_DOWNLOAD_SAVE = PATH_WORKSPACE + "\\download-temp";
        private static readonly string PATH_EXTRACT       = PATH_WORKSPACE + "\\files";

        private static readonly string PATH_UPDATER_BASE = PATH_EXTRACT + "\\updater.exe";
        private static readonly string PATH_UPDATER_RUN  = PATH_WORKSPACE + "\\updater.exe";


        private enum Sequence
        {
            Initialize,
            AppListDownloadStart,
            AppListCheck,
            DownloadFileCheck,
            Complete,
        }


        private static Sequence seq_ = 0;

        private static WebDownloader downloader_ = null;


        private static string WorkspacePath    { get; set; }
        private static string DownloadSavePath { get; set; }
        private static string ExtractPath      { get; set; }
        private static string UpdaterPath      { get; set; }


        public static void Startup()
        {
            WorkspacePath = Program.GetWorkspaceDirectory(PATH_WORKSPACE);
            DownloadSavePath = Program.GetWorkspaceDirectory(PATH_DOWNLOAD_SAVE);
            ExtractPath = Program.GetWorkspaceDirectory(PATH_EXTRACT);
            UpdaterPath = Program.GetWorkspaceDirectory(PATH_UPDATER_RUN);
        }

        public static void Shutdown()
        {
        }

        public static void Poll()
        {
            SequencePoll();
        }

        private static void SequencePoll()
        {
            switch (seq_) {
                case Sequence.Initialize:           Sequence_Initialize();            break;
                case Sequence.AppListDownloadStart: Sequence_AppListDownloadStart();  break;
                case Sequence.AppListCheck:         Sequence_AppListCheck();          break;
                case Sequence.DownloadFileCheck:    Sequence_DownloadFileCheck();     break;
                case Sequence.Complete:                                               break;
                default:                                seq_++;                       break;
            }
        }

        private static void Sequence_Initialize()
        {
            /* ワークスペースを削除 */
            Shell.rm(WorkspacePath);

            /* シーケンス更新 */
            seq_++;
        }

        private static void Sequence_AppListDownloadStart()
        {
            /* 自動更新が有効ではないときは無視 */
            if (!ConfigManager.System.ApplicationCore.NewVersionAutoUpdate.Value)return;

            /* バージョンリストのダウンロード開始 */
            downloader_ = new WebDownloader();
            downloader_.DownloadString(ConfigManager.Fixed.ApplicationListUrl.Value);

            /* シーケンス更新 */
            seq_++;
        }

        private static void Sequence_AppListCheck()
        {
            /* アプリケーションリストをダウンロードするまで待つ */
            if (!downloader_.IsComplete)return;

            var app_infos = SystemInfo.ParseFromXml(downloader_.ResultString);

            /* バージョンリストが解析できなかった場合は終了 */
            if (app_infos == null) {
                seq_ = Sequence.Complete;
                return;
            }

            /* 現在より新しいバージョンのアプリ情報を降順で取得 */
            var app_infos_new =
                from info in app_infos
                where info.Name == ConfigManager.Fixed.ApplicationName.Value
                from ver in info.Versions
                where ver.IsNewVersion(Program.Version)
                orderby ver.ToVersionCode() descending
                select ver.DownloadUrl;
            
            /* 現在より新しいバージョンがなければ終了 */
            if (app_infos_new.Count() == 0) {
                seq_ = Sequence.Complete;
                return;
            }

            /* ワークスペースを初期化 */
            Shell.mkdir(WorkspacePath);

            /* ファイルダウンロード開始 */
            downloader_ = new WebDownloader();
            downloader_.DownloadFile(app_infos_new, DownloadSavePath);

            /* シーケンス更新 */
            seq_++;
        }

        private static void Sequence_DownloadFileCheck()
        {
            /* ファイルのダウンロードが完了するまで待つ */
            if (!downloader_.IsComplete)return;

            /* ファイルが存在しない場合は終了 */
            if (!File.Exists(DownloadSavePath)) {
                seq_ = Sequence.Complete;
                return;
            }

            /* ダウンロードファイルを展開 */
            Shell.rm(ExtractPath);
            ZipFile.ExtractToDirectory(DownloadSavePath, ExtractPath);
            
            /* Exeが存在するかどうかを確認 */
            var path_exe = Shell.find(ExtractPath, Path.GetFileName(Application.ExecutablePath), true);

            /* Exeが存在しない場合は終了 */
            if ((path_exe == null) || (path_exe.Length == 0)) {
                seq_ = Sequence.Complete;
                return;
            }

            /* ファイルを全て展開先ルートへ移動 */
            Shell.mv(Path.GetDirectoryName(path_exe[0]), ExtractPath);

            /* シーケンス更新 */
            seq_++;
        }

        public static bool UpdateExec()
        {
            /* 自動更新が有効ではないときは無視 */
            if (!ConfigManager.System.ApplicationCore.NewVersionAutoUpdate.Value)return (false);

            /* アップデート対象ディレクトリが存在しないときは無視 */
            if (!Directory.Exists(ExtractPath))return (false);

            /* アップデーターをアップデートディレクトリにコピー */
            Shell.cp(PATH_UPDATER_BASE, UpdaterPath);
            if (!File.Exists(UpdaterPath))return (false);

            /* アップデート開始 */
            Process.Start(
                UpdaterPath,
                String.Format(
                    "\"pid={0}\" \"src={1}\" \"dst={2}\" \"exe={3}\"",
                    Process.GetCurrentProcess().Id,
                    ExtractPath,
                    Application.StartupPath,
                    Application.ExecutablePath)
                );
            
            return (true);
        }
    }
}
