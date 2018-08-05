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


        private class PacketComparer : IComparer<PacketObject>
        {
            public int Compare(PacketObject packet_1, PacketObject packet_2)
            {
                return (packet_1.MakeTime.CompareTo(packet_2.MakeTime));
            }
        }

        private static readonly PacketComparer  PacketComparerObject = new PacketComparer();


        private class Page
        {
            private List<PacketObject> packets_ = new List<PacketObject>();

            private DateTime           first_packet_time_ = DateTime.MinValue;
            private DateTime           last_packet_time_ = DateTime.MinValue;

            public IEnumerable<PacketObject> Packets
            {
                get
                {
                    return (packets_);
                }
            }

            public int PacketCount
            {
                get { return (packets_.Count); }
            }

            public DateTime FirstPacketTime
            {
                get { return ((packets_.Count > 0) ? (packets_.First().MakeTime) : (DateTime.MinValue)); }
            }

            public DateTime LastPacketTime
            {
                get { return ((packets_.Count > 0) ? (packets_.Last().MakeTime) : (DateTime.MinValue)); }
            }

            public void Add(PacketObject packet)
            {
                if (   (packets_.Count == 0)
                    || (packet.MakeTime >= packets_.Last().MakeTime)
                ) {
                    packets_.Add(packet);

                } else if (packet.MakeTime < packets_.First().MakeTime) {
                    packets_.Insert(0, packet);

                } else {
                    var index = Math.Abs(packets_.BinarySearch(packet, PacketComparerObject));

                    /* 同一時間のパケットがある場合は境界までシフト */
                    while (index < packets_.Count) {
                        if (packet.MakeTime != packets_[index].MakeTime)break;
                        index++;
                    }

                    if (index < packets_.Count) {
                        packets_.Insert(index, packet);
                    } else {
                        packets_.Add(packet);
                    }
                }

                last_packet_time_ = packets_.Last().MakeTime;
            }
        }


        private List<Page> pages_ = new List<Page>();

        private long packet_count_max_ = 0;
        private long packet_count_     = 0;

        private int  packet_count_page_max_ = 0;

        private DateTime last_packet_time_ = DateTime.MinValue;


        public PacketContainerLarge(ulong packet_count_max)
        {
            packet_count_max_ = Math.Min((long)packet_count_max, PACKET_COUNT_TOTAL_MAX);
            packet_count_max_ = Math.Max(packet_count_max_, PACKET_COUNT_TOTAL_MIN);

            packet_count_page_max_ = (int)((packet_count_max_ + 9) / 10);
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
            pages_ = new List<Page>();

            packet_count_ = 0;

            GC.Collect();
        }

        public void Add(PacketObject packet)
        {
            if (packet == null)return;

            /* === 挿入先ページを取得 === */
            var page = (Page)null;

            if (pages_.Count == 0) {
                /* ページが存在しないときは新規ページに追加 */
                page = new Page();
                pages_.Add(page);

            } else if (packet.MakeTime >= last_packet_time_) {
                /* 最後のパケットよりも遅いときは最後のページに追加 */
                page = pages_.Last();

                /* 1ページあたりの最大パケット数に達しているときは新規ページに追加 */
                if (page.PacketCount >= packet_count_page_max_) {
                    page = new Page();
                    pages_.Add(page);
                }

            } else if ((packet.MakeTime >= pages_.Last().FirstPacketTime) && (packet.MakeTime <= pages_.Last().FirstPacketTime)) {
                /* 最後のページの範囲なら最後のページに追加する */
                page = pages_.Last();

            } else {
                /* 挿入先ページを検索する */
                page = pages_.First();
                foreach (var page_tmp in pages_) {
                    if (packet.MakeTime > page_tmp.LastPacketTime)continue;
                    page = page_tmp;
                }
            }

            page.Add(packet);

            packet_count_++;

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
                foreach (var packet in page.Packets) {
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
