namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrameGateRedirectPanel
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.GView_RedirectList = new System.Windows.Forms.DataGridView();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GView_RedirectList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.GView_RedirectList);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(742, 505);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(742, 505);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // GView_RedirectList
            // 
            this.GView_RedirectList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GView_RedirectList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GView_RedirectList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GView_RedirectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GView_RedirectList.Location = new System.Drawing.Point(0, 0);
            this.GView_RedirectList.MultiSelect = false;
            this.GView_RedirectList.Name = "GView_RedirectList";
            this.GView_RedirectList.RowTemplate.Height = 21;
            this.GView_RedirectList.Size = new System.Drawing.Size(742, 505);
            this.GView_RedirectList.TabIndex = 0;
            this.GView_RedirectList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GView_RedirectList_CellValueChanged);
            this.GView_RedirectList.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.GView_RedirectList_DefaultValuesNeeded);
            // 
            // MainFrameGateRedirectPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "MainFrameGateRedirectPanel";
            this.Size = new System.Drawing.Size(742, 505);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GView_RedirectList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.DataGridView GView_RedirectList;
    }
}
