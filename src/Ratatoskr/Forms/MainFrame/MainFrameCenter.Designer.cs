namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrameCenter
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
            this.DockPanel_Main = new Ratatoskr.Forms.MainFrame.MainFrameDockPanel();
            this.PacketConverter_Main = new Ratatoskr.Forms.MainFrame.MainFramePacketConverterPanel();
            this.GatePanel_Main = new Ratatoskr.Forms.MainFrame.MainFrameGatePanel();
            this.SuspendLayout();
            // 
            // DockPanel_Main
            // 
            this.DockPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel_Main.Location = new System.Drawing.Point(0, 32);
            this.DockPanel_Main.Name = "DockPanel_Main";
            this.DockPanel_Main.Size = new System.Drawing.Size(571, 375);
            this.DockPanel_Main.TabIndex = 1;
            // 
            // PacketConverter_Main
            // 
            this.PacketConverter_Main.AutoSize = true;
            this.PacketConverter_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.PacketConverter_Main.Location = new System.Drawing.Point(0, 32);
            this.PacketConverter_Main.Name = "PacketConverter_Main";
            this.PacketConverter_Main.Size = new System.Drawing.Size(571, 0);
            this.PacketConverter_Main.TabIndex = 2;
            // 
            // GatePanel_Main
            // 
            this.GatePanel_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.GatePanel_Main.Location = new System.Drawing.Point(0, 0);
            this.GatePanel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.GatePanel_Main.Name = "GatePanel_Main";
            this.GatePanel_Main.Size = new System.Drawing.Size(571, 32);
            this.GatePanel_Main.TabIndex = 0;
            // 
            // MainFrameCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DockPanel_Main);
            this.Controls.Add(this.PacketConverter_Main);
            this.Controls.Add(this.GatePanel_Main);
            this.Name = "MainFrameCenter";
            this.Size = new System.Drawing.Size(571, 407);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MainFrameGatePanel GatePanel_Main;
        private MainFrameDockPanel DockPanel_Main;
        private MainFramePacketConverterPanel PacketConverter_Main;
    }
}
