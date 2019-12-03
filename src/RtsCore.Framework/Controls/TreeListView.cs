using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtsCore.Framework.Controls
{
    public partial class TreeListView : UserControl
    {
        public class TreeListViewItem
        {
            private List<string> display_items_ = new List<string>();

            public List<string> DisplayItems
            {
                get { return (display_items_); }
            }

            public Color DisplayColor { get; set; } = SystemColors.Window;
            public object Tag { get; set; } = null;
        }

        public class TreeListExpandItem : TreeNode
        {
            private List<TreeNode> tree_nodes_ = new List<TreeNode>();

            public List<TreeNode> TreeNodes
            {
                get { return (tree_nodes_); }
            }
        }

        private class TreeListViewNode
        {
            public TreeListViewItem   ItemInfo;
            public TreeListExpandItem ExpandItem;
        }


        private List<TreeListViewNode> items_all_ = new List<TreeListViewNode>();


        public TreeListView()
        {
            InitializeComponent();
        }

        private void AddItems(TreeListViewItem item)
        {
        }
    }
}
