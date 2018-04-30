using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Controls
{
    public partial class FileExplorerEx : UserControl
    {
        public class TreeNodeInfo
        {
            public DateTime BuildTime { get; set; }
            public string   Url       { get; set; }
        }

        private enum TreeViewIconId
        {
            Directory,
            File,
        }

        private FileSystemWatcher watcher_ = new FileSystemWatcher();
        private string url_root_;


        public delegate void FileDoubleClickHandler(object sender, string file_url);
        public FileDoubleClickHandler FileDoubleClick = delegate (object sender, string file_url) { };

        
        public FileExplorerEx()
        {
            InitializeComponent();

            var image_list = new ImageList();

            image_list.Images.Add(TreeViewIconId.Directory.ToString(), Properties.Resources.folder_16x16);
            image_list.Images.Add(TreeViewIconId.File.ToString(), Properties.Resources.file_16x16);

            TView_FileList.ImageList = image_list;

            watcher_.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            watcher_.IncludeSubdirectories = true;
            watcher_.SynchronizingObject = this;
            watcher_.Created += Watcher_Updated;
            watcher_.Deleted += Watcher_Updated;
            watcher_.Changed += Watcher_Updated;
            watcher_.Renamed += Watcher_Renamed;
        }

        public string RootUrl
        {
            get
            {
                return (url_root_);
            }
            set
            {
                url_root_ = value;

                watcher_.EnableRaisingEvents = false;
                watcher_.Path = value;
                watcher_.EnableRaisingEvents = true;

                TView_FileList.Nodes.Clear();

                UpdateFileNode(url_root_, TView_FileList.Nodes, 2);
            }
        }

        private TreeNode CreateFileNode(string name, string url, uint scan_level = 0)
        {
            var node = new TreeNode();

            node.Text = name;
            node.Tag = new TreeNodeInfo() { BuildTime = DateTime.Now, Url = url };

            if (Directory.Exists(url)) {
                node.ImageKey = TreeViewIconId.Directory.ToString();
            } else {
                node.ImageKey = TreeViewIconId.File.ToString();
            }
            node.SelectedImageKey = node.ImageKey;

            UpdateFileNode(node, scan_level);

            return (node);
        }

        private void UpdateFileNode(string path, TreeNodeCollection nodes, uint scan_level = 0)
        {
            nodes.Clear();

            if ((scan_level > 0) && (Directory.Exists(path))) {
                foreach (var path_sub in Directory.EnumerateFileSystemEntries(path)) {
                    var node_sub = CreateFileNode(Path.GetFileName(path_sub), path_sub, scan_level - 1);

                    if (node_sub == null)continue;

                    nodes.Add(node_sub);
                }
            }
        }

        private void UpdateFileNode(TreeNode node, uint scan_level = 0)
        {
            var node_info = node.Tag as TreeNodeInfo;

            if (node_info == null)return;

            UpdateFileNode(node_info.Url, node.Nodes, scan_level);
        }

        private void Watcher_Updated(object sender, FileSystemEventArgs e)
        {
            UpdateFileNode(url_root_, TView_FileList.Nodes, 2);
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            UpdateFileNode(url_root_, TView_FileList.Nodes, 2);
        }

        protected virtual void OnFileDoubleClick(string file_url)
        {
            FileDoubleClick(this, file_url);
        }

        private void TView_FileList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            UpdateFileNode(e.Node, 2);
        }

        private void TView_FileList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node_info = e.Node.Tag as TreeNodeInfo;

            if (node_info == null)return;

            OnFileDoubleClick(node_info.Url);
        }

        private void Btn_OpenExplorer_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(url_root_);
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {

        }
    }
}
