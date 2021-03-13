namespace Ratatoskr.Forms
{
	partial class RadioButtonListBox
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
            this.FLPanel_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FLPanel_Main
            // 
            this.FLPanel_Main.AutoScroll = true;
            this.FLPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FLPanel_Main.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FLPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.FLPanel_Main.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.FLPanel_Main.Name = "FLPanel_Main";
            this.FLPanel_Main.Size = new System.Drawing.Size(342, 138);
            this.FLPanel_Main.TabIndex = 0;
            this.FLPanel_Main.WrapContents = false;
            // 
            // RadioButtonListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FLPanel_Main);
            this.Name = "RadioButtonListBox";
            this.Size = new System.Drawing.Size(342, 138);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel FLPanel_Main;
	}
}
