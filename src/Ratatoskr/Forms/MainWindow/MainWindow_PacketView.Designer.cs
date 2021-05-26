
namespace Ratatoskr.Forms.MainWindow
{
	partial class MainWindow_PacketView
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            ((System.ComponentModel.ISupportInitialize)(this.DockPanel_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // DockPanel_Main
            // 
            this.DockPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel_Main.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.DockPanel_Main.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.DockPanel_Main.Location = new System.Drawing.Point(0, 22);
            this.DockPanel_Main.Name = "DockPanel_Main";
            this.DockPanel_Main.Padding = new System.Windows.Forms.Padding(6);
            this.DockPanel_Main.ShowAutoHideContentOnHover = false;
            this.DockPanel_Main.Size = new System.Drawing.Size(537, 486);
            this.DockPanel_Main.TabIndex = 5;
            // 
            // PacketConverter_Main
            // 
            this.PacketConverter_Main.AutoSize = true;
            this.PacketConverter_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.PacketConverter_Main.Location = new System.Drawing.Point(0, 0);
            this.PacketConverter_Main.Name = "PacketConverter_Main";
            this.PacketConverter_Main.Size = new System.Drawing.Size(537, 22);
            this.PacketConverter_Main.TabIndex = 4;
            // 
            // MainWindow_PacketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DockPanel_Main);
            this.Controls.Add(this.PacketConverter_Main);
            this.Name = "MainWindow_PacketView";
            this.Size = new System.Drawing.Size(537, 508);
            ((System.ComponentModel.ISupportInitialize)(this.DockPanel_Main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private DockPanelEx DockPanel_Main;
		private MainWindow_PacketConverterPanel PacketConverter_Main;
	}
}
