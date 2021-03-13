namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_FrameCenter
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
            this.DockPanel_Main = new Ratatoskr.Forms.DockPanelEx();
            this.PacketConverter_Main = new Ratatoskr.Forms.MainWindow.MainWindow_PacketConverterPanel();
            this.GatePanel_Main = new Ratatoskr.Forms.MainWindow.MainWindow_GatePanel();
            ((System.ComponentModel.ISupportInitialize)(this.DockPanel_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // DockPanel_Main
            // 
            this.DockPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel_Main.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.DockPanel_Main.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.DockPanel_Main.Location = new System.Drawing.Point(0, 76);
            this.DockPanel_Main.Name = "DockPanel_Main";
            this.DockPanel_Main.Padding = new System.Windows.Forms.Padding(6);
            this.DockPanel_Main.ShowAutoHideContentOnHover = false;
            this.DockPanel_Main.Size = new System.Drawing.Size(571, 331);
            this.DockPanel_Main.TabIndex = 3;
            this.DockPanel_Main.DockContentClosed += new Ratatoskr.Forms.DockPanelEx.DockContentClosedHandler(this.DockPanel_Main_DockContentClosed);
            // 
            // PacketConverter_Main
            // 
            this.PacketConverter_Main.AutoSize = true;
            this.PacketConverter_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.PacketConverter_Main.Location = new System.Drawing.Point(0, 54);
            this.PacketConverter_Main.Name = "PacketConverter_Main";
            this.PacketConverter_Main.Size = new System.Drawing.Size(571, 22);
            this.PacketConverter_Main.TabIndex = 2;
            // 
            // GatePanel_Main
            // 
            this.GatePanel_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.GatePanel_Main.Location = new System.Drawing.Point(0, 0);
            this.GatePanel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.GatePanel_Main.Name = "GatePanel_Main";
            this.GatePanel_Main.Size = new System.Drawing.Size(571, 54);
            this.GatePanel_Main.TabIndex = 0;
            // 
            // MainWindow_FrameCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DockPanel_Main);
            this.Controls.Add(this.PacketConverter_Main);
            this.Controls.Add(this.GatePanel_Main);
            this.Name = "MainWindow_FrameCenter";
            this.Size = new System.Drawing.Size(571, 407);
            ((System.ComponentModel.ISupportInitialize)(this.DockPanel_Main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MainWindow_GatePanel GatePanel_Main;
        private MainWindow_PacketConverterPanel PacketConverter_Main;
        private DockPanelEx DockPanel_Main;
    }
}
