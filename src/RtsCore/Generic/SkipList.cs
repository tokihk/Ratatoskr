using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Generic
{
    public class SkipList<Type>
        : IEnumerable<Type>
    {
        private const int   GC_COLLECT_STEP = 20;

        private const ulong TOTAL_ITEM_LIMIT = 0x7FFFFFFFFFFFFFFFUL;
        private const int   BLOCK_ITEM_LIMIT = 0xFFFF;


        private class BlockL2 : IEnumerable<Type>
        {
            private List<Type>    items_ = new List<Type>();
            private Type[]        items_cache_ = null;
            private bool          items_cache_lock_ = false;


            public BlockL2()
            {
            }

            public int Count
            {
                get { return (items_.Count); }
            }

            private void CacheUpate()
            {
                if (items_cache_lock_)return;

                items_cache_ = items_.ToArray();

                items_cache_lock_ = true;
            }

            public void Clear()
            {
                items_ = new List<Type>();
                items_cache_lock_ = false;
            }

            public void RemoveAt(int index)
            {
                items_.RemoveAt(index);
                items_cache_lock_ = false;
            }

            public void RemoveRange(int index, int count)
            {
                items_.RemoveRange(index, count);
                items_cache_lock_ = false;
            }

            public void Add(Type value)
            {
                items_.Add(value);
                items_cache_lock_ = false;
            }

            public void AddRange(IEnumerable<Type> values)
            {
                items_.AddRange(values);
                items_cache_lock_ = false;
            }

            public void Insert(int index, Type value)
            {
                items_.Insert(index, value);
                items_cache_lock_ = false;
            }

            public void InsertRange(int index, IEnumerable<Type> values)
            {
                items_.InsertRange(index, values);
                items_cache_lock_ = false;
            }

            public Type GetAt(int index)
            {
                CacheUpate();

                return (items_cache_[index]);
            }

            public void SetAt(int index, Type value)
            {
                items_[index] = value;
                items_cache_lock_ = false;
            }

            public Type this[int index]
            {
                get { return (GetAt(index)); }
                set { SetAt(index, value);   }
            }

            public IEnumerator<Type> GetEnumerator()
            {
                CacheUpate();

                foreach (var item in items_cache_) {
                    yield return (item);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (this.GetEnumerator());
            }
        }

        private readonly ulong item_count_max_ = 0;

        private List<BlockL2> block_l1_ = new List<BlockL2>();
        private ulong item_count_ = 0;

        private int gc_collect_count_ = 0;


        public SkipList(ulong total_max = TOTAL_ITEM_LIMIT)
        {
            item_count_max_ = Math.Max(total_max, 1);
            item_count_max_ = Math.Min(total_max, TOTAL_ITEM_LIMIT);
        }

        public SkipList(IEnumerable<Type> values, ulong total_max = TOTAL_ITEM_LIMIT)
            : this(total_max)
        {
            AddRange(values);
        }

        public ulong CountMax
        {
            get { return (item_count_max_); }
        }

        public ulong Count
        {
            get { return (item_count_); }
        }

        public Type First
        {
            get { return (block_l1_.First().First()); }
        }

        public Type Last
        {
            get { return (block_l1_.Last().Last()); }
        }

        private void GCCollect()
        {
            if (gc_collect_count_++ < GC_COLLECT_STEP)return;

            GC.Collect();

            gc_collect_count_ = 0;
        }

        public void Clear()
        {
            block_l1_ = new List<BlockL2>();
            item_count_ = 0;

            GCCollect();
        }

        public void RemoveRange(ulong index, ulong count)
        {
            var remove_count = (ulong)0;
            var remove_block = false;

            count = Math.Min(count, item_count_ - index);

            item_count_ -= (ulong)count;

            foreach (var block_l2 in block_l1_) {
                if (index >= (ulong)block_l2.Count) {
                    index -= (ulong)block_l2.Count;
                    continue;
                }
            
                /* 削除アイテム数を計算 */
                remove_count = Math.Min(count, (ulong)(block_l2.Count - (int)index));

                /* 実際のアイテムを削除 */
                if (remove_count >= (ulong)block_l2.Count) {
                    /* --- 全削除 --- */
                    block_l2.Clear();
                    remove_block = true;

                } else {
                    /* --- 部分削除 --- */
                    block_l2.RemoveRange((int)index, (int)remove_count);
                }

                /* 残り削除数を更新 */
                count -= remove_count;

                /* これ以降はリストの先頭が削除開始位置 */
                index = 0;

                /* 削除完了 */
                if (count == 0)break;
            }

            /* 空のブロックを削除 */
            if (remove_block) {
                block_l1_.RemoveAll(block_l2 => block_l2.Count == 0);
            }

            GCCollect();
        }

        public void RemoveAt(ulong index)
        {
            RemoveRange(index, 1);
        }

        public void InsertRange(long index, IEnumerable<Type> values)
        {
            /* 最大データ数よりも大きいときは最後尾を優先としてシュリンク */
            if ((ulong)values.Count() > item_count_max_) {
                values = values.Skip(values.Count() - (int)item_count_max_);
            }

            /* 挿入後に最大データ数を超える場合は事前に削除 */
            if (((ulong)values.Count() + item_count_) > item_count_max_) {
                RemoveRange(0, (ulong)values.Count() + item_count_ - item_count_max_);
            }

            /* ブロックが存在しない場合は初期化 */
            if (block_l1_.Count == 0) {
                block_l1_.Add(new BlockL2());
            }

            /* 挿入先ブロックの初期値は終端 */
            var l1_index = Math.Max(0, block_l1_.Count - 1);
            var l2_block = block_l1_.Last();
            var l2_index = l2_block.Count;

            /* 挿入先を検索(indexが範囲内の場合のみ) */
            if ((index >= 0) && (index < block_l1_.Count)) {
                foreach (var (block_l2_v, block_l2_i) in block_l1_.Select((v, i) => (v, i))) {
                    if (index >= block_l2_v.Count) {
                        index -= block_l2_v.Count;
                        continue;
                    }

                    l1_index = block_l2_i;
                    l2_block = block_l2_v;
                    l2_index = (int)index;

                    break;
                }
            }

            /* 挿入先L2ブロックが一定値以上の場合は新規ブロックを作成 */
            if (l2_block.Count >= BLOCK_ITEM_LIMIT) {
                if (l2_index == 0) {
                    /* --- 先頭挿入の場合は前ブロックを追加 --- */
                    block_l1_.Insert(l1_index, l2_block = new BlockL2());
                    l2_index = 0;
                } else if (l2_index >= l2_block.Count) {
                    /* --- 終端挿入の場合は後ブロックを追加 --- */
                    l1_index = Math.Min(l1_index + 1, block_l1_.Count);
                    block_l1_.Insert(l1_index, l2_block = new BlockL2());
                    l2_index = 0;
                } else {
                    /* --- 中間挿入の場合はそのまま追加 --- */
                }
            }

            /* 挿入処理 */
            l2_block.InsertRange(l2_index, values);

            item_count_ += (ulong)values.Count();
        }

        public void Insert(int index, Type value)
        {
            InsertRange(index, new Type[] { value });
        }

        public void Add(Type value)
        {
            Insert(-1, value);
        }

        public void AddRange(IEnumerable<Type> values)
        {
            InsertRange(-1, values);
        }

        public Type GetAt(ulong index)
        {
            foreach (var block_l2 in block_l1_) {
                if (index >= (ulong)block_l2.Count) {
                    index -= (ulong)block_l2.Count;
                    continue;
                }

                return (block_l2[(int)index]);
            }

            throw new IndexOutOfRangeException();
        }

        public void SetAt(ulong index, IEnumerable<Type> values)
        {
            RemoveRange(index, (ulong)values.Count());

            InsertRange((long)index, values);
        }

        public void SetAt(ulong index, Type value)
        {
            SetAt(index, new [] { value });
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

        public Type this[ulong index]
        {
            get { return (GetAt(index)); }
            set { SetAt(index, value);   }
        }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var block_l2 in block_l1_) {
                foreach (var item in block_l2) {
                    yield return (item);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.GetEnumerator());
        }
    }
}
