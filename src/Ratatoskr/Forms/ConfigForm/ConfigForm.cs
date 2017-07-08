using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.ConfigForm
{
    internal partial class ConfigForm : Form
    {
        private enum PageId
        {
            None,
            Language,
            AutoUpdate,
            AutoTimeStamp,
            AutoSave,
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


        private ConfigPage page_current_ = null;

        public OptionConfig Config { get; private set; }


        public ConfigForm()
        {
            InitializeComponent();
            InitializePageMenu();
        }

        public ConfigForm(OptionConfig config) : this()
        {
            Config = config;
        }

        private void InitializePageMenu()
        {
            var items = new [] {
                new { level = 0, text = "アプリケーション",   page = PageId.None },
                new { level = 1, text = "言語",               page = PageId.Language },
                new { level = 1, text = "自動更新",           page = PageId.AutoUpdate },
                new { level = 0, text = "ツール",             page = PageId.None },
                new { level = 1, text = "自動タイムスタンプ", page = PageId.AutoTimeStamp },
                new { level = 0, text = "ログ",               page = PageId.None },
                new { level = 1, text = "自動保存",           page = PageId.AutoSave },
            };

            var node_stack = new Stack<TreeNode>();
            var title = new StringBuilder();

            foreach (var item in items) {
                /* 挿入先レベルのノードまで移動 */
                while (node_stack.Count > item.level) {
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
                var node = new TreeNode();

                node.Text = item.text;
                node.Tag = new PageInfo(title.ToString(), item.page);

                /* ノードを追加 */
                node_root.Add(node);

                /* ノードスタックへ追加 */
                node_stack.Push(node);
            }
        }

        private ConfigPage GetPage(PageId id)
        {
            switch (id) {
                case PageId.Language:      return (new Pages.ConfigPage_Language());
                case PageId.AutoUpdate:    return (new Pages.ConfigPage_AutoUpdate());
                case PageId.AutoTimeStamp: return (new Pages.ConfigPage_AutoTimeStamp());
                case PageId.AutoSave:      return (new Pages.ConfigPage_AutoSave());
                default:                   return (null);
            }
        }

        private void MovePage(PageInfo info)
        {
            if (info == null)return;

            var page = GetPage(info.PageId);

            if (page == null)return;

            /* 表示ページの設定情報を反映 */
            if (page_current_ != null) {
                page_current_.FlushConfig();
            }

            /* 新しいページで設定情報を読み込み */
            page_current_ = page;
            page_current_.LoadConfig(Config);

            /* ページ切り替え */
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
