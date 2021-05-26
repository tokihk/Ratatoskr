using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Forms.ConfigEditor
{
    internal partial class ConfigEditorForm : Form
    {
        private enum PageId
        {
            None,
            Color,
            Language,
            AutoUpdate,
            AutoTimeStamp,
            AutoSave,
            System,
        }

        private sealed class PageInfo
        {
            public string Title  { get; } = "";
            public PageId PageId { get; } = PageId.None;

            public PageInfo(string title, PageId id)
            {
                Title = title;
                PageId = id;
            }
        }


        private ConfigEditorPage page_current_ = null;


		public ConfigEditorData Config { get; }


        private ConfigEditorForm()
        {
            InitializeComponent();
            InitializePageMenu();
        }

        public ConfigEditorForm(ConfigEditorData config) : this()
        {
			Config = config;
        }

        private void InitializePageMenu()
        {
            var items = new [] {
                new { level = "+",  text = "Application",    page = PageId.None },
                new { level = "++", text = "Color",          page = PageId.Color },
                new { level = "+",  text = "Tool",           page = PageId.None },
                new { level = "++", text = "Auto timestamp", page = PageId.AutoTimeStamp },
                new { level = "+",  text = "Log",            page = PageId.None },
                new { level = "++", text = "Auto save",      page = PageId.AutoSave },
                new { level = "+",  text = "System",         page = PageId.System },
            };

            var node_stack = new Stack<TreeNode>();
            var title = new StringBuilder();

            foreach (var item in items) {
                /* 挿入先レベルのノードまで移動 */
                while (node_stack.Count > (item.level.Length - 1)) {
                    node_stack.Pop();
                }
                
                /* 挿入先ノードを取得 */
                var node_root = (node_stack.Count > 0) ? (node_stack.Peek().Nodes) : (TView_Menu.Nodes);

                /* タイトル作成 */
                title.Clear();
                foreach (var node_one in node_stack) {
                    title.AppendFormat("{0} - ", (node_one.Tag as PageInfo).Title);
                }
                title.Append(item.text);

                /* ノード作成 */
                var node = new TreeNode()
                {
                    Text = item.text,
                    Tag = new PageInfo(title.ToString(), item.page),
                };

                /* ノードを追加 */
                node_root.Add(node);

                /* ノードスタックへ追加 */
                node_stack.Push(node);
            }
        }

        private ConfigEditorPage LoadEditPageControl(PageId id)
        {
            switch (id) {
                case PageId.Color:               return (new ConfigEditorPage_Color());
                case PageId.Language:            return (new ConfigEditorPage_Language());
                case PageId.AutoUpdate:          return (new ConfigEditorPage_AutoUpdate());
                case PageId.AutoTimeStamp:       return (new ConfigEditorPage_AutoTimeStamp());
                case PageId.AutoSave:            return (new ConfigEditorPage_AutoSave());
                case PageId.System:              return (new ConfigEditorPage_System());
                default:                         return (null);
            }
        }

        private void MovePage(PageInfo info)
        {
            if (info == null)return;

            var page = LoadEditPageControl(info.PageId);

            if (page == null)return;

            /* 表示ページの設定情報を反映 */
            if (page_current_ != null) {
                page_current_.FlushConfig();
            }

            /* 新しいページで設定情報を読み込み */
            page_current_ = page;
            page_current_.LoadConfig(Config);

            /* ページ切り替え */
            page_current_.Dock = DockStyle.Fill;
            Panel_PageContents.Controls.Clear();
            Panel_PageContents.Controls.Add(page_current_);

            /* タイトル更新 */
            Label_PageTitle.Text = info.Title;
        }

        private void TView_Menu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MovePage(e.Node.Tag as PageInfo);
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            /* 表示ページの設定情報を反映 */
            if (page_current_ != null) {
                page_current_.FlushConfig();
            }

            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
