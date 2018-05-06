using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.FileFormats;
using Ratatoskr.Gate.AutoTimeStamp;
using Ratatoskr.Gate.AutoPacketSave;
using Ratatoskr.PacketConverters;
using Ratatoskr.Packet;

namespace Ratatoskr.Gate
{
    internal static class GatePacketManager
    {
        private static bool enable_ = true;

        private static PacketContainer packets_;
        private static object          packets_sync_ = new object();

        private static IAsyncResult ar_load_ = null;
        private static IAsyncResult ar_save_ = null;


        public static PacketManager BasePacketManager { get; private set; }


        public delegate void EventHandler();
        public delegate void PacketEventHandler(IEnumerable<PacketObject> packets);

        public static event EventHandler       RawPacketCleared = delegate() { };
        public static event PacketEventHandler RawPacketEntried = delegate(IEnumerable<PacketObject> packets) { };


        public static void Startup()
        {
            packets_ = CreatePacketContainer();

            /* パケットマネージャー初期化 */
            BasePacketManager = new PacketManager(true);
            BasePacketManager.PacketEntry += OnPacketEntry;
        }

        public static void Shutdown()
        {
        }

        public static void Poll()
        {
            AutoTimeStampManager.Poll();

            AutoPacketSaveManager.Poll();

            /* パケット処理 */
            PacketPoll();
        }

        private static void PacketPoll()
        {
            if (!enable_)return;
            if ((ar_load_ != null) && (!ar_load_.IsCompleted))return;
            if ((ar_save_ != null) && (!ar_save_.IsCompleted))return;

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

        public static IEnumerable<PacketObject> GetPackets()
        {
            return (packets_);
        }

        public static PacketContainer CreatePacketContainer()
        {
            return (new PacketContainer((ulong)ConfigManager.System.ApplicationCore.RawPacketCountLimit.Value));
        }

        public static void ClearPacket()
        {
            AutoTimeStampManager.ClearPacket();

            /* コンテナ初期化 */
            lock (packets_sync_) {
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
            if (packets == null)return;

            /* パケット一覧に追加 */
            packets_.AddRange(packets);

            /* 自動保存モジュールに転送 */
            AutoPacketSaveManager.Output(packets);

            /* 描画実行 */
            FormTaskManager.DrawPacketPush(packets);

            /* イベント通知 */
            RawPacketEntried(packets);
        }

        public static void SetSystemEvent(DateTime dt, string title, string text)
        {
            var packet = new PacketObject(
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

        public static void LoadPacketFile(IEnumerable<string> paths, PacketLogReader reader, FileFormatOption option)
        {
            if (paths == null)return;
            if (reader == null)return;

            if (IsLoadBusy)return;
            if (IsSaveBusy)return;

            /* パケットクリア */
            ClearPacket();

            /* 読込処理を開始 */
            ar_load_ = (new LoadPacketFilesTaskDelegate(LoadPacketFilesTask)).BeginInvoke(paths, reader, option, null, null);
        }

        private delegate void LoadPacketFilesTaskDelegate(IEnumerable<string> paths, PacketLogReader reader, FileFormatOption option);
        private static void LoadPacketFilesTask(IEnumerable<string> paths, PacketLogReader reader, FileFormatOption option)
        {
            var packets_new = CreatePacketContainer();

            foreach (var (path_value, path_index) in paths.Select((value, index) => (value, index))) {
                /* ステータステキストを更新 */
                FormUiManager.SetStatusText(
                    StatusTextID.SaveLoadEventFile,
                    String.Format(
                        "{0} {1} / {2}",
                        ConfigManager.Language.MainMessage.EventFileLoading.Value,
                        path_index + 1,
                        paths.Count()));
                
                /* プログレスバーを初期化 */
                FormUiManager.SetProgressBar(0, false);

                /* ファイルを1つずつ処理 */
                var task = (new LoadPacketFileExecTaskDelegate(LoadPacketFileExecTask)).BeginInvoke(
                                packets_new, path_value, reader, option, null, null);

                /* 完了待ち */
                while (!task.IsCompleted) {
                    System.Threading.Thread.Sleep(100);
                    FormUiManager.SetProgressBar((byte)(reader.ProgressNow / (Math.Max(reader.ProgressMax / 100, 1))), false);
                }
            }

            /* コンテナ差し替え */
            packets_ = packets_new;

            /* ステータスバーを終了 */
            FormUiManager.SetStatusText(StatusTextID.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileLoadComplete.Value);
            FormUiManager.SetProgressBar(100, true);

            /* 再描画 */
            FormTaskManager.RedrawPacketRequest();
        }

        private delegate void LoadPacketFileExecTaskDelegate(PacketContainer packets, string path, PacketLogReader reader, FileFormatOption option);
        private static void LoadPacketFileExecTask(PacketContainer packets, string path, PacketLogReader reader, FileFormatOption option)
        {
            /* ファイルオープン */
            if (!reader.Open(option, path)) {
                return;
            }

            var packet = (PacketObject)null;

            while ((packet = reader.ReadPacket()) != null) {
                packets.Add(packet);
            }

            reader.Close();
        }

        public static void SavePacketFile(string path, PacketLogWriter writer, FileFormatOption option, IEnumerable<PacketConverterInstance> pcvt_list)
        {
            if (IsLoadBusy)return;
            if (IsSaveBusy)return;

            /* タスク開始 */
            ar_save_ = (new SavePacketFileTaskDelegate(SavePacketFileTask)).BeginInvoke(path, writer, option, pcvt_list, null, null);
        }

        private delegate void SavePacketFileTaskDelegate(string path, PacketLogWriter writer, FileFormatOption option, IEnumerable<PacketConverterInstance> pcvt_list);
        private static void SavePacketFileTask(string path, PacketLogWriter writer, FileFormatOption option, IEnumerable<PacketConverterInstance> pcvt_list)
        {
            /* ファイルオープン */
            if (!writer.Open(option, path, false)) {
                return;
            }

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
                PacketConverterManager.InputStatusClear(pcvt_list);

                var task_packets = (IEnumerable<PacketObject>)null;

                foreach (var packet in packets_) {
                    /* ベースパケットをパケット変換 */
                    task_packets = PacketConverterManager.InputPacket(pcvt_list, packet);

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
                task_packets = PacketConverterManager.InputBreakOff(pcvt_list);
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
