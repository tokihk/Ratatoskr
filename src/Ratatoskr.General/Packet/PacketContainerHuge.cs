using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormats;

namespace Ratatoskr.General.Packet
{
    internal class PacketContainerHuge : IPacketContainer
    {
        private const long PACKET_COUNT_TOTAL_MAX = 999999999999;
        private const int  PACKET_COUNT_PAGE_MAX  = 99999;         // 1ページ毎のパケット最大数
//        private const int  PACKET_COUNT_PAGE_MAX  = 20;          // 1ページ毎のパケット最大数
        private const int  STATIC_PAGE_NUMBER_FRONT = 100;         // 常にロードしておくページ数
        private const int  STATIC_PAGE_NUMBER_BACK  = 100;         // 常にロードしておくページ数


        private class PacketComparer : IComparer<PacketObject>
        {
            public int Compare(PacketObject packet_1, PacketObject packet_2)
            {
                return (packet_1.MakeTime.CompareTo(packet_2.MakeTime));
            }
        }

        private static readonly PacketComparer  PacketComparerObject = new PacketComparer();
        private static readonly FileFormatClass PageFileFormat = new FileFormats.PacketLog_Rtcap.FileFormatClassImpl();

        private class Page : IDisposable
        {
            private string page_file_path_;

            private object             page_sync_ = new object();
            private bool               page_load_state_ = true;
            private bool               page_update_state_ = false;

            private List<PacketObject> page_packets_ = new List<PacketObject>();
            private int                page_packet_count_ = 0;
            private DateTime           last_packet_time_ = DateTime.MinValue;

            private IAsyncResult ar_page_in_task_ = null;
            private IAsyncResult ar_page_out_task_ = null;


            public Page(string file_path)
            {
                page_file_path_ = file_path;
            }

            public void Dispose()
            {
                try {
                    if (File.Exists(page_file_path_)) {
                        File.Delete(page_file_path_);
                    }
                } catch {
                }
            }

            public bool PageFileAccessBusy
            {
                get
                {
                    return (   ((ar_page_in_task_ != null) && (!ar_page_in_task_.IsCompleted))
                            || ((ar_page_out_task_ != null) && (!ar_page_out_task_.IsCompleted)));
                }
            }

            public IEnumerable<PacketObject> Packets
            {
                get
                {
                    PageIn();

                    return (page_packets_);
                }
            }

            public int PacketCount
            {
                get { return (page_packet_count_); }
            }

            public DateTime LastPacketTime
            {
                get { return (last_packet_time_); }
            }

            public void SortAdd(IEnumerable<PacketObject> packets)
            {
                PageIn();

                lock (page_sync_) {
                    foreach (var packet in packets) {
                        SortAddCore(packet);
                    }
                }
            }

            public void SortAdd(PacketObject packet)
            {
                PageIn();

                lock (page_sync_) {
                    SortAddCore(packet);
                }
            }

            private void SortAddCore(PacketObject packet)
            {
                if (   (page_packets_.Count == 0)
                    || (packet.MakeTime >= last_packet_time_)
                ) {
                    page_packets_.Add(packet);

                } else if (packet.MakeTime <= page_packets_.First().MakeTime) {
                    page_packets_.Insert(0, packet);

                } else {
                    var index = Math.Abs(page_packets_.BinarySearch(packet, PacketComparerObject));

                    page_packets_.Insert(index, packet);
                }

                last_packet_time_ = page_packets_.Last().MakeTime;

                page_packet_count_++;

                page_update_state_ = true;
            }

            public int Remove(int index, int count)
            {
                PageIn();

                lock (page_sync_) {
                    count = Math.Min(page_packets_.Count - index, count);

                    if (count > 0) {
                        page_packets_.RemoveRange(index, count);
                        page_packet_count_ -= count;
                    }
                }

                return (count);
            }

            public void PageIn()
            {
                PageInAsync();

                while (PageFileAccessBusy) {
                    System.Threading.Thread.Sleep(1);
                }
            }

            public void PageInAsync()
            {
                do {
                    lock (page_sync_) {
                        if (page_load_state_)return;

                        if ((ar_page_in_task_ != null) && (!ar_page_in_task_.IsCompleted))return;

                        if ((ar_page_out_task_ != null) && (!ar_page_out_task_.IsCompleted))continue;

                        ar_page_in_task_ = (new PageInTaskHandler(PageInTask)).BeginInvoke(null, null);
                    }
                } while (false);
            }

