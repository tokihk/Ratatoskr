namespace Ratatoskr.Forms.MainFrame
{
    partial class MainForm_GatePanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Panel_GateList
            // 
            this.Panel_GateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_GateList.Location = new System.Drawing.Point(0, 0);
            this.Panel_GateList.Name = "Panel_GateList";
            this.Panel_GateList.Size = new System.Drawing.Size(366, 220);
            this.Panel_GateList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 2);
            this.label1.TabIndex = 1;
            // 
            // MainFrameGatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_GateList);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainFrameGatePanel";
            this.Size = new System.Drawing.Size(366, 220);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel Panel_GateList;
        private System.Windows.Forms.Label label1;
    }
}
