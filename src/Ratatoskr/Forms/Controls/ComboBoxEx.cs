using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Controls
{
    internal class ComboBoxEx : ComboBox
    {
        public class ItemGroup
        {
            public ObjectCollection Items     { get; }
            public Color            ForeColor { get; set; } = Color.Transparent;
            public Color            BackColor { get; set; } = Color.Transparent;

            public ItemGroup(ComboBox control)
            {
                Items = new ObjectCollection(control);
            }
        }


        public HashSet<int> Separators { get; } = new HashSet<int>();

        private List<ItemGroup> item_group_list_ = new List<ItemGroup>();

        private Color fore_color_ = Color.Transparent;
        private Brush fore_brush_ = null;

        private Color back_color_ = Color.Transparent;
        private Brush back_brush_ = null;


        public ComboBoxEx()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        public void ClearItemGroup()
        {
            item_group_list_ = new List<ItemGroup>();
        }

        public ItemGroup AddItemGroup()
        {
            var group = new ItemGroup(this);

            item_group_list_.Add(group);

            return (group);
        }

        private (ItemGroup group, int index_rel) GetItemGroupInfo(int index_abs)
        {
            foreach (var group in item_group_list_) {
                if (index_abs < group.Items.Count) {
                    return (group, index_abs);
                }

                index_abs -= group.Items.Count;
            }

            return (null, 0);
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            /* アイテムグループをベースのアイテムに変換する */
            if (item_group_list_.Count > 0) {
                Items.Clear();
                foreach (var group in item_group_list_) {
                    foreach (var item in group.Items) {
                        Items.Add(item);
                    }
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var item_text = Items[e.Index].ToString();
            var item_group = GetItemGroupInfo(e.Index);

            var item_fore_color = ForeColor;

            if (item_group.group != null) {
                if (item_group.group.ForeColor != Color.Transparent) {
                    item_fore_color = item_group.group.ForeColor;
                }
            }

            /* キャッシュの色と異なる場合のみブラシ作成 */
            if (fore_color_ != item_fore_color) {
                fore_color_ = item_fore_color;
                fore_brush_?.Dispose();
                fore_brush_ = new SolidBrush(fore_color_);
            }

            /* 背景描画 */
            e.DrawBackground();

            /* テキスト描画 */
            e.Graphics.DrawString(item_text, e.Font, fore_brush_, e.Bounds);

            /* グループの境目を表示する */
            if ((e.Index > 0) && (item_group.group != null) && (item_group.index_rel == 0)) {
                e.Graphics.DrawLine(Pens.Gray, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);
            }

            e.DrawFocusRectangle();
        }
    }
}
