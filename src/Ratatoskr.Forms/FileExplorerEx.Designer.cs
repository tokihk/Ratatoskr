namespace Ratatoskr.Forms
{
    partial class FileExplorerEx
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerEx));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.TView_FileList = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Btn_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_OpenExplorer = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.Label_RootDirName = new System.Windows.Forms.ToolStripLabel();
            this.Btn_OpenRootDir = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TView_FileList);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(533, 481);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(533, 506);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // TView_FileList
            // 
            this.TView_FileList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TView_FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TView_FileList.Location = new System.Drawing.Point(0, 0);
            this.TView_FileList.Name = "TView_FileList";
            this.TView_FileList.Size = new System.Drawing.Size(533, 481);
            this.TView_FileList.TabIndex = 0;
            this.TView_FileList.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TView_FileList_BeforeExpand);
            this.TView_FileList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TView_FileList_NodeMouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Refresh,
            this.toolStripSeparator1,
            this.Btn_OpenExplorer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(81, 39);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Visible = false;
            // 
            // Btn_Refresh
            // 
            this.Btn_Refresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Btn_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Refresh.Enabled = false;
            this.Btn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Refresh.Image")));
            this.Btn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Refresh.Name = "Btn_Refresh";
            this.Btn_Refresh.Size = new System.Drawing.Size(36, 36);
            this.Btn_Refresh.Text = "toolStripButton1";
            this.Btn_Refresh.Click += new System.EventHandler(this.Btn_Refresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // Btn_OpenExplorer
            // 
            this.Btn_OpenExplorer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Btn_OpenExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_OpenExplorer.Image = ((System.Drawing.Image)(resources.GetObject("Btn_OpenExplorer.Image")));
            this.Btn_OpenExplorer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_OpenExplorer.Name = "Btn_OpenExplorer";
            this.Btn_OpenExplorer.Size = new System.Drawing.Size(36, 36);
            this.Btn_OpenExplorer.Text = "toolStripButton2";
            this.Btn_OpenExplorer.Click += new System.EventHandler(this.Btn_OpenExplorer_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_RootDirName,
            this.Btn_OpenRootDir});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(533, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 1;
            // 
            // Label_RootDirName
            // 
            this.Label_RootDirName.Name = "Label_RootDirName";
            this.Label_RootDirName.Size = new System.Drawing.Size(10, 22);
            this.Label_RootDirName.Text = ".";
            // 
            // Btn_OpenRootDir
            // 
            this.Btn_OpenRootDir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Btn_OpenRootDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_OpenRootDir.Image = ((System.Drawing.Image)(resources.GetObject("Btn_OpenRootDir.Image")));
            this.Btn_OpenRootDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_OpenRootDir.Name = "Btn_OpenRootDir";
            this.Btn_OpenRootDir.Size = new System.Drawing.Size(23, 22);
            this.Btn_OpenRootDir.Text = "toolStripButton1";
            this.Btn_OpenRootDir.Click += new System.EventHandler(this.Btn_OpenRootDir_Click);
            // 
            // FileExplorerEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "FileExplorerEx";
            this.Size = new System.Drawing.Size(533, 506);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Btn_Refresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Btn_OpenExplorer;
        private System.Windows.Forms.TreeView TView_FileList;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel Label_RootDirName;
        private System.Windows.Forms.ToolStripButton Btn_OpenRootDir;
    }
}
