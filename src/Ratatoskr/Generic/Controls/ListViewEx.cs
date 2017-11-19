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

//        private SkipList<ListViewItem> item_list_ = new SkipList<ListViewItem>();
        private SequentialList<ListViewItem> item_list_ = new SequentialList<ListViewItem>(10000);

        private long  drop_index_ = -1;

        private bool busy_item_select_ = false;


        public ListViewEx()
        {
            VirtualMode = true;
            DoubleBuffered = true;
        }

        public IEnumerable<ListViewItem> ItemList
        {
            get { return (item_list_.ToArray()); }
        }

        public bool ReadOnly { get; set; } = false;

        public int ItemCount
        {
            get { return (item_list_.Count); }
        }

        public ListViewItem ItemAt(int index)
        {
            if (index < 0)return (null);
            if (index >= item_list_.Count)return (null);

            return (item_list_[index]);
        }

        public void ItemClear()
        {
            item_list_.Clear();
            VirtualListSize = item_list_.Count;

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        private void RemoveItemAtBase(IEnumerable<int> indices)
        {
            foreach (var index in indices) {
                item_list_.RemoveAt(index);
            }
            VirtualListSize = item_list_.Count;
        }

        public void RemoveItemAt(int index)
        {
            RemoveItemAtBase(new [] { index });

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveItemAt(IEnumerable<int> indices)
        {
            RemoveItemAtBase(indices);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        private void InsertItemBase(int index, IEnumerable<ListViewItem> items)
        {
            item_list_.InsertRange(index, items);
            VirtualListSize = item_list_.Count;
        }

        public void InsertItem(int index, IEnumerable<ListViewItem> items)
        {
            InsertItemBase(index, items);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        public void InsertItem(int index, ListViewItem item)
        {
            InsertItem(index, new ListViewItem[] { item });
        }

        public void AddItem(ListViewItem item)
        {
            InsertItem(ItemCount, item);
        }

        public void AddItem(IEnumerable<ListViewItem> items)
        {
            InsertItem(ItemCount, items);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
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
                for (var value = 0; value < item_list_.Count; value++) {
                    SelectedIndices.Add(value);
                }
                busy_item_select_ = false;
                OnSelectedIndexChanged(EventArgs.Empty);

            } else if ((!ReadOnly) && (e.KeyData == (Keys.Delete))) {
            /* === Delete (削除) === */
                RemoveItemAt((IEnumerable<int>)SelectedIndices);
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
                move_items.Push(item_list_[value]);
                RemoveItemAtBase(new [] { value });
            }

            /* === 挿入位置(アイテム)を取得する === */
            var insert_pos = PointToClient(new Point(e.X, e.Y));
            var insert_obj = GetItemAt(insert_pos.X, insert_pos.Y);
            var insert_index = 0;

            if (insert_obj != null) {
                insert_index = insert_obj.Index;
            } else if (insert_pos.Y > 0) {
                insert_index = item_list_.Count;
            }

            /* === 挿入する === */
            InsertItemBase(insert_index, move_items);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            e.Item = item_list_[e.ItemIndex];

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
