namespace Ratatoskr.Forms.MainFrame
{
    partial class MainForm_WatchListPanel
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
            this.DGView_WatchList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGView_WatchList)).BeginInit();
            this.SuspendLayout();
            // 
            // DGView_WatchList
            // 
            this.DGView_WatchList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DGView_WatchList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGView_WatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGView_WatchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGView_WatchList.Location = new System.Drawing.Point(0, 0);
            this.DGView_WatchList.MultiSelect = false;
            this.DGView_WatchList.Name = "DGView_WatchList";
            this.DGView_WatchList.RowTemplate.Height = 21;
            this.DGView_WatchList.Size = new System.Drawing.Size(670, 515);
            this.DGView_WatchList.TabIndex = 0;
            this.DGView_WatchList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGView_WatchList_CellBeginEdit);
            this.DGView_WatchList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGView_WatchList_CellEndEdit);
            this.DGView_WatchList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGView_WatchList_CellValueChanged);
            this.DGView_WatchList.CurrentCellDirtyStateChanged += new System.EventHandler(this.DGView_WatchList_CurrentCellDirtyStateChanged);
            this.DGView_WatchList.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.DGView_WatchList_DefaultValuesNeeded);
            this.DGView_WatchList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DGView_WatchList_EditingControlShowing);
            this.DGView_WatchList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGView_WatchList_KeyDown);
            // 
            // MainFrameWatchListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DGView_WatchList);
            this.Name = "MainFrameWatchListPanel";
            this.Size = new System.Drawing.Size(670, 515);
            ((System.ComponentModel.ISupportInitialize)(this.DGView_WatchList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGView_WatchList;
    }
}
