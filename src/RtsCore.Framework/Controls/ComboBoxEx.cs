using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtsCore.Framework.Controls
{
    public class ComboBoxEx : RoundComboBox
    {
        public class ItemGroup
        {
            public ObjectCollection Items     { get; }
            public Color            ForeColor { get; set; } = Color.Transparent;
            public Color            BackColor { get; set; } = Color.Transparent;
            public Font             Font      { get; set; } = null;

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

        private (ItemGroup group, int index_rel, int group_item_count) GetItemGroupInfo(int index_abs)
        {
            foreach (var group in item_group_list_) {
                if (index_abs < group.Items.Count) {
                    return (group, index_abs, group.Items.Count);
                }

                index_abs -= group.Items.Count;
            }

            return (null, 0, 0);
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

            var item_font = e.Font;
            var item_fore_color = ForeColor;
            var item_back_color = BackColor;

            if (item_group.group != null) {
                if (item_group.group.Font != null) {
                    item_font = item_group.group.Font;
                }
                if (item_group.group.ForeColor != Color.Transparent) {
                    item_fore_color = item_group.group.ForeColor;
                }
                if (item_group.group.BackColor != Color.Transparent) {
                    item_back_color = item_group.group.BackColor;
                }
            }

            /* キャッシュの色と異なる場合のみブラシ作成 */
            if (fore_color_ != item_fore_color) {
                fore_color_ = item_fore_color;
                fore_brush_?.Dispose();
                fore_brush_ = new SolidBrush(fore_color_);
            }

            if (back_color_ != item_back_color) {
                back_color_ = item_back_color;
                back_brush_?.Dispose();
                back_brush_ = new SolidBrush(back_color_);
            }

            /* 指定の背景色で上書き */
            if (item_back_color != Color.Transparent) {
//                e.Graphics.FillRectangle(back_brush_, e.Bounds);
            }

            /* 背景描画 */
            e.DrawBackground();

            /* テキスト描画 */
            e.Graphics.DrawString(item_text, item_font, fore_brush_, e.Bounds);

            /* グループの境目を表示する */
            if ((item_group.group != null) && ((item_group.index_rel + 1) >= item_group.group_item_count)) {
                e.Graphics.DrawLine(Pens.Gray, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }

            e.DrawFocusRectangle();
        }
    }
}
