namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrameGatePanel
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
            if (disposing && (components != null))
            {
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
            this.Panel_GateList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // Panel_GateList
            // 
            this.Panel_GateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_GateList.Location = new System.Drawing.Point(0, 0);
            this.Panel_GateList.Name = "Panel_GateList";
            this.Panel_GateList.Size = new System.Drawing.Size(100, 45);
            this.Panel_GateList.TabIndex = 0;
            // 
            // MainFrameGatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_GateList);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainFrameGatePanel";
            this.Size = new System.Drawing.Size(100, 45);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel Panel_GateList;
    }
}
