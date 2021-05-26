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
            this.Tab_Frame = new System.Windows.Forms.TabControl();
            this.Panel_Frame = new System.Windows.Forms.Panel();
            this.GatePanel_Main = new Ratatoskr.Forms.MainWindow.MainWindow_GatePanel();
            this.SuspendLayout();
            // 
            // Tab_Frame
            // 
            this.Tab_Frame.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tab_Frame.Dock = System.Windows.Forms.DockStyle.Left;
            this.Tab_Frame.Location = new System.Drawing.Point(0, 70);
            this.Tab_Frame.Multiline = true;
            this.Tab_Frame.Name = "Tab_Frame";
            this.Tab_Frame.SelectedIndex = 0;
            this.Tab_Frame.Size = new System.Drawing.Size(24, 422);
            this.Tab_Frame.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tab_Frame.TabIndex = 2;
            this.Tab_Frame.Selected += new System.Windows.Forms.TabControlEventHandler(this.Tab_Frame_Selected);
            // 
            // Panel_Frame
            // 
            this.Panel_Frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Frame.Location = new System.Drawing.Point(24, 70);
            this.Panel_Frame.Name = "Panel_Frame";
            this.Panel_Frame.Size = new System.Drawing.Size(664, 422);
            this.Panel_Frame.TabIndex = 3;
            // 
            // GatePanel_Main
            // 
            this.GatePanel_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.GatePanel_Main.Location = new System.Drawing.Point(0, 0);
            this.GatePanel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.GatePanel_Main.Name = "GatePanel_Main";
            this.GatePanel_Main.Size = new System.Drawing.Size(688, 70);
            this.GatePanel_Main.TabIndex = 0;
            // 
            // MainWindow_FrameCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Frame);
            this.Controls.Add(this.Tab_Frame);
            this.Controls.Add(this.GatePanel_Main);
            this.Name = "MainWindow_FrameCenter";
            this.Size = new System.Drawing.Size(688, 492);
            this.ResumeLayout(false);

        }

        #endregion

        private MainWindow_GatePanel GatePanel_Main;
		private System.Windows.Forms.TabControl Tab_Frame;
		private System.Windows.Forms.Panel Panel_Frame;
	}
}