            private delegate void PageInTaskHandler();
            private void PageInTask()
            {
                try {
                    var packets = new List<PacketObject>();

                    if (File.Exists(page_file_path_)) {
                        var reader = PageFileFormat.CreateReader() as PacketLogReader;
                        var option = PageFileFormat.CreateReaderOption();

                        if (   (reader != null)
                            && (reader.Open(option, page_file_path_))
                        ) {
                            var packet = (PacketObject)null;

                            while ((packet = reader.ReadPacket()) != null) {
                                packets.Add(packet);
                            }

                            reader.Close();
                        }
                    }

                    /* 新しいパケットと差し替え */
                    lock (page_sync_) {
                        page_packets_ = packets;
                        page_load_state_ = true;
                    }

                } catch {
                }
            }

            public void PageOut()
            {
                PageOutAsync();

                while (PageFileAccessBusy) {
                    System.Threading.Thread.Sleep(1);
                }
            }

            public void PageOutAsync()
            {
                lock (page_sync_) {
                    if (PageFileAccessBusy)return;

                    if (!page_load_state_)return;
                    if (!page_update_state_)return;

                    ar_page_out_task_ = (new PageOutTaskHandler(PageOutTask)).BeginInvoke(null, null);
                }
            }

            private delegate void PageOutTaskHandler();
            private void PageOutTask()
            {
                try {
                    var writer = PageFileFormat.CreateWriter() as PacketLogWriter;
                    var option = PageFileFormat.CreateWriterOption();

                    if (writer == null)return;
                    if (!writer.Open(option, page_file_path_))return;

                    foreach (var packet in page_packets_) {
                        writer.WritePacket(packet);
                    }

                    writer.Close();

                    /* ページアウトしたらメモリをクリア */
                    lock (page_sync_) {
                        page_packets_ = null;
                        page_load_state_ = false;

                        GC.Collect();
                    }

                } catch {
                }
            }
        }


        private string     page_file_dir_ = null;
        private List<Page> pages_ = new List<Page>();
        private int        page_file_no_ = 1;

        private long packet_count_max_ = 0;
        private long packet_count_     = 0;

        private DateTime last_packet_time_ = DateTime.MinValue;

        private object page_sync_ = new object();


        public PacketContainerHuge(string page_file_dir, ulong packet_count_max)
        {
            page_file_dir_ = page_file_dir;

            /* for Debug */
//            packet_count_max = 99999999;
            packet_count_max = 9999999;

            packet_count_max_ = Math.Min((long)packet_count_max, PACKET_COUNT_TOTAL_MAX);
            packet_count_max_ = Math.Max(packet_count_max_, PACKET_COUNT_PAGE_MAX * 2);
        }

        public void Dispose()
        {
            pages_ = null;

            /* ページファイルを全て削除 */
            if (Directory.Exists(page_file_dir_)) {
                Native.Shell.rm(page_file_dir_);
            }

            GC.Collect();
        }

        public ulong Count
        {
            get { return ((ulong)packet_count_); }
        }

        private string GetNewPageFileName()
        {
            var page_file_path = string.Format("{0}\\{1:000000000}", page_file_dir_, page_file_no_);

            page_file_no_++;
            if (page_file_no_ > PACKET_COUNT_PAGE_MAX) {
                page_file_no_ = 1;
            }

            return (page_file_path);
        }

        private bool PageOutCheck(int page_no)
        {
            if (page_no < STATIC_PAGE_NUMBER_FRONT)return (false);

            if (pages_.Count < STATIC_PAGE_NUMBER_BACK)return (false);

            if (page_no >= pages_.Count - STATIC_PAGE_NUMBER_FRONT)return (false);

            return (true);
        }

        private void PageOut()
        {
            var page_index = 0;

            foreach (var page in pages_) {
                if (PageOutCheck(page_index)) {
                    page.PageOutAsync();
                }

                page_index++;
            }
        }

        public void Clear()
        {
            var pages = pages_;

            pages_ = new List<Page>();

            (new ClearTaskHandler(ClearTask)).BeginInvoke(pages, null, null);
        }

