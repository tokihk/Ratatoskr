//#define USE_DOTNET_SORTEDLIST

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormats;

namespace Ratatoskr.Packet
{
    internal class PacketContainerLarge : IPacketContainer
    {
        private const long PACKET_COUNT_TOTAL_MAX = 999999999999;
        private const long PACKET_COUNT_TOTAL_MIN = 99999;

        private class Layer1 : IEnumerable<PacketObject>
        {
#if USE_DOTNET_SORTEDLIST
            private SortedList<DateTime, List<PacketObject>> packets_ = new SortedList<DateTime, List<PacketObject>>();
#else
            private List<KeyValuePair<DateTime, List<PacketObject>>> packets_ = new List<KeyValuePair<DateTime, List<PacketObject>>>();
#endif

            private int packet_count_ = 0;


            public int PacketCount
            {
                get { return (packet_count_); }
            }

            public DateTime FirstPacketTime
            {
                get { return ((packets_.Count > 0) ? (packets_.First().Key) : (DateTime.MinValue)); }
            }

            public DateTime LastPacketTime
            {
                get { return ((packets_.Count > 0) ? (packets_.Last().Key) : (DateTime.MinValue)); }
            }

#if !USE_DOTNET_SORTEDLIST
            private List<PacketObject> SearchInsertBlock(PacketObject packet)
            {
                var index_first  = 0;
                var index_last   = packets_.Count;
                var list_index = 0;
                var list_temp  = packets_.First();

                /* バイナリサーチで挿入位置にあたりを付ける */
                while (index_first < index_last) {
                    list_index = index_first + (index_last - index_first) / 2;
                    list_temp = packets_[list_index];

                    if (list_temp.Key < packet.MakeTime) {
                        index_first = list_index + 1;
                    } else {
                        index_last = list_index;
                    }
                }

                /* 検出リストの時間が一致しない場合は新規生成 */
                if (list_temp.Key != packet.MakeTime) {
                    list_temp = new KeyValuePair<DateTime, List<PacketObject>>(packet.MakeTime, new List<PacketObject>());
                    packets_.Insert(index_first, list_temp);
                } else {
                    list_temp = packets_[index_first];
                }

                return (list_temp.Value);
            }
#endif

            public void Add(PacketObject packet)
            {
#if USE_DOTNET_SORTEDLIST
                var packet_list = (List<PacketObject>)null;

                if (!packets_.TryGetValue(packet.MakeTime, out packet_list)) {
                    packet_list = new List<PacketObject>();
                    packets_.Add(packet.MakeTime, packet_list);
                }

                packet_list.Add(packet);
                packet_count_++;
#else
                var packet_list = (List<PacketObject>)null;

                /* 挿入ブロック取得 */
                if ((packets_.Count == 0) || (packet.MakeTime > packets_.Last().Key)) {
                    packet_list = new List<PacketObject>();
                    packets_.Add(new KeyValuePair<DateTime, List<PacketObject>>(packet.MakeTime, packet_list));
                } else if (packet.MakeTime < packets_.First().Key) {
                    packet_list = new List<PacketObject>();
                    packets_.Insert(0, new KeyValuePair<DateTime, List<PacketObject>>(packet.MakeTime, packet_list));
                } else {
                    packet_list = SearchInsertBlock(packet);
                }

                /* 挿入 */
                packet_list.Add(packet);
                packet_count_++;
#endif
            }

            public IEnumerator<PacketObject> GetEnumerator()
            {
                foreach (var packet_list in packets_) {
                    foreach (var packet in packet_list.Value) {
                        yield return (packet);
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (this.GetEnumerator());
            }
        }


        private List<Layer1> pages_ = new List<Layer1>();

        private long packet_count_max_ = 0;
        private long packet_count_     = 0;

        private int  packet_count_page_max_ = 0;

        private DateTime last_packet_time_ = DateTime.MinValue;


        public PacketContainerLarge(ulong packet_count_max)
        {
            packet_count_max_ = Math.Min((long)packet_count_max, PACKET_COUNT_TOTAL_MAX);
            packet_count_max_ = Math.Max(packet_count_max_, PACKET_COUNT_TOTAL_MIN);

            packet_count_page_max_ = (int)((packet_count_max_ + 999) / 1000);
        }

        public void Dispose()
        {
            pages_ = null;

            GC.Collect();
        }

        public ulong Count
        {
            get { return ((ulong)packet_count_); }
        }

        public void Clear()
        {
            pages_ = new List<Layer1>();

            packet_count_ = 0;

            GC.Collect();
        }

        public void Add(PacketObject packet)
        {
            if (packet == null)return;

            /* === 挿入先ページを取得 === */
            var page = (Layer1)null;

            if (pages_.Count == 0) {
                /* ページが存在しないときは新規ページに追加 */
                page = new Layer1();
                pages_.Add(page);

            } else if (packet.MakeTime >= last_packet_time_) {
                /* 最後のパケットよりも遅いときは最後のページに追加 */
                page = pages_.Last();

                /* 1ページあたりの最大パケット数に達しているときは新規ページに追加 */
                if (page.PacketCount >= packet_count_page_max_) {
                    page = new Layer1();
                    pages_.Add(page);
                }

            } else if (packet.MakeTime >= pages_.Last().FirstPacketTime) {
                /* 最後のページの範囲なら最後のページに追加する */
                page = pages_.Last();

            } else {
                /* 挿入先ページを検索する */
                page = pages_.First();
                foreach (var page_tmp in pages_) {
                    if (packet.MakeTime < page_tmp.FirstPacketTime)break;
                    page = page_tmp;
                }
            }

            page.Add(packet);

            packet_count_++;

            last_packet_time_ = pages_.Last().LastPacketTime;

            /* パケット最大量をオーバーした場合は最初のページを削除 */
            if (packet_count_ > packet_count_max_) {
                if (pages_.Count > 0) {
                    page = pages_.First();

                    packet_count_ -= page.PacketCount;

                    pages_.RemoveAt(0);
                }
            }
        }

        public void AddRange(IEnumerable<PacketObject> packets)
        {
            if (packets == null)return;

            /* 挿入パケットを時系列でソート */
            packets = packets.OrderBy(item => item.MakeTime);

            foreach (var packet in packets) {
                Add(packet);
            }
        }

        public IEnumerator<PacketObject> GetEnumerator()
        {
            foreach (var page in pages_) {
                foreach (var packet in page) {
                    yield return (packet);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }
    }
}
