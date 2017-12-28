using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Generic.Container;

namespace Ratatoskr.Generic.Controls
{
    internal class ListViewEx : ListView
    {
        public event EventHandler UpdatedList;          // イベント - リスト更新時

        private SkipList<ListViewItem> items_ = new SkipList<ListViewItem>();

        private long  drop_index_ = -1;

        private bool busy_item_select_ = false;


        public ListViewEx()
        {
            VirtualMode = true;
            DoubleBuffered = true;
        }

        public IEnumerable<ListViewItem> ItemList
        {
            get { return (items_.ToArray()); }
        }

        public bool ReadOnly { get; set; } = false;

        public ulong ItemCountMax
        {
            get
            {
                return (items_.CountMax);
            }
            set
            {
                items_ = new SkipList<ListViewItem>(value);
            }
        }

        public int ItemCount
        {
            get { return ((int)items_.Count); }
        }

        public ListViewItem ItemElementAt(int index)
        {
            return (items_[(ulong)index]);
        }

        public void ItemClear()
        {
            items_.Clear();

            /* すぐにメモリに反映させるためにGCを手動リセット */
            GC.Collect();

            VirtualListSize = (int)items_.Count;

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        private void ItemRemoveAtBase(IEnumerable<int> indices)
        {
            foreach (var index in indices) {
                items_.RemoveAt((ulong)index);
            }

            /* すぐにメモリに反映させるためにGCを手動リセット */
            GC.Collect();

            VirtualListSize = (int)items_.Count;
        }

        public void ItemRemoveAt(int index)
        {
            ItemRemoveAtBase(new [] { index });

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        public void ItemRemoveAt(IEnumerable<int> indices)
        {
            ItemRemoveAtBase(indices);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        private void ItemAddBase(IEnumerable<ListViewItem> items)
        {
            items_.AddRange(items);

            VirtualListSize = (int)items_.Count;
        }

        public void ItemAddRange(IEnumerable<ListViewItem> items)
        {
            ItemAddBase(items);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        public void ItemAdd(ListViewItem item)
        {
            ItemAddRange(new [] { item });
        }

        private void ItemInsertBase(int index, IEnumerable<ListViewItem> items)
        {
            items_.InsertRange(index, items);

            VirtualListSize = (int)items_.Count;
        }

        protected override void OnLeave(EventArgs e)
        {
            Update();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A)) {
                /* === CTRL + A (全選択) === */
                busy_item_select_ = true;
                for (var value = 0; value < (int)items_.Count; value++) {
                    SelectedIndices.Add(value);
                }
                busy_item_select_ = false;
                OnSelectedIndexChanged(EventArgs.Empty);

            } else if ((!ReadOnly) && (e.KeyData == (Keys.Delete))) {
                /* === Delete (削除) === */
                ItemRemoveAt((IEnumerable<int>)SelectedIndices);
            }

            base.OnKeyDown(e);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (ReadOnly)return;

            var items = new List<int>();

            /* --- 選択アイテムを取得 --- */
            if (SelectedIndices.Count > 0) {
                foreach (int value in SelectedIndices) {
                    items.Add(value);
                }
            }
            if (items.Count == 0)return;

            /* --- ドラッグしたアイテムを登録してドラッグ開始 --- */
            DoDragDrop(items.ToArray(), DragDropEffects.Move);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (ReadOnly)return;

            e.Effect = e.AllowedEffect;
        }

        protected override void OnDragLeave(EventArgs e)
        {
            if (ReadOnly)return;

            base.OnDragLeave(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (ReadOnly)return;

            var item_pos = PointToClient(new Point(e.X, e.Y));
            var item = GetItemAt(item_pos.X, item_pos.Y);

            /* --- 自動スクロール --- */
            if (item_pos.Y < FontHeight) {
                EnsureVisible(Math.Max(TopItem.Index - 1, 0));
            } else if (item != null) {
                EnsureVisible(Math.Min(item.Index + 1, ItemCount - 1));
            }

            if (item == null)return;

            drop_index_ = item.Index;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (ReadOnly)return;

            var item_drag = (int[])e.Data.GetData(typeof(int[]));

            if (item_drag == null)return;

            /* === 移動するアイテムを抽出する === */
            /* 削除するとインデックスがずれてしまうため降順で検索する */
            var move_items = new Stack<ListViewItem>();

            foreach (var value in item_drag.OrderByDescending(item => item)) {
                move_items.Push(items_[(ulong)value]);
                ItemRemoveAtBase(new [] { value });
            }

            /* === 挿入位置(アイテム)を取得する === */
            var insert_pos = PointToClient(new Point(e.X, e.Y));
            var insert_obj = GetItemAt(insert_pos.X, insert_pos.Y);
            var insert_index = 0;

            if (insert_obj != null) {
                insert_index = insert_obj.Index;
            } else if (insert_pos.Y > 0) {
                insert_index = (int)items_.Count;
            }

            /* === 挿入する === */
            ItemInsertBase(insert_index, move_items);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            e.Item = items_[(ulong)e.ItemIndex];

            base.OnRetrieveVirtualItem(e);
        }

        protected override void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            base.OnVirtualItemsSelectionRangeChanged(e);

            if (e.IsSelected) {
                busy_item_select_ = true;
                SelectedIndices.Clear();
                for (var index = e.StartIndex; index <= e.EndIndex; index++) {
                    SelectedIndices.Add(index);
                }
                busy_item_select_ = false;

                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (busy_item_select_)return;

            base.OnSelectedIndexChanged(e);
        }
    }
}