        private delegate void ClearTaskHandler(List<Page> pages);
        private void ClearTask(List<Page> pages)
        {
            foreach (var page in pages) {
                page.Dispose();
            }
        }

        public void Add(PacketObject packet)
        {
            if (packet == null)return;

            lock (page_sync_) {
                AddCore(packet);
            }
        }

        public void AddRange(IEnumerable<PacketObject> packets)
        {
            if (packets == null)return;

            /* 挿入パケットを時系列でソート */
            packets = packets.OrderBy(item => item.MakeTime);

            lock (page_sync_) {
                foreach (var packet in packets) {
                    AddCore(packet);
                }
            }
        }

        private void AddCore(PacketObject packet)
        {
            /* === 挿入先ページを取得 === */
            var page = (Page)null;

            if (pages_.Count == 0) {
                /* ページが存在しないときは新規ページに追加 */
                page = new Page(GetNewPageFileName());
                pages_.Add(page);

            } else if ((pages_.Count == 1) || (packet.MakeTime >= pages_.Last().LastPacketTime)) {
                /* ページが1つしか存在しないとき or 最後のパケットよりも遅いパケット は最後のページに追加 */
                page = pages_.Last();

                /* 1ページあたりの最大パケット数に達しているときは新規ページに追加 */
                if (page.PacketCount >= PACKET_COUNT_PAGE_MAX) {
                    page = new Page(GetNewPageFileName());
                    pages_.Add(page);

                    PageOut();
                }
            } else {
                /* 挿入先ページを検索する */
                page = pages_.First();
                foreach (var page_tmp in pages_) {
                    if (packet.MakeTime > page_tmp.LastPacketTime)continue;
                    page = page_tmp;
                }
            }

            page.SortAdd(packet);

            packet_count_++;

            /* パケット最大量をオーバーした場合は最初のページを削除 */
            if (packet_count_ > packet_count_max_) {
                Debugger.DebugManager.MessageOut("Remove start");
                if (pages_.Count > 0) {
                    page = pages_.First();

                    packet_count_ -= page.PacketCount;
                    page.Dispose();

                    pages_.RemoveAt(0);
                }
                Debugger.DebugManager.MessageOut("Remove stop");
            }
        }

        private void RemoveCore(ulong index, ulong count)
        {
            var page_index = -1;
            var remove_count = 0;
            var remove_page_index = -1;
            var remove_page_count = 0;

            count = Math.Min((ulong)packet_count_ - index, count);

            if (count < 0)return;

            foreach (var page in pages_) {
                page_index++;

                /* 消すデータがなくなったら終了 */
                if (count == 0)break;

                /* 消去開始ページが見つかるまで繰り返し */
                if (index >= (ulong)page.PacketCount) {
                    index -= (ulong)page.PacketCount;
                    continue;
                }

                /* 消去パケット数取得 */
                remove_count = (int)Math.Min(count, (ulong)page.PacketCount - index);

                if (remove_count < page.PacketCount) {
                    /* 部分削除 */
                    page.Remove((int)index, remove_count);

                } else {
                    /* ページ全体を削除 */
                    page.Dispose();

                    /* 削除ページ情報を更新 */
                    if (remove_page_index < 0) {
                        remove_page_index = page_index;
                    }
                    remove_page_count++;
                }

                packet_count_ -= remove_count;
            }

            /* 削除ページをリストから削除 */
            if (remove_page_count > 0) {
                pages_.RemoveRange(remove_page_index, remove_page_count);
            }
        }

        public IEnumerator<PacketObject> GetEnumerator()
        {
            /* 読込タスク開始 */
            (new GetEnumeratorTaskHandler(GetEnumeratorTask)).BeginInvoke(null, null);

            var page_index = 0;

            foreach (var page in pages_) {
                foreach (var packet in page.Packets) {
                    yield return (packet);
                }

                /* 閲覧が終わったページは解放 */
                if (PageOutCheck(page_index)) {
                    page.PageOutAsync();
                }

                page_index++;
            }
        }

        private delegate void GetEnumeratorTaskHandler();
        private void GetEnumeratorTask()
        {
            foreach (var page in pages_) {
                page.PageIn();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }
    }
}
