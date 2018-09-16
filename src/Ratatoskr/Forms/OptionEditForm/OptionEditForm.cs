using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditForm : Form
    {
        private const int NOTIFY_MAIL_CONFIG_NUM = 5;

        private enum PageId
        {
            None,
            Language,
            AutoUpdate,
            AutoTimeStamp,
            AutoSave,
            System,
            Notify_MailList,
            Notify_Mail_Top,
            Notify_Mail_Last = Notify_Mail_Top + NOTIFY_MAIL_CONFIG_NUM - 1,
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


        private OptionEditPage page_current_ = null;

        public OptionDataMap Config { get; private set; }


        public OptionEditForm()
        {
            InitializeComponent();
            InitializePageMenu();
        }

        public OptionEditForm(OptionDataMap config) : this()
        {
            Config = config;
        }

        private void InitializePageMenu()
        {
            var items = new [] {
//                new { level = 0, text = "Application",   page = PageId.None },
//                new { level = 1, text = "Language",               page = PageId.Language },
//                new { level = 1, text = "Auto update",           page = PageId.AutoUpdate },
                new { level = 0, text = "Tool",           page = PageId.None },
                new { level = 1, text = "Auto timestamp", page = PageId.AutoTimeStamp },
                new { level = 0, text = "Log",            page = PageId.None },
                new { level = 1, text = "Auto save",      page = PageId.AutoSave },
                new { level = 0, text = "System",         page = PageId.System },
//                new { level = 0, text = "Notify setting", page = PageId.None },
//                new { level = 1, text = "Mail setting",   page = PageId.Notify_Mail_Top },

#if false
                new { level = 1, text = "Mail",           page = PageId.None },
                new { level = 2, text = "Setting 1",      page = PageId.Notify_Mail_Top + 0 },
                new { level = 2, text = "Setting 2",      page = PageId.Notify_Mail_Top + 1 },
                new { level = 2, text = "Setting 3",      page = PageId.Notify_Mail_Top + 2 },
                new { level = 2, text = "Setting 4",      page = PageId.Notify_Mail_Top + 3 },
                new { level = 2, text = "Setting 5",      page = PageId.Notify_Mail_Top + 4 },
#endif
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

        private OptionEditPage GetPage(PageId id)
        {
            if (   (id >= PageId.Notify_Mail_Top)
                && (id <= PageId.Notify_Mail_Last)
            ) {
                var config_no = id - PageId.Notify_Mail_Top;

                while (config_no >= Config.MailList.Count) {
                    Config.MailList.Add(new MailConfig());
                }

                return (new OptionEditPage_NotifyMail(Config.MailList[config_no]));

            } else {
                switch (id) {
                    case PageId.Language:            return (new OptionEditPage_Language());
                    case PageId.AutoUpdate:          return (new OptionEditPage_AutoUpdate());
                    case PageId.AutoTimeStamp:       return (new OptionEditPage_AutoTimeStamp());
                    case PageId.AutoSave:            return (new OptionEditPage_AutoSave());
                    case PageId.System:              return (new OptionEditPage_System());
                    case PageId.Notify_MailList:     return (new OptionEditPage_NotifyMailList());
                    default:                         return (null);
                }
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
