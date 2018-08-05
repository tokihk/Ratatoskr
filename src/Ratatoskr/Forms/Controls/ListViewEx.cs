using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Generic.Container;

namespace Ratatoskr.Forms.Controls
{
    internal class ListViewEx : ListView
    {
        private const int ITEM_SELECT_TASK_IVAL = 1;
        private const int ITEM_SELECT_STEP      = 100;

        private List<object> items_ = new List<object>();

        private int          item_count_max_ = 0;

        private long  drop_index_ = -1;

        private Timer     item_select_task_ = new Timer();
        private Stopwatch item_select_step_timer_ = new Stopwatch();
        private int       item_select_index_ = 0;

        public event EventHandler UpdatedList;                  // イベント - リスト更新時
        public event EventHandler ItemSelectBusyStatusChanged;  // イベント - 選択処理の開始/終了

        public ListViewEx()
        {
            VirtualMode = true;
            DoubleBuffered = true;

            item_select_task_.Interval = ITEM_SELECT_TASK_IVAL;
            item_select_task_.Tick += OnItemSelectTask;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (items_ != null) {
                items_.Clear();
                items_ = null;
            }
        }

        public IEnumerable<object> ItemList
        {
            get { return (items_.ToArray()); }
        }

        public bool ReadOnly       { get; set; } = false;
        public bool ItemSelectBusy { get; private set; } = false;

        public int ItemCountMax
        {
            get { return (item_count_max_); }
            set
            {
                item_count_max_ = value;
                
                if (items_.Count > item_count_max_) {
                    items_.RemoveRange(0, items_.Count - item_count_max_);
                }
            }
        }

        public int ItemCount
        {
            get { return (items_.Count); }
        }

        public object ItemElementAt(int index)
        {
            return (items_[index]);
        }

        public void ItemClear()
        {
            items_ = new List<object>();

            /* すぐにメモリに反映させるためにGCを手動リセット */
            GC.Collect();

            VirtualListSize = items_.Count;

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        private void ItemRemoveAtBase(IEnumerable<int> indices)
        {
            foreach (var index in indices) {
                items_.RemoveAt(index);
            }

            /* すぐにメモリに反映させるためにGCを手動リセット */
            GC.Collect();

            VirtualListSize = items_.Count;
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

        private void ItemAddBase(IEnumerable<object> items)
        {
            var over_count = (long)items_.Count + items.Count() - item_count_max_;

            if (over_count > 0) {
                items_.RemoveRange(0, (int)Math.Max(over_count, (long)item_count_max_ / 10));
            }

            items_.AddRange(items);

            VirtualListSize = items_.Count;
        }

        public void ItemAddRange(IEnumerable<object> items)
        {
            ItemAddBase(items);

            /* === 更新イベント === */
            UpdatedList?.Invoke(this, EventArgs.Empty);
        }

        public void ItemAdd(object item)
        {
            ItemAddRange(new [] { item });
        }

        private void ItemInsertBase(int index, IEnumerable<object> items)
        {
            var over_count = (long)items_.Count + items.Count() - item_count_max_;

            if (over_count > 0) {
                items_.RemoveRange(0, (int)Math.Max(over_count, (long)item_count_max_ / 10));
            }

            items_.InsertRange(index, items);

            VirtualListSize = items_.Count;
        }

        public void SelectAllItems()
        {
            if (ItemSelectBusy)return;

            item_select_index_ = 0;

            ItemSelectBusy = true;
            ItemSelectBusyStatusChanged?.Invoke(this, EventArgs.Empty);

            item_select_task_.Stop();
            item_select_task_.Start();
        }

        private void OnItemSelectTask(object sender, EventArgs e)
        {
            item_select_step_timer_.Restart();

            while (item_select_step_timer_.ElapsedMilliseconds < ITEM_SELECT_STEP) {
                if (item_select_index_ >= VirtualListSize)break;
                SelectedIndices.Add(item_select_index_++);
            }

            FormUiManager.SetPopupText(string.Format("Item select busy {0} / {1}", item_select_index_, VirtualListSize));

            if (item_select_index_ >= VirtualListSize) {
                item_select_task_.Stop();

                ItemSelectBusy = false;
                ItemSelectBusyStatusChanged?.Invoke(this, EventArgs.Empty);

                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            Update();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A)) {
                /* === CTRL + A (全選択) === */
                SelectAllItems();

#if false
                item_select_busy_ = true;
                
                for (var value = 0; value < items_.Count; value++) {
                    SelectedIndices.Add(value);
                }

                item_select_busy_ = false;
                OnSelectedIndexChanged(EventArgs.Empty);
#endif

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
                for (int index = 0; index < SelectedIndices.Count; index++) {
                    items.Add(SelectedIndices[index]);
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
            var move_items = new Stack<object>();

            foreach (var value in item_drag.OrderByDescending(item => item)) {
                move_items.Push(items_[value]);
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

        protected override void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            base.OnVirtualItemsSelectionRangeChanged(e);

            if (e.IsSelected) {
                ItemSelectBusy = true;
                SelectedIndices.Clear();
                for (var index = e.StartIndex; index <= e.EndIndex; index++) {
                    SelectedIndices.Add(index);
                }
                ItemSelectBusy = false;

                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (ItemSelectBusy)return;

            base.OnSelectedIndexChanged(e);
        }
    }
}
