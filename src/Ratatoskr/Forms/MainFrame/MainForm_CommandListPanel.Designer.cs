namespace Ratatoskr.Forms.MainFrame
{
    partial class MainForm_CommandListPanel
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
            this.GView_CmdList = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GView_CmdList)).BeginInit();
            this.SuspendLayout();
            // 
            // GView_CmdList
            // 
            this.GView_CmdList.AllowUserToOrderColumns = true;
            this.GView_CmdList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GView_CmdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GView_CmdList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.GView_CmdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GView_CmdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GView_CmdList.Location = new System.Drawing.Point(0, 62);
            this.GView_CmdList.Name = "GView_CmdList";
            this.GView_CmdList.RowTemplate.Height = 21;
            this.GView_CmdList.Size = new System.Drawing.Size(777, 411);
            this.GView_CmdList.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(777, 62);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // MainFrameCommandListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GView_CmdList);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainFrameCommandListPanel";
            this.Size = new System.Drawing.Size(777, 473);
            ((System.ComponentModel.ISupportInitialize)(this.GView_CmdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GView_CmdList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
