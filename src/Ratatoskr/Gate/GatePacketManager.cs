using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Debugger;
using Ratatoskr.Forms;
using Ratatoskr.Gate.AutoTimeStamp;
using Ratatoskr.Gate.AutoLogger;
using Ratatoskr.FileFormat;
using Ratatoskr.PacketConverter;
using Ratatoskr.Native.Windows;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Gate
{
    internal static class GatePacketManager
    {
		private static bool enable_  = true;

        private static IPacketContainer packets_ = null;
        private static readonly object  packets_sync_ = new object();

        private static IAsyncResult ar_load_ = null;
        private static IAsyncResult ar_save_ = null;


        public static PacketManager BasePacketManager { get; private set; }


        public static event Action             RawPacketCleared = delegate() { };
        public static event PacketEventHandler RawPacketEntried = delegate(IEnumerable<PacketObject> packets) { };


		public static void Initialize()
		{
		}

		public static void Startup()
        {
			/* パケットマネージャー初期化 */
			BasePacketManager = new PacketManager(true);
			BasePacketManager.PacketEntry += OnPacketEntry;

			/* 使用していないパケットキャッシュを削除する */
			//            ResetPacketCacheDirectory();

			packets_ = CreatePacketContainer();
        }

        public static void Shutdown()
        {
			packets_?.Dispose();
        }

        public static void Poll()
        {
            AutoTimeStampManager.Poll();

            AutoLogManager.Poll();

            /* パケット処理 */
            PacketPoll();
        }

        private static void PacketPoll()
        {
            if (!Enable || IsLoadBusy || IsSaveBusy)return;

            var rate = (ulong)0;

            /* 溜まっている全イベントパケットを取得 */
            AddPacket(BasePacketManager.DequeueAll(ref rate));

            /* 通信レート更新 */
            FormUiManager.SetCommRate(rate);
        }

        private static void OnPacketEntry(PacketObject packet, ref bool ignore)
        {
            AutoTimeStampManager.OnEntryPacket(packet);
        }

        public static bool Enable
        {
            get { return (enable_); }
            set { enable_ = value;  }
        }

        public static ulong PacketCount
        {
            get { return (packets_.Count); }
        }

        public static IPacketContainerReadOnly GetPackets()
        {
            return (packets_);
        }

        private static string GetPacketCacheDirectory()
        {
            return (Program.GetWorkspaceDirectory("packet-cache"));
        }

        private static void ResetPacketCacheDirectory()
        {
            var cache_dir_base = GetPacketCacheDirectory();

            if (!Directory.Exists(cache_dir_base))return;

            var process_id_str = "";
            var process_id = 0;
            var process = (Process)null;

            foreach (var path in Directory.EnumerateDirectories(cache_dir_base)) {
                process_id_str = Path.GetFileName(path);

                if (!int.TryParse(process_id_str, out process_id))continue;

                try {
                    process = Process.GetProcessById(process_id);
                    process.Dispose();
                } catch {
                    /* プロセスが存在しない場合はキャッシュを削除 */
                    Shell.rm(path);
                }
            }
        }

        public static IPacketContainer CreatePacketContainer()
        {
#if false
            /* プロセス番号でルートディレクトリを作成する */
            var work_dir_base = string.Format(
                                    "{0}\\{1}",
                                    Program.GetWorkspaceDirectory("packet-cache"),
                                    Process.GetCurrentProcess().Id);

            /* 重複しない作業ディレクトリを作成する */
            var work_dir = "";

            do {
                work_dir = work_dir_base + "\\" + Path.GetRandomFileName();
            } while (Directory.Exists(work_dir));

            Directory.CreateDirectory(work_dir);
#endif

//            return (new PacketContainerHuge(work_dir, (ulong)ConfigManager.System.ApplicationCore.RawPacketCountLimit.Value));
//            return (new PacketContainerHuge(work_dir, (ulong)999999));

            return (new PacketContainerLarge((ulong)ConfigManager.System.ApplicationCore.RawPacketCountLimit.Value));
//            return (new PacketContainerLarge((ulong)ConfigManager.System.ApplicationCore.RawPacketCountLimit.Value));
//            return (new PacketContainerNormal((ulong)ConfigManager.System.ApplicationCore.RawPacketCountLimit.Value));
        }

        public static void ClearPacket()
        {
            AutoTimeStampManager.ClearPacket();

            /* コンテナ初期化 */
            lock (packets_sync_) {
                packets_?.Dispose();
                packets_ = CreatePacketContainer();
            }

            BasePacketManager.Clear();

            /* イベント通知 */
            RawPacketCleared();

            /* UIパケットをクリア */
            FormTaskManager.DrawPacketClear();
        }

        private static void AddPacket(IEnumerable<PacketObject> packets)
        {
            if (   (packets == null)
				|| (packets_ == null)
			) {
				return;
			}

            /* パケット一覧に追加 */
            packets_.AddRange(packets);

            /* 描画実行 */
            FormTaskManager.DrawPacketPush(packets);

            /* イベント通知 */
            RawPacketEntried(packets);
        }

        public static void SetSystemEvent(DateTime dt, string title, string text)
        {
            var packet = new PacketObject(
                                "System",
                                PacketFacility.System,
                                "",
                                PacketPriority.Notice,
                                PacketAttribute.Message,
                                dt,
                                title,
                                PacketDirection.Recv,
                                "",
                                "",
                                0x00,
                                text,
                                null);
           
            BasePacketManager.Enqueue(packet);
        }

        public static void SetComment(DateTime dt, string text)
        {
            SetSystemEvent(dt, "Comment", text);
        }

        public static void SetComment(string text)
        {
            SetComment(DateTime.UtcNow, text);
        }

        public static void SetTimeStamp(string info)
        {
            SetSystemEvent(DateTime.UtcNow, info, "--------------------------------------------");
        }

        public static void SetWatchEvent(DateTime dt, string text)
        {
            SetSystemEvent(dt, "WatchEvent", text);
        }

        public static bool IsLoadBusy
        {
            get { return ((ar_load_ != null) && (!ar_load_.IsCompleted)); }
        }

        public static bool IsSaveBusy
        {
            get { return ((ar_save_ != null) && (!ar_save_.IsCompleted)); }
        }

        public static void LoadPacketFile(IEnumerable<FileControlParam> files)
        {
            if (   (files == null)
                || (files.Count() == 0)
            ) {
                return;
            }

            if (IsLoadBusy)return;
            if (IsSaveBusy)return;

            /* パケットクリア */
            ClearPacket();

            /* 読込処理を開始 */
            ar_load_ = (new LoadPacketFilesTaskDelegate(LoadPacketFilesTask)).BeginInvoke(files, null, null);
        }

        private delegate void LoadPacketFilesTaskDelegate(IEnumerable<FileControlParam> files);
        private static void LoadPacketFilesTask(IEnumerable<FileControlParam> files)
        {
            var packets_new = CreatePacketContainer();

            foreach (var (file, index) in files.Select((value, index) => (value, index))) {
                /* ステータステキストを更新 */
                FormUiManager.SetStatusText(
                    StatusTextID.SaveLoadEventFile,
                    String.Format(
                        "{0} {1} / {2} ({3})",
                        ConfigManager.Language.MainMessage.EventFileLoading.Value,
                        index + 1,
                        files.Count(),
                        packets_new.Count));

				/* PacketLogReaderが生成できなければ読み込まない */                
				var reader = file.Format.CreateReader() as PacketLogReader;

				if (reader == null)continue;

				/* ファイルを開くことができない場合は何もしない */
				if (!reader.Open(file.Option, file.FilePath)) {
					continue;
				}

				DebugManager.MessageOut(string.Format("LoadPacketFile - Start [{0}]", Path.GetFileName(file.FilePath)));

                /* プログレスバーを初期化 */
                FormUiManager.SetProgressBar(0, false);

                /* ファイルを1つずつ処理 */
                var task = (new LoadPacketFileExecTaskDelegate(LoadPacketFileExecTask)).BeginInvoke(
                                packets_new, reader, null, null);

                /* 完了待ち */
                while (!task.IsCompleted) {
                    System.Threading.Thread.Sleep(100);
                    FormUiManager.SetProgressBar((byte)(reader.ProgressNow / (Math.Max(reader.ProgressMax / 100, 1))), false);
                }
            }

            /* コンテナ差し替え */
            packets_?.Dispose();
            packets_ = packets_new;

            /* ステータスバーを終了 */
            FormUiManager.SetStatusText(StatusTextID.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileLoadComplete.Value);
            FormUiManager.SetProgressBar(100, true);

            /* 再描画 */
            FormTaskManager.RedrawPacketRequest();
        }

        private delegate void LoadPacketFileExecTaskDelegate(IPacketContainer packets, PacketLogReader reader);
        private static void LoadPacketFileExecTask(IPacketContainer packets, PacketLogReader reader)
        {
            PacketObject packet;

            while ((packet = reader.ReadPacket()) != null) {
                packets.Add(packet);
            }

            reader.Close();

            DebugManager.MessageOut("LoadPacketFile - Complete");
        }

        public static void SavePacketFile(FileControlParam file, IEnumerable<PacketConverterInstance> pcvt_list)
        {
            if (file == null)return;
            if (file.FilePath == null)return;

            if (IsLoadBusy)return;
            if (IsSaveBusy)return;

            /* タスク開始 */
            ar_save_ = (new SavePacketFileTaskDelegate(SavePacketFileTask)).BeginInvoke(file, pcvt_list, null, null);
        }

        private delegate void SavePacketFileTaskDelegate(FileControlParam file, IEnumerable<PacketConverterInstance> pcvt_list);
        private static void SavePacketFileTask(FileControlParam file, IEnumerable<PacketConverterInstance> pcvt_list)
        {
			var writer = file.Format.CreateWriter() as PacketLogWriter;

			if (writer == null)return;

            /* ファイルオープン */
            if (!writer.Open(file.Option, file.FilePath, false)) {
                return;
            }

			DebugManager.MessageOut(string.Format("SavePacketFile - Start [{0}]", Path.GetFileName(file.FilePath)));

            var progress_max = Math.Max(packets_.Count, 1);

            /* ステータスバーを初期化 */
            FormUiManager.SetStatusText(StatusTextID.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileSaving.Value);
            FormUiManager.SetProgressBar(0, true);

            var task_method = new SavePacketFileExecTaskDelegate(SavePacketFileExecTask);
            var task_result = (IAsyncResult)null;

            /* --- フィルタ適用有り --- */
            if (pcvt_list != null) {
                var count = (ulong)0;

                /* 変換器リセット */
                PacketConvertManager.InputStatusClear(pcvt_list);

                var task_packets = (IEnumerable<PacketObject>)null;

                foreach (var packet in packets_) {
                    /* ベースパケットをパケット変換 */
                    task_packets = PacketConvertManager.InputPacket(pcvt_list, packet);

                    /* 直前の出力の完了待ち */
                    if (task_result != null) {
                        task_method.EndInvoke(task_result);
                    }

                    /* 変換後のパケットをファイル出力開始 */
                    task_result = task_method.BeginInvoke(writer, task_packets, null, null);

                    /* プログレスバー更新 */
                    FormUiManager.SetProgressBar((byte)((double)(++count) / progress_max * 100), false);
                }

                /* 変換器内の残りパケットを処理 */
                task_packets = PacketConvertManager.InputBreakOff(pcvt_list);
                if (task_packets != null) {
                    /* 書込みタスクの完了待ち */
                    if (task_result != null) {
                        task_method.EndInvoke(task_result);
                    }

                    /* 残りパケットを出力 */
                    task_result = task_method.BeginInvoke(writer, task_packets, null, null);
                }

                /* 書込みタスクの完了待ち */
                if (task_result != null) {
                    task_method.EndInvoke(task_result);
                }

            } else {
                /* --- フィルタ適用無し --- */
                task_result = task_method.BeginInvoke(writer, packets_, null, null);
                
                /* 完了待ち */
                while (!task_result.IsCompleted) {
                    System.Threading.Thread.Sleep(100);
                    FormUiManager.SetProgressBar((byte)(writer.ProgressNow / (Math.Max(writer.ProgressMax / 100, 1))), false);
                }
            }

            /* ステータスバーを終了 */
            FormUiManager.SetStatusText(StatusTextID.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileSaveComplete.Value);
            FormUiManager.SetProgressBar(100, true);

            DebugManager.MessageOut("SavePacketFile - Complete");

            /* ファイルクローズ */
            writer.Close();
        }

        private delegate void SavePacketFileExecTaskDelegate(PacketLogWriter writer, IEnumerable<PacketObject> packets);
        private static void SavePacketFileExecTask(PacketLogWriter writer, IEnumerable<PacketObject> packets)
        {
            writer.WritePacket(packets);
        }
    }
}
