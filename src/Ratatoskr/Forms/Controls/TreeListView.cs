using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Controls
{
    internal class TreeListView : ListView
    {
        private readonly Image ICON_TREE_NODE_CONTAINER_EXPAND   = Properties.Resources.expand_16x16;
        private readonly Image ICON_TREE_NODE_CONTAINER_COLLAPSE = Properties.Resources.collapse_16x16;

        private Color fore_color_cache_;
        private Brush fore_brush_cache_;

        private StringFormat node_text_sformat = new StringFormat();


        public TreeListView()
        {
            View = View.Details;
            OwnerDraw = true;

            fore_color_cache_ = ForeColor;
            fore_brush_cache_ = new SolidBrush(fore_color_cache_);

            node_text_sformat.Alignment = StringAlignment.Near;
            node_text_sformat.Trimming = StringTrimming.EllipsisWord;
            node_text_sformat.LineAlignment = StringAlignment.Far;
            node_text_sformat.FormatFlags = StringFormatFlags.LineLimit;

            DrawColumnHeader += OnListViewDrawColumnHeader;
            DrawSubItem += OnListViewDrawSubItem;
        }


        public List<TreeListViewNode> Nodes { get; } = new List<TreeListViewNode>();


        private void UpdateListView()
        {
            void UpdateListViewSub(TreeListViewNode node)
            {
                if (node == null)return;

                Items.Add(CreateListViewItem(node));

                if (node.IsExpanded) {
                    foreach (var node_sub in node.Nodes) {
                        UpdateListViewSub(node_sub as TreeListViewNode);
                    }
                }
            }

            /* 最新のノード情報を元に再構築する */
            BeginUpdate();
            {
                Items.Clear();

                foreach (var node in Nodes) {
                    UpdateListViewSub(node);
                }
            }
            EndUpdate();
        }

        private ListViewItem CreateListViewItem(TreeListViewNode node)
        {
            var item = new ListViewItem();

            item.Text = (new string(' ', node.Level)) + node.Text;
            item.Tag = node;

            for (var value_index = 0;
                 (value_index < (Columns.Count - 1)) && (value_index < node.Values.Count);
                 value_index++
            ) {
                var item_value = node.Values[value_index];
                var item_sub = new ListViewItem.ListViewSubItem();

                item_sub.Text = item_value.ToString();
                item_sub.Tag = item_value;

                item.SubItems.Add(item_sub);
            }

            return (item);
        }

        public void UpdateView()
        {
            UpdateListView();
        }

        private void ExpandNode(TreeListViewNode node, bool expand)
        {
            if (expand) {
                if ((node.IsExpanded) || (node.Nodes.Count == 0))return;

                node.Expand();

            } else {
                if (!node.IsExpanded)return;

                node.Collapse();
            }

            var top_item_index = 0;

            /* 現在のスクロール位置と選択ノードをバックアップ */
            if (Items.Count > 0) {
                top_item_index = TopItem.Index;
            }

            UpdateListView();

            TopItem = Items[top_item_index];
        }

        private int GetNodeLevelOffset()
        {
            return (ICON_TREE_NODE_CONTAINER_EXPAND.Width);
        }

        private int GetNodeDrawOffset(TreeListViewNode node)
        {
            return (node.Level * GetNodeLevelOffset());
        }

        private Image GetNodeIcon(TreeListViewNode node)
        {
            if (node.Nodes.Count > 0) {
                return ((node.IsExpanded) ? (ICON_TREE_NODE_CONTAINER_EXPAND) : (ICON_TREE_NODE_CONTAINER_COLLAPSE));

            } else {
                return (null);
            }
        }

        private TreeListViewNode GetNodeFromPoint(Point pos, bool icon_only)
        {
            var item = GetItemAt(pos.X, pos.Y);

            if (item == null)return (null);

            var node = item.Tag as TreeListViewNode;

            if (node == null)return (null);

            if (icon_only) {
                if ((pos.X >= GetNodeDrawOffset(node)) && (pos.X <= GetNodeDrawOffset(node) + GetNodeLevelOffset())) {
                    return (item.Tag as TreeListViewNode);
                } else {
                    return (null);
                }
            }

            return (item.Tag as TreeListViewNode);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var node = GetNodeFromPoint(e.Location, true);

            if (node == null)return;

            ExpandNode(node, !node.IsExpanded);
        }

        private void OnListViewDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

#if false
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            var node = e.Item.Tag as TreeListViewNode;

            if (node != null) {
                var draw_offset = GetNodeDrawOffset(node);
                var draw_icon = GetNodeIcon(node);
                var rect_base = GetItemRect(e.ItemIndex);
                var rect_item = new Rectangle(rect_base.Left + draw_offset, 0, draw_offset + rect_base.Width, rect_base.Height);
                
                if (fore_color_ != ForeColor) {
                    fore_color_ = ForeColor;
                    fore_brush_?.Dispose();
                    fore_brush_ = new SolidBrush(fore_color_);
                }

//                e.DrawBackground();

                if (draw_icon != null) {
//                    e.Graphics.DrawImage(draw_icon, rect_item.Location);
                }
//                e.Graphics.DrawString(node.Text, Font, fore_brush_, new Point(rect_item.Location.X + ICON_TREE_NODE_CONTAINER_EXPAND.Width, 0));

            } else {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.DrawText();
            }

//            base.OnDrawItem(e);
        }
#endif

        private void OnListViewDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var node = e.Item.Tag as TreeListViewNode;

            if ((node != null) && (e.ColumnIndex == 0)) {
                var draw_offset = GetNodeDrawOffset(node);
                var draw_icon = GetNodeIcon(node);
                var rect_base = e.Bounds;
                var rect_item = new Rectangle(rect_base.Left + draw_offset, 0, draw_offset + rect_base.Width, rect_base.Height);
                
                if (fore_color_cache_ != e.Item.ForeColor) {
                    fore_color_cache_ = e.Item.ForeColor;
                    fore_brush_cache_?.Dispose();
                    fore_brush_cache_ = new SolidBrush(fore_color_cache_);
                }

//                e.DrawBackground();

                if (draw_icon != null) {
                    e.Graphics.DrawImage(
                        draw_icon,
                        new Rectangle(
                            draw_offset,
                            e.Bounds.Top + (e.Bounds.Height - draw_icon.Height) / 2,
                            draw_icon.Width,
                            draw_icon.Height));
                }

                draw_offset += GetNodeLevelOffset();

                e.Graphics.DrawString(
                    node.Text,
                    e.Item.Font,
                    fore_brush_cache_,
                    new Rectangle(draw_offset, e.Bounds.Top, e.Bounds.Width - draw_offset, e.Bounds.Height),
                    node_text_sformat);

                e.DrawFocusRectangle(e.Bounds);

            } else {
                e.DrawDefault = true;
            }
        }
    }
}
