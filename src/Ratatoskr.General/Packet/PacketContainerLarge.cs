using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General.Packet
{
    public class PacketContainerLarge
        : IPacketContainer
    {
        private const ulong PACKET_COUNT_TOTAL_MAX = 999999999999;
        private const ulong PACKET_COUNT_TOTAL_MIN = 99999;
//        private const int   PACKET_COUNT_LAYER_MAX = 1024;
        private const int   PACKET_COUNT_LAYER_MAX = 512;


        private abstract class LayerBase : IEnumerable<PacketObject>
        {
            public abstract int   LayerCount  { get; }
            public abstract ulong PacketCount { get; }

            public abstract DateTime FirstPacketTime { get; }
            public abstract DateTime LastPacketTime  { get; }

            public abstract void RemoveLayerAt(int index);
            public abstract void Insert(PacketObject packet);

            public abstract IEnumerator<PacketObject> GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (GetEnumerator());
            }
        }

        private class LastLayer : LayerBase
        {
            private List<KeyValuePair<DateTime, List<PacketObject>>> packets_ = new List<KeyValuePair<DateTime, List<PacketObject>>>();
            private ulong packet_count_ = 0;


            public override int LayerCount
            {
                get { return (packets_.Count); }
            }

            public override ulong PacketCount
            {
                get { return (packet_count_); }
            }

            public override DateTime FirstPacketTime
            {
                get { return ((packets_.Count > 0) ? (packets_.First().Key) : (DateTime.MinValue)); }
            }

            public override DateTime LastPacketTime
            {
                get { return ((packets_.Count > 0) ? (packets_.Last().Key) : (DateTime.MinValue)); }
            }

            public override void RemoveLayerAt(int index)
            {
                if (index < packets_.Count) {
                    var block = packets_[index];

                    packets_.RemoveAt(index);

                    packet_count_ -= (ulong)block.Value.Count;
                }
            }

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

            public override void Insert(PacketObject packet)
            {
                var packet_list = (List<PacketObject>)null;

                /* 挿入ブロック取得 */
                if ((packets_.Count == 0) || (packet.MakeTime > packets_.Last().Key)) {
                    packet_list = new List<PacketObject>();
                    packets_.Add(new KeyValuePair<DateTime, List<PacketObject>>(packet.MakeTime, packet_list));

                } else if (packet.MakeTime < packets_.First().Key) {
                    packet_list = new List<PacketObject>();
                    packets_.Insert(0, new KeyValuePair<DateTime, List<PacketObject>>(packet.MakeTime, packet_list));

                } else if (packet.MakeTime == packets_.Last().Key) {
                    packet_list = packets_.Last().Value;

                } else if (packet.MakeTime == packets_.First().Key) {
                    packet_list = packets_.First().Value;

                } else {
                    packet_list = SearchInsertBlock(packet);
                }

                /* 挿入 */
                packet_list.Add(packet);

                packet_count_++;
            }

            public override IEnumerator<PacketObject> GetEnumerator()
            {
                foreach (var packet_list in packets_) {
                    foreach (var packet in packet_list.Value) {
                        yield return (packet);
                    }
                }
            }
        }

        private abstract class LayerMiddle<LayerT> : LayerBase
            where LayerT : LayerBase, new()
        {
            private List<LayerT>        layers_ = new List<LayerT>();
            private ulong               packet_count_ = 0;


            protected IEnumerable<LayerT> Layers
            {
                get { return (layers_); }
            }

            public override int LayerCount
            {
                get { return (layers_.Count); }
            }

            public override ulong PacketCount
            {
                get { return (packet_count_); }
            }

            public override DateTime FirstPacketTime
            {
                get { return ((layers_.Count > 0) ? (layers_.Last().FirstPacketTime) : (DateTime.MinValue)); }
            }

            public override DateTime LastPacketTime
            {
                get { return ((layers_.Count > 0) ? (layers_.Last().LastPacketTime) : (DateTime.MinValue)); }
            }

            public override void RemoveLayerAt(int index)
            {
                if (index < layers_.Count) {
                    var layer = layers_[index];

                    layers_.RemoveAt(index);

                    packet_count_ -= layer.PacketCount;
                }
            }

            private LayerT SearchInsertLayer(PacketObject packet)
            {
                var index_first  = 0;
                var index_last   = layers_.Count;
                var list_index = 0;
                var list_temp  = layers_.First();

                /* バイナリサーチで挿入レイヤーにあたりを付ける */
                while (index_first < index_last) {
                    list_index = index_first + (index_last - index_first) / 2;
                    list_temp = layers_[list_index];

                    if (list_temp.LastPacketTime < packet.MakeTime) {
                        index_first = list_index + 1;
                    } else {
                        index_last = list_index;
                    }
                }

                /* シーケンシャルサーチで挿入レイヤーを特定する
                   バイナリサーチで1つずれることがあるので1つ下げる */
                index_first = Math.Max(index_first - 1, 0);

                while (index_first < layers_.Count) {
                    if (packet.MakeTime <= layers_[index_first].LastPacketTime)break;
                    index_first++;
                }

                if (index_first >= layers_.Count) {
                    /* 挿入レイヤーが見つからなかった場合は新規生成 */
                    list_temp = new LayerT();
                    layers_.Add(list_temp);

                } else {
                    list_temp = layers_[index_first];
                }

                return (list_temp);
            }

            public override void Insert(PacketObject packet)
            {
                var layer = (LayerT)null;

                /* 挿入レイヤー取得 */
                if (layers_.Count == 0) {
                    layers_.Add(new LayerT());
                }

                if (packet.MakeTime >= layers_.Last().LastPacketTime) {
                    layer = layers_.Last();

                    if (layer.LayerCount >= PACKET_COUNT_LAYER_MAX) {
                        layer = new LayerT();
                        layers_.Add(layer);
                    }

                } else if (packet.MakeTime <= layers_.First().FirstPacketTime) {
                    layer = layers_.First();

                    if (layer.LayerCount >= PACKET_COUNT_LAYER_MAX) {
                        layer = new LayerT();
                        layers_.Insert(0, layer);
                    }

                } else {
                    layer = SearchInsertLayer(packet);
                }

                /* 挿入 */
                layer.Insert(packet);

                packet_count_++;
            }

            public override IEnumerator<PacketObject> GetEnumerator()
            {
                foreach (var layer in Layers) {
                    foreach (var packet in layer) {
                        yield return (packet);
                    }
                }
            }
        }

        private class Layer1 : LayerMiddle<LastLayer> { }
        private class Layer2 : LayerMiddle<Layer1> { }
        private class Layer3 : LayerMiddle<Layer2> { }


        private LayerBase first_layer_ = new Layer3();

        private ulong packet_count_max_ = 0;


        public PacketContainerLarge(ulong packet_count_max)
        {
            packet_count_max_ = Math.Min(packet_count_max, PACKET_COUNT_TOTAL_MAX);
            packet_count_max_ = Math.Max(packet_count_max_, PACKET_COUNT_TOTAL_MIN);

            first_layer_ = CreateFirstLayer();
        }

        public void Dispose()
        {
            first_layer_ = null;

            GC.Collect();
        }

        private LayerBase CreateFirstLayer()
        {
            if (packet_count_max_ < PACKET_COUNT_LAYER_MAX) {
                return (new LastLayer());
            } else if (packet_count_max_ < Math.Pow(PACKET_COUNT_LAYER_MAX, 2)) {
                return (new Layer1());
            } else if (packet_count_max_ < Math.Pow(PACKET_COUNT_LAYER_MAX, 3)) {
                return (new Layer2());
            } else {
                return (new Layer3());
            }
        }

        public ulong Count
        {
            get { return ((ulong)first_layer_.PacketCount); }
        }

        public void Clear()
        {
            first_layer_ = CreateFirstLayer();

            GC.Collect();
        }

        public void Add(PacketObject packet)
        {
            if (packet == null)return;

            /* 追加後にパケット最大数を超える場合は最初のレイヤーを削除 */
            if (first_layer_.PacketCount >= packet_count_max_) {
                first_layer_.RemoveLayerAt(0);
            }

            first_layer_.Insert(packet);
        }

        public void AddRange(IEnumerable<PacketObject> packets)
        {
            if (packets == null)return;

            /* 挿入パケットを時系列でソート */
//            packets = packets.OrderBy(item => item.MakeTime);

            foreach (var packet in packets) {
                Add(packet);
            }
        }

        public IEnumerator<PacketObject> GetEnumerator()
        {
            foreach (var packet in first_layer_) {
                yield return (packet);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }
    }
}
