using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Container
{
    public sealed class SkipList<Type>
        : IEnumerable<Type>
    {
        private static readonly int OPTIMIZE_COUNT = 1000;


        private List<List<Type>> node_list_ = new List<List<Type>>();

        private int count_ = 0;

        private int node_item_max_ = 1000;

        private int optimize_count_ = 0;


        public SkipList()
        {
        }

        public SkipList(IEnumerable<Type> values)
        {
            AddRange(values);
        }

        public int Count
        {
            get { return (count_); }
        }

        public int BlockCount
        {
            get { return (node_list_.Count); }
        }

        public Type GetAt(int index)
        {
            foreach (var node in node_list_.Select((v,i) => new {v,i})) {
                if (index < node.v.Count) {
                /* --- 該当ページヒット --- */
                    return (node.v[index]);

                } else {
                /* --- 検索継続 --- */
                    index -= node.v.Count;
                }
            }

            throw new IndexOutOfRangeException();
        }

        public void SetAt(int index, Type value)
        {
            foreach (var node in node_list_.Select((v,i) => new {v,i})) {
                if (index < node.v.Count) {
                /* --- 該当ページヒット --- */
                    node.v[index] = value;
                    return;
                } else {
                /* --- 検索継続 --- */
                    index -= node.v.Count;
                }
            }

            throw new IndexOutOfRangeException();
        }

        public Type[] ToArray()
        {
            var values = new Type[count_];
            var count = 0;

            foreach (var value in this) {
                values[count++] = value;
            }

            return (values);
        }

        public Type[][] ToBlockArray()
        {
            var values = new List<Type[]>();

            foreach (var node in node_list_) {
                values.Add(node.ToArray());
            }

            return (values.ToArray());
        }

        public Type this[int index]
        {
            get { return (GetAt(index)); }
            set { SetAt(index, value);   }
        }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var node in node_list_) {
                foreach (var value in node) {
                    yield return (value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }

        public void Insert(int index, Type value)
        {
            if (index < 0)return;

            /* === 挿入先ノードを取得 === */
            var node_i = (List<Type>)null;

            if (index < count_) {
            /* --- アイテム数よりインデックス値が小さい場合は既存ノードから検索 --- */
                foreach (var node in node_list_) {
                    if (index < node.Count) {
                        node_i = node;
                        break;
                    }

                    index -= node.Count;
                }
            }

            /* --- ノードが見つからない場合は最後のノードを取得 --- */
            if ((node_i == null) && (node_list_.Count > 0)) {
                node_i = node_list_.Last();
                if (node_i.Count >= node_item_max_) {
                /* --- ノードのアイテム数が最大値に達した時はキャンセル --- */
                    node_i = null;
                }
            }

            /* --- ノードが見つからない場合はノードを新規作成 --- */
            if (node_i == null) {
                node_i = new List<Type>();
                node_list_.Add(node_i);
                index = 0;
            }

            /* === ノードにアイテムを追加 === */
            if (index <= node_i.Count) {
                node_i.Insert(index, value);
            } else {
                node_i.Add(value);
            }
            count_++;

            /* === 最適化 === */
            AutoOptimize();
        }

        public void InsertRange(int index, IEnumerable<Type> values)
        {
            foreach (var value in values) {
                Insert(index++, value);
            }
        }

        public void Add(Type value)
        {
            Insert(int.MaxValue, value);
        }

        public void AddRange(IEnumerable<Type> values)
        {
            foreach (var value in values) {
                Add(value);
            }
        }

        public void Replace(int index, IEnumerable<Type> values)
        {
            var copy_size = 0;

            foreach (var node in node_list_.Select((v,i) => new {v,i})) {
                if (values.Count() == 0)break;
                
                if (index < node.v.Count) {
                /* --- 該当ページヒット --- */
                    copy_size = Math.Min(values.Count(), node.v.Count - index);

                    /* --- コピー --- */
                    node.v.RemoveRange(0, copy_size);
                    node.v.InsertRange(0, values.Take(copy_size));

                    values = values.Skip(copy_size);
                    index = 0;

                } else {
                /* --- 検索継続 --- */
                    index -= node.v.Count;
                }
            }
        }

        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public void RemoveAt(IEnumerable<int> indices)
        {
            /* --- インデックスリストを降順に並べ替える --- */
            indices = indices.OrderByDescending(index => index);

            /* --- 削除する --- */
            foreach (var index in indices) {
                RemoveAt(index);
            }
        }

        public void RemoveRange(int index, int count)
        {
            if (index >= count_)return;

            /* === 削除 === */
            var remove_size = 0;

            foreach (var node in node_list_) {
                if (count == 0)break;

                if (index < node.Count) {
                    /* --- ノード内の削除アイテム数を取得 --- */
                    remove_size = Math.Min(node.Count - index, count);

                    /* --- ノード内のアイテムを削除 --- */
                    node.RemoveRange(index, remove_size);

                    count -= remove_size;
                    count_ -= remove_size;
                    index = 0;
                } else {
                    index -= node.Count;
                }
            }

            /* === 最適化 === */
            AutoOptimize();
        }

        public void Clear()
        {
            node_list_ = new List<List<Type>>();
            count_ = 0;
            optimize_count_ = 0;
        }

        public Type ItemAt(int index)
        {
            index = Math.Min(index, count_ - 1);

            var node_i = (List<Type>)null;

            foreach (var node in node_list_) {
                if (index < node.Count) {
                    node_i = node;
                    break;
                }

                index -= node.Count;
            }

            return (node_i[index]);
        }

        private void Optimize()
        {
            var item_seeds_ = (IEnumerable<Type>)null;
            var over_size = 0;

            /* === ノードのアイテム最大数で詰めていく === */
            foreach (var node in node_list_) {
                /* --- 前のノードから溢れたアイテムがある場合は先頭から挿入 --- */
                if (item_seeds_ != null) {
                    node.InsertRange(0, item_seeds_);
                    item_seeds_ = null;
                }

                /* === ノードのアイテム最大数と一致しないノードを最適化 === */
                if (node.Count > node_item_max_) {
                    /* --- 溢れたアイテムをノードから削除して次のノードへ追加 --- */
                    over_size = node.Count - node_item_max_;
                    item_seeds_ = node.GetRange(node_item_max_, over_size);
                    node.RemoveRange(node_item_max_, over_size);

                } else if (node.Count < node_item_max_) {
                    /* --- ノードのアイテム最大数に満たない場合は次のノードへ強制的に追加 --- */
                    item_seeds_ = node.GetRange(0, node.Count);
                    node.Clear();
                }
            }

            /* === 空ノードを削除 === */
            node_list_.RemoveAll(node => node.Count == 0);

            /* === 溢れたアイテムがある場合は新規ノードで追加する === */
            if (item_seeds_ != null) {
                while (item_seeds_.Count() > 0) {
                    over_size = Math.Min(item_seeds_.Count(), node_item_max_);
                    node_list_.Add(new List<Type>(item_seeds_.Take(over_size)));
                    item_seeds_ = item_seeds_.Skip(over_size);
                }
            }
        }

        private void AutoOptimize()
        {
            optimize_count_++;
            if (optimize_count_ >= OPTIMIZE_COUNT) {
                optimize_count_ = 0;
                Optimize();
            }
        }
    }
}
