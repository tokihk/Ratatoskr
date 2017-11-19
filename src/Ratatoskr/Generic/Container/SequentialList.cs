using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Container
{
    public sealed class SequentialList<Type>
        : IEnumerable<Type>
    {
        private class BlockL4
        {
            public List<Type>    Items { get; } = new List<Type>();
            public int           Count { get; set; } = 0;
        }

        private class BlockL3
        {
            public List<BlockL4> Items { get; } = new List<BlockL4>();
            public int           Count { get; set; } = 0;

            public BlockL3()
            {
                Items.Add(new BlockL4());
            }
        }

        private class BlockL2
        {
            public List<BlockL3> Items { get; } = new List<BlockL3>();
            public int           Count { get; set; } = 0;

            public BlockL2()
            {
                Items.Add(new BlockL3());
            }
        }

        private class BlockL1
        {
            public List<BlockL2> Items { get; } = new List<BlockL2>();
            public int           Count { get; set; } = 0;

            public BlockL1()
            {
                Items.Add(new BlockL2());
            }
        }

        private class BlockInfo
        {
            public BlockL1 BlockL1 = null;
            public BlockL2 BlockL2 = null;
            public BlockL3 BlockL3 = null;
            public BlockL4 BlockL4 = null;

            public int RelativeIndexL1 = 0;
            public int RelativeIndexL2 = 0;
            public int RelativeIndexL3 = 0;
            public int RelativeIndexL4 = 0;

            public int BlockIndexL1toL2 = 0;
            public int BlockIndexL2toL3 = 0;
            public int BlockIndexL3toL4 = 0;
            public int BlockIndexL4toTarget = 0;
        }

        private BlockL1 block_l1_ = new BlockL1();

        private int total_max_ = 0;
        private int block_max_ = 0;


        public SequentialList(int total_max = int.MaxValue)
        {
            total_max_ = Math.Max(total_max, 1);
            block_max_ = (int)(Math.Pow(total_max_, 1 / 4) + 1);

            Normalize();
        }

        public SequentialList(IEnumerable<Type> values, int total_max = int.MaxValue)
            : this(total_max)
        {
            AddRange(values);
        }

        public int Count
        {
            get { return (block_l1_.Count); }
        }

        private void Normalize()
        {
            if (block_l1_.Items.Count == 0) {
                block_l1_.Items.Add(new BlockL2());
            }

            var block_l2 = block_l1_.Items.First();

            if (block_l2.Items.Count == 0) {
                block_l2.Items.Add(new BlockL3());
            }

            var block_l3 = block_l2.Items.First();

            if (block_l3.Items.Count == 0) {
                block_l3.Items.Add(new BlockL4());
            }
        }

        private BlockInfo FindBlock(int index_abs)
        {
            /* 指定インデックスが範囲外のときは失敗 */
            if ((index_abs < 0) || (index_abs >= block_l1_.Count)) {
                return (null);
            }

            var block_info = new BlockInfo()
            {
                BlockL1 = block_l1_,
                RelativeIndexL1 = index_abs,
            };

            /* L1ブロックからL2ブロックを検索する */
            foreach (var block_l2 in block_l1_.Items.Select((v, i) => new {v, i})) {
                /* 該当L2ブロック検出までループ */
                if (index_abs >= block_l2.v.Count) {
                    index_abs -= block_l2.v.Count;
                    continue;
                }

                block_info.BlockIndexL1toL2 = block_l2.i;
                block_info.RelativeIndexL2 = index_abs;
                block_info.BlockL2 = block_l2.v;

                /* L2ブロックからL3ブロックを検索する */
                foreach (var block_l3 in block_l2.v.Items.Select((v, i) => new {v, i})) {
                    /* 該当L3ブロック検出までループ */
                    if (index_abs >= block_l3.v.Count) {
                        index_abs -= block_l3.v.Count;
                        continue;
                    }

                    block_info.BlockIndexL2toL3 = block_l3.i;
                    block_info.RelativeIndexL3 = index_abs;
                    block_info.BlockL3 = block_l3.v;

                    /* L3ブロックからL4ブロックを検索する */
                    foreach (var block_l4 in block_l3.v.Items.Select((v, i) => new {v, i})) {
                        /* 該当L4ブロック検出までループ */
                        if (index_abs >= block_l4.v.Count) {
                            index_abs -= block_l4.v.Count;
                            continue;
                        }

                        block_info.BlockIndexL3toL4 = block_l4.i;
                        block_info.BlockIndexL4toTarget = index_abs;
                        block_info.RelativeIndexL4 = index_abs;
                        block_info.BlockL4 = block_l4.v;

                        return (block_info);
                    }
                }
            }

            return (null);
        }

        public void Clear()
        {
            block_l1_ = new BlockL1();
        }

        public void RemoveRange(int index, int count)
        {
            var remove_count = 0;

            foreach (var block_l2 in block_l1_.Items) {
                if (index >= block_l2.Count) {
                    index -= block_l2.Count;
                }
            
                foreach (var block_l3 in block_l2.Items) {
                    if (index >= block_l3.Count) {
                        index -= block_l3.Count;
                    }

                    foreach (var block_l4 in block_l3.Items) {
                        if (index >= block_l4.Count) {
                            index -= block_l4.Count;
                        }

                        remove_count = Math.Min(count, block_l4.Count - index);
                        count -= remove_count;

                        /* 実際のアイテムを削除 */
                        block_l4.Items.RemoveRange(index, remove_count);

                        /* 全カレントブロックから削除したアイテム数を減算 */
                        block_l4.Count -= remove_count;
                        block_l3.Count -= remove_count;
                        block_l2.Count -= remove_count;
                        block_l1_.Count -= remove_count;

                        /* これ以降はリストの先頭が削除開始位置 */
                        index = 0;

                        /* 削除完了 */
                        if (count == 0)break;
                    }

                    /* 空のブロックを削除 */
                    block_l3.Items.RemoveAll(block_l4 => block_l4.Items.Count == 0);

                    /* 削除完了 */
                    if (count == 0)break;
                }

                /* 空のブロックを削除 */
                block_l2.Items.RemoveAll(block_l3 => block_l3.Items.Count == 0);

                /* 削除完了 */
                if (count == 0)break;
            }

            /* 空のブロックを削除 */
            block_l1_.Items.RemoveAll(block_l2 => block_l2.Items.Count == 0);

            Normalize();
        }

        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public void InsertRange(int index, IEnumerable<Type> values)
        {
            /* 最大データ数よりも大きいときは最後尾を優先としてシュリンク */
            if (values.Count() > total_max_) {
                values = values.Skip(values.Count() - total_max_);
            }

            /* 挿入後に最大データ数を超える場合は事前に削除 */
            if ((values.Count() + block_l1_.Count) > total_max_) {
                RemoveRange(0, values.Count() + block_l1_.Count - total_max_);
            }

            var insert_count = 0;

process_l1:
            foreach (var block_l2 in block_l1_.Items) {
                if (index >= block_l2.Count) {
                    index -= block_l2.Count;
                }

process_l2:
                foreach (var block_l3 in block_l2.Items) {
                    if (index >= block_l3.Count) {
                        index -= block_l3.Count;
                    }

process_l3:
                    foreach (var block_l4 in block_l3.Items) {
                        if (index >= block_l4.Count) {
                            index -= block_l4.Count;
                        }

                        insert_count = Math.Min(block_max_ - block_l4.Items.Count, values.Count());

                        /* 全カレントブロックに挿入数を加算 */
                        block_l4.Count += insert_count;
                        block_l3.Count += insert_count;
                        block_l2.Count += insert_count;
                        block_l1_.Count += insert_count;

                        /* アイテムを追加 */
                        block_l4.Items.InsertRange(Math.Min(block_l4.Items.Count, index), values.Take(insert_count));

                        /* 残りアイテムを更新 */
                        values = values.Skip(insert_count);

                        /* 挿入完了 */
                        if (values.Count() == 0)break;
                    }

                    /* 挿入完了 */
                    if (values.Count() == 0)break;

                    /* 挿入アイテムが残っているがこのブロックにはアイテムが追加できないのでスキップ */
                    if (block_l3.Items.Count >= block_max_)break;

                    /* アイテムを挿入して処理をやり直し */
                    block_l3.Items.Add(new BlockL4());
                    goto process_l3;
                }

                /* 挿入完了 */
                if (values.Count() == 0)break;

                /* 挿入アイテムが残っているがこのブロックにはアイテムが追加できないのでスキップ */
                if (block_l2.Items.Count >= block_max_)break;

                /* アイテムを挿入して処理をやり直し */
                block_l2.Items.Add(new BlockL3());
                goto process_l2;
            }

            /* 挿入完了 */
            if (values.Count() == 0)return;

            /* 挿入アイテムが残っているがこのブロックにはアイテムが追加できないのでスキップ */
            if (block_l1_.Items.Count >= block_max_)return;

            /* アイテムを挿入して処理をやり直し */
            block_l1_.Items.Add(new BlockL2());
            goto process_l1;
        }

        public void Insert(int index, Type value)
        {
            InsertRange(index, new Type[] { value });
        }

        public void Add(Type value)
        {
            Insert(int.MaxValue, value);
        }

        public void AddRange(IEnumerable<Type> values)
        {
            InsertRange(int.MaxValue, values);
        }

        public Type GetAt(int index)
        {
            foreach (var block_l2 in block_l1_.Items) {
                if (index >= block_l2.Count) {
                    index -= block_l2.Count;
                }
            
                foreach (var block_l3 in block_l2.Items) {
                    if (index >= block_l3.Count) {
                        index -= block_l3.Count;
                    }

                    foreach (var block_l4 in block_l3.Items) {
                        if (index >= block_l4.Count) {
                            index -= block_l4.Count;
                        }

                        return (block_l4.Items[index]);
                    }
                }
            }

            throw new IndexOutOfRangeException();
        }

        public void SetAt(int index, Type value)
        {
            foreach (var block_l2 in block_l1_.Items) {
                if (index >= block_l2.Count) {
                    index -= block_l2.Count;
                }
            
                foreach (var block_l3 in block_l2.Items) {
                    if (index >= block_l3.Count) {
                        index -= block_l3.Count;
                    }

                    foreach (var block_l4 in block_l3.Items) {
                        if (index >= block_l4.Count) {
                            index -= block_l4.Count;
                        }

                        block_l4.Items[index] = value;
                    }
                }
            }

            throw new IndexOutOfRangeException();
        }

        public Type[] ToArray()
        {
            var values = new Type[block_l1_.Count];
            var count = 0;

            foreach (var value in this) {
                values[count++] = value;
            }

            return (values);
        }

        public Type this[int index]
        {
            get { return (GetAt(index)); }
            set { SetAt(index, value);   }
        }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var block_l2 in block_l1_.Items) {
                foreach (var block_l3 in block_l2.Items) {
                    foreach (var block_l4 in block_l3.Items) {
                        foreach (var item in block_l4.Items) {
                            yield return (item);
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }
    }
}
